using TMPro;
using UnityEngine;

public class DialogPanelController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI questionText;
    [SerializeField] private RectTransform answersContainer;
    [SerializeField] private AnswerChoice answerPrefab;

    public void ShowDialog(string question, string[] answerList)
    {
        questionText.text = question;
        ClearAnswers();

        foreach (string answer in answerList)
        {
            AnswerChoice answerChoice = Instantiate(answerPrefab, answersContainer);
            answerChoice.Initialize(answer);
        }
    }

    public void ClearAnswers()
    {
        foreach (Transform child in answersContainer)
        {
            Destroy(child.gameObject);
        }
    }
}
