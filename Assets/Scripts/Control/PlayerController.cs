using Combat;
using Movement;
using UnityEngine;

namespace Control
{
    public class PlayerController : MonoBehaviour
    {
        public Mover mover;
        public Fighter fighter;

        private Camera m_mainCamera;

        private void Start()
        {
            m_mainCamera = Camera.main;
        }

        private void Update()
        {
            if (InteractWithCombat()) return;
            if (InteractWithMovement()) return;

            Debug.LogError("Nothing to do.");
        }

        private bool InteractWithCombat()
        {
            var ray = GetMouseRay();
            var hitInfos = Physics.RaycastAll(ray);
            foreach (var hitInfo in hitInfos)
            {
                var combatTarget = hitInfo.transform.GetComponent<CombatTarget>();
                if (combatTarget != null)
                {
                    if (Input.GetMouseButtonDown(1))
                    {
                        fighter.Attack(combatTarget);
                    }

                    return true;
                }
            }

            return false;
        }

        private bool InteractWithMovement()
        {
            var ray = GetMouseRay();
            var hasHit = Physics.Raycast(ray, out var hitInfo);
            if (hasHit)
            {
                if (Input.GetMouseButton(1))
                {
                    mover.StartMoveAction(hitInfo.point);
                }

                return true;
            }

            return false;
        }

        private Ray GetMouseRay()
        {
            return m_mainCamera.ScreenPointToRay(Input.mousePosition);
        }
    }
}