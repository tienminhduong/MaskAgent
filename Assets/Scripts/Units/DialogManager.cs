using UnityEngine;

public class DialogManager : MonoBehaviour
{
    [SerializeField] private DialogPanelController dialogPanelController;
    // private IInteractable interactingHuman;

    // private void Start()
    // {
    //    dialogPanelController.gameObject.SetActive(true);
    //    dialogPanelController.ShowDialog(
    //        "What would you like to do?",
    //        new string[] { "Explore the area", "Check inventory", "Rest for a while" }
    //    );
    // }

    public void ShowQuestionDialog()
    {
        QuestionManager.Instance.GenerateQuestion();

        dialogPanelController.gameObject.SetActive(true);
        dialogPanelController.ShowDialog(
            QuestionManager.Instance.CurrentQuestion.QuestionText,
            QuestionManager.Instance.CurrentQuestion.Options.ToArray()
        );
    }

    public void ShowHumanInteractDialog(IInteractable human)
    {
        Debug.Log("ShowHumanInteractDialog called");
        if (human is not BaseCharacter)
            return;

        dialogPanelController.gameObject.SetActive(true);
        dialogPanelController.ShowDialog(
            "What do you need from me?",
            new string[] { ButtonOption.LURE }
        );
    }
}
