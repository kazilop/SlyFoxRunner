using UnityEngine;
using UnityEngine.Advertisements;

public class AdManager : MonoBehaviour, IUnityAdsInitializationListener
{

    public static AdManager Instance;

    [SerializeField] string _androidGameId;
    [SerializeField] string _iOSGameId;
    [SerializeField] bool _testMode = false;
    private string _gameId;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        InitializeAds();
    }


    public void InitializeAds()
    {
        _gameId = (Application.platform == RuntimePlatform.IPhonePlayer)
            ? _iOSGameId
            : _androidGameId;
        Advertisement.Initialize(_gameId, _testMode, this);
    }

    public void OnInitializationComplete()
    {
        Debug.Log("Ads initailization complete");
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log("Ads error");
    }
   
}