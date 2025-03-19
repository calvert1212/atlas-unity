using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

[RequireComponent(typeof(NavMeshAgent))]
public class NavController : MonoBehaviour
{
    public NavMeshAgent agent;
    public float margin = 0.1f;
    public UnityEvent<float> OnPathingDistanceChanged;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public void SetTarget(Vector3 target)
    {
        if(Vector3.Distance(transform.position,target) > margin)
        {
            agent.SetDestination(target);
        }
    }

    // Preupdate is called before the first frame update
    void Update()
    {
        if(agent.enabled && agent.remainingDistance != 0 && agent.speed != 0)
        {
            OnPathingDistanceChanged.Invoke(agent.remainingDistance);
        }
    }
}
