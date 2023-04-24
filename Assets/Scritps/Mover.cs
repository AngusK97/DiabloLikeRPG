using UnityEngine;
using UnityEngine.AI;

public class Mover : MonoBehaviour
{
    public Transform target;
    public NavMeshAgent agent;

    private Ray m_lastRay;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            MoveToCursor();    
        }
    }

    private void MoveToCursor()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        var hasHit = Physics.Raycast(ray, out var hitInfo);
        if (hasHit)
        {
            agent.destination = hitInfo.point;
        }
    }
}
