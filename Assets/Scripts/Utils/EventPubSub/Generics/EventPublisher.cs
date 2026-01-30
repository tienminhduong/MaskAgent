using UnityEngine;
using UnityEngine.Events;

namespace SOEventSystem
{
    public class EventPublisher<T> : ScriptableObject
    {
        private UnityAction<T> _onEventRaised;

        public void ListenEvent(UnityAction<T> listener)
        {
            _onEventRaised += listener;
        }

        public void UnListenEvent(UnityAction<T> listener)
        {
            _onEventRaised -= listener;
        }

        public void RaiseEvent(T parameter)
        {
            _onEventRaised?.Invoke(parameter);
        }
    }
}