using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class StartScene : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI startText;

    [Header("Blink Settings")]
    [SerializeField] private float blinkSpeed = 1.5f;
    [SerializeField] private float pulseScale = 1.05f;

    private InputAction anyKeyAction;
    private bool started = false;

    private void Awake()
    {
        // Any key / button / stick
        anyKeyAction = new InputAction(
            "AnyKey",
            binding: "<Keyboard>/anyKey"
        );

        anyKeyAction.AddBinding("<Gamepad>/*");
    }

    private void OnEnable()
    {
        anyKeyAction.Enable();
    }

    private void OnDisable()
    {
        anyKeyAction.Disable();
    }

    private void Start()
    {
        StartCoroutine(BlinkAndPulse());
    }

    private void Update()
    {
        if (started) return;

        if (anyKeyAction.triggered)
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

        SceneManager.LoadScene("LevelSelectionScene");
    }
    private IEnumerator BlinkAndPulse()
    {
        Vector3 baseScale = startText.transform.localScale;
        float time = 0f;

        while (true)
        {
            time += Time.deltaTime * blinkSpeed;
            float alpha = (Mathf.Sin(time) + 1f) * 0.5f;
            startText.alpha = alpha;
            float scale = Mathf.Lerp(1f, pulseScale, alpha);
            startText.transform.localScale = baseScale * scale;

            yield return null;
        }
    }

}
