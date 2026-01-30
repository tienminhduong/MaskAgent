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
            if (eventPublisher != null)
            {
                eventPublisher.ListenEvent(OnEventRaised);
            }
        }

        private void OnDisable()
        {
            if (eventPublisher != null)
            {
                eventPublisher.UnListenEvent(OnEventRaised);
            }
        }

        private void OnEventRaised(T parameter)
        {
            response?.Invoke(parameter);
        }
    }

}