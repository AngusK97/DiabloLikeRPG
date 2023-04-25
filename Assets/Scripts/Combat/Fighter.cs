using Core;
using Movement;
using UnityEngine;

namespace Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        public Mover mover;
        public ActionScheduler scheduler;
        public Animator animator;
        
        public float weaponRange = 2f;
        public float timeBetweenAttack = 1f;

        private Transform m_targetTransform;

        private float m_timeSinceLastAttack = 0f;

        private void Update()
        {
            m_timeSinceLastAttack += Time.deltaTime;
            
            if (m_targetTransform != null)
            {
                var distance = Vector3.Distance(transform.position, m_targetTransform.position);
                if (distance < weaponRange)
                {
                    mover.Cancel();
                    AttackBehaviour();
                }
            }
        }

        private void AttackBehaviour()
        {
            if (m_timeSinceLastAttack > timeBetweenAttack)
            {
                animator.SetTrigger("attack");
                m_timeSinceLastAttack = 0f;
            }
        }

        public void Attack(CombatTarget combatTarget)
        {
            scheduler.StartAction(this);
            m_targetTransform = combatTarget.transform;
            mover.MoveTo(m_targetTransform.position);

        }

        public void Cancel()
        {
            m_targetTransform = null;
        }

        private void Hit()
        {
            
        }
    }
}
