using UnityEngine;

public class Target : MonoBehaviour
{
    public int scoreValue = 100;
    public GameObject hitEffect; // optional particle prefab
    public AudioClip hitSound;   // optional audio
    private AudioSource audioSource;

    void Start()
    {
        // Optional: Get or add an AudioSource component
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null && hitSound != null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            // 1. Play optional sound
            if (hitSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(hitSound);
            }

            // 2. Spawn hit effect
            if (hitEffect != null)
            {
                Instantiate(hitEffect, transform.position, Quaternion.identity);
            }

            // 3. Add score
            UIManager uiManager = FindFirstObjectByType<UIManager>();
            if (uiManager != null)
            {
                uiManager.AddScore(scoreValue);
            }

            // 4. Destroy target (or delay if playing sound)
            Destroy(gameObject);
        }
    }
}
