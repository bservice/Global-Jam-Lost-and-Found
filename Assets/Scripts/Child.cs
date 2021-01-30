using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Child : MonoBehaviour
{
    public string correctItem;
    public int textBoxNumber; //Number used to figure out what textbox to display
    private ChildQueue manager; //Stores the manager for the game

    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<ChildQueue>();
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
        AssignTextBox();
    }

    //Overloaded method for children with no real item
    public void AssignItem(string item)
    {
        correctItem = item;
        AssignTextBox();
    }

    //Method assigns textbox
    public void AssignTextBox()
    {
        //Sets the child's dialogue hint to match the item they have
        switch (correctItem)
        {
            case "Ball": textBoxNumber = 0; break;
            case "Blocks": textBoxNumber = 1; break;
            case "Cactus": textBoxNumber = 2; break;
            case "Cat": textBoxNumber = 3; break;
            case "Crayons": textBoxNumber = 4; break;
            case "Game Boy": textBoxNumber = 5; break;
            case "Glasses": textBoxNumber = 6; break;
            case "Glove": textBoxNumber = 7; break;
            case "Hat": textBoxNumber = 8; break;
            case "Juice Box": textBoxNumber = 9; break;
            case "Key": textBoxNumber = 10; break;
            case "Lipstick": textBoxNumber = 11; break;
            case "Lunch Box": textBoxNumber = 12; break;
            case "No 1": textBoxNumber = 13; break;
            case "No 2": textBoxNumber = 14; break;
            case "No 3": textBoxNumber = 15; break;
            case "Pencil Box": textBoxNumber = 16; break;
            case "RC Car": textBoxNumber = 17; break;
            case "Watch": textBoxNumber = 18; break;
            case "Water Bottle": textBoxNumber = 19; break;
            case "Sword": textBoxNumber = 20; break;
            case "Yo-Yo": textBoxNumber = 21; break;
            case "Bag": textBoxNumber = 22; break;
            case "Ball Alt": textBoxNumber = 23; break;
            case "No 4": textBoxNumber = 24; break;
        }
    }

    //Displays textbox relating to correct item's hint
    public void DisplayTextbox()
    {
        manager.textboxes[textBoxNumber].SetActive(true);
    }
    //Hides textbox relating to correct item's hint
    public void HideTextbox()
    {
        manager.textboxes[textBoxNumber].SetActive(false);
    }

}
