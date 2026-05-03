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

    [Header("Text Blip SFX")]
    public AudioClip blipLow;       
    public AudioClip blipHigh;      
    [Tooltip("Play a blip every N characters. 1 = every char (loud!), 3 = every 3rd char (good default)")]
    public int blipEveryNChars = 3;
    [Range(0f, 1f)] public float blipVolume = 0.3f;

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

        bool useLow = true; 
        int charsTyped = 0;

        for (int i = 0; i < text.Length; i++)
        {
            char c = text[i];
            dialogueText.text += c;

            if (!char.IsWhiteSpace(c))
            {
                if (charsTyped % Mathf.Max(1, blipEveryNChars) == 0)
                {
                    PlayBlip(useLow);
                    useLow = !useLow;
                }
                charsTyped++;
            }

            yield return new WaitForSeconds(delay);
        }

        isTyping = false;
        if (continueIndicator) continueIndicator.SetActive(true);
    }

    private void PlayBlip(bool low)
    {
        if (sfxSource == null) return;
        var clip = low ? blipLow : blipHigh;
        if (clip == null) return;
        sfxSource.PlayOneShot(clip, blipVolume);
    }
}