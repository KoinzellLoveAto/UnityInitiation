using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CubeAIController : MonoBehaviour
{
    public NavMeshAgent navAgent;

    public Transform TargetPoint;

    private void Update()
    {
        navAgent.SetDestination(TargetPoint.position);
    }
}
