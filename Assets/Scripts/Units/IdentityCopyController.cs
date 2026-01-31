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

        initialPos = popupPanel.anchoredPosition;

        Vector3 worldPos = player.transform.position + new Vector3(0, 1.5f, 0);
        Vector2 screenPos = Camera.main.WorldToScreenPoint(worldPos);

        float ratio = 1 / canvas.localScale.x;
        targetPos = screenPos * ratio;

        popupPanel.gameObject.SetActive(true);
        StartCoroutine(StartScanning(player));
    }


    private IEnumerator StartScanning(PlayerController player)
    {
        isCopying = true;

        Sequence popupSequence = DOTween.Sequence();
        popupSequence.Append(popupPanel.DOAnchorPos(targetPos, popupExpandDuration).SetEase(Ease.OutBack));
        popupSequence.Append(popupPanel.DOScale(1f, popupExpandDuration).SetEase(Ease.OutBack));

        while (popupSequence.IsActive() && !popupSequence.IsComplete())
        {
            if (!player.IsCopyPressed)
            {
                EndCopy(player);
            }

            yield return null;
        }

        // start bounce icon
        maskIcon.localScale = Vector3.one;
        maskBounceTween = maskIcon.DOScale(1.15f, 0.5f).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);

        float elapsed = 0f;
        while (elapsed < scanDuration)
        {
            elapsed += Time.deltaTime;
            copySlider.fillAmount = Mathf.Clamp01(elapsed / scanDuration);
            
            if (player.PlayerInteractLogic.OverlappedInteractable == null || !player.IsCopyPressed)
            {
                EndCopy(player);
            }

            yield return null;
        }

        EndCopy(player);
    }

    public void EndCopy(PlayerController player)
    {
        isCopying = false;

        // TO DO: Apply the copied identity to the player

        popupPanel.localScale = Vector3.zero;
        popupPanel.anchoredPosition = initialPos;

        if (maskBounceTween != null && maskBounceTween.IsActive())
        {
            maskBounceTween.Kill();
            maskIcon.localScale = Vector3.one;
        }

        player.Fsm.ChangeState(new IdleState());
    }
}
