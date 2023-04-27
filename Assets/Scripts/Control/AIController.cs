using Combat;
using Core;
using UnityEngine;

namespace Control
{
    public class AIController : MonoBehaviour
    {
        public Fighter fighter;
        public Health health;
        public float chaseDistance = 5f;

        private GameObject m_player;
        private bool m_isAttacking;

        private void Start()
        {
            m_player = GameObject.FindWithTag("Player");
            m_isAttacking = false;
        }

        private void Update()
        {
            if (health.IsDead)
                return;
            
            if (InAttackRangeOfPlayer() < chaseDistance && fighter.CanAttack(m_player))
            {
                // if (!m_isAttacking)
                // {
                    fighter.Attack(m_player);
                    m_isAttacking = true;
                // }
            }
            else
            {
                // if (m_isAttacking)
                // {
                    fighter.Cancel();
                    m_isAttacking = false;
                // }
            }
        }

        private float InAttackRangeOfPlayer()
        {
            return Vector3.Distance(m_player.transform.position, transform.position);
        }
    }
}
