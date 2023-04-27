using UnityEngine;

namespace Core
{
    public class ActionScheduler : MonoBehaviour
    {
        private IAction m_currentAction;
        
        public void StartAction(IAction action)
        {
            if (m_currentAction == action)
                return;

            if (m_currentAction != null)
            {
                m_currentAction.Cancel();
            }

            m_currentAction = action;
        }

        public void CancelCurrentAction()
        {
            StartAction(null);
        }
    }
}
