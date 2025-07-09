using UnityEngine;
using UnityEngine.UI;

public class GestureManager : MonoBehaviour
{
    public Image reactionImage;
    public Sprite defaultSprite;
    public Sprite waveSprite;
    public Sprite facePalmSprite;
    public Sprite xArmsSprite;
    public Sprite thumbsUpSprite;

    public void OnGestureRecognized(string gesture)
    {
        switch (gesture)
        {
            case "wave":
                reactionImage.sprite = waveSprite;
                break;
            case "facepalm":
                reactionImage.sprite = facePalmSprite;
                break;
            case "xarms":
                reactionImage.sprite = xArmsSprite;
                break;
            case "thumbsup":
                reactionImage.sprite = thumbsUpSprite;
                break;
            default:
                reactionImage.sprite = defaultSprite;
                break;
        }
    }
}
