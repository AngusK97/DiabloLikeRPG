using UnityEngine;
using UnityEngine.AI;

namespace Movement
{
    public class Mover : MonoBehaviour
    {
        public NavMeshAgent agent;
        public Animator animator;

        public float forwardSpeed;

        private void Update()
        {
            UpdateAnimator();
        }

        private void UpdateAnimator()
        {
            var globalVelocity = agent.velocity;
            var localVelocity = transform.InverseTransformDirection(globalVelocity);
            forwardSpeed = localVelocity.z;
            animator.SetFloat("forwardSpeed", forwardSpeed);
        }

        public void MoveTo(Vector3 destination)
        {
            agent.destination = destination;
        }
    }
}
