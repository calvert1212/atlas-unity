using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(Rigidbody))]
public class ThrowableWithForce : UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable
{
    private Rigidbody rb;

    protected override void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody>();
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);

        // Use controller velocity and angular velocity
        if (args.interactorObject is UnityEngine.XR.Interaction.Toolkit.Interactors.XRBaseInteractor interactor)
        {
            rb.linearVelocity = interactor.GetComponent<Rigidbody>().linearVelocity;
            rb.angularVelocity = interactor.GetComponent<Rigidbody>().angularVelocity;
        }
    }
}
