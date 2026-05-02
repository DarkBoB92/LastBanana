using UnityEngine;

[System.Serializable]
public class DialogueLine
{
    public string speaker;          // "Banana", "T-Rex", "Cleopatra", "Narrator", etc.
    [TextArea(2, 6)]
    public string text;

    [Header("Optional")]
    public Sprite speakerSprite;    // overrides default for this line; leave null to use defaults
    public int rotStageOverride = -1; // -1 = no change; 1..5 = force banana to this rot stage
    public AudioClip sfx;           // plays when this line appears
}