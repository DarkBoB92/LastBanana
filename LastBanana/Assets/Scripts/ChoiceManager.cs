using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChoiceManager : MonoBehaviour
{
    [Header("UI References")]
    public GameObject choicePanel;
    public Button choiceButtonPrefab;
    public Transform choiceButtonContainer;

    [Header("Audio")]
    public AudioSource sfxSource;
    public AudioClip selectSfx;
    [Range(0f, 1f)] public float selectVolume = 0.6f;
    public AudioClip hoverSfx;
    [Range(0f, 1f)] public float hoverVolume = 0.25f;

    private List<Button> spawnedButtons = new List<Button>();
    private Action<Choice> onChoiceSelected;

    public void ShowChoices(List<Choice> choices, Action<Choice> callback)
    {
        ClearButtons();
        onChoiceSelected = callback;
        choicePanel.SetActive(true);

        foreach (var choice in choices)
        {
            var btn = Instantiate(choiceButtonPrefab, choiceButtonContainer);
            var label = btn.GetComponentInChildren<TextMeshProUGUI>();
            if (label) label.text = choice.label;

            var capturedChoice = choice;
            btn.onClick.AddListener(() => Select(capturedChoice));

            AddHoverHandler(btn.gameObject);

            spawnedButtons.Add(btn);
        }
    }

    public void Hide()
    {
        choicePanel.SetActive(false);
        ClearButtons();
    }

    private void Select(Choice choice)
    {
        if (sfxSource && selectSfx) sfxSource.PlayOneShot(selectSfx, selectVolume);
        Hide();
        onChoiceSelected?.Invoke(choice);
    }

    private void AddHoverHandler(GameObject buttonGO)
    {
        var trigger = buttonGO.GetComponent<EventTrigger>();
        if (trigger == null) trigger = buttonGO.AddComponent<EventTrigger>();

        var entry = new EventTrigger.Entry { eventID = EventTriggerType.PointerEnter };
        entry.callback.AddListener((_) => PlayHover());
        trigger.triggers.Add(entry);
    }

    private void PlayHover()
    {
        if (sfxSource && hoverSfx) sfxSource.PlayOneShot(hoverSfx, hoverVolume);
    }

    private void ClearButtons()
    {
        foreach (var b in spawnedButtons) if (b) Destroy(b.gameObject);
        spawnedButtons.Clear();
    }
}