using UnityEngine;
using UnityEngine.AI;

public class Mover : MonoBehaviour
{
    public Transform target;
    public NavMeshAgent agent;

    private void Update()
    {
        agent.destination = target.position;
    }
}
