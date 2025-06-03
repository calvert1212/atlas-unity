using UnityEngine;
using UnityEngine.XR.ARFoundation;
using System.Collections.Generic;

[RequireComponent(typeof(ARPlane))]
[RequireComponent(typeof(LineRenderer))]
public class PlaneOutline : MonoBehaviour
{
    private ARPlane plane;
    private LineRenderer lineRenderer;
    private Vector2[] boundaryPoints = new Vector2[0];

    void Awake()
    {
        plane = GetComponent<ARPlane>();
        lineRenderer = GetComponent<LineRenderer>();
    }

    void Update()
    {
        boundaryPoints = plane.boundary.ToArray();

        lineRenderer.positionCount = boundaryPoints.Length;
        for (int i = 0; i < boundaryPoints.Length; i++)
        {
            Vector3 point3D = new Vector3(boundaryPoints[i].x, 0, boundaryPoints[i].y);
            lineRenderer.SetPosition(i, point3D);
        }

        lineRenderer.loop = true;
    }
}
