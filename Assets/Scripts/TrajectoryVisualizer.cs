using UnityEngine;

public class TrajectoryVisualizer : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public int lineSegmentCount = 20;
    public float timeStep = 0.05f;
    public float projectileForce = 5f;
    public Transform launchPoint;
    public Camera arCamera;

    void Update()
    {
        Vector3[] points = new Vector3[lineSegmentCount];

        Vector3 startPos = launchPoint.position;
        Vector3 startVel = arCamera.transform.forward * projectileForce;

        for (int i = 0; i < lineSegmentCount; i++)
        {
            float t = i * timeStep;
            Vector3 point = startPos + startVel * t + 0.5f * Physics.gravity * t * t;
            points[i] = point;
        }

        lineRenderer.positionCount = lineSegmentCount;
        lineRenderer.SetPositions(points);
    }
}
