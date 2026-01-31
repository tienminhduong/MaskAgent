using SOEventSystem;
using UnityEngine;

public class WinZone : MonoBehaviour
{
    [SerializeField] private VoidPublisher onLevelComplete;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            onLevelComplete.RaiseEvent();
        }
    }
}
