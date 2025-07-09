using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Hands;
using UnityEngine.XR.Management;
using System.Collections.Generic;

public class UniversalGestureRecognizer : MonoBehaviour
{
    [Header("Gesture Reaction System")]
    public GestureManager gestureManager;

    [Header("References")]
    public Transform head;
    public Transform leftHandTransform;
    public Transform rightHandTransform;

    private XRHandSubsystem handSubsystem;
    private InputDevice rightController;
    private Vector3 lastRightPos;
    private float waveTimer;
    private int waveCount;

    void Start()
    {
        handSubsystem = GetXRHandSubsystem();
        rightController = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
        if (rightHandTransform != null)
            lastRightPos = rightHandTransform.position;
    }

    void Update()
    {
        if (gestureManager == null)
            return;

        if (handSubsystem == null)
            handSubsystem = GetXRHandSubsystem();

        DetectWaveGesture();
        DetectFacePalm();
        DetectXArmsPose();
        DetectThumbsUp();
    }

    // ------------------------
    // Gesture Detection Logic
    // ------------------------

    private void DetectWaveGesture()
    {
        if (rightController.TryGetFeatureValue(CommonUsages.devicePosition, out Vector3 currentPosition))
        {
            float delta = currentPosition.x - lastRightPos.x;

            if (Mathf.Abs(delta) > 0.1f)
            {
                waveTimer += Time.deltaTime;
                waveCount++;
                if (waveCount > 3 && waveTimer < 1.5f)
                {
                    gestureManager.OnGestureRecognized("wave");
                    waveCount = 0;
                    waveTimer = 0;
                }
            }
            else
            {
                waveTimer = 0;
                waveCount = 0;
            }

            lastRightPos = currentPosition;
        }
    }

    private void DetectFacePalm()
    {
        if (rightHandTransform == null || head == null) return;

        float distance = Vector3.Distance(rightHandTransform.position, head.position);
        if (distance < 0.2f)
        {
            gestureManager.OnGestureRecognized("facepalm");
        }
    }

    private void DetectXArmsPose()
    {
        if (leftHandTransform == null || rightHandTransform == null) return;

        bool isCrossed = rightHandTransform.position.x < leftHandTransform.position.x;
        float yDifference = Mathf.Abs(rightHandTransform.position.y - leftHandTransform.position.y);

        if (isCrossed && yDifference < 0.1f)
        {
            gestureManager.OnGestureRecognized("xarms");
        }
    }

    private void DetectThumbsUp()
    {
        if (handSubsystem == null) return;

        var rightHand = handSubsystem.rightHand;
        if (!rightHand.isTracked) return;

        if (!rightHand.GetJoint(XRHandJointID.ThumbTip).TryGetPose(out Pose thumbPose) ||
            !rightHand.GetJoint(XRHandJointID.Wrist).TryGetPose(out Pose wristPose) ||
            !rightHand.GetJoint(XRHandJointID.IndexTip).TryGetPose(out Pose indexPose) ||
            !rightHand.GetJoint(XRHandJointID.MiddleTip).TryGetPose(out Pose middlePose) ||
            !rightHand.GetJoint(XRHandJointID.RingTip).TryGetPose(out Pose ringPose) ||
            !rightHand.GetJoint(XRHandJointID.LittleTip).TryGetPose(out Pose littlePose))
        {
            return;
        }

        bool thumbExtended = thumbPose.position.y > wristPose.position.y + 0.05f;
        bool fingersFolded = indexPose.position.y < wristPose.position.y &&
                             middlePose.position.y < wristPose.position.y &&
                             ringPose.position.y < wristPose.position.y &&
                             littlePose.position.y < wristPose.position.y;

        if (thumbExtended && fingersFolded)
        {
            gestureManager.OnGestureRecognized("thumbsup");
        }
    }

    // ------------------------
    // Helper Functions
    // ------------------------

    private XRHandSubsystem GetXRHandSubsystem()
    {
        List<XRHandSubsystem> subsystems = new List<XRHandSubsystem>();
        SubsystemManager.GetSubsystems(subsystems);
        foreach (var s in subsystems)
            if (s.running) return s;

        return null;
    }
}
