using UnityEngine;

public class BananaState : MonoBehaviour
{
    [Header("Rot Stage Sprites")]
    public Sprite[] rotStageSprites = new Sprite[5];

    [Header("Placeholder Colors")]
    public Color[] placeholderColors = new Color[]
    {
        new Color(1f, 0.95f, 0.2f),   // bright yellow
        new Color(0.95f, 0.85f, 0.2f), // freckled yellow
        new Color(0.8f, 0.7f, 0.15f),  // spotted
        new Color(0.55f, 0.4f, 0.1f),  // brown
        new Color(0.3f, 0.2f, 0.05f)   // mush
    };

    [Header("References")]
    public SpriteRenderer bananaRenderer;
    public TMPro.TextMeshPro placeholderLabel;

    private int currentStage = 1;
    public int CurrentStage => currentStage;

    void Start() => SetStage(1);

    public void SetStage(int stage)
    {
        currentStage = Mathf.Clamp(stage, 1, 5);
        int idx = currentStage - 1;

        if (rotStageSprites != null && idx < rotStageSprites.Length && rotStageSprites[idx] != null)
        {
            bananaRenderer.sprite = rotStageSprites[idx];
            bananaRenderer.color = Color.white;
            if (placeholderLabel) placeholderLabel.text = "";
        }
        else
        {
            bananaRenderer.sprite = null;
            bananaRenderer.color = placeholderColors[idx];
            if (placeholderLabel) placeholderLabel.text = $"BANANA\nstage {currentStage}";
        }
    }
}