using SOEventSystem;
using TMPro;
using UnityEngine;

public class AnswerChoice : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI answerText;
    [SerializeField] private VoidPublisher lureOptionSelectedEvent;
    [SerializeField] private StringPublisher answerSelectedEvent;

    public void Initialize(string text)
    {
        answerText.text = text;
    }

    public void HandleClick()
    {
        if (answerText.text == ButtonOption.LURE)
        {
            Debug.Log("Lure option selected.");
            lureOptionSelectedEvent.RaiseEvent();
        }
        else
        {
            bool isCorrect = QuestionManager.Instance.EvaluateAnswer(answerText.text);
            Debug.Log($"Answer selected: {answerText.text}. Correct: {isCorrect}");
            answerSelectedEvent.RaiseEvent(answerText.text);
        }
    }
}
