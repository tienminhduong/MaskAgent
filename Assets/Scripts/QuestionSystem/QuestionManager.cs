using System.Collections.Generic;
using UnityEngine;

public class QuestionManager : Singleton<QuestionManager>
{
    [Header("SOs")]
    [SerializeField] private QuestionTypeConfig[] questionTypeConfig;
    [SerializeField] private List<HumanInfo> inLevelHumanInfos = new();

    [Header("Readonly")]
    [SerializeField] private List<HumanInfo> copiedHumanInfos = new();
    [SerializeField] private HumanInfo playerHumanInfo;

    [SerializeField] private Question currentQuestion;

    public void GenerateQuestion()
    {
        // select random question from config
        var typeConfig = questionTypeConfig[Random.Range(0, questionTypeConfig.Length)];
        var correctHumanInfo = copiedHumanInfos[Random.Range(0, copiedHumanInfos.Count)];

        var questionText = typeConfig.Questions[Random.Range(0, typeConfig.Questions.Count)];
        questionText = FormatQuestionText(questionText, playerHumanInfo.Name, correctHumanInfo.Name);

        var correctAnswer = GetInfoProperty(correctHumanInfo, typeConfig.Type);
        var wrongAnswers = new List<string>();

        int loopGuard = 0;
        while (wrongAnswers.Count < 2)
        {
            loopGuard++;
            if (loopGuard > 100)
            {
                Debug.LogWarning("GenerateAgeTypeQuestion: Loop guard triggered");
                break;
            }

            var randomHumanInfo = inLevelHumanInfos[Random.Range(0, inLevelHumanInfos.Count)];
            var wrongAnswer = GetInfoProperty(randomHumanInfo, typeConfig.Type);
            if (wrongAnswer != correctAnswer && !wrongAnswers.Contains(wrongAnswer))
            {
                wrongAnswers.Add(wrongAnswer);
            }
        }

        currentQuestion = new Question
        {
            QuestionText = questionText,
            CorrectAnswer = correctAnswer,
            Options = new List<string>(wrongAnswers) { correctAnswer }
        };

        currentQuestion.Options.Shuffle();
    }

    private string GetInfoProperty(HumanInfo humanInfo, QuestionTypeConfig.QuestionType infoType)
    {
        return infoType switch
        {
            QuestionTypeConfig.QuestionType.AgeQuestion => humanInfo.Age.ToString(),
            QuestionTypeConfig.QuestionType.DOBQuestion => humanInfo.DOB,
            QuestionTypeConfig.QuestionType.RoleQuestion => humanInfo.Role.ToString(),
            QuestionTypeConfig.QuestionType.AddressQuestion => humanInfo.Address,
            _ => throw new System.NotImplementedException(),
        };
    }

    private string FormatQuestionText(string template, string yourName, string name)
    {
        bool isYou = name == yourName;
        return template.Replace("{name}", isYou ? "you" : name)
                       .Replace("{padj}", isYou ? "your" : $"{name}'s")
                       .Replace("{do}", isYou ? "do" : "does")
                       .Replace("{be}", isYou ? "are" : "is")
                       .Replace("{bev2}", isYou ? "were" : "was");
    }

    public bool EvaluateAnswer(string answer)
    {
        return currentQuestion.CorrectAnswer == answer;
    }

    public Question CurrentQuestion => currentQuestion;
}