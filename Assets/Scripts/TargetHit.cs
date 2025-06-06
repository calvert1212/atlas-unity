using UnityEngine;

public class TargetHit : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ammo"))
        {
            GameManager.Instance.AddScore(10);
            Destroy(gameObject);
        }
    }
}