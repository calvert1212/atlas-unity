using UnityEngine;

public class TargetSpawner : MonoBehaviour
{
    public GameObject targetPrefab;
    public int numberOfTargets = 5;

    public void SpawnTargets()
    {
        for (int i = 0; i < numberOfTargets; i++)
        {
            Vector3 center = PlaneSelectionManager.selectedPlane.transform.position;
            Vector3 randomPos = center + new Vector3(Random.Range(-0.5f, 0.5f), 0, Random.Range(-0.5f, 0.5f));
            Instantiate(targetPrefab, randomPos, Quaternion.identity);
        }
    }
}