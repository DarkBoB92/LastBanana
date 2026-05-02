using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [Header("Main Menu")]
    public GameObject mainMenuPanel;
    public Button startButton;
    public Button endingsButton;
    public Button quitButton;

    [Header("Endings Screen")]
    public GameObject endingsPanel;
    public Transform endingsListContainer;
    public TextMeshProUGUI endingsCountLabel;
    public Button backButton;

    [Header("Ending Entry Prefab")]
    public GameObject endingEntryPrefab; // a TextMeshProUGUI inside a layout-friendly container

    [Header("Game Scene")]
    public string gameSceneName = "Main";

    void Start()
    {
        startButton.onClick.AddListener(OnStart);
        endingsButton.onClick.AddListener(OnShowEndings);
        quitButton.onClick.AddListener(OnQuit);
        backButton.onClick.AddListener(OnHideEndings);

        endingsPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

    private void OnStart() => SceneManager.LoadScene(gameSceneName);

    private void OnQuit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    private void OnShowEndings()
    {
        BuildEndingsList();
        mainMenuPanel.SetActive(false);
        endingsPanel.SetActive(true);
    }

    private void OnHideEndings()
    {
        endingsPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
        ClearEndingsList();
    }

    private void BuildEndingsList()
    {
        ClearEndingsList();

        endingsCountLabel.text = $"{EndingsRegistry.UnlockedCount()} / {EndingsRegistry.TotalCount()} discovered";

        foreach (var ending in EndingsRegistry.AllEndings)
        {
            var entry = Instantiate(endingEntryPrefab, endingsListContainer);
            var label = entry.GetComponentInChildren<TextMeshProUGUI>();
            if (label == null) continue;

            if (EndingsRegistry.IsUnlocked(ending.id))
            {
                label.text = $"<size=28><b><color=#FFD84A>{ending.title}</color></b></size>\n\n<size=22>{ending.text}</size>";
            }
            else
            {
                label.text = $"<size=28><b><color=#777777>??? — Locked</color></b></size>\n\n<size=22><i><color=#888888>An ending you haven't found yet.</color></i></size>";
            }
        }
    }

    private void ClearEndingsList()
    {
        for (int i = endingsListContainer.childCount - 1; i >= 0; i--)
            Destroy(endingsListContainer.GetChild(i).gameObject);
    }
}