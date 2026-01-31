using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProfilePanelController : MonoBehaviour
{
    [SerializeField] private GameObject profilePanel;
    [SerializeField] private RectTransform profileTransform;

    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI dateOBText;
    [SerializeField] private TextMeshProUGUI ageText;
    [SerializeField] private TextMeshProUGUI roleText;
    [SerializeField] private TextMeshProUGUI addressText;

    [SerializeField] private Image profileImage;

    public void ShowProfilePanel(HumanInfo info)
    {
        nameText.text = info.Name;
        dateOBText.text = info.DOB;
        ageText.text = info.Age.ToString();
        roleText.text = info.Role.ToString();
        addressText.text = info.Address;

        profileImage.sprite = info.profileImage;

        profilePanel.SetActive(true);

        profileTransform.localScale = Vector3.zero;
        profileTransform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack).onComplete = () =>
        {
            PauseGame(true);
        };
    }

    public void PauseGame(bool value)
    {
        Time.timeScale = value ? 0f : 1f;
    }
}
