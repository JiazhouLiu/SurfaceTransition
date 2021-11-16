using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VRTK;

public class PrefabManager : MonoBehaviour
{
    [Header("Experiment")]
    public Transform leftHand;
    public VRTK_ControllerEvents leftControllerEvents;
    public int leftHandIndex;
    public Transform rightHand;
    public VRTK_ControllerEvents rightControllerEvents;
    public int rightHandIndex;
    public Transform leftFoot;
    public Transform rightFoot;
    public FootGestureController_Development FG;
    [Header("Body-Tracking")]
    public float armLength;
    public Transform waist;
    public Transform SphereCenter;
    [Header("FoR transform")]
    public Transform Wall;
    public Transform TableTop;
    public Transform FloorSurface;
    [Header("Dashboards")]
    public Transform HeadLevelDisplay;
    public Transform WaistLevelDisplay;
    public Transform GroundDisplay;
    public Transform WallDisplay;
    public Transform TableTopDisplay;

    [Header("Reference")]
    public Transform LandmarkParent;
    public Transform DetailedViewParent;

    private Transform CurrentLandmarkParent;
    private Transform CurrentDetailedViewParent;
    public ReferenceFrames CurrentLandmarkFOR;
    public ReferenceFrames CurrentDetailedViewFOR;
    public int LandmarkDataset;
    public int DetailedViewDataset;

    [HideInInspector]
    public bool Reset = false;

    // Start is called before the first frame update
    void Start()
    {
        CurrentLandmarkParent = LandmarkParent.Find(LandmarkDataset.ToString());
        CurrentDetailedViewParent = DetailedViewParent.Find(DetailedViewDataset.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Transform GetCurrentLandmarkParent()
    {
        return CurrentLandmarkParent;
    }

    public Transform GetCurrentDetailedViewParent()
    {
        return CurrentDetailedViewParent;
    }

    public ReferenceFrames GetCurrentLandmarkFOR()
    {
        return CurrentLandmarkFOR;
    }

    public ReferenceFrames GetCurrentDetailedViewFOR()
    {
        return CurrentDetailedViewFOR;
    }
}
