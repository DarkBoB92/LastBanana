using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [Header("UI References")]
    public GameObject dialoguePanel;
    public TextMeshProUGUI speakerLabel;
    public TextMeshProUGUI dialogueText;
    public GameObject continueIndicator;

    [Header("Typewriter")]
    public float charactersPerSecond = 45f;

    [Header("Audio")]
    public AudioSource sfxSource;

    private Coroutine typingRoutine;
    private string currentFullText;
    private bool isTyping;
    public bool IsTyping => isTyping;

    public void ShowLine(DialogueLine line)
    {
        dialoguePanel.SetActive(true);
        speakerLabel.text = string.IsNullOrEmpty(line.speaker) ? "" : line.speaker;
        currentFullText = line.text;

        if (typingRoutine != null) StopCoroutine(typingRoutine);
        typingRoutine = StartCoroutine(TypeText(line.text));

        if (line.sfx && sfxSource) sfxSource.PlayOneShot(line.sfx);
    }

    public void CompleteLine()
    {
        if (typingRoutine != null) StopCoroutine(typingRoutine);
        dialogueText.text = currentFullText;
        isTyping = false;
        if (continueIndicator) continueIndicator.SetActive(true);
    }

    public void Hide()
    {
        dialoguePanel.SetActive(false);
        if (continueIndicator) continueIndicator.SetActive(false);
    }

    private IEnumerator TypeText(string text)
    {
        isTyping = true;
        if (continueIndicator) continueIndicator.SetActive(false);
        dialogueText.text = "";
        float delay = 1f / Mathf.Max(1f, charactersPerSecond);

        for (int i = 0; i < text.Length; i++)
        {
            dialogueText.text += text[i];
            yield return new WaitForSeconds(delay);
        }

        isTyping = false;
        if (continueIndicator) continueIndicator.SetActive(true);
    }
}