using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class CarAI : MonoBehaviour
{
    public Transform[] waypoints; // Set waypoints in the Inspector
    private int currentWaypointIndex = 0;
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false; // Prevent automatic rotation
        agent.updateUpAxis = false;   // Since it's 2D, we disable the Up axis
        MoveToNextWaypoint();
    }

    void MoveToNextWaypoint()
    {
        if (waypoints.Length == 0) return;

        agent.SetDestination(waypoints[currentWaypointIndex].position);
    }

    void Update()
    {
        if (!agent.pathPending && agent.remainingDistance < 0.1f)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
            MoveToNextWaypoint();
        }
    }
}
