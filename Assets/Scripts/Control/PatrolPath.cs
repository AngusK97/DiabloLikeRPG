using UnityEngine;

namespace Control
{
    public class PatrolPath : MonoBehaviour
    {
        private const float WayPointGizmosRadius = 0.3f;

        private void OnDrawGizmos()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                var j = GetNextIndex(i);
                Gizmos.DrawSphere(GetWayPoint(i), WayPointGizmosRadius);
                Gizmos.DrawLine(GetWayPoint(i), GetWayPoint(j));
            }
        }

        public int GetNextIndex(int i)
        {
            if (i == transform.childCount - 1)
                return 0;
            return i + 1;
        }

        public Vector3 GetWayPoint(int index)
        {
            return transform.GetChild(index).position;
        }
    }
}
