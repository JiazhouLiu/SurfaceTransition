using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Circle_v1 : MonoBehaviour
{
    [Header("Reference")]
    public Transform User;
    public GameObject ObjectPrefab;

    [Header("Variables")]
    public float ObjectSize = 0.5f;
    public float ObjectDistance = 0.1f;
    public int ObjectNumber = 6;
    public float UserHeightOffset = 1.8f;
    public float minRadius = 0.7f;
    public int smoothDelta = 10;
    public float animationSpeed = 10;
    public bool EXP = true;

    private float radius;
    private float perimeter;
    private float angleOffset;

    private LineRenderer lineRenderer;

    private int vertexCount = 0;

    // Start is called before the first frame update
    private void Awake()
    {
        radius = 0f;
        lineRenderer = GetComponent<LineRenderer>();
        for (int i = 0; i < ObjectNumber; i++)
        {
            GameObject go = Instantiate(ObjectPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            go.transform.SetParent(transform);
            go.transform.localScale = Vector3.one * ObjectSize;
        }
        ObjectDistance += ObjectSize;
    }

    private void Update()
    {
        if (Input.GetKey("w"))
            User.position += Vector3.forward * 0.03f;

        if (Input.GetKey("s"))
            User.position += Vector3.back * 0.03f;

        if (Input.GetKey("a"))
            User.position += Vector3.left * 0.03f;

        if (Input.GetKey("d"))
            User.position += Vector3.right * 0.03f;


        if (User.position.z < 0)
        {
            if (EXP)
                radius = Mathf.Abs(User.position.z * Mathf.Exp(1));
            else
                radius = Mathf.Abs(User.position.z);

            if (radius < minRadius)
                radius = minRadius;

            //Debug.Log("radius: " + radius);
            perimeter = 2 * radius * Mathf.PI;

            if (perimeter > ObjectNumber * ObjectDistance)
            {
                vertexCount = (int)(perimeter / ObjectDistance) + 1;

                int smoothVertexCount = vertexCount * smoothDelta;

                angleOffset = Vector3.SignedAngle(User.forward, Vector3.zero - User.position, Vector3.up);
                //Debug.Log("angle offset: " + angleOffset);

                SetupCircle(radius, smoothVertexCount, (angleOffset));

                if (vertexCount > ObjectNumber * 2 && smoothVertexCount / 4 - (3 * smoothDelta) > 0)
                {
                    int j = smoothVertexCount / 4 - (3 * smoothDelta);
                    foreach (Transform t in transform)
                    {
                        //t.position = Vector3.Lerp(t.position, lineRenderer.GetPosition(j), Time.deltaTime * animationSpeed);
                        t.position = lineRenderer.GetPosition(j);
                        //if(t.position.z > 0)
                        //    t.position = new Vector3(t.position.x, t.position.y, 0);
                        j = j + smoothDelta;
                        t.LookAt(User.transform.position + Vector3.up * UserHeightOffset);
                        t.localEulerAngles = new Vector3(t.localEulerAngles.x + 180, t.localEulerAngles.y, t.localEulerAngles.z + 180);
                    }
                }
                else
                {
                    foreach (Transform t in transform)
                    {
                        //t.position = Vector3.Lerp(t.position, lineRenderer.GetPosition(t.GetSiblingIndex() * smoothDelta), Time.deltaTime * animationSpeed);
                        t.position = lineRenderer.GetPosition(t.GetSiblingIndex() * smoothDelta);

                        //if (t.position.z > 0)
                        //    t.position = new Vector3(t.position.x, t.position.y, 0);
                        t.LookAt(User.transform.position + Vector3.up * UserHeightOffset);
                        t.localEulerAngles = new Vector3(t.localEulerAngles.x + 180, t.localEulerAngles.y, t.localEulerAngles.z + 180);
                    }
                }

            }
            else
            {
                perimeter = ObjectNumber * ObjectDistance;
                vertexCount = ObjectNumber;
                int smoothVertexCount = vertexCount * smoothDelta;
                angleOffset = Vector3.SignedAngle(User.forward, Vector3.zero - User.position, Vector3.up);
                //Debug.Log("angle offset: " + angleOffset);

                SetupCircle(radius, smoothVertexCount, (angleOffset));

                foreach (Transform t in transform)
                {
                    //t.position = Vector3.Lerp(t.position, lineRenderer.GetPosition(t.GetSiblingIndex() * smoothDelta), Time.deltaTime * animationSpeed);
                    t.position = lineRenderer.GetPosition(t.GetSiblingIndex() * smoothDelta);

                    //if (t.position.z > 0)
                    //    t.position = new Vector3(t.position.x, t.position.y, 0);
                    t.LookAt(User.transform.position + Vector3.up * UserHeightOffset);
                    t.localEulerAngles = new Vector3(t.localEulerAngles.x + 180, t.localEulerAngles.y, t.localEulerAngles.z + 180);
                }
            }
        }
        else if (User.position.z >= 0)
        {
            radius = minRadius;

            perimeter = ObjectNumber * ObjectDistance;
            vertexCount = ObjectNumber;
            int smoothVertexCount = vertexCount * smoothDelta;
            angleOffset = Vector3.SignedAngle(User.forward, Vector3.zero - User.position, Vector3.up);
            //Debug.Log("angle offset: " + angleOffset);

            SetupCircle(radius, smoothVertexCount, (angleOffset));

            foreach (Transform t in transform)
            {

                //t.position = Vector3.Lerp(t.position, lineRenderer.GetPosition(t.GetSiblingIndex() * smoothDelta), Time.deltaTime * animationSpeed);
                t.position = lineRenderer.GetPosition(t.GetSiblingIndex() * smoothDelta);
                //if (t.position.z > 0)
                //    t.position = new Vector3(t.position.x, t.position.y, 0);
                t.LookAt(User.transform.position + Vector3.up * UserHeightOffset);
                t.localEulerAngles = new Vector3(t.localEulerAngles.x + 180, t.localEulerAngles.y, t.localEulerAngles.z + 180);
            }
        }
    }

    private void SetupCircle(float r, int vCount, float angleOffset)
    {
        float deltaTheta = (2f * Mathf.PI) / vCount;
        float theta = -Mathf.Deg2Rad * angleOffset;

        if (EXP && User.position.z < 0 && r > minRadius)
        {
            Vector3 normV = User.position.normalized * r;
            transform.position = normV;
        }
        else
            transform.position = User.position;

        lineRenderer.positionCount = vCount;
        for (int i = 0; i < lineRenderer.positionCount; i++)
        {
            Vector3 pos = new Vector3(r * Mathf.Cos(theta) + transform.position.x, 1f, r * Mathf.Sin(theta) + transform.position.z);
            lineRenderer.SetPosition(i, pos);
            theta += deltaTheta;
        }
    }

    //public Vector3 GetPositionFromCircle(Vector3 v) {
    //    float n = (1.57f - v.x) / 3.14f * 500;
    //    Vector3 pos = lineRenderer.GetPosition((int) n);
    //    currentIndex = (int) n;
    //    return new Vector3(pos.x, v.y, pos.z);
    //}
}
