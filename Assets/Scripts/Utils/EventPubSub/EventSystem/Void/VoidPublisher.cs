using UnityEngine;
using UnityEngine.Events;

namespace SOEventSystem
{
    [CreateAssetMenu(fileName = "New VoidPublisher", menuName = "Scriptable Objects/Event System/VoidPublisher")]
    public class VoidPublisher : ScriptableObject
    {
        private UnityAction _onEventRaise;

        public void ListenEvent(UnityAction action)
        {
            _onEventRaise += action;
        }

        public void UnlistenEvent(UnityAction action)
        {
            _onEventRaise -= action;
        }

        public void RaiseEvent()
        {
            _onEventRaise?.Invoke();
        }
    }
}