using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveDroneNavMesh : MonoBehaviour
{
    private Transform player;
    private NavMeshAgent agent;

    [SerializeField] private float stoppingDistance = 2f;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer > stoppingDistance)
        {
            // Get vector that points from Drone to the player
            Vector3 directionToPlayer = player.position - transform.position;
            // Calculate destination point that is stoppingDistance value away
            Vector3 destination = player.position + directionToPlayer * stoppingDistance;
            agent.SetDestination(destination);
        }
        else
        {
            // Stop agent when it's within stopping distance
            agent.ResetPath();
        }
    }
}
