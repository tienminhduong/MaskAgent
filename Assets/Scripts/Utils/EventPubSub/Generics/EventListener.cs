using UnityEngine;
using UnityEngine.Events;

namespace SOEventSystem
{
    public class EventListener<T> : MonoBehaviour
    {
        [SerializeField] private EventPublisher<T> eventPublisher;
        [SerializeField] private UnityEvent<T> response;

        private void OnEnable()
        {
            eventPublisher.ListenEvent(OnEventRaised);
        }

        private void OnDisable()
        {
            eventPublisher.UnListenEvent(OnEventRaised);
        }

        private void OnEventRaised(T parameter)
        {
            response?.Invoke(parameter);
        }
    }

}