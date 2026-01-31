using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IdentityCopyController : MonoBehaviour
{
    [SerializeField] private RectTransform canvas;
    [SerializeField] private RectTransform popupPanel;
    [SerializeField] private Image copySlider;
    [SerializeField] private RectTransform maskIcon;
 
    [SerializeField] private float popupExpandDuration = 1.5f;
    [SerializeField] private float scanDuration = 2.0f;

    private bool isCopying = false;

    private Vector2 initialPos;
    private Vector2 targetPos;

    private Tween maskBounceTween;
    private Coroutine scanningRoutine;

    // ================= Resume data =================

    private IInteractable cachedTarget;
    private float cachedProgress; // 0..scanDuration

    public bool IsCopying => isCopying;

    private void Awake()
    {
        popupPanel.transform.localScale = Vector3.zero;
        copySlider.fillAmount = 0f;
    }

    public void StartCopy(PlayerController player)
    {
        if (isCopying)
            return;

        var currentTarget = player.PlayerInteractLogic.OverlappedInteractable;

        // Nếu là human khác → reset progress
        if (currentTarget != cachedTarget)
        {
            cachedTarget = currentTarget;
            cachedProgress = 0f;
        }

        Vector3 playerPos = player.transform.position;
        Vector2 screenPos = Camera.main.WorldToScreenPoint(playerPos);

        float ratio = 1 / canvas.localScale.x;
        initialPos = screenPos * ratio;

        Vector3 worldPos = player.transform.position + new Vector3(0, 1.5f, 0);
        screenPos = Camera.main.WorldToScreenPoint(worldPos);
        targetPos = screenPos * ratio;

        popupPanel.gameObject.SetActive(true);

        scanningRoutine = StartCoroutine(StartScanning(player));
    }

    private IEnumerator StartScanning(PlayerController player)
    {
        isCopying = true;

        popupPanel.localScale = Vector3.zero;
        popupPanel.anchoredPosition = initialPos;

        // load lại progress cũ
        copySlider.fillAmount = cachedProgress / scanDuration;

        Sequence popupSequence = DOTween.Sequence();
        popupSequence.Append(popupPanel.DOAnchorPos(targetPos, popupExpandDuration).SetEase(Ease.OutBack));
        popupSequence.Join(popupPanel.DOScale(1f, popupExpandDuration).SetEase(Ease.OutBack));

        yield return popupSequence.WaitForCompletion();

        maskIcon.localScale = Vector3.one;
        maskBounceTween = maskIcon.DOScale(1.15f, 0.5f)
            .SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Yoyo);

        float elapsed = cachedProgress;

        while (elapsed < scanDuration)
        {
            if (!player.IsCopyPressed ||
                player.PlayerInteractLogic.OverlappedInteractable != cachedTarget)
            {
                // lưu lại tiến trình
                cachedProgress = elapsed;
                EndCopy(player, false);
                yield break;
            }

            elapsed += Time.deltaTime;
            copySlider.fillAmount = elapsed / scanDuration;

            yield return null;
        }

        // copy xong → clear cache
        cachedProgress = 0f;
        cachedTarget = null;

        EndCopy(player, true);
    }

    public void EndCopy(PlayerController player, bool success = false)
    {
        if (!isCopying)
            return;

        isCopying = false;

        if (scanningRoutine != null)
        {
            StopCoroutine(scanningRoutine);
            scanningRoutine = null;
        }

        if (maskBounceTween != null && maskBounceTween.IsActive())
        {
            maskBounceTween.Kill();
            maskIcon.localScale = Vector3.one;
        }

        popupPanel.localScale = Vector3.zero;
        popupPanel.anchoredPosition = initialPos;

        if (success)
        {
            player.ChangeIdentity();
        }

        player.ForceStopChecking();
    }
}
