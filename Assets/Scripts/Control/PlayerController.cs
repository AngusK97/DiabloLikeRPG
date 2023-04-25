using Movement;
using UnityEngine;

namespace Control
{
    public class PlayerController : MonoBehaviour
    {
        public Mover mover;

        private Camera m_mainCamera;

        private void Start()
        {
            m_mainCamera = Camera.main;
        }

        private void Update()
        {
            if (Input.GetMouseButton(1))
            {
                MoveToCursor();    
            }
        }
    
        private void MoveToCursor()
        {
            var ray = m_mainCamera.ScreenPointToRay(Input.mousePosition);
            var hasHit = Physics.Raycast(ray, out var hitInfo);
            if (hasHit)
            {
                mover.MoveTo(hitInfo.point);
            }
        }
    }
}
