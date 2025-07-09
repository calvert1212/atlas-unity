using UnityEngine;

public class ForceHighlight : MonoBehaviour
{
    private Renderer rend;
    private Color originalColor;

    void Start()
    {
        rend = GetComponent<Renderer>();
        originalColor = rend.material.color;
    }

    public void Highlight()
    {
        rend.material.color = Color.cyan;
    }

    public void RemoveHighlight()
    {
        rend.material.color = originalColor;
    }
}
