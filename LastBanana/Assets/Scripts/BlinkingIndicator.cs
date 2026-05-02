using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class BlinkingIndicator : MonoBehaviour
{
    [Tooltip("How fast the indicator blinks (cycles per second)")]
    public float blinkRate = 2f;

    [Tooltip("Lowest alpha during blink (0 = fully invisible, 1 = fully visible)")]
    [Range(0f, 1f)] public float minAlpha = 0.1f;

    [Tooltip("Highest alpha during blink")]
    [Range(0f, 1f)] public float maxAlpha = 1f;

    private TextMeshProUGUI text;

    void Awake() => text = GetComponent<TextMeshProUGUI>();

    void OnEnable()
    {
        // reset to visible immediately when re-enabled
        if (text != null) SetAlpha(maxAlpha);
    }

    void Update()
    {
        float t = (Mathf.Sin(Time.time * blinkRate * Mathf.PI * 2f) + 1f) * 0.5f;
        float alpha = Mathf.Lerp(minAlpha, maxAlpha, t);
        SetAlpha(alpha);
    }

    private void SetAlpha(float a)
    {
        var c = text.color;
        c.a = a;
        text.color = c;
    }
}