using UnityEngine;
public class FacePalmRecognizer : MonoBehaviour
{
    public Transform head;
    public Transform rightHand;
    public GestureManager gestureManager;

    void Update()
    {
        float distance = Vector3.Distance(rightHand.position, head.position);
        if (distance < 0.2f)
        {
            gestureManager.OnGestureRecognized("facepalm");
        }
    }
}
