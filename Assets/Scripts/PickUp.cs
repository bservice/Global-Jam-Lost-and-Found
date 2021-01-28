using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public string name;
    private Inventory inventory;
    public InteractingObjects interObjRef;
    public GameManager gManager;

    public bool frozen = false;
    public bool talking = false;

    Vector2 cursorPosition;

    private bool added;

    private PauseTest pauseMenu;

    private AudioSource soundEffect;

    // Cursor Controls
    public Texture2D specialTexture;
    public Texture2D normalTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;

    // Dialogue for the item once it's in the inventory.
    public Dialogue invDialogue;

    public string Name
    {
        get { return name; }
    }

    public bool Added
    {
        get { return added; }
        set
        {
            added = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {        
        inventory = FindObjectOfType<Inventory>();
        pauseMenu = FindObjectOfType<PauseTest>();
        added = false;
        soundEffect = GetComponent<AudioSource>();

        if (this.tag == "Ball" || this.tag == "Screwdriver") { frozen = true; }

        gManager = FindObjectOfType<GameManager>();

        if(pauseMenu == null)
        {
            pauseMenu = new PauseTest();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!pauseMenu.Paused)
        {
            CheckForClick();
            if (this.tag == "Badge") { UseItem("Badge", "t_badge"); }

            if (this.tag == "Stick") { UseItem(this.tag, "t_Tree"); }

            if (this.tag == "Wrench") { UseItem(this.tag, "t_MechanicFairy"); }

            if (this.tag == "Mitten") { UseItem(this.tag, "t_GreenFairy"); }

            if (this.tag == "Wrench") { UseItem(this.tag, "t_MechanicFairy"); }

            if (this.tag == "Scissors") { UseItem(this.tag, "t_Web"); }

            if (this.tag == "Mitten") { UseItem(this.tag, "t_GreenFairy"); }

            if (this.tag == "Ball" || this.tag == "Screwdriver")
            {
                if(!this.frozen && this.transform.position.y > -0.25)
                {
                    Vector3 newY = this.transform.position;
                    newY.y -= 0.01f;
                    this.transform.position = newY;
                }

                UseItem("Ball", "Target"); 
                UseItem("Screwdriver", "Target"); 
            }
        }

        if (inventory.HaveItem("Cheese"))
        {
            gManager.hasCheese = true;
        }
        if (inventory.HaveItem("Pizzabox"))
        {
            gManager.hasPizzaBox = true;
        }
        if (inventory.HaveItem("Tomato"))
        {
            gManager.hasTomatoe = true;
        }
        if (inventory.HaveItem("Pizzabox"))
        {
            gManager.hasPizzaBox = true;
        }
        if (inventory.HaveItem("Water Sludge"))
        {
            gManager.hasWater = true;
        }
        Debug.Log(gManager.hasWater);
    }

    //Method to check for click
    public bool CheckForClick()
    {
        if (!pauseMenu.Paused)
        {
            //Grab vector2 for cursor to use in AABB math
            cursorPosition = Input.mousePosition;
            cursorPosition = Camera.main.ScreenToWorldPoint(cursorPosition);

            //Selection for objects
            if (Input.GetMouseButtonDown(0) && !this.frozen && !this.talking)
            {
                //AABB collision test for cursor
                if (cursorPosition.x < this.GetComponent<BoxCollider2D>().bounds.max.x && cursorPosition.x > this.GetComponent<BoxCollider2D>().bounds.min.x)
                {
                    //Potential collision!
                    //Check the next condition in a nested if statement, just to not have a ton of &'s and to be more efficient
                    if (cursorPosition.y > this.GetComponent<BoxCollider2D>().bounds.min.y && cursorPosition.y < this.GetComponent<BoxCollider2D>().bounds.max.y)
                    {
                        //Collision!

                        //If the item is clicked on again after it's been added to the inventory, remove it
                        if (added)
                        {
                            //transform.position = new Vector2(100.0f, 100.0f);  Commenting this out so that players don't accidentally delete their items.
                            if (interObjRef != null)
                                interObjRef.Temp = this;
                            //inventory.RemoveItem(this);  Commenting this out so that players don't accidentally delete their items.
                            // Adding fun dialogue instead!
                            FindObjectOfType<DialogueManager>().StartDialogue(invDialogue);
                        }
                        else
                        {
                            //Checking to make sure the inventory isn't full
                            if (inventory.Count < 16)
                            {
                                //Add to inventory
                                inventory.AddItem(this);
                                added = true;
                                soundEffect.PlayOneShot(soundEffect.clip);
                            }
                        }
                        return true;
                    }
                }
            }
            return false;
        }
        return false;
    }

    // Triggers dialogue about the pickup.
    public void OnMouseDown()
    {
        if (!pauseMenu.Paused)
        {
            if (!added)
            {
                //GetComponent<SpriteRenderer>().enabled = false;
                GetComponent<DialogueTrigger>().TriggerDialogue();
            }
        }
    }

    //To pull an item from your inventory to use or of the ground
    public void UseItem(Vector2 target, string itemTag)
    {
        if (!inventory.HaveItem(this.name))
        {
            //Debug.Log("HIT return");
            //return;
        }

        //To store the mouses position
        Vector2 locationOfMouse = Input.mousePosition;
        //Grab vector2 for cursor to use in AABB math
        cursorPosition = Input.mousePosition;
        cursorPosition = Camera.main.ScreenToWorldPoint(cursorPosition);

        //Selection for objects
        if (Input.GetMouseButton(1) && !this.frozen)
        {
            //AABB collision test for cursor
            if (cursorPosition.x < this.GetComponent<BoxCollider2D>().bounds.max.x && cursorPosition.x > this.GetComponent<BoxCollider2D>().bounds.min.x)
            {
                //Potential collision!
                //Check the next condition in a nested if statement, just to not have a ton of &'s and to be more efficient
                if (cursorPosition.y > this.GetComponent<BoxCollider2D>().bounds.min.y && cursorPosition.y < this.GetComponent<BoxCollider2D>().bounds.max.y)
                {
                    //Collision!
                    this.transform.position = cursorPosition;

                    //to see if the item is near the target zone
                    if (Vector2.Distance(this.transform.position, target) <= 0.2f)
                    {
                        //Validating item
                        if (this.tag != itemTag)
                        {
                            Debug.Log("HIT RETURN: " + this.tag + " " + itemTag);
                            return;
                        }
                        //Do something with the target zone and/or the object in use
                        HitZone(itemTag);
                    }
                }
            }
        }
    }

    //To pull an item from your inventory to use or of the ground using tags
    public void UseItem(string itemTag, string targetTag)
    {
        if (!inventory.HaveItem(this.name))
        {
            //Debug.Log("HIT return");
            //return;
            //pos = this.transform.position;
        }

        //To store the mouses position
        Vector2 locationOfMouse = Input.mousePosition;
        //Grab vector2 for cursor to use in AABB math
        cursorPosition = Input.mousePosition;
        cursorPosition = Camera.main.ScreenToWorldPoint(cursorPosition);

        //To get an item with the desired tag
        GameObject taggedItem;
        taggedItem = GameObject.FindWithTag(targetTag);

        //Selection for objects
        if (Input.GetMouseButton(1) && !this.frozen && !this.talking)
        {
            //AABB collision test for cursor
            if (cursorPosition.x < this.GetComponent<BoxCollider2D>().bounds.max.x && cursorPosition.x > this.GetComponent<BoxCollider2D>().bounds.min.x)
            {
                //Potential collision!
                //Check the next condition in a nested if statement, just to not have a ton of &'s and to be more efficient
                if (cursorPosition.y > this.GetComponent<BoxCollider2D>().bounds.min.y && cursorPosition.y < this.GetComponent<BoxCollider2D>().bounds.max.y)
                {
                    //Collision!
                    this.transform.position = cursorPosition;
                    
                    //to see if the item is near the target zone
                    if (Vector2.Distance(this.transform.position, taggedItem.transform.position) <= 0.2f)
                    {
                        //Validating item
                        if (this.tag != itemTag)
                        {
                            Debug.Log("HIT RETURN: " + this.tag + " " + itemTag);
                            return;
                        }
                        //Do something with the target zone and/or the object in use
                        HitZone(itemTag);
                        if (name == "Badge")
                        {
                            inventory.AddBadge();
                        }
                        added = false;
                        inventory.RemoveItem(this);
                    }
                }
            }
        }
    }

    public void HitZone(string item)
    {
        switch (item)
        {
            case "Cheese":
                break;
            case "Baseball":
                break;
            case "RC":
                break;
            case "Apple":
                break;
            case "Stick":
                //Ball interaction
                PickUp[] items = FindObjectsOfType<PickUp>();
                for(int i = 0; i < items.Length; i++)
                {
                    if(items[i].tag == "Ball") { items[i].frozen = false; }
                }
                break;
            case "Wrench":
                //Mechanic Fairy interaction
                //Drops screwdriver
                PickUp[] itemss = FindObjectsOfType<PickUp>();
                for (int i = 0; i < itemss.Length; i++)
                {
                    if (itemss[i].tag == "Screwdriver") { itemss[i].frozen = false; }
                }
                gManager.conditionalBools[15] = true;
                break;
            case "Glasses":
                break;
            case "Flashlight":
                break;
            case "Badge":
                //Employee Dialogue
                gManager.conditionalBools[10] = true;
                break;
            case "HouseKey":
                break;
            case "Scissors":
                //Cuts scissors

                break;
            case "Mitten":
                //Orange Fairy interaction
                gManager.conditionalBools[13] = true;
                Debug.Log("HIT");

                break;
            case "Pizza":
                break;
            case "IDcard":
                break;
            case "Pencil":
                break;
            case "Battery":
                break;
            case "Rock":
                break;
            case "Newspaper":
                break;
            case "Key":
                break;
            case "Cookie":
                break;
            case "Soda":
                break;
            case "Crowbar":
                break;
            case "Papers":
                break;
            case "Screwdriver":
                //FFFFFFFFFFFFFFFFFF

                break;
            case "Quarters":
                break;
            case "Ball":
                break;
            case "PizzaBox":
                break;
            default:
                break;

        }
    }

    // Cursor changers
    void OnMouseEnter()
    {
        Cursor.SetCursor(specialTexture, hotSpot, cursorMode);
    }

    void OnMouseExit()
    {
        Cursor.SetCursor(normalTexture, hotSpot, cursorMode);
    }
}
