using UnityEngine;
using System;

public class GameStats : MonoBehaviour
{
    public static GameStats Instance { get { return instance; } }
    private static GameStats instance;

    public float score;
    public float highscore;
    public float distanceModifier = 0.2f;

    public int totalGold;
    public int GoldCollectedThisRound;
    public float pointsPerGold = 10.0f;

    private float lastScoreUpdate;
    private float scoreUpdateDelta = 0.2f;

    public Action<int> OnCollectGold;
    public Action<float> OnScoreChange;

    private void Awake()
    {
        instance = this;
    }
    private void Update()
    {
        float s = GameManager.Instance.motor.transform.position.z * distanceModifier;
        s += GoldCollectedThisRound * pointsPerGold;

        if (s > score)
        {
            score = s;

            if (Time.time - lastScoreUpdate > scoreUpdateDelta)
            {
                lastScoreUpdate = Time.time;
                OnScoreChange?.Invoke(score);
            }
        }
    }
    public void CollectGold()
    {
        GoldCollectedThisRound++;
        OnCollectGold?.Invoke(GoldCollectedThisRound);
    }

    public void ResetSession()
    {
        score = 0;
        GoldCollectedThisRound = 0;

        OnCollectGold?.Invoke(GoldCollectedThisRound);
        OnScoreChange?.Invoke(score);
    }

    public string ScoreToText()
    {
        return score.ToString("000000");
    }

    public string GoldToText()
    {
        return GoldCollectedThisRound.ToString("000");
    }
}
