using UnityEngine;
using TMPro;
using DG.Tweening;

public class Note : MonoBehaviour, IInteractable
{
    [Header("Settings")]
    [SerializeField] string noteInfo;
    [SerializeField] GameObject notePrefab; // The UI Prefab to spawn

    private GameObject _activeDialog;
    private Canvas _targetCanvas;
    private bool isPopUp = false;

    void Start()
    {
        // Find or create a Canvas to hold the notes
        _targetCanvas = FindOrCreateNoteCanvas();
    }

    private Canvas FindOrCreateNoteCanvas()
    {
        // Look for a Canvas tagged "MainCanvas" or just any Canvas
        Canvas canvas = GameObject.FindFirstObjectByType<Canvas>();

        if (canvas == null)
        {
            GameObject go = new GameObject("NoteCanvas");
            canvas = go.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvas.sortingOrder = 999; // Ensure it is on top
        }

        return canvas;
    }

    public void PopUp()
    {
        if (isPopUp) return;

        // Instantiate the dialog and parent it to the Canvas
        _activeDialog = Instantiate(notePrefab, _targetCanvas.transform);

        // Reset scale for the animation
        _activeDialog.transform.localScale = Vector3.zero;

        // Set the text
        TextMeshProUGUI tmpText = _activeDialog.GetComponentInChildren<TextMeshProUGUI>();
        if (tmpText != null) tmpText.text = noteInfo;

        // Animate in
        _activeDialog.transform
            .DOScale(1f, 0.25f)
            .SetEase(Ease.OutBack);

        isPopUp = true;
    }

    public void PopDown()
    {
        if (!isPopUp || _activeDialog == null) return;

        // Store reference to the one we are closing
        GameObject dialogToDestroy = _activeDialog;

        dialogToDestroy.transform
            .DOScale(0f, 0.2f)
            .SetEase(Ease.InBack)
            .OnComplete(() => Destroy(dialogToDestroy));

        _activeDialog = null;
        isPopUp = false;
    }

    public void Interacted(IInteractable interacted)
    {
        if (isPopUp) PopDown();
        else PopUp();
    }

    public void OverlapExited(IInteractable overlapExited)
    {
        PopDown();
    }

    public void Overlapped(IInteractable overlapped)
    {
        // Logic for when player enters trigger (optional)
    }
}