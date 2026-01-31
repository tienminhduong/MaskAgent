using SOEventSystem;
using UnityEngine;

public class InteractableBox : MonoBehaviour
{
    [SerializeField] private BaseCharacter character;
    [SerializeField] private InteractablePublisher onOverlapEvent;
    [SerializeField] private InteractablePublisher onOverlapExitEvent;
    IInteractable interactable;

    private void Awake()
    {
        interactable = transform.parent.gameObject.GetComponent<IInteractable>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        onOverlapEvent.RaiseEvent(interactable);
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        onOverlapExitEvent.RaiseEvent(interactable);
    }
}