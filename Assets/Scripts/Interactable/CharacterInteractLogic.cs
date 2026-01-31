using UnityEngine;

public class CharacterInteractLogic : MonoBehaviour
{
    [SerializeField] private IInteractable overlappedInteractable;

    public void SetOverlappedInteractable(IInteractable interactable)
    {
        overlappedInteractable = interactable;
        Debug.Log("I see you");
    }

    public void RemoveOverlappedInteractable(IInteractable interactable)
    {
        if (overlappedInteractable == interactable)
        {
            overlappedInteractable = null;
        }
    }

    public void Interact()
    {
        Debug.Log($"Interacting with {overlappedInteractable}");
        overlappedInteractable?.Interacted(overlappedInteractable);
    }
}
