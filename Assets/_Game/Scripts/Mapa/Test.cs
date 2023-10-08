using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Test : MonoBehaviour
{
    [SerializeField] private Transform destination;
    [SerializeField] private NavMeshAgent agent;

    private void Start()
    {
        StartCoroutine(SetDestination());
    }

    IEnumerator SetDestination()
    {
        yield return new WaitForSeconds(2f);
        agent.SetDestination(destination.position);
    }
}
