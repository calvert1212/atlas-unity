using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour
{
    public GameObject zombie;
    private GameObject[] zombieClones;
    public int zombieCount = 10;
    public int zombieHealth = 30;
    public Transform[] spawnPoints;
    public float spawnTime = 3f;

    int spawnIndex = 0, zombieIndex = 0;
    float nextSpawn = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        if(zombie == null || spawnPoints.Length == 0)
        {
            enabled = false;
        }
        zombieClones = new GameObject[zombieCount];
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time > nextSpawn)
        {
            nextSpawn = Time.time + spawnTime;
            spawnIndex = (spawnIndex + 1) % spawnPoints.Length;
            if(zombieClones[zombieIndex] != null && !zombieClones[zombieIndex].activeSelf)
            {
                zombieClones[zombieIndex].GetComponent<Health>()?.SetHealth(zombieHealth);
                zombieClones[zombieIndex].transform.position = spawnPoints[spawnIndex].position;
                zombieClones[zombieIndex].transform.rotation = spawnPoints[spawnIndex].rotation;
                zombieClones[zombieIndex].SetActive(true);
                zombieIndex++;
            }
            else if(zombieClones[zombieIndex] == null)
            {
                zombieClones[zombieIndex] = Instantiate(zombie, spawnPoints[spawnIndex].position, spawnPoints[spawnIndex].rotation);
                zombieClones[zombieIndex].GetComponent<Health>()?.SetHealth(zombieHealth);
                zombieIndex++;
            }
            zombieIndex = (zombieIndex + 1) % zombieCount;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        foreach(Transform point in spawnPoints)
        {
            Gizmos.DrawWireSphere(point.position, 0.5f);
        }
    }
}
