using UnityEngine;
using UnityEngine.XR;
using System.Collections.Generic;

public class WaveGestureRecognizer : MonoBehaviour
{
    public GestureManager gestureManager;

    private InputDevice rightHand;
    private Vector3 lastPosition;
    private float waveTimer = 0f;
    private int waveCount = 0;

    void Start()
    {
        var devices = new List<InputDevice>();
        InputDevices.GetDevicesAtXRNode(XRNode.RightHand, devices);
        if (devices.Count > 0) rightHand = devices[0];

        lastPosition = transform.position;
    }

    void Update()
    {
        if (rightHand.TryGetFeatureValue(CommonUsages.devicePosition, out Vector3 currentPosition))
        {
            float delta = currentPosition.x - lastPosition.x;

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

            lastPosition = currentPosition;
        }
    }
}
