using UnityEngine;

namespace Core
{
    public class Health : MonoBehaviour
    {
        public Animator animator;
        public ActionScheduler scheduler;
        public float healthPoints = 100f;
        public bool IsDead => m_isDead;

        private bool m_isDead;

        public void TakeDamage(float damage)
        {
            var newHealth = healthPoints - damage;
            if (newHealth < 0)
            {
                healthPoints = 0f;
                Die();
            }
            else
            {
                healthPoints = newHealth;
            }
        }

        private void Die()
        {
            if (m_isDead) return;
            
            m_isDead = true;
            animator.SetTrigger("die");
            scheduler.CancelCurrentAction();
        }
    }
}
