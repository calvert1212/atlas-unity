using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class MultiPointGrab : UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable
{
    private UnityEngine.XR.Interaction.Toolkit.Interactors.XRBaseInteractor secondInteractor;
    private Transform originalAttach;

    protected override void Awake()
    {
        base.Awake();
        originalAttach = attachTransform;
    }

    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        base.ProcessInteractable(updatePhase);

        if (interactorsSelecting.Count == 2)
        {
            var primary = interactorsSelecting[0];
            var secondary = interactorsSelecting[1];

            // Position sword based on midpoint
            Vector3 midpoint = (primary.transform.position + secondary.transform.position) / 2f;
            transform.position = midpoint;

            // Align rotation with direction between hands
            Vector3 dir = secondary.transform.position - primary.transform.position;
            if (dir.sqrMagnitude > 0.001f)
            {
                transform.rotation = Quaternion.LookRotation(dir, primary.transform.up);
            }
        }
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        if (interactorsSelecting.Count == 1)
        {
            base.OnSelectEntered(args);
        }
        else if (interactorsSelecting.Count == 2)
        {
            secondInteractor = args.interactorObject as UnityEngine.XR.Interaction.Toolkit.Interactors.XRBaseInteractor;
        }
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        if ((UnityEngine.Object)args.interactorObject != (UnityEngine.Object)secondInteractor)
        {
        }
        else
        {
            secondInteractor = null;
        }
        base.OnSelectExited(args);
    }
}
