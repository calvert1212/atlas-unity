using UnityEngine;
public class XArmsRecognizer : MonoBehaviour
{
    public Transform leftHand, rightHand;
    public GestureManager gestureManager;

    void Update()
    {
        bool isCrossed = rightHand.position.x < leftHand.position.x;
        float yDifference = Mathf.Abs(rightHand.position.y - leftHand.position.y);

        if (isCrossed && yDifference < 0.1f)
        {
            gestureManager.OnGestureRecognized("xarms");
        }
    }
}
