using SOEventSystem;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinZone : MonoBehaviour
{
    [SerializeField] private VoidPublisher onLevelComplete;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(PlaySequence());
        }
    }
    private IEnumerator PlaySequence()
    {
        if (Transition.TryGetInstance())
        {
            Debug.Log("Got it");
        }
        Transition.Instance.FadeOut();

        yield return new WaitForSeconds(1.5f);

        onLevelComplete.RaiseEvent();
    }
}
