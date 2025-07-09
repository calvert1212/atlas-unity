using UnityEngine;
using UnityEngine.XR.Hands;
using UnityEngine.XR.Management;
using System.Collections.Generic;

public class ThumbsUpRecognizer : MonoBehaviour
{
    public GestureManager gestureManager;

    private XRHandSubsystem handSubsystem;

    void Start()
    {
        handSubsystem = GetXRHandSubsystem();
    }

    void Update()
    {
        if (handSubsystem == null || gestureManager == null)
            return;

        XRHand rightHand = handSubsystem.rightHand;

        if (rightHand.isTracked && IsThumbUp(rightHand))
        {
            gestureManager.OnGestureRecognized("thumbsup");
        }
    }

    private XRHandSubsystem GetXRHandSubsystem()
    {
        List<XRHandSubsystem> subsystems = new List<XRHandSubsystem>();
        SubsystemManager.GetSubsystems(subsystems);

        foreach (var subsystem in subsystems)
        {
            if (subsystem.running)
                return subsystem;
        }

        return null;
    }

    private bool IsThumbUp(XRHand hand)
    {
        // Get joint references
        XRHandJoint thumbTip = hand.GetJoint(XRHandJointID.ThumbTip);
        XRHandJoint wrist = hand.GetJoint(XRHandJointID.Wrist);
        XRHandJoint indexTip = hand.GetJoint(XRHandJointID.IndexTip);
        XRHandJoint middleTip = hand.GetJoint(XRHandJointID.MiddleTip);
        XRHandJoint ringTip = hand.GetJoint(XRHandJointID.RingTip);
        XRHandJoint littleTip = hand.GetJoint(XRHandJointID.LittleTip);

        // Try to get poses for all joints
        if (thumbTip.TryGetPose(out Pose thumbPose) &&
            wrist.TryGetPose(out Pose wristPose) &&
            indexTip.TryGetPose(out Pose indexPose) &&
            middleTip.TryGetPose(out Pose middlePose) &&
            ringTip.TryGetPose(out Pose ringPose) &&
            littleTip.TryGetPose(out Pose littlePose))
        {
            bool thumbExtended = thumbPose.position.y > wristPose.position.y + 0.05f;
            bool fingersFolded = indexPose.position.y < wristPose.position.y &&
                                 middlePose.position.y < wristPose.position.y &&
                                 ringPose.position.y < wristPose.position.y &&
                                 littlePose.position.y < wristPose.position.y;

            return thumbExtended && fingersFolded;
        }

        return false;
    }
}