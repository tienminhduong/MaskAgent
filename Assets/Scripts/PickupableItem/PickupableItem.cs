using UnityEngine;

public class PickupableItem : MonoBehaviour
{
    [SerializeField] SpriteRenderer sprite;
    [SerializeField] ItemType type;
    bool isPlayerInside = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Here you can add logic to add the item to the player's inventory
            Debug.Log($"Interacting with: {type.name}");

            isPlayerInside = true;

            // Show UI feedback for picking up the item
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
    }
}
