using UnityEngine;
using UnityEngine.AI;

public class CarAI : MonoBehaviour
{
    public Transform[] waypoints; // Set waypoints in the Inspector
    private int currentWaypointIndex = 0;
    private NavMeshAgent agent;
    private Car car;
    private bool isOutOfFuel = false;

    public void Initialize()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false; // Prevent automatic rotation by NavMeshAgent
        agent.updateUpAxis = false;   // Since it's 2D, we disable the Up axis

        // Disable local avoidance
        agent.obstacleAvoidanceType = ObstacleAvoidanceType.NoObstacleAvoidance;

        // Set initial Z rotation to -90 degrees
        transform.rotation = Quaternion.Euler(0, 0, -90);
    }

    public void SetCar(Car assignedCar)
    {
        car = assignedCar;
        if (car != null)
        {
            agent.speed = car.GetCurrentSpeed(); // Update NavMeshAgent speed dynamically
            agent.acceleration = car.Acceleration;
        }

        MoveToNextWaypoint();
    }

    void MoveToNextWaypoint()
    {
        if (waypoints.Length == 0) return;

        if (car != null) // Only move if there is fuel
        {
            agent.SetDestination(waypoints[currentWaypointIndex].position);
        }
    }

    void Update()
    {
        if (car != null)
        {
            agent.speed = car.GetCurrentSpeed(); // Update NavMeshAgent speed dynamically
            agent.acceleration = car.Acceleration;
        }

        // If the car has fuel but was previously out of fuel, restart movement
        if (car.IsOutOfFuel())
        {
            Debug.LogError("STOP!!!!");
            agent.isStopped = true;
        }
        else if (!car.IsOutOfFuel() && !agent.pathPending && agent.remainingDistance < 0.4f)
        {
            agent.isStopped = false;
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
            MoveToNextWaypoint();
        }
        else if (!car.IsOutOfFuel())
        {
            agent.isStopped = false;
            agent.SetDestination(waypoints[currentWaypointIndex].position);
        }

        RotateTowardsMovementDirection(); // Handle rotation
    }

    /// <summary>
    /// Rotates the car to face the movement direction.
    /// </summary>
    void RotateTowardsMovementDirection()
    {
        if (agent.velocity.sqrMagnitude > 0.01f) // Only rotate if moving
        {
            float angle = Mathf.Atan2(agent.velocity.y, agent.velocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle - 90); // Adjusted for initial -90° rotation
        }
    }
}
