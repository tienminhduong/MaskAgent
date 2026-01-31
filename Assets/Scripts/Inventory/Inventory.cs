using UnityEngine;
using System.Collections.Generic;

public class Inventory : MonoBehaviour
{
    #region singleton

    private static Inventory _instance;
    public static Inventory Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<Inventory>();
                if (_instance == null)
                {
                    GameObject singletonObject = new GameObject(typeof(Inventory).Name);
                    _instance = singletonObject.AddComponent<Inventory>();
                }
            }
            return _instance;
        }
    }

    #endregion
    [SerializeField] private List<ItemType> currentObjects;

    public bool hasObject(ItemType objectType)
    {
        foreach (var obj in currentObjects)
        {
            if (obj == objectType)
            {
                return true;
            }
        }
        return false;
    }

    public void removeObject(ItemType objectType)
    {
        currentObjects.Remove(objectType);
    }

    public void addObject(ItemType objectType)
    {
        currentObjects.Add(objectType);
    }
}
