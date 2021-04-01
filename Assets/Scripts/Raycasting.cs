using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycasting : MonoBehaviour
{
    [SerializeField] private Camera main;
    [SerializeField] private Animator anim;
    public static Transform _selected;

    void Update()
    {
        if (_selected != null)
        {
            var renderer = _selected.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material.SetColor("_Color", Color.grey);
                anim.SetBool("InteractionOpened", false);
            }
            _selected = null;
        } 
        else anim.SetBool("InteractionOpened", false);

        var ray = main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 4f))
        {
            if (hit.transform.CompareTag("Collectable"))
            {
                var selected = hit.transform.GetComponent<Renderer>();
                if (selected != null)
                {
                    selected.material.SetColor("_Color", Color.white);
                    anim.SetBool("InteractionOpened", true);
                    _selected = hit.transform;
                }
                if (Input.GetButtonDown("Submit"))
                {
                    hit.transform.GetComponent<ItemPick>().Pick();
                    anim.SetBool("InteractionOpened", false);
                    _selected = null;
                }
            }
        }
    }
}
