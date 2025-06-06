using UnityEngine;

public class Projectile : MonoBehaviour
{
    private void Start()
    {
        Destroy(gameObject, 5f); // Clean up after 5 seconds
    }
}
