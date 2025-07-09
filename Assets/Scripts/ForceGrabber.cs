using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

[RequireComponent(typeof(LineRenderer))]
public class ForceGrabber : MonoBehaviour
{
    [Header("Force Grab Settings")]
    public float maxDistance = 10f;
    public float pullForceMultiplier = 200f;
    public LayerMask grabbableLayers;
    public Transform handTarget; // Your XR hand’s transform

    [Header("Input Action")]
    public InputActionReference grabAction; // Link to your trigger (e.g., rightTrigger)

    private Rigidbody heldObject;
    private float holdTime;

    private LineRenderer laser;
    private ForceHighlight currentHighlight;

    void Start()
    {
        laser = GetComponent<LineRenderer>();
        grabAction.action.Enable();
    }

    void Update()
    {
        UpdateLaser();

        if (grabAction.action.WasPressedThisFrame())
            TryStartGrab();

        if (grabAction.action.IsPressed())
        {
            holdTime += Time.deltaTime;
            if (heldObject)
                ApplyForcePull();
        }

        if (grabAction.action.WasReleasedThisFrame())
        {
            holdTime = 0f;
            heldObject = null;
        }
    }

    void UpdateLaser()
    {
        Vector3 start = transform.position;
        Vector3 end = transform.position + transform.forward * maxDistance;

        laser.SetPosition(0, start);
        laser.SetPosition(1, end);

        RaycastHit hit;
        if (Physics.Raycast(start, transform.forward, out hit, maxDistance, grabbableLayers))
        {
            ForceHighlight fh = hit.collider.GetComponent<ForceHighlight>();
            if (fh != null && fh != currentHighlight)
            {
                ClearHighlight();
                currentHighlight = fh;
                currentHighlight.Highlight();
            }
        }
        else
        {
            ClearHighlight();
        }
    }

    void TryStartGrab()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, maxDistance, grabbableLayers))
        {
            if (hit.rigidbody != null)
            {
                heldObject = hit.rigidbody;
                heldObject.useGravity = true;
            }
        }
    }

    void ApplyForcePull()
    {
        if (!heldObject) return;

        Vector3 direction = (handTarget.position - heldObject.position);
        float force = pullForceMultiplier * holdTime;
        heldObject.AddForce(direction.normalized * force, ForceMode.Force);
    }

    void ClearHighlight()
    {
        if (currentHighlight != null)
        {
            currentHighlight.RemoveHighlight();
            currentHighlight = null;
        }
    }
}
