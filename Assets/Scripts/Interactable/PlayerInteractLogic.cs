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

    public void Lure(Role playerRole)
    {
        if (overlappedInteractable == null)
        {
            Debug.Log("No interactable to lure.");
            return;
        }
        Debug.Log($"Luring {overlappedInteractable} as {playerRole}");
        if (overlappedInteractable is ILureable lureable)
        {
            lureable?.OnLured(playerRole);
        }
    }
}