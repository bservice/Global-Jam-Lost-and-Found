using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChildQueue : MonoBehaviour
{
    //Variables holding the active child and item
    private Child activeChild;
    private PickUp activeItem;

    //Lists of possible children to show up to Lost and Found and list of possible items to be in the Lost and Found
    public List<Child> possibleChildren;
    public List<PickUp> possibleItems;

    //List of possible sprites for the children to have
    public List<Sprite> possibleSprites;

    //List of items that are on display and available and list for their coordinates
    public List<PickUp> availableItems;
    public List<Vector2> itemCoords;

    //AudioSource for correct and incorrect sounds
    private AudioSource soundEffect;

    //Audio clips
    public AudioClip correct;
    public AudioClip incorrect;

    //Style for score text
    private GUIStyle style;

    //Score
    private int score;

    //Keep track of used items
    private int usedItems;

    //Object to hold score when game is over
    private ScoreHolder scoreHolder;

    //Pause menu object
    public PauseTest pauseMenu;

    //List of all the textboxes 
    public List<GameObject> textboxes;

    //List of "No" items
    private List<string> noItems;



    //Property to get the number of possible children
    public int ChildCount
    {
        get { return possibleChildren.Count; }
    }

    // Start is called before the first frame update
    void Start()
    {
        style = new GUIStyle();
        style.fontSize = 25;
        style.normal.textColor = Color.black;
        scoreHolder = FindObjectOfType<ScoreHolder>();
        soundEffect = GetComponent<AudioSource>();
        availableItems = new List<PickUp>();
        GetAvailableItems();
        noItems = new List<string>();
        FillNoList();
        AssignItemsToChildren();
        CreateNewChild();
        score = 0;
        usedItems = 0;        
    }

    // Update is called once per frame
    void Update()
    {
        if (!pauseMenu.Paused)
        {
            //Only allow update if there are still available children and items 
            if (possibleChildren.Count >= 0 && usedItems < 15 && activeChild != null)
            {
                UpdateChild();
            }
            //If there are no more items or children, end game
            else
            {
                GameOver();
            }
        }
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
            //Update how many items were used
            usedItems++;
            //Checking if the item the player clicked is the correct one
            if(CheckChildItem())
            {
                //100 points for correct guess
                score += 100;
                soundEffect.PlayOneShot(correct);
            }
            else
            {
                //Lose 25 on wrong guess
                score -= 25;
                soundEffect.PlayOneShot(incorrect);
            }
            //Set the active item to null so the child doesn't keep getting updated until a new one is clicked
            activeItem = null;
            //Get the previous position of the active child so that the new child can be placed ***********NOTE: this will probably be a static number so this can be replaced
            //prevChildX = activeChild.transform.position.x;
            //prevChildY = activeChild.transform.position.y;
            //Move child way out of the way
            activeChild.transform.position = new Vector2(200.0f, 200.0f);
            //Remove the active child's hint
            activeChild.HideTextbox();
            //Set the active child to null so that it can be set new
            activeChild = null;
            //As long as the possible children is above zero, keep creating new children
            if(possibleChildren.Count > 0)
                CreateNewChild();
        }
    }

    //Creates new child
    public void CreateNewChild()
    {
        //Get random number in range of the list
        int rand = Random.Range(0, possibleChildren.Count);
        //Set the active child to the one at the random index in the list
        activeChild = possibleChildren[rand];
        //Remove the child from the possible children list
        possibleChildren.RemoveAt(rand);
        //Set the child's position
        activeChild.transform.position = new Vector2(-1.7f, 0.07f);
        //Give the child a random sprite
        int rand2 = Random.Range(0, possibleSprites.Count);
        activeChild.GetComponent<SpriteRenderer>().sprite = possibleSprites[rand2];
        activeChild.DisplayTextbox();
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
            availableItems[i].transform.position = itemCoords[i];
        }        
    }

    //Assign all children an item or no item
    public void AssignItemsToChildren()
    {
        //Loop through possible children until everyone item has been assigned
        for (int i = 0; i < possibleChildren.Count; i++)
        {
            //As long as there are available items, assign one to the child or possibly get a "none" assignment 
            if (availableItems.Count > 0)
            {
                //Random percent of being a child with no item
                //int noItemRand = Random.Range(0, 100);
                //if (0 <= noItemRand && noItemRand <= 10)
                //{
                    //possibleChildren[i].AssignItem("None");
                //}
                //If the child doesn't get "none" go give them a random item
                //else
                //{
                    //Get random number in range of the list
                    int rand = Random.Range(0, availableItems.Count);
                    //Assign the item to the current child at the random index
                    possibleChildren[i].AssignItem(availableItems[rand]);
                    //Remove the item from availability
                    availableItems.RemoveAt(rand);

                
                //}
            }
            //If there are still children that need an assignment and there are no items left, the rest get "none"
            else
            {
                //Get random number in range of the list
                int rand = Random.Range(0, noItems.Count);
                //Assign child one of the 4 "No items"
                possibleChildren[i].AssignItem(noItems[rand]);
                //Remove the no item from availibility
                noItems.RemoveAt(rand);
            }
        }
    }
    
    //Method to remove item from available items, called in PickUp.cs when item is clicked
    public void RemoveItem(PickUp item)
    {
        availableItems.Remove(item);
    }

    //Method for if you deny a child an item
    public void Deny()
    {
        if (activeChild != null)
        {
            //Move child way out of the way
            activeChild.transform.position = new Vector2(200.0f, 200.0f);
            //Remove the active child's hint
            activeChild.HideTextbox();
            //Set the active child to null so that it can be set new
            activeChild = null;
            //As long as the available items is above zero, keep creating new children
            if (possibleChildren.Count > 0)
                CreateNewChild();
        }
    }

    //Displaying the score
    private void OnGUI()
    {
        //Score
        GUI.Label(new Rect(130.0f, 300.0f, 22, 19), score.ToString(), style);
    }

    //End game method will be called once there are no more kids or there are no items left
    public void GameOver()
    {
        scoreHolder.GetScore(score);
        SceneManager.LoadScene("GameOver");
    }

    //Fill no items list with the 4 no's
    public void FillNoList()
    {
        noItems.Add("No 1");
        noItems.Add("No 2");
        noItems.Add("No 3");
        noItems.Add("No 4");
    }

}
