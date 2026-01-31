using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using DG.Tweening;
public class Door : MonoBehaviour, IInteractable
{
    [SerializeField] SpriteRenderer sprite;
    [SerializeField] List<ItemType> requiredItems;
    BoxCollider2D doorCollider;
    bool isOpen = false;

    void Start()
    {
        doorCollider = gameObject.AddComponent<BoxCollider2D>();
        doorCollider.size = new Vector2(sprite.size.x, sprite.size.y);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    bool IsAllRequiredItemsCollected()
    {
        foreach (var item in requiredItems)
        {
            if (!Inventory.Instance.HasItem(item))
            {
                return false;
            }
        }
        return true;
    }

    void ClearAllRequiredItemsFromInventory()
    {
       foreach (var item in requiredItems)
       {
            Inventory.Instance.RemoveItem(item);  
       }
    }

    void Open()
    { 
        Debug.Log("Door: Opened.");
        isOpen = true;
    }

    public void Interacted(IInteractable interacted)
    {
        if(!IsAllRequiredItemsCollected())
        {
            Debug.Log("Door: You don't have all required items to open this door.");
            return;
        }
        if (isOpen)
        {
            Debug.Log("Door: Already opened.");
        }
        Open();
        ClearAllRequiredItemsFromInventory();
        Destroy(gameObject);
    }

    public void Overlapped(IInteractable overlapped)
    {
        throw new System.NotImplementedException();
    }

    public void OverlapExited(IInteractable overlapExited)
    {
        throw new System.NotImplementedException();
    }
    
}
