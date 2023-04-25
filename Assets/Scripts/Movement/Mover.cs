using Core;
using UnityEngine;
using UnityEngine.AI;

namespace Movement
{
    public class Mover : MonoBehaviour, IAction
    {
        public NavMeshAgent agent;
        public Animator animator;
        public ActionScheduler scheduler;
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
            agent.isStopped = false;
            agent.destination = destination;
        }

        public void StartMoveAction(Vector3 destination)
        {
            scheduler.StartAction(this);
            MoveTo(destination);
        }

        public void Cancel()
        {
            agent.isStopped = true;
        }
    }
}
