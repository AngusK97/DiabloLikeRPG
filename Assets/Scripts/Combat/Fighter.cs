using Core;
using Movement;
using UnityEngine;

namespace Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        public Mover mover;
        public float weaponRange = 2f;
        public ActionScheduler scheduler;

        private Transform m_targetTransform;

        private void Update()
        {
            if (m_targetTransform != null)
            {
                var distance = Vector3.Distance(transform.position, m_targetTransform.position);
                if (distance < weaponRange)
                {
                    mover.Cancel();
                }
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
    }
}
