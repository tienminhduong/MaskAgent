using UnityEngine;

public class PlayerInteractLogic : MonoBehaviour
{
    [SerializeField] private IInteractable overlappedInteractable;
    public IInteractable OverlappedInteractable => overlappedInteractable;
    public Vector3 CheckpointPosition { get; private set; }


    public void SetOverlappedInteractable(IInteractable interactable)
    {
        overlappedInteractable = interactable;

        if (interactable is Checkpoint checkpoint)
            CheckpointPosition = checkpoint.transform.position;
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

    public bool Lure(Role playerRole)
    {
        if (overlappedInteractable == null)
        {
            Debug.Log("No interactable to lure.");
            return true;
        }
        Debug.Log($"Luring {overlappedInteractable} as {playerRole}");
        if (overlappedInteractable is ILureable lureable)
            return lureable?.OnLured(playerRole) ?? true;
        return true;
    }
}