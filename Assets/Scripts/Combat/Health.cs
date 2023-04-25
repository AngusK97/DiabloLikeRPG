using UnityEngine;

namespace Combat
{
    public class Health : MonoBehaviour
    {
        public float health = 100f;

        public void TakeDamage(float damage)
        {
            health = Mathf.Max(0f, health - damage);
        }
    }
}
