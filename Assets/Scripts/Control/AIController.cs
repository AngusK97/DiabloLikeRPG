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
        public PatrolPath patrolPath;
        public float waypointTolerance = 1f; 
        public float chaseDistance = 5f;
        public float suspicionTime = 5f;
        public float waypointDwellTime = 3f;

        private GameObject m_player;
        private Vector3 m_guardPosition;
        private float m_timeSinceLastSawPlayer;

        private int m_currentWaypointIndex = 0;
        private float m_timeSinceArriveAtWaypoint;

        private void Start()
        {
            m_player = GameObject.FindWithTag("Player");
            m_guardPosition = transform.position;
            m_timeSinceLastSawPlayer = Mathf.Infinity;
            m_timeSinceArriveAtWaypoint = Mathf.Infinity;
        }

        private void Update()
        {
            if (health.IsDead)
                return;

            if (InAttackRangeOfPlayer() < chaseDistance && fighter.CanAttack(m_player))
            {
                AttackBehaviour();
            }
            else if (m_timeSinceLastSawPlayer < suspicionTime)
            {
                SuspicionBehaviour();
            }
            else
            {
                PatrolBehaviour();
            }

            UpdateTimers();
        }

        private void UpdateTimers()
        {
            m_timeSinceLastSawPlayer += Time.deltaTime;
            m_timeSinceArriveAtWaypoint += Time.deltaTime;
        }

        private void PatrolBehaviour()
        {
            var nextPosition = m_guardPosition;

            if (patrolPath != null)
            {
                if (AtWaypoint())
                {
                    m_timeSinceArriveAtWaypoint = 0f;
                    CycleWaypoint();
                }
                nextPosition = GetCurrentWaypoint();
            }

            if (m_timeSinceArriveAtWaypoint > waypointDwellTime)
            {
                mover.StartMoveAction(nextPosition);
            }
        }

        private bool AtWaypoint()
        {
            var distanceToWaypoint = Vector3.Distance(transform.position, GetCurrentWaypoint());
            return distanceToWaypoint < waypointTolerance;
        }

        private void CycleWaypoint()
        {
            m_currentWaypointIndex = patrolPath.GetNextIndex(m_currentWaypointIndex);
        }

        private Vector3 GetCurrentWaypoint()
        {
            return patrolPath.GetWayPoint(m_currentWaypointIndex);
        }

        private void SuspicionBehaviour()
        {
            scheduler.CancelCurrentAction();
        }

        private void AttackBehaviour()
        {
            m_timeSinceLastSawPlayer = 0f;
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