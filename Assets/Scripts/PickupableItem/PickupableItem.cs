using UnityEngine;

public class PickupableItem : MonoBehaviour
{
    [SerializeField] ItemType type;
    [SerializeField] SpriteRenderer spriteRenderer;
    bool isPlayerInside = false;

    private void Start()
    {
        spriteRenderer.sprite = type.sprite;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player is inside");
            // Hide UI feedback for picking up the item
            //...
        }
    }

    public void PickUp()
    {
        Inventory.Instance.AddObject(type);
        Destroy(gameObject);
    }
}
