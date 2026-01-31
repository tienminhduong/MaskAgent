using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestionDatabase", menuName = "Scriptable Objects/QuestionDatabase")]
public class NoteInfoDatabase : ScriptableObject
{
    public List<NoteInfo> QuestionList = new List<NoteInfo>();

    public string CreateQuestion(HumanInfo humanInfo)
    {
        int randomIndex = UnityEngine.Random.Range(0, QuestionList.Count);
        NoteInfo question = QuestionList[randomIndex];

        string result = "";

        int infoIndex = 0;

        for (int i = 0; i < question.QuestionSegments.Count; i++)
        {
            if (i == question.InfoPositions[infoIndex].Position)
            {
                result += GetInfoByInfoType(humanInfo, question.InfoPositions[infoIndex].Info) + " ";
                infoIndex++;
            }
            result += question.QuestionSegments[i] + " ";
        }

        for (int i = infoIndex; i < question.InfoPositions.Count; i++)
        {
            result += GetInfoByInfoType(humanInfo, question.InfoPositions[i].Info) + " ";
        }
        return result;
    }

    public string GetInfoByInfoType(HumanInfo humanInfo, InfoType infoType)
    {
        switch (infoType)
        {
            case InfoType.Name:
                return humanInfo.Name;
            case InfoType.Address:
                return humanInfo.Address;
            case InfoType.RoomType:
                return "Room";
            case InfoType.Age:
                return humanInfo.Age.ToString();
            case InfoType.Role:
                return humanInfo.Role.ToString();
            case InfoType.DOB:
                return humanInfo.DOB;
        }
        return "";
    }
}

[Serializable]
public class NoteInfo
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

[Serializable]
public enum InfoType
{
    Name = 0,
    Age = 1,
    DOB = 2,
    Role = 3,
    Address = 4,
    RoomType = 5,
}
