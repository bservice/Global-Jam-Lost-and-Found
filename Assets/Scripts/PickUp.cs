using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public string name;
    private ChildQueue childQueue;
    //public InteractingObjects interObjRef;
    //public GameManager gManager;

    public bool frozen = false;
    public bool talking = false;

    Vector2 cursorPosition;

    //private PauseTest pauseMenu;

    private AudioSource soundEffect;

    // Cursor Controls
    public Texture2D specialTexture;
    public Texture2D normalTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;

    // Dialogue for the item once it's in the inventory.
    //public Dialogue invDialogue;
    public string Name
    {
        get { return name; }
    }


    // Start is called before the first frame update
    void Start()
    {        
        //pauseMenu = FindObjectOfType<PauseTest>();
        soundEffect = GetComponent<AudioSource>();
        childQueue = FindObjectOfType<ChildQueue>();


        //gManager = FindObjectOfType<GameManager>();

        /*
        if(pauseMenu == null)
        {
            pauseMenu = new PauseTest();
        }
        */
    }

    // Update is called once per frame
    void Update()
    {
        if(childQueue.ChildCount > 0)
        {
            if (CheckForClick())
            {
                childQueue.AddActiveItem(this);
                //childQueue.RemoveItem(this);
                transform.position = new Vector2(100.0f, 100.0f);
            }
        }        
        
        //if (!pauseMenu.Paused)
        //{
            
        //}
        
    }

    //Method to check for click
    public bool CheckForClick()
    {
        //if (!pauseMenu.Paused)
        //{
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
                        return true;
                    }
                }
            }
            return false;
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
