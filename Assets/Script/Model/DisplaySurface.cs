using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplaySurface : MonoBehaviour {
    public Transform mappedReferenceFrame;

    public void CalculatePositionOnScreen(VisInteractionController_Development chart, out Vector3 pos, out Quaternion rot, ReferenceFrames rf)
    {
        // Temporarily remove parent for calculations
        Transform oldParent = chart.transform.parent;
        Vector3 oldLocalPosition = chart.transform.localPosition;
        Vector3 oldLocalRotation = chart.transform.localEulerAngles;
        chart.transform.parent = null;

        // Store the previous position and rotation
        Vector3 oldPos = chart.transform.position;
        Quaternion oldRot = chart.transform.rotation;

        BoxCollider b = chart.GetComponent<BoxCollider>();

        if(rf != ReferenceFrames.Floor)
            chart.transform.rotation = transform.rotation;
        pos = chart.transform.position;

        rot = transform.rotation;

        //pos = transform.InverseTransformPoint(pos);
        if (rf == ReferenceFrames.Floor)
        {
            pos.y = b.size.z * 0.5f;
        }
        else if (rf == ReferenceFrames.Body)
        {
            Vector3 centerPoint = mappedReferenceFrame.GetComponent<ReferenceFrameController_Development>().mappedTransform.position;
            chart.transform.LookAt(centerPoint);
            chart.transform.localEulerAngles = new Vector3(chart.transform.localEulerAngles.x + 180, chart.transform.localEulerAngles.y, chart.transform.localEulerAngles.z + 180);
            rot = chart.transform.rotation;
        }
        else if (rf == ReferenceFrames.Shelves)
        {
            rot = Quaternion.Euler(90, oldLocalRotation.y, oldLocalRotation.z);
            pos = oldLocalPosition;
        }

        // Restore the original position, rotation and parent
        chart.transform.SetParent(oldParent);
        chart.transform.position = oldPos;
        chart.transform.rotation = oldRot;
    }

    private Vector3 MovePositionInsideScreen(Vector3 position, Vector3 vertex)
    {
        Vector3 localPos = transform.InverseTransformPoint(position);
        Vector3 localVertex = transform.InverseTransformPoint(vertex);
        
        // Case 1: vertex is too far to the left
        if (localVertex.x <= -0.5f)
        {
            float delta = Mathf.Abs(-0.5f - localVertex.x);
            localPos.x += delta;
        }
        // Case 2: vertex is too far to the right
        else if (0.5f <= localVertex.x)
        {
            float delta = localVertex.x - 0.5f;
            localPos.x -= delta;
        }
        // Case 3: vertex is too far to the top
        if (0.5f <= localVertex.y)
        {
            float delta = localVertex.y - 0.5f;
            localPos.y -= delta;
        }
        // Case 4: vertex is too far to the bottom
        else if (localVertex.y <= -0.5f)
        {
            float delta = Mathf.Abs(-0.5f - localVertex.y);
            localPos.y += delta;
        }
        // Case 5: vertex is behind the screen
        if (0f <= localVertex.z)
        {
            float delta = localVertex.z;
            localPos.z -= delta;
        }

        return transform.TransformPoint(localPos);
    }
}
