using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item")]
public class Item : ScriptableObject
{
    public string itemName = "Empty";
    public Sprite icon = null;
    public string description = null;
    public uint amount = 0;
    public bool isFragile = false;
}
