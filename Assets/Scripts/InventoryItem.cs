using UnityEngine;
using UnityEngine.UI;


public class InventoryItem : MonoBehaviour
{
    [SerializeField] private Image icon;
    public Text amount;
    public Item _item;

    public void Add(Item item)
    {
        _item = item;
        icon.sprite = item.icon;
        icon.enabled = true;
        amount.enabled = true;
    }

    public void Remove()
    {
        _item = null;
        icon.sprite = null;
        icon.enabled = false;
        amount.enabled = false;
    }
}