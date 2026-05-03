using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewScene", menuName = "LastBanana/Scene Data")]
public class SceneData : ScriptableObject
{
    [Header("Scene Identity")]
    public string sceneTitle;       
    public Sprite background;      
    public Color placeholderColor = new Color(0.2f, 0.4f, 0.2f);

    [Header("Banana State")]
    public int bananaRotStage = 1; 

    [Header("NPCs in Scene")]
    public Sprite npcSprite;      
    public string npcName = "NPC";
    public Color npcPlaceholderColor = new Color(0.7f, 0.3f, 0.3f);

    [Header("Music")]
    public AudioClip backgroundMusic;

    [Header("Dialogue")]
    public List<DialogueLine> lines = new List<DialogueLine>();

    [Header("Choices (shown after last line)")]
    public List<Choice> choices = new List<Choice>();
}