using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameStateDeath : GameState
{
    public GameObject deathUI;
    [SerializeField] private TextMeshProUGUI highscore;
    [SerializeField] private TextMeshProUGUI currentScore;
    [SerializeField] private TextMeshProUGUI goldTotal;
    [SerializeField] private TextMeshProUGUI currentGold;

    [SerializeField] private Image completionCircle;

    private float deathTime;

    public float timeToDecision = 3.0f;
    public override void Construct()
    {
        GameManager.Instance.motor.PausePlayer();
        deathUI.SetActive(true);
        completionCircle.gameObject.SetActive(true);
        deathTime = Time.time;

        if (SaveManager.Instance.save.Highscore < (int)GameStats.Instance.score)
        {
            SaveManager.Instance.save.Highscore = (int)GameStats.Instance.score;
            currentScore.color = Color.green;
        }
        else
        {
            currentScore.color = Color.white;
        }

        SaveManager.Instance.save.Gold += GameStats.Instance.GoldCollectedThisRound;

        SaveManager.Instance.Save();

        highscore.text = "Highscore: " + SaveManager.Instance.save.Highscore;
        currentScore.text = GameStats.Instance.ScoreToText();
        goldTotal.text = "Total gold: " + SaveManager.Instance.save.Gold;
        currentGold.text = GameStats.Instance.GoldToText();
    }

    public override void UpdateState()
    {
        float ratio = (Time.time - deathTime) / timeToDecision;
        completionCircle.color = Color.Lerp(Color.green, Color.red, ratio);
        completionCircle.fillAmount = 1 - ratio;

        if(ratio > 1)
        {
            completionCircle.gameObject.SetActive(false);
        }
    }

    public override void Detruct()
    {
        deathUI.SetActive(false);
    }
    public void ToMenu()
    {


        brain.ChangeState(GetComponent<GameStateInit>());

        GameManager.Instance.motor.ResetPlayer();
       
        GameManager.Instance.worldGeneration.ResetWorld();
        GameManager.Instance.sceneChunkGeneration.ResetWorld();

        
    }

    public void ResumeGame()
    {
        brain.ChangeState(GetComponent<GameStateGame>());
        GameManager.Instance.motor.RespawnPlayer();      
    }
}
