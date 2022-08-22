using System;

[System.Serializable]
public class SaveState 
{
    [NonSerialized] private const int ITEM_COUNT = 16;
    public int Highscore { get; set; }
    public int Gold { get; set; }
    public DateTime LastSaveTime { get; set; }
    public int CurrentItem { get; set; }
    public byte[] UnlockedItemFlag { get; set; }

    public SaveState()
    {
        Highscore = 0;
        Gold = 0;
        LastSaveTime = DateTime.Now;
        CurrentItem = 0;

        UnlockedItemFlag = new byte[ITEM_COUNT];
        UnlockedItemFlag[0] = 1;
    }
}
