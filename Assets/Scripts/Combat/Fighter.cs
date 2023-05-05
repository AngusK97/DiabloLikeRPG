using System;
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

        private Health m_target;

        private float m_timeSinceLastAttack = 0f;

        private void Start()
        {
            m_timeSinceLastAttack = timeBetweenAttack;
        }

        private void Update()
        {
            m_timeSinceLastAttack += Time.deltaTime;

            if (m_target == null)
                return;

            if (m_target.IsDead)
                return;

            var distance = Vector3.Distance(transform.position, m_target.transform.position);
            if (distance < weaponRange)
            {
                mover.Cancel();
                AttackBehaviour();
            }
            else
            {
                mover.MoveTo(m_target.transform.position);
            }
        }

        private void AttackBehaviour()
        {
            transform.LookAt(m_target.transform);
            if (m_timeSinceLastAttack > timeBetweenAttack)
            {
                animator.ResetTrigger("stopAttack");
                animator.SetTrigger("attack");
                m_timeSinceLastAttack = 0f;
            }
        }

        public bool CanAttack(GameObject combatTarget)
        {
            if (combatTarget == null) return false;
            
            var health = combatTarget.GetComponent<Health>();
            return health != null && !health.IsDead;
        }

        public void Attack(GameObject combatTarget)
        {
            scheduler.StartAction(this);
            m_target = combatTarget.GetComponent<Health>();
        }

        public void Cancel()
        {
            m_target = null;
            animator.ResetTrigger("attack");
            animator.SetTrigger("stopAttack");
        }

        public void Hit()
        {
            if (m_target == null)
                return;
            
            m_target.TakeDamage(10);
        }
    }
}
