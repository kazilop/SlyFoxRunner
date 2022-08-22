using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameStateShoper : GameState
{
    public GameObject shopUI;
    public TextMeshProUGUI totalGold;
    public TextMeshProUGUI currentItemName;
    public ItemLogic itemLogic;

    public GameObject itemPrefab;
    public Transform itemContainer;

    private Item[] items;

    private void Start()
    {
          items = Resources.LoadAll<Item>("Items/");
          PopulateShop();
    }

    public override void Construct()
    {
        GameManager.Instance.ChangeCamera(GameCamera.Shop);
        shopUI.SetActive(true);
        totalGold.text = SaveManager.Instance.save.Gold.ToString();
    }

    public override void Detruct()
    {
        shopUI.SetActive(false);
    }

    public void GoHome()
    {
        brain.ChangeState(GetComponent<GameStateInit>());
    }

    public void PopulateShop()
    {
        for (int i = 0; i < items.Length; i++)
        {
            int index = i;

            GameObject go = Instantiate(itemPrefab, itemContainer);

            go.GetComponent<Button>().onClick.AddListener(() => OnItemClick(index));
            go.transform.GetChild(1).GetComponent<Image>().sprite = items[index].Thumbnail;
            go.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = items[index].ItemName;
            go.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = items[index].ItemPrice.ToString();
        }
    }

    private void OnItemClick(int i)
    {
        currentItemName.text = items[i].ItemName;
        itemLogic.SelectItem(i);
    }
}
