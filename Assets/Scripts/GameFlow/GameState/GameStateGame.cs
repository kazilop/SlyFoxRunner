using UnityEngine;
using TMPro;

public class GameStateGame : GameState
{
    public GameObject gameUI;
    [SerializeField] TextMeshProUGUI goldCount;
    [SerializeField] TextMeshProUGUI scoreCount;
    public override void Construct()
    {
        GameManager.Instance.motor.ResumePlayer();
        GameManager.Instance.ChangeCamera(GameCamera.Game);

        GameStats.Instance.OnCollectGold += OnCollectGold;
        GameStats.Instance.OnScoreChange += OnScoreChange;

        gameUI.SetActive(true);
    }

    private void OnCollectGold(int amount)
    {
        goldCount.text = GameStats.Instance.GoldToText();
    }

    private void OnScoreChange(float score)
    {
        scoreCount.text = GameStats.Instance.ScoreToText();
    }
    
    public override void UpdateState()
    {
        GameManager.Instance.worldGeneration.ScanPosition();  
    }

    public override void Detruct()
    {
        gameUI.SetActive(false);

        GameStats.Instance.OnCollectGold -= OnCollectGold;
        GameStats.Instance.OnScoreChange -= OnScoreChange;
    }
}
