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
        
        foreach (string answer in answerList)
        {
            AnswerChoice answerChoice = Instantiate(answerPrefab, answersContainer);
            answerChoice.Initialize(answer);
        }
    }
}
