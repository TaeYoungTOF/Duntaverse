using System;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    [SerializeField] private Text dialogueText;

    private string[] dialogues;
    private int currentIndex = 0;
    private Action onFinish;
    private bool isDialogueStart = false;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (!UIManager.Instance.dialoguePanel.activeSelf) return;

        if (isDialogueStart)
        {
            isDialogueStart = false;
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space)|| Input.GetMouseButtonDown(0))
        {
            currentIndex++;
            if (currentIndex < dialogues.Length)
            {
                dialogueText.text = dialogues[currentIndex];
            }
            else
            {
                UIManager.Instance.ChangePanel(PanelName.Main);
                onFinish?.Invoke();
            }
        }
    }

    public void Show(string[] dialogues, Action onFinish)
    {
        this.dialogues = dialogues;
        currentIndex = 0;
        this.onFinish = onFinish;

        UIManager.Instance.ChangePanel(PanelName.Dialogue);
        dialogueText.text = dialogues[currentIndex];

        isDialogueStart = true;
    }
}
