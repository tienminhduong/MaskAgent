using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WrongAnswerBank", menuName = "Scriptable Objects/WrongAnswerBank")]
public class WrongAnswerBank : ScriptableObject
{
    public List<string> WrongAnswers = new();
}