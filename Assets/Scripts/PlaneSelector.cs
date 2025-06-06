using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlaneSelectionManager : MonoBehaviour
{
    public static ARPlane selectedPlane;
    public GameObject startButton;

    private ARPlaneManager planeManager;
    private ARRaycastManager raycastManager;

    void Start()
    {
        planeManager = GetComponent<ARPlaneManager>();
        raycastManager = GetComponent<ARRaycastManager>();
    }

    void Update()
    {
        if (selectedPlane != null) return;

        if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            List<ARRaycastHit> hits = new List<ARRaycastHit>();
            raycastManager.Raycast(Input.GetTouch(0).position, hits, TrackableType.PlaneWithinPolygon);

            if (hits.Count > 0)
            {
                var plane = planeManager.GetPlane(hits[0].trackableId);
                selectedPlane = plane;

                foreach (var p in planeManager.trackables)
                {
                    if (p != selectedPlane) p.gameObject.SetActive(false);
                }

                planeManager.enabled = false;
                startButton.SetActive(true);
            }
        }
    }
}