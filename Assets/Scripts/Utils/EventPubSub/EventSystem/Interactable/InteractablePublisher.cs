using UnityEngine;

namespace SOEventSystem
{
    [CreateAssetMenu(fileName = "InteractablePublisher", menuName = "Scriptable Objects/Event System/InteractablePublisher")]
    public class InteractablePublisher : EventPublisher<IInteractable>
    {
    }
}