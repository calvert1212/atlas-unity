using UnityEngine;

public class LeverAction : MonoBehaviour
{
    [Tooltip("The AudioSource to play when the lever is used.")]
    public AudioSource radioAudioSource;

    [Tooltip("The AudioClip to play on interaction.")]
    public AudioClip[] songs;

    private bool isActive = false;
    private float timer = 0f;

    // Call this method when the lever is interacted with
    public void OnLeverInteracted()
    {
        if (radioAudioSource == null || songs == null || songs.Length == 0)
            return;

        // Pick a random song different from the current one
        int newIndex;
        do
        {
            newIndex = Random.Range(0, songs.Length);
        } while (songs.Length > 1 && radioAudioSource.clip == songs[newIndex]);

        radioAudioSource.clip = songs[newIndex];
        radioAudioSource.Play();
        radioAudioSource.gameObject.SetActive(true);

        isActive = true;
        timer = 3f;
    }

    void Update()
    {
        if (isActive)
        {
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                radioAudioSource.Stop();
                radioAudioSource.gameObject.SetActive(false);
                isActive = false;
            }
        }
    }
}