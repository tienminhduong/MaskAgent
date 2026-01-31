using UnityEngine;

public class PickupableItem : MonoBehaviour, IInteractable
{
    [SerializeField] ItemType type;
    [SerializeField] SpriteRenderer spriteRenderer;
    bool isPlayerInside = false;

    private void Start()
    {
        //spriteRenderer.sprite = type.sprite;
    }

    /*private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player is inside");
            isPlayerInside = true;
            // Hide UI feedback for picking up the item
            //...
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInside = false;
            // Hide UI feedback for picking up the item
            //...
        }
    }*/

    public void Interacted(IInteractable interacted)
    {
        Inventory.Instance.AddItem(type);
        Destroy(gameObject);
    }
    public void Overlapped(IInteractable overlapped) {
        Debug.Log("Overlapped with PickupableItem");
    }
    public void OverlapExited(IInteractable overlapExited) {
        Debug.Log("Exited PickupableItem");
    }
}
