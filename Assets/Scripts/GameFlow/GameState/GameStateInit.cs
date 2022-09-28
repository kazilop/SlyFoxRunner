using UnityEngine;
using TMPro;
using UnityEngine.Advertisements;

public class GameStateInit : GameState
{
    public GameObject menuUI;
    [SerializeField] private TextMeshProUGUI hiscoreText;
    [SerializeField] private TextMeshProUGUI goldCountText;

    private RewardedAds _rewardedAds;
    public override void Construct()
    {
        
        GameManager.Instance.ChangeCamera(GameCamera.Init);

        hiscoreText.text = "Highscore: " + SaveManager.Instance.save.Highscore.ToString();
        goldCountText.text = "Gold: " + SaveManager.Instance.save.Gold.ToString();

        menuUI.SetActive(true);

        _rewardedAds = GetComponent<RewardedAds>();
       
        // _rewardedAds.ShowAd();
    }

    public override void Detruct()
    {
        menuUI.SetActive(false);
    }

    public void OnPlayClick()
    {
        brain.ChangeState(GetComponent<GameStateGame>());
        GameStats.Instance.ResetSession();
    }

    public void OnShopClick()
    {
        brain.ChangeState(GetComponent<GameStateShoper>());
    }

    public void OnAboutClick()
    {
        brain.ChangeState(GetComponent<GameStateAbout>());
    }

}
