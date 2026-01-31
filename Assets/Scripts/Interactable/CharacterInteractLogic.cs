using SOEventSystem;
using UnityEngine;

public class CharacterInteractLogic : MonoBehaviour
{
    [SerializeField] private IInteractable overlappedInteractable;
    [SerializeField] private LimitedRoles limitedRoles;
    [SerializeField] private VoidPublisher onLimitedRoleInteractAttemptEvent;
    [SerializeField] private VoidPublisher onGameOverEvent;
    private BaseCharacter baseCharacter;

    void Awake()
    {
        baseCharacter = transform.parent.GetComponent<BaseCharacter>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.parent.TryGetComponent(out IInteractable interactable))
        {
            SetOverlappedInteractable(interactable);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.parent.TryGetComponent(out IInteractable interactable))
        {
            RemoveOverlappedInteractable(interactable);
        }
    }

    public void SetOverlappedInteractable(IInteractable interactable)
    {
        if (interactable is PlayerController playerController)
        {
            if (playerController.CopiedInfo == null || playerController.CopiedInfo.Name == baseCharacter.HumanInfo.Name)
            {
                onGameOverEvent.RaiseEvent();
                Debug.Log("Game Over triggered due to identity match.");
                return;
            }

            if (limitedRoles.Roles.Contains(playerController.PlayerInfo.Role))
            {
                overlappedInteractable = interactable;
                onLimitedRoleInteractAttemptEvent.RaiseEvent();
            }
        }
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
