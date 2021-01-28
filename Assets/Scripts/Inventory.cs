using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Inventory : MonoBehaviour
{
    private Inventory[] inventories;
    public List<PickUp> inventory;
    public List<string> removedInventory;
    private PickUp[] items;

    public PickUp badge;

    private float invX;
    private float invY;

    private int prevCount;
    private int rowCount;

    public int Count
    {
        get { return inventory.Count; }
    }

    // Start is called before the first frame update
    void Start()
    {
        //Following code prevents more than one inventory from being created
        inventories = FindObjectsOfType<Inventory>();
        if (inventories.Length > 1)
        {
            Destroy(inventories[1]);
            Destroy(inventories[1].gameObject);
        }

        invX = -1.323f;
        //invY = -0.788f;
        invY = -5.0f;

        rowCount = 0;

        items = FindObjectsOfType<PickUp>();

        removedInventory = new List<string>();

        //Allows the inventory to be accessed in other scenes
        DontDestroyOnLoad(this);

        if(badge != null)
        DontDestroyOnLoad(badge);
    }

    // Update is called once per frame
    void Update()
    {
        //Checking to see if the scene is the main menu, if so, clear reset inventory
        if(SceneManager.GetActiveScene().name == "MainMenu" || SceneManager.GetActiveScene().name == "Credits")
        {
            ResetInventory();
        }

        items = FindObjectsOfType<PickUp>();
        CheckForDuplicates();
        if (inventory.Count > 0)
        {            
            if (prevCount != inventory.Count)
            {
                if(inventory.Count > 8)
                {
                    //If the inventory goes to the next row, run two loops for each row
                    for (int i = 0; i < 8; i++)
                    {
                        DisplayItem(inventory[i], invX, invY, i);
                    }
                    for (int i = 8; i < inventory.Count; i++)
                    {
                        //Set new x and y for the lower row
                        float x = -1.689f;
                        //float y = -1.146f;
                        float y = -1.346f;
                        //Rowcount serves as "i"
                        //Only used because i does not start at 0
                        rowCount++;
                        DisplayItem(inventory[i], x, y, rowCount);
                    }
                    //Reset row count
                    rowCount = 0;
                }
                else
                {
                    //if there is only one row, display as normal
                    invX = -1.323f;
                    //invY = -0.788f;
                    invY = -0.988f;
                    for (int i = 0; i < inventory.Count; i++)
                    {
                        DisplayItem(inventory[i], invX, invY, i);
                    }
                }
                SaveList();
            }
        }
        prevCount = inventory.Count;
    }

    //Add item to list
    public void AddItem(PickUp item)
    {
        inventory.Add(item);
    }

    //Remove item from list
    public void  RemoveItem(PickUp item)
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            if(inventory[i].Name == item.Name)
            {
                inventory[i].Added = false;
                removedInventory.Add(inventory[i].Name);
                inventory.RemoveAt(i);
            }
        }       
    }    

    public void DisplayItem(PickUp item, float x, float y, int index)
    {
        //Display at the x value according to the index of where it is in the list
        x += 0.366f * (float)index;

        //Display the item
        item.transform.position = new Vector2(x, y);
    }

    //Save items in the inventory so they traverse the scenes
    public void SaveList()
    {
        if (inventory.Count > 0)
        {
            for (int i = 0; i < inventory.Count; i++)
            {
                DontDestroyOnLoad(inventory[i]);
            }
        }
    }

    //Check if there are duplicate items
    public void CheckForDuplicates()
    {
        //Loop through all items on the scene
        for (int i = 0; i < items.Length; i++)
        {
            //Continue if item is not added
            if (!items[i].Added)
            {
                //Check all of the items in the inventory
                for (int j = 0; j < inventory.Count; j++)
                {
                    //If the item is not added and is in the inventory, it is a duplicate
                    if (inventory[j].Name == items[i].Name)
                    {
                        //Destroy duplicate
                        Destroy(items[i].gameObject);
                    }
                }
                //If the item is not added and the removed inventory list has at least one thing inside of it, continue check
                if(removedInventory.Count > 0)
                {
                    //Check all of the items in the removed items inventory
                    for (int j = 0; j < removedInventory.Count; j++)
                    {
                        //If the item is not added and is in the removed inventory, it is a duplicate
                        if (removedInventory[j] == items[i].Name)
                        {
                            //Destroy duplicate
                            Destroy(items[i].gameObject);
                        }
                    }
                }
            }
        }
    }

    //Returns an item in the inventory if the player has it
    public PickUp GetItemByName(string name)
    {
        for(int i = 0; i < inventory.Count; i++)
        {
            if(name == inventory[i].Name)
            {
                return inventory[i];
            }
        }

        return null;
    }

    //Returns true or false depending on if you have an item or not
    public bool HaveItem(string name)
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            if (name == inventory[i].Name)
            {
                return true;
            }
        }

        return false;
    }

    //To combine items
    void CombineItems(PickUp obj1, PickUp obj2, PickUp newObj)
    {

        RemoveItem(obj1);
        RemoveItem(obj2);
        AddItem(newObj);
    }

    //Reset method
    public void ResetInventory()
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            inventory[i].Added = false;
            Destroy(inventory[i]);
            Destroy(inventory[i].gameObject);
        }

        removedInventory.Clear();
        inventory.Clear();
    }

    public void AddBadge()
    {
        badge.Added = true;
        AddItem(badge);
        if (inventory.Count > 8)
        {
            //If the inventory goes to the next row, run two loops for each row
            for (int i = 0; i < 8; i++)
            {
                DisplayItem(inventory[i], invX, invY, i);
            }
            for (int i = 8; i < inventory.Count; i++)
            {
                //Set new x and y for the lower row
                float x = -1.689f;
                //float y = -1.146f;
                float y = -1.346f;
                //Rowcount serves as "i"
                //Only used because i does not start at 0
                rowCount++;
                DisplayItem(inventory[i], x, y, rowCount);
            }
            //Reset row count
            rowCount = 0;
        }
        else
        {
            //if there is only one row, display as normal
            invX = -1.689f;
            //invY = -0.788f;
            invY = -0.988f;
            for (int i = 0; i < inventory.Count; i++)
            {
                DisplayItem(inventory[i], invX, invY, i);
            }
        }
        SaveList();
    }
}

