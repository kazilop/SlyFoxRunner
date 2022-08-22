using UnityEngine;

[CreateAssetMenu(fileName = "Items")]
public class Item : ScriptableObject
{
    public string ItemName;
    public int ItemPrice;
    public Sprite Thumbnail;
    public GameObject Model;
}
