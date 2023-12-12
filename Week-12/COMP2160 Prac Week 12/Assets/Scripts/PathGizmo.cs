using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]

public class PathGizmo : MonoBehaviour
{
    private NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void OnDrawGizmos()
    {
        if (agent == null || agent.path == null)
        {
            return;
        }

        // Draw lines between each corner.
        for (int i = 1; i < agent.path.corners.Length; i++)
        {
            Gizmos.DrawLine(agent.path.corners[i - 1], agent.path.corners[i]);
        }
    }
}
