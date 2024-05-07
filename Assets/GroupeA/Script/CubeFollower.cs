using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CubeFollower : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform target;

    private void Update()
    {
        agent.SetDestination(target.position);
    }
}
