using System;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public enum PostDialogueAction
    {
        None,
        Quest
    }

    [SerializeField, TextArea(2, 5)] private string[] dialogues;

    [SerializeField] private PostDialogueAction postDialogueAction = PostDialogueAction.None;

    [SerializeField] private GameObject targetPanel;

    public void StartInteraction(Action onFinish)
    {
        DialogueManager.Instance.Show(dialogues, () =>
        {
            HandlePostDialogueAction();
            onFinish?.Invoke();
        });
    }

    private void HandlePostDialogueAction()
    {
        switch(postDialogueAction)
        {
            case PostDialogueAction.None:
                break;
            case PostDialogueAction.Quest:
                if (targetPanel != null)
                {
                    UIManager.Instance.ChangePanel(PanelName.None);
                    targetPanel.SetActive(true);
                }
                break;
        }
    }

    public void CloseTargetPanel()
    {
        targetPanel.SetActive(false);
        UIManager.Instance.ChangePanel(PanelName.Main);
    }
}