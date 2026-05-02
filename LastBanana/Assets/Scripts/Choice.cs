using UnityEngine;

[System.Serializable]
public class Choice
{
    [TextArea(1, 3)]
    public string label;            // shown on the button
    public SceneData nextScene;     // which scene to load next
    public int startAtLineIndex = 0; // optional: jump into a specific line of that scene
    public bool endsGame = false;   // if true, shows credits instead
    [TextArea(1, 4)]
    public string endingText;       // shown on game-over if endsGame is true
}