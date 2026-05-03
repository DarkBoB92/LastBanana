using UnityEngine;

[System.Serializable]
public class Choice
{
    [TextArea(1, 3)]
    public string label;            
    public SceneData nextScene;     
    public int startAtLineIndex = 0; 
    public bool endsGame = false;   
    [TextArea(1, 4)]
    public string endingText;       

    [Header("Ending tracking")]
    public string endingId;       
    public string endingTitle;    
}