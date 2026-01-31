using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestionTypeConfig", menuName = "Scriptable Objects/QuestionTypeConfig")]
public class QuestionTypeConfig : ScriptableObject
{
    public enum QuestionType
    {
        AgeQuestion,
        DOBQuestion,
        RoleQuestion,
        AddressQuestion
    }

    public QuestionType Type;
    public List<string> Questions = new();
}