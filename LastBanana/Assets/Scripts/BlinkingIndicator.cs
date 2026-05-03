using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class BlinkingIndicator : MonoBehaviour
{
    public float blinkRate = 2f;

    [Range(0f, 1f)] public float minAlpha = 0.1f;

    [Range(0f, 1f)] public float maxAlpha = 1f;

    private TextMeshProUGUI text;

    void Awake() => text = GetComponent<TextMeshProUGUI>();

    void OnEnable()
    {
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