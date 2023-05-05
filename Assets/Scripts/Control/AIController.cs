using Combat;
using Core;
using Movement;
using UnityEngine;

namespace Control
{
    public class AIController : MonoBehaviour
    {
        public ActionScheduler scheduler;
        public Fighter fighter;
        public Health health;
        public Mover mover;
        public float chaseDistance = 5f;
        public float suspicionTime = 5f;

        private GameObject m_player;
        private Vector3 m_guardPosition;
        private float m_timeSinceLastSawPlayer;

        private void Start()
        {
            m_player = GameObject.FindWithTag("Player");
            m_guardPosition = transform.position;
            m_timeSinceLastSawPlayer = Mathf.Infinity;
        }

        private void Update()
        {
            if (health.IsDead)
                return;

            if (InAttackRangeOfPlayer() < chaseDistance && fighter.CanAttack(m_player))
            {
                m_timeSinceLastSawPlayer = 0f;
                AttackBehaviour();
            }
            else if (m_timeSinceLastSawPlayer < suspicionTime)
            {
                SuspicionBehaviour();
            }
            else
            {
                GuardBehaviour();
            }

            m_timeSinceLastSawPlayer += Time.deltaTime;
        }

        private void GuardBehaviour()
        {
            mover.StartMoveAction(m_guardPosition);
        }

        private void SuspicionBehaviour()
        {
            scheduler.CancelCurrentAction();
        }

        private void AttackBehaviour()
        {
            fighter.Attack(m_player);
        }

        private float InAttackRangeOfPlayer()
        {
            return Vector3.Distance(m_player.transform.position, transform.position);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
        }
    }
}