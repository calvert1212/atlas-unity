using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;
using System.Collections.Generic;

public class PlaneSelector : MonoBehaviour
{
    public ARRaycastManager raycastManager;
    public GameObject startButtonPrefab;

    private GameObject placedButton;
    private bool planeSelected = false;

    static List<ARRaycastHit> hits = new();

    void Update()
    {
        if (planeSelected || Input.touchCount == 0)
            return;

        Touch touch = Input.GetTouch(0);
        if (touch.phase != TouchPhase.Began)
            return;

        if (raycastManager.Raycast(touch.position, hits, TrackableType.PlaneWithinPolygon))
        {
            Pose hitPose = hits[0].pose;

            placedButton = Instantiate(startButtonPrefab, hitPose.position, hitPose.rotation);
            planeSelected = true;
        }
    }
}
