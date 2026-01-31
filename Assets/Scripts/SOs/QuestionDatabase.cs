using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestionDatabase", menuName = "Scriptable Objects/QuestionDatabase")]
public class QuestionDatabase : ScriptableObject
{
    public List<Question> QuestionList = new List<Question>();

    public void CreateQuestion(HumanInfo humanInfo)
    {

    }
}

[Serializable]
public class Question
{
    public List<string> QuestionSegments = new List<string>();
    public List<InfoPosition> InfoPositions = new List<InfoPosition>();
}

[Serializable]
public class InfoPosition
{
    public InfoType Info;
    public int Position;
}

public enum InfoType
{
    Name = 0,
    Age = 1,
    DOB = 2,
    Role = 3,
    Address = 4,
}
