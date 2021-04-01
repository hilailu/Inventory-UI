using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPick : MonoBehaviour
{
    [SerializeField] private Item item;
    public void OnMouseDown()
    {
        Pick();
        Raycasting._selected = null;
    }
    public void Pick()
    {
        if (Raycasting._selected != null && InventoryManager.instance.Add(item))
            Destroy(gameObject);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Terrain" && transform.GetComponent<ItemPick>().item.isFragile)
        {
            transform.GetComponentInChildren<ParticleSystem>().Play();
            Destroy(transform.gameObject, 0.8f);
        }
    }
}
