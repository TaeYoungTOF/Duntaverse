using UnityEngine;
using UnityEngine.UI;

public enum PanelName
{
    None,
    Main,
    Dialogue,
    Status
}

public class UIManager : MonoBehaviour
{

    public static UIManager Instance { get; private set; }

    [SerializeField] private GameObject mainPanel;
    [SerializeField] public GameObject dialoguePanel;
    [SerializeField] private GameObject statusPanel;
    public bool IsMainPanelOpen => mainPanel != null && mainPanel.activeSelf;
    
    [SerializeField] private Text Status_playerName;
    [SerializeField] private Text Status_playerJob;
    [SerializeField] private Text Status_playerPlaneScore;

    private void Awake()
    {
        Instance = this;

        mainPanel.SetActive(true);
        dialoguePanel.SetActive(false);
        statusPanel.SetActive(false);
    }

    private void Update()
    {
        UpdateStatusPanel();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (statusPanel.activeSelf)
            {
                ChangePanel(PanelName.Main);
            }
            else
            {
                ChangePanel(PanelName.Status);
            }
        }
    }

    private void UpdateStatusPanel()
    {
        Status_playerName.text = Player.Instance.PlayerName;
        Status_playerJob.text = Player.Instance.PlayerJob.ToString();
        Status_playerPlaneScore.text = $"{GameManager.Instance.FlappyPlaneScore.ToString()} M";
    }

    public void ChangePanel(PanelName panelName)
    {
        mainPanel.SetActive(panelName == PanelName.Main);
        dialoguePanel.SetActive(panelName == PanelName.Dialogue);
        statusPanel.SetActive(panelName == PanelName.Status);
    }
}
