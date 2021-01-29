using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildQueue : MonoBehaviour
{
    //Variables holding the active child and item
    private Child activeChild;
    private PickUp activeItem;

    //Lists of possible children to show up to Lost and Found and list of possible items to be in the Lost and Found
    public List<Child> possibleChildren;
    public List<PickUp> possibleItems;

    //List of items that are on display and available
    public List<PickUp> availableItems;

    private float prevChildX;
    private float prevChildY;

    // Start is called before the first frame update
    void Start()
    {
        availableItems = new List<PickUp>();
        GetAvailableItems();
        prevChildX = 0.0f;
        prevChildY = 0.0f;
        CreateNewChild(prevChildX, prevChildY);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateChild();
    }

    //Uses the active child and active item to use Child's check item method
    public bool CheckChildItem()
    {
        return activeChild.CheckItem(activeItem);
    }

    //Add the item that is clicked to be the active one, called in PickUp.cs
    public void AddActiveItem(PickUp item)
    {
        activeItem = item;
    }

    //Checks if the child recieved the right item and removes child from queue
    //TO-DO: 
    // -Update the score based on if it was right or not
    public void UpdateChild()
    {
        if (activeItem != null)
        {
            //Checking if the item the player clicked is the correct one
            bool test = CheckChildItem();
            Debug.Log(test);
            //Set the active item to null so the child doesn't keep getting updated until a new one is clicked
            activeItem = null;
            //Get the previous position of the active child so that the new child can be placed ***********NOTE: this will probably be a static number so this can be replaced
            prevChildX = activeChild.transform.position.x;
            prevChildY = activeChild.transform.position.y;
            //Move child way out of the way
            activeChild.transform.position = new Vector2(200.0f, 200.0f);
            //Set the active child to null so that it can be set new
            activeChild = null;
            //As long as the available items is above zero, keep creating new children **************NOTE: This will probably be replaced with a limit so that not every kid is used every time
            if(availableItems.Count > 0)
                CreateNewChild(prevChildX, prevChildY);
        }
    }

    //Creates new child
    public void CreateNewChild(float x, float y)
    {
        //Get random number in range of the list
        int rand = Random.Range(0, possibleChildren.Count);
        //Set the active child to the one at the random index in the list
        activeChild = possibleChildren[rand];
        //Remove the child from the possible children list
        possibleChildren.RemoveAt(rand);
        //Set the child's position
        activeChild.transform.position = new Vector2(x, y);
        //Assign an item to a child
        AssignChildItem();
    }

    //Get 15 random items from the possible items list and add them to the available items list
    public void GetAvailableItems()
    {
        //Add 15 items from possible to available
        for(int i = 0; i < 15; i++)
        {
            //Get random number in range of the list
            int rand = Random.Range(0, possibleItems.Count);
            //Add a random item to available
            availableItems.Add(possibleItems[rand]);
            //Remove the item from possible items so there are no duplicates
            possibleItems.RemoveAt(rand);
            //*****TO-DO:******
            //Move each of the items into the correct spot on the shelf
        }        
    }

    //Assign child an item
    public void AssignChildItem()
    {
        //Random percent of being a child with no item
        int noItemRand = Random.Range(0, 100);
        if(0 <= noItemRand && noItemRand <= 10)
        {
            activeChild.AssignItem("None");
            return;
        }
        //Get random number in range of the list
        int rand = Random.Range(0, availableItems.Count);
        //Assign the item to the active child at the random index
        activeChild.AssignItem(availableItems[rand]);
    }
    
    //Method to remove item from available items, called in PickUp.cs when item is clicked
    public void RemoveItem(PickUp item)
    {
        availableItems.Remove(item);
    }
}
