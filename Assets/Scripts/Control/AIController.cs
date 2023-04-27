using Combat;
using Core;
using Movement;
using UnityEngine;

namespace Control
{
    public class AIController : MonoBehaviour
    {
        public Fighter fighter;
        public Health health;
        public Mover mover;
        public float chaseDistance = 5f;

        private GameObject m_player;
        private Vector3 m_guardPosition;

        private void Start()
        {
            m_player = GameObject.FindWithTag("Player");
            m_guardPosition = transform.position;
        }

        private void Update()
        {
            if (health.IsDead)
                return;

            if (InAttackRangeOfPlayer() < chaseDistance && fighter.CanAttack(m_player))
            {
                fighter.Attack(m_player);
            }
            else
            {
                mover.StartMoveAction(m_guardPosition);
            }
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