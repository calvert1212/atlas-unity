using UnityEngine;
using System.Collections;

public class Slingshot : MonoBehaviour
{
    private Vector2 startTouchPos;
    private Vector3 startWorldPos;
    private Rigidbody rb;
    public float forceMultiplier = 0.05f;
    private bool launched = false;
    public LineRenderer lineRenderer;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        startWorldPos = transform.position;
        if (lineRenderer != null)
        {
            lineRenderer.positionCount = 2;
        }
    }

    void Update()
    {
        if (launched) return;

        if (Input.touchCount == 1)
        {
            var touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                startTouchPos = touch.position;
            }
            else if (touch.phase == TouchPhase.Moved && lineRenderer != null)
            {
                Vector2 current = touch.position;
                Vector2 drag = startTouchPos - current;
                Vector3 force = new Vector3(drag.x, drag.y, drag.magnitude) * forceMultiplier;
                lineRenderer.SetPosition(0, transform.position);
                lineRenderer.SetPosition(1, transform.position + force);
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                Vector2 endTouchPos = touch.position;
                Vector2 dragVector = startTouchPos - endTouchPos;
                Vector3 force = new Vector3(dragVector.x, dragVector.y, dragVector.magnitude) * forceMultiplier;
                rb.AddForce(force, ForceMode.Impulse);
                launched = true;

                if (lineRenderer != null)
                {
                    lineRenderer.positionCount = 0;
                }

                StartCoroutine(ResetAfterSeconds(2));
                GameManager.Instance.UseAmmo();
            }
        }
    }

    IEnumerator ResetAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        transform.position = startWorldPos;
        launched = false;
    }
}