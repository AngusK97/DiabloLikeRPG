using UnityEngine;
using UnityEngine.AI;

public class Mover : MonoBehaviour
{
    public NavMeshAgent agent;
    public Animator animator;

    public float forwardSpeed;

    private void Update()
    {
        if (Input.GetMouseButton(1))
        {
            MoveToCursor();    
        }

        var globalVelocity = agent.velocity;
        var localVelocity = transform.InverseTransformDirection(globalVelocity);
        forwardSpeed = localVelocity.z;
        animator.SetFloat("forwardSpeed", forwardSpeed);
    }

    private void MoveToCursor()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        var hasHit = Physics.Raycast(ray, out var hitInfo);
        if (hasHit)
        {
            agent.destination = hitInfo.point;
        }
    }
}
