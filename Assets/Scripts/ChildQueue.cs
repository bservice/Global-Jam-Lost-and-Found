using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildQueue : MonoBehaviour
{
    public Child activeChild;
    private PickUp activeItem;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(activeItem != null)
        {
            Debug.Log(CheckChildItem());
            activeItem = null;
        }
    }

    public bool CheckChildItem()
    {
        return activeChild.CheckItem(activeItem);
    }

    public void AddActiveItem(PickUp item)
    {
        activeItem = item;
    }
}
