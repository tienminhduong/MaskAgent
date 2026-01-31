using TMPro;
using UnityEngine;

public class AnswerChoice : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI answerText;

    public void Initialize(string text)
    {
        answerText.text = text;
    }
}
