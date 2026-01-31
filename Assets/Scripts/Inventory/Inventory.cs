using UnityEngine;

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
}
