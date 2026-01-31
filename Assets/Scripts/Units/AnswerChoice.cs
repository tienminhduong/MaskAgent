using SOEventSystem;
using TMPro;
using UnityEngine;

public class AnswerChoice : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI answerText;
    [SerializeField] private VoidPublisher lureOptionSelectedEvent;
    [SerializeField] private VoidPublisher correctAnswerSelectedEvent;
    [SerializeField] private VoidPublisher wrongAnswerSelectedEvent;
    [SerializeField] private VoidPublisher okOptionSelectedEvent;
    [SerializeField] private VoidPublisher wrongOkSelectedEvent;

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
        else if (answerText.text == ButtonOption.OK)
        {
            Debug.Log("OK option selected.");
            okOptionSelectedEvent.RaiseEvent();
        }
        else if (answerText.text == ButtonOption.WRONGOK)
        {
            Debug.Log("Wrong OK option selected.");
            wrongOkSelectedEvent.RaiseEvent();
        }
        else
        {
            bool isCorrect = QuestionManager.Instance.EvaluateAnswer(answerText.text);
            Debug.Log($"Answer selected: {answerText.text}. Correct: {isCorrect}");
            if (isCorrect)
            {
                correctAnswerSelectedEvent.RaiseEvent();
            }
            else
            {
                wrongAnswerSelectedEvent.RaiseEvent();
            }
        }
    }
}
