
using System.Collections.Generic;

[System.Serializable]
public class EndingsData
{
    public Dictionary<MainGameManager.GameEnds, bool> endings = new Dictionary<MainGameManager.GameEnds, bool>();

    public EndingsData()
    {
        this.endings.Add(MainGameManager.GameEnds.WalkAway, false);
        this.endings.Add(MainGameManager.GameEnds.Warrior, false);
        this.endings.Add(MainGameManager.GameEnds.Creative, false);
        this.endings.Add(MainGameManager.GameEnds.Mentor, false);
        this.endings.Add(MainGameManager.GameEnds.Rebel, false);
        this.endings.Add(MainGameManager.GameEnds.Secret, false);
    }
}
