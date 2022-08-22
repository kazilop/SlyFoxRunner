using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameStateShop : GameState
{
    public GameObject shopUI;
    public TextMeshProUGUI totalGold;

    public GameObject itemPrefab;
    public Transform itemContainer;

    private Item[] items;

    private void Awake()
    {
      //  items = Resources.LoadAll<Item>("Items/");
      //  PopulateShop();
    }

    public override void Construct()
    {
        GameManager.Instance.ChangeCamera(GameCamera.Shop);

        totalGold.text = SaveManager.Instance.save.Gold.ToString();

        shopUI.SetActive(true);
    }

    public override void Detruct()
    {
        shopUI.SetActive(false);
    }

    public void OnHomeClick()
    {
        brain.ChangeState(GetComponent<GameStateInit>());
        GameStats.Instance.ResetSession();
    }

    public void GoHome()
    {
        brain.ChangeState(GetComponent<GameStateInit>());
    }

    public void PopulateShop()
    {
        for(int i = 0; i < items.Length; i++)
        {
            GameObject go = Instantiate(itemPrefab, itemContainer);

            go.GetComponent<Button>().onClick.AddListener(() => OnItemClick(i));
            go.transform.GetChild(1).GetComponent<Image>().sprite = items[i].Thumbnail;
            go.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = items[i].ItemName;
            go.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = items[i].ItemPrice.ToString();
        }
    }

    private void OnItemClick(int i)
    {
        Debug.Log(" Item " + i);
    }
}
