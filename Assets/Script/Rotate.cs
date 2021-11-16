using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float speed = 5;
    private bool rotation = false;
    private bool vertical = true;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("r")) {
            if (!rotation)
                rotation = true;
        }

        if (rotation) {
            if (vertical)
                transform.localEulerAngles = Vector3.Lerp(transform.localEulerAngles, Vector3.zero, Time.deltaTime * speed);
            else
                transform.localEulerAngles = Vector3.Lerp(transform.localEulerAngles, new Vector3(90, 0, 0), Time.deltaTime * speed);
        }

        if (Mathf.Abs(transform.localEulerAngles.x - 90) < 0.01f && !vertical) {
            rotation = false;
            vertical = true;
        }
        if(Mathf.Abs(transform.localEulerAngles.x) < 0.01f && vertical) {
            rotation = false;
            vertical = false;
        }

            
                
            
    }
}
