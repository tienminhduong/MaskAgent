using UnityEngine;

public class NoteInfoGenerator : MonoBehaviour
{
    [SerializeField] private QuestionDatabase _questionDatabase = null;
    [SerializeField] private HumanInfo _humanInfo = null;

    private string _infoString = "";

    private void Start()
    {
        _infoString = _questionDatabase.CreateQuestion(_humanInfo);
        Debug.Log(_infoString);
    }
}
