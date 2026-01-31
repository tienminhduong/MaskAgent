using UnityEngine;

public class TestLureable : MonoBehaviour, ILureable, IInteractable
{
    [SerializeField] private HumanInfo humanInfo;
    public HumanInfo HumanInfo => humanInfo;

    public int Direction;

    void Update()
    {
        transform.Translate(Direction * Time.deltaTime * Vector2.right);
    }

    public bool OnLured(Role lurerRole)
    {
        Debug.Log($"Attempting to lure {humanInfo.Name} as {lurerRole}");
        if (!((ILureable)this).IsLureable(lurerRole)) return false;

        Debug.Log("Lured " + humanInfo.Name);
        Direction *= -1;
        return true;
    }

    public void Interacted(IInteractable interacted)
    {
        if (interacted != (IInteractable)this) return;

        Debug.Log("Interacted with " + humanInfo.Name);
        Direction = 0;
    }

    public void Overlapped(IInteractable overlapped)
    {
        if (overlapped != (IInteractable)this) return;

        Debug.Log("Overlapped with " + humanInfo.Name);
    }

    public void OverlapExited(IInteractable overlapExited)
    {
        if (overlapExited != (IInteractable)this) return;
    }

    public void Interacted(IInteractable interacted)
    {
        if (interacted != (IInteractable)this) return;

        Debug.Log("Interacted with " + humanInfo.Name);
        Direction = 0;
    }

    public void Overlapped(IInteractable overlapped)
    {
        if (overlapped != (IInteractable)this) return;

        Debug.Log("Overlapped with " + humanInfo.Name);
    }

    public void OverlapExited(IInteractable overlapExited)
    {
        if (overlapExited != (IInteractable)this) return;
    }
}
