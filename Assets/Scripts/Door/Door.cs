using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using DG.Tweening;
public class Door : MonoBehaviour, IInteractable
{
    [SerializeField] SpriteRenderer sprite;
    [SerializeField] List<ItemType> requiredItems;
    BoxCollider2D leftCollider;
    BoxCollider2D rightCollider;
    bool isOpen = false;

    void Start()
    {
        //Set up colliders, left and right fit the whole door sprite
        leftCollider = gameObject.AddComponent<BoxCollider2D>();
        rightCollider = gameObject.AddComponent<BoxCollider2D>();

        leftCollider.offset = new Vector2(-sprite.size.x / 4, 0);
        leftCollider.size = new Vector2(sprite.size.x / 2, sprite.size.y);
        rightCollider.offset = new Vector2(sprite.size.x / 4, 0);
        rightCollider.size = new Vector2(sprite.size.x / 2, sprite.size.y);

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

    void Open()
    {
        // Animate door opening with scaling down the y axis to 0 and max (both side)

        // Use two parralel LeanTween for scalling colliders to left and right
        transform.D

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
