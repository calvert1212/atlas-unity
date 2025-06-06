using UnityEngine;

public class TargetMover : MonoBehaviour
{
    public float speed = 0.2f;
    private Vector3 direction;

    void Start()
    {
        direction = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }
}
