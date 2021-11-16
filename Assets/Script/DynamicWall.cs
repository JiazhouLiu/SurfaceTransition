//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class DynamicWall : MonoBehaviour
//{
//    public Transform Human;
//    public Transform Wall;
//    public LineRenderer Circle;
//    public Circle CircleScript;


//    // Start is called before the first frame update
//    void Start()
//    {
        
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        float zDistance = Mathf.Abs(Human.position.z - transform.position.z);
//        if (zDistance <= 1.5f && zDistance >= 1f)
//        {
//            Wall.GetComponent<MeshRenderer>().material.color = new Color(1, 1, 1, (zDistance - 1) * 2);
//            Circle.material.color = new Color(1, 1, 1, (1.5f - zDistance) * 2);
//            Wall.gameObject.SetActive(false);
//            CircleScript.ShowingWall = false;
//        }
//        else if (zDistance < 1f)
//        {
//            Wall.GetComponent<MeshRenderer>().material.color = new Color(1, 1, 1, 0);
//            Circle.material.color = new Color(1, 1, 1, 1);
//        }else if(zDistance > 1.5f)
//        {
//            Wall.gameObject.SetActive(true);
//            CircleScript.ShowingWall = true;
//            Wall.GetComponent<MeshRenderer>().material.color = new Color(1, 1, 1, 1);
//            Circle.material.color = new Color(1, 1, 1, 0);
//        }
//    }
//}
