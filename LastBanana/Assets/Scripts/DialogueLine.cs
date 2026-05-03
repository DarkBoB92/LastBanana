using UnityEngine;

[System.Serializable]
public class DialogueLine
{
    public string speaker;          
    [TextArea(2, 6)]
    public string text;

    [Header("Optional")]
    public Sprite speakerSprite;    
    public int rotStageOverride = -1;
    public AudioClip sfx;           
}