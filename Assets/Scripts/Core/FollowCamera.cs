using UnityEngine;

namespace Core
{
    public class FollowCamera : MonoBehaviour
    {
        public Transform target;

        private void LateUpdate()
        {
            transform.position = target.position;
        }
    }
}
