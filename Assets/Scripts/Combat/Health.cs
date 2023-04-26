using UnityEngine;

namespace Combat
{
    public class Health : MonoBehaviour
    {
        public Animator animator;
        public float healthPoints = 100f;
        public bool IsDead => m_isDead;

        private bool m_isDead;

        public void TakeDamage(float damage)
        {
            var newHealth = healthPoints - damage;
            if (newHealth < 0)
            {
                healthPoints = 0f;
                if (!m_isDead)
                    Die();
            }
            else
            {
                healthPoints = newHealth;
            }
        }

        private void Die()
        {
            m_isDead = true;
            animator.SetTrigger("die");
        }
    }
}
