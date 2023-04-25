using UnityEngine;

namespace Combat
{
    public class AnimatorBridge : MonoBehaviour
    {
        public Fighter fighter;
    
        public void Hit()
        {
            fighter.Hit();
        }
    }
}
