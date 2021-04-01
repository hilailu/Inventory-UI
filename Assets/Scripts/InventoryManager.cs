using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System;

public class InventoryManager : MonoBehaviour
{
    #region Singleton
    public static InventoryManager instance;
    private void Awake()
    {
        if (instance != null)
            return;
        instance = this;
    }
    #endregion

    [SerializeField] private GameObject panel;
    [SerializeField] private Text descText;
    [SerializeField] private Text itemName;
    [SerializeField] private GameObject description;
    [SerializeField] private GameObject player;
    public List<Item> items = new List<Item>(); 
    private string prevClickedButton = "";
    private int storage = 7;
    public InventoryItem[] inventoryItems;

    public Action OnInventoryChanged;

    void Start()
    {
        instance.OnInventoryChanged += UpdateUI;
        inventoryItems = panel.GetComponentsInChildren<InventoryItem>(true);
        UpdateUI();
    }

    private void UpdateUI()
    {
        for (int i = 0; i < inventoryItems.Length; i++)
        {
            if (i < items.Count)
            {
                inventoryItems[i].Add(items[i]);
                inventoryItems[i].amount.text = items[i].amount.ToString();
            }
            else
            {
                inventoryItems[i].Remove();
            }
        }
    }

    public bool Add(Item item)
    {
        if (items.Count >= storage)
            return false;
        if (!items.Contains(item))
            items.Add(item);
        item.amount++;
        OnInventoryChanged?.Invoke();
        return true;
    }

    public void Remove(Item item)
    {
        items.Remove(item);
        OnInventoryChanged?.Invoke();
    }

    public void OnClick()
    {
        var button = EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
        var item = button.GetComponentInChildren<InventoryItem>()._item;
        if (item == null)
            description.SetActive(false);
        else
        {
            if (description.activeSelf && item.itemName == prevClickedButton) 
                description.SetActive(false);
            else
            {
                description.SetActive(true);
                descText.text = item.description;
                itemName.text = item.itemName;
            }
            prevClickedButton = item.itemName;
        }
    }

    public void Drop()
    {
        InventoryItem invItem = default;
        inventoryItems = panel.GetComponentsInChildren<InventoryItem>(true);
        foreach (InventoryItem t in inventoryItems)
        {
            if (t._item.itemName == prevClickedButton)
            {
                invItem = t;
                break;
            }
        }
        if (invItem._item.amount > 0)
        {
            invItem.amount.text = (--invItem._item.amount).ToString();
            Vector3 place = player.transform.position + player.transform.forward*2f;
            Instantiate(Resources.Load(invItem._item.itemName), place, Quaternion.identity);
        }
        if (invItem._item.amount <= 0)
        {
            Remove(invItem._item);
            description.SetActive(false);
        }
    }
}
