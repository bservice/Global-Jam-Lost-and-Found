using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Child : MonoBehaviour
{
    public string correctItem;
    public int textBoxNumber; //Number used to figure out what textbox to display
    private List<GameObject> textboxes; //List of all the textboxes 

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
            case "Ball": textBoxNumber = 0; break;
            case "Blocks": textBoxNumber = 1; break;
            case "Signed Ball": textBoxNumber = 1; break;
            case "Cat": textBoxNumber = 1; break;
            case "Cactus": textBoxNumber = 1; break;
            case "Crayons": textBoxNumber = 1; break;
            case "Game Boy": textBoxNumber = 1; break;
            case "Glasses": textBoxNumber = 1; break;
            case "Glove": textBoxNumber = 1; break;
            case "Hat": textBoxNumber = 1; break;
            case "Juice Box": textBoxNumber = 1; break;
            case "": textBoxNumber = 1; break;
            case "Game Boy": textBoxNumber = 1; break;
            case "Game Boy": textBoxNumber = 1; break;
            case "Game Boy": textBoxNumber = 1; break;
            case "Game Boy": textBoxNumber = 1; break;
            case "Game Boy": textBoxNumber = 1; break;
            case "Game Boy": textBoxNumber = 1; break;

        }
    }

    //Overloaded method for children with no real item
    public void AssignItem(string item)
    {
        correctItem = item;
    }

    //Displays textbox relating to correct item's hint
    public void DisplayTextbox()
    {
        textboxes[textBoxNumber].SetActive(true);
    }
    //Hides textbox relating to correct item's hint
    public void HideTextbox()
    {
        textboxes[textBoxNumber].SetActive(false);
    }

}
