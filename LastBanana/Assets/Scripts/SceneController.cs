using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    [Header("Starting Scene")]
    public SceneData startingScene;

    [Header("References")]
    public DialogueManager dialogueManager;
    public ChoiceManager choiceManager;
    public BananaState banana;

    [Header("Background")]
    public SpriteRenderer backgroundRenderer;

    [Header("NPC")]
    public SpriteRenderer npcRenderer;
    public TextMeshPro npcPlaceholderLabel;

    [Header("Scene Title Card")]
    public GameObject titleCardPanel;
    public TextMeshProUGUI titleCardLabel;
    public float titleCardDuration = 2f;

    [Header("Ending Screen")]
    public GameObject endingPanel;
    public TextMeshProUGUI endingText;

    [Header("Audio")]
    public AudioSource musicSource;

    private SceneData currentScene;
    private int currentLineIndex;
    private bool waitingForChoice;
    private bool sceneFinished;

    void Start()
    {
        if (endingPanel) endingPanel.SetActive(false);
        if (titleCardPanel) titleCardPanel.SetActive(false);
        StartCoroutine(LoadScene(startingScene));
    }

    void Update()
    {
        if (sceneFinished || waitingForChoice) return;
        if (currentScene == null) return;

        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            if (dialogueManager.IsTyping) dialogueManager.CompleteLine();
            else AdvanceLine();
        }
    }

    private IEnumerator LoadScene(SceneData scene, int startAtLine = 0)
    {
        currentScene = scene;
        currentLineIndex = startAtLine;
        sceneFinished = false;
        waitingForChoice = false;

        // background
        if (scene.background != null)
        {
            backgroundRenderer.sprite = scene.background;
            backgroundRenderer.color = Color.white;
        }
        else
        {
            backgroundRenderer.sprite = null;
            backgroundRenderer.color = scene.placeholderColor;
        }

        // npc
        if (scene.npcSprite != null)
        {
            npcRenderer.sprite = scene.npcSprite;
            npcRenderer.color = Color.white;
            if (npcPlaceholderLabel) npcPlaceholderLabel.text = "";
        }
        else
        {
            npcRenderer.sprite = null;
            npcRenderer.color = scene.npcPlaceholderColor;
            if (npcPlaceholderLabel) npcPlaceholderLabel.text = scene.npcName;
        }

        // banana rot stage
        banana.SetStage(scene.bananaRotStage);

        // music
        if (musicSource && scene.backgroundMusic && musicSource.clip != scene.backgroundMusic)
        {
            musicSource.clip = scene.backgroundMusic;
            musicSource.loop = true;
            musicSource.Play();
        }

        // title card
        if (titleCardPanel && !string.IsNullOrEmpty(scene.sceneTitle))
        {
            titleCardLabel.text = scene.sceneTitle;
            titleCardPanel.SetActive(true);
            yield return new WaitForSeconds(titleCardDuration);
            titleCardPanel.SetActive(false);
        }

        // start dialogue
        ShowCurrentLine();
    }

    private void ShowCurrentLine()
    {
        if (currentScene == null || currentLineIndex >= currentScene.lines.Count)
        {
            ShowChoices();
            return;
        }

        var line = currentScene.lines[currentLineIndex];
        if (line.rotStageOverride > 0) banana.SetStage(line.rotStageOverride);
        dialogueManager.ShowLine(line);
    }

    private void AdvanceLine()
    {
        currentLineIndex++;
        ShowCurrentLine();
    }

    private void ShowChoices()
    {
        dialogueManager.Hide();

        if (currentScene.choices == null || currentScene.choices.Count == 0)
        {
            sceneFinished = true;
            return;
        }

        waitingForChoice = true;
        choiceManager.ShowChoices(currentScene.choices, OnChoiceSelected);
    }

    private void OnChoiceSelected(Choice choice)
    {
        waitingForChoice = false;

        if (choice.endsGame)
        {
            ShowEnding(choice.endingText);
            return;
        }

        if (choice.nextScene != null)
        {
            StartCoroutine(LoadScene(choice.nextScene, choice.startAtLineIndex));
        }
        else
        {
            sceneFinished = true;
        }
    }

    private void ShowEnding(string text)
    {
        sceneFinished = true;
        dialogueManager.Hide();
        if (endingPanel)
        {
            endingPanel.SetActive(true);
            if (endingText) endingText.text = text;
        }
    }
}