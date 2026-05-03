using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceManager : MonoBehaviour
{
    [Header("UI References")]
    public GameObject choicePanel;
    public Button choiceButtonPrefab;
    public Transform choiceButtonContainer;

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
        Hide();
        onChoiceSelected?.Invoke(choice);
    }

    private void ClearButtons()
    {
        foreach (var b in spawnedButtons) if (b) Destroy(b.gameObject);
        spawnedButtons.Clear();
    }
}