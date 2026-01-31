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

    public void SetOverlappedInteractable(IInteractable interactable)
    {
        if (interactable is PlayerController playerController)
        {
            if (playerController.CopiedInfo == null || playerController.CopiedInfo.Name.Equals(baseCharacter.HumanInfo.Name))
            {
                onGameOverEvent.RaiseEvent();
                Debug.Log("Thua goi");
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
