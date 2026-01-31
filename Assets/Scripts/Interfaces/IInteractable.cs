public interface IInteractable
{
    void Interacted(IInteractable interacted);
    void Overlapped(IInteractable overlapped);
    void OverlapExited(IInteractable overlapExited);
}