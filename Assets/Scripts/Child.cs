using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Child : MonoBehaviour
{
    public string correctItem;
    public int textBoxNumber; //Number used to figure out what textbox to display

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Method to check if the item is the correct one or not
    public bool CheckItem(PickUp item)
    {
        if(item.Name == correctItem)
        {
            return true;
        }
        return false;
    }

    //Method to assign a child the correct item
    public void AssignItem(PickUp item)
    {
        correctItem = item.name;

        //Sets the child's dialogue hint to match the item they have
        switch (item.name)
        {
            case "Baseball": textBoxNumber = 0; break;
            case "Signed Ball": textBoxNumber = 1; break;
            

        }
    }

    //Overloaded method for children with no real item
    public void AssignItem(string item)
    {
        correctItem = item;
    }
}
