using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerData
{
    [JsonProperty("coins")]
    public int coins;
    [JsonProperty("position")]
    public float[] position;
    [JsonProperty("items")]
    public List<Item> items;

    public PlayerData(PlayerController player)
    {
        coins = player.money;
        position = new float[3];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;
        items = new List<Item>(InventoryManager.instance.items);
    }
}
