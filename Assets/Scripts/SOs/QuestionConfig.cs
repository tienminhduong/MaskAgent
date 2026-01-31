using UnityEngine;

[CreateAssetMenu(fileName = "Question", menuName = "Scriptable Objects/Question")]
public class QuestionConfig : ScriptableObject
{
    public string QuestionText;
    public string CorrectAnswer;
    public WrongAnswerBank WrongAnswerBank;
}