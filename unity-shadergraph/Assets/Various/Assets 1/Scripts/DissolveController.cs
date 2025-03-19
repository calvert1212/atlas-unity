using UnityEngine;

public class DissolveController : MonoBehaviour
{
    public Material dissolveMaterial;
    public ParticleSystem dissolveParticles;
    private float dissolveValue = 0.0f;
    private bool isDissolving = false;

    void Update()
    {
        if (isDissolving)
        {
            dissolveValue += Time.deltaTime * 0.5f;
            dissolveMaterial.SetFloat("_DisintegrationAmount", dissolveValue);

            // Trigger particles when dissolve reaches 50%
            if (dissolveValue >= 0.5f && !dissolveParticles.isPlaying)
            {
                dissolveParticles.Play();
            }

            // Destroy object after full dissolve
            if (dissolveValue >= 1.0f)
            {
                Destroy(gameObject);
            }
        }
    }

    public void StartDissolve()
    {
        isDissolving = true;
    }
}
