using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCloth : MonoBehaviour
{
    
    [Header("Reference")]
    public Transform User;
    public GameObject ObjectPrefab;

    [Header("Variables")]
    public float ObjectSize = 0.5f;
    public float ObjectDistance = 0.1f;
    public int ObjectNumber = 6;
    public float UserHeightOffset = 1.8f;
    public float speed = 1;

    private List<Transform> children;

    // Start is called before the first frame update
    void Start()
    {
        children = new List<Transform>();

        for (int i = 0; i < ObjectNumber; i++)
        {
            GameObject go = Instantiate(ObjectPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            go.transform.SetParent(transform);
            go.transform.localScale = Vector3.one * ObjectSize;

            children.Add(go.transform);
        }

        InitialiseObjectPositions(children);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(".")) { // push
            Pushdown();
        }

        if(Input.GetKey(",")) // pull
        {
            PullUp();
        }
    }

    private void Pushdown() {
        foreach (Transform t in children) {
            if (t.position.y > ObjectSize / 2) // move down
            {
                Move(t, transform.up * -0.01f * speed);
            }
            else
            {
                if (t.localEulerAngles.x < 90)
                    t.rotation = Quaternion.Euler(new Vector3(1.5f * speed, 0, 0)) * t.rotation;
                else {
                    if (t.localScale.x != 1.5f * ObjectSize)
                        t.localScale = Vector3.one * 2f * ObjectSize;

                    t.localEulerAngles = new Vector3(90, 0, 0);
                }
                   

                if (t.position.y > 0.03f)
                    Move(t, transform.up * -0.003f * speed);
                else
                    t.position = new Vector3(t.position.x, 0.03f, t.position.z);


                Move(t, transform.forward * -0.01f * speed);
            }
        }
    }

    private void PullUp() {
        foreach (Transform t in children) {

            if (t.localPosition.z < 0) // move up
            {
                Move(t, transform.forward * 0.01f * speed);
            }
            else
            {
                if (t.localScale.x != 1f * ObjectSize)
                    t.localScale = Vector3.one * ObjectSize;

                if (t.localEulerAngles.x > 0 && t.localEulerAngles.x <= 90)
                    t.rotation = Quaternion.Euler(new Vector3(-1.5f * speed, 0, 0)) * t.rotation;
                else
                    t.localEulerAngles = new Vector3(0, 0, 0);
                    

                if (t.localPosition.z > 0)
                    t.localPosition = new Vector3(t.localPosition.x, t.localPosition.y, 0);

                Move(t, transform.up * 0.01f * speed);
            }
        }
    }

    //StartCoroutine(Flip(t, new Vector3(t.localPosition.x, ObjectSize / 2, 0), new Vector3(-90, 0, 0), 0.5f));

    //// rotate coroutine with animation
    //private IEnumerator Flip(Transform go, Vector3 position, Vector3 angles, float duration)
    //{
    //    if (go != null)
    //    {
    //        //object.GetComponent<VRTK_InteractableObject>().isUsable = false;
    //        Quaternion startRotation = go.rotation;
    //        Quaternion endRotation = Quaternion.Euler(angles) * startRotation;
    //        for (float t = 0; t < duration; t += Time.deltaTime)
    //        {
    //            go.localPosition = Vector3.Lerp(go.localPosition, position, t / duration / 20);
    //            go.rotation = Quaternion.Lerp(startRotation, endRotation, t / duration);
    //            yield return null;
    //        }
    //        //object.GetComponent<VRTK_InteractableObject>().isUsable = true;
    //    }
    //}

    //private IEnumerator AnimateMove(Transform go, Vector3 position, Vector3 angles, float duration)
    //{
    //    if (go != null)
    //    {
    //        //object.GetComponent<VRTK_InteractableObject>().isUsable = false;
    //        Quaternion startRotation = go.rotation;
    //        Quaternion endRotation = Quaternion.Euler(angles) * startRotation;
    //        for (float t = 0; t < duration; t += Time.deltaTime)
    //        {
    //            go.localPosition = Vector3.Lerp(go.localPosition, position, t / duration / 20);
    //            go.rotation = Quaternion.Lerp(startRotation, endRotation, t / duration);
    //            yield return null;
    //        }
    //        //object.GetComponent<VRTK_InteractableObject>().isUsable = true;
    //    }
    //}


    private void Move(Transform t, Vector3 moveDir) {
        t.position += moveDir;
    }

    public void InitialiseObjectPositions(List<Transform> currentObjects)
    {
        List<Vector3> objectPositions = new List<Vector3>();

        foreach (Transform t in currentObjects)
        {
            // get random available positions and assign it to Objects
            Vector3 newPosition = GetAvaiableRandomPosition(objectPositions);
            objectPositions.Add(newPosition);

            t.localPosition = newPosition;
        }
    }

    private Vector3 GetAvaiableRandomPosition(List<Vector3> currentList)
    {
        Vector3 tmpPosition = Vector3.zero;
        tmpPosition = new Vector3(Random.Range(-2f, 2f), Random.Range(ObjectSize / 2, 2), 0);

        if (currentList.Count > 0)
        {
            foreach (Vector3 v in currentList)
            {
                if (Vector3.Distance(v, tmpPosition) < ObjectSize * 2.2f)
                {
                    tmpPosition = GetAvaiableRandomPosition(currentList);
                }
            }
        }

        return tmpPosition;
    }
}
