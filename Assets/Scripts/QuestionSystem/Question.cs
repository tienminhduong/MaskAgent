using System;
using System.Collections.Generic;

[Serializable]
public class Question
{
    public string QuestionText;
    public string CorrectAnswer;
    public List<string> Options;
}