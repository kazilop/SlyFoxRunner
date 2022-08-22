using System.Collections.Generic;
using UnityEngine;

public class ItemLogic : MonoBehaviour
{
    [SerializeField] private Transform itemContainer;
    private List<GameObject> itemModels = new List<GameObject>();
    [SerializeField] private Item[] items;

    private void Awake()
    {
        items = Resources.LoadAll<Item>("Items/");
       // SpawnItemOnPlayer();
    }

    private void Start()
    {
        SpawnItemOnPlayer();
        SelectItem(SaveManager.Instance.save.CurrentItem);
    }

    private void SpawnItemOnPlayer()
    {
        for (int i = 0; i < items.Length; i++)
        {
            itemModels.Add(Instantiate(items[i].Model, itemContainer) as GameObject);
        }
    }

    public void DisableAllItems()
    {
        for(int i = 0; i < itemModels.Count; i++)
        {
            itemModels[i].SetActive(false);
        }
    }

    public void SelectItem(int index)
    {
        DisableAllItems();
        itemModels[index].SetActive(true);
    }
}
