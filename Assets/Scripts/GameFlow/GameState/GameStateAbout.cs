using UnityEngine;

public class GameStateAbout : GameState
{
    public GameObject AboutUI;


    public override void Construct()
    {
        AboutUI.SetActive(true);
    }

    public override void Detruct()
    {
        AboutUI.SetActive(false);
    }

    public void ToMenu()
    {
        brain.ChangeState(GetComponent<GameStateInit>());
        GameStats.Instance.ResetSession();
    }
}
