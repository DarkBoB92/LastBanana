using System.Collections.Generic;
using UnityEngine;

public static class EndingsRegistry
{
    // master list of all endings in the game; order = display order on the endings screen
    public static readonly List<EndingInfo> AllEndings = new List<EndingInfo>
    {
        new EndingInfo("trex_sacrifice", "The Asteroid Theory",
            "You bravely leap into the T-Rex's mouth. The T-Rex chokes. The T-Rex dies. Scientists will later blame an asteroid. They will be wrong."),
        new EndingInfo("egypt_mummy", "Cultural Heritage",
            "You are wrapped in linen and entombed for 2,000 years. In 1922, an archaeologist opens your sarcophagus and immediately faints. You are now in the British Museum. Some would call this a win."),
        new EndingInfo("diner_paradox", "A Real Character",
            "You explain time travel in detail. The teenager has a stroke. History never produces a time machine. You are stranded in 1955 forever, slowly browning in a diner booth. The waitress thinks you're 'a real character.'"),
        new EndingInfo("war_messiah", "The Banana Messiah",
            "You become the Banana Messiah. You unite the fruit. You are loved. You are also extremely brown by this point. You die a hero, three days later, of natural causes. Statues are erected."),
        new EndingInfo("war_traitor", "The Operatic Tragedy",
            "Your betrayal becomes the central tragedy of the Fruit Wars. They write operas about you. None of them are flattering."),
        new EndingInfo("kitchen_bread", "Banana Bread",
            "You become the finest loaf of banana bread the kitchen has ever produced. The owner posts a photo. It gets 47 likes. You would have wanted it this way."),
    };

    private const string PrefKeyPrefix = "ending_unlocked_";

    public static void Unlock(string endingId)
    {
        if (string.IsNullOrEmpty(endingId)) return;
        PlayerPrefs.SetInt(PrefKeyPrefix + endingId, 1);
        PlayerPrefs.Save();
    }

    public static bool IsUnlocked(string endingId)
    {
        if (string.IsNullOrEmpty(endingId)) return false;
        return PlayerPrefs.GetInt(PrefKeyPrefix + endingId, 0) == 1;
    }

    public static int UnlockedCount()
    {
        int count = 0;
        foreach (EndingInfo e in AllEndings) if (IsUnlocked(e.id)) count++;
        return count;
    }

    public static int TotalCount() => AllEndings.Count;

    // for testing — wipe all unlocks
    public static void ResetAll()
    {
        foreach (EndingInfo e in AllEndings) PlayerPrefs.DeleteKey(PrefKeyPrefix + e.id);
        PlayerPrefs.Save();
    }
}

[System.Serializable]
public class EndingInfo
{
    public string id;
    public string title;
    public string text;

    public EndingInfo(string id, string title, string text)
    {
        this.id = id;
        this.title = title;
        this.text = text;
    }
}