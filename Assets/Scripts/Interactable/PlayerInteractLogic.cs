using UnityEngine;

public class PlayerInteractLogic : MonoBehaviour
{
    [SerializeField] private IInteractable overlappedInteractable;

    public void SetOverlappedInteractable(IInteractable interactable)
    {
        overlappedInteractable = interactable;
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