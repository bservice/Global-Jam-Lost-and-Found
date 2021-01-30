using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoButton : MonoBehaviour
{
    public ChildQueue childQueue;

    private AudioSource soundEffect;

    public PauseTest pauseMenu;

    private Sprite spriteUnpressed;

    public Sprite spritePressed; //The sprite to display when the button is pressed

    bool pressed;

    float time; //Keeps the button down for a certain amount of time

    //Uncomment once we have no sound
    //public AudioClip noSound;

    Vector2 cursorPosition;

    // Start is called before the first frame update
    void Start()
    {
        soundEffect = GetComponent<AudioSource>();
        spriteUnpressed = this.GetComponent<SpriteRenderer>().sprite;
    }

    // Update is called once per frame
    void Update()
    {
        if (!pauseMenu.Paused)
        {
            //On click
            if (CheckForClick())
            {
                buttonPress();
                time = 0.0f;
                pressed = true;
                //Uncomment once we have no sound
                //soundEffect.PlayOneShot(noSound);
                childQueue.Deny();
            }
            //Make the button come up .5s after being pressed
            if(pressed)
            {
                time += Time.deltaTime;
                if (time > 0.3f)
                {
                    pressed = false;
                }
            }
            else
            {
                buttonUnpress();
            }
            
        }
    }

    //Checks if the button is clicked
    public bool CheckForClick()
    {
        //if (!pauseMenu.Paused)
        //{
        //Grab vector2 for cursor to use in AABB math
        cursorPosition = Input.mousePosition;
        cursorPosition = Camera.main.ScreenToWorldPoint(cursorPosition);

        //Selection for objects
        if (Input.GetMouseButtonDown(0))
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

    //Change sprites based on state
    public void buttonPress()
    {
        this.GetComponent<SpriteRenderer>().sprite = spritePressed;
        
    }
    public void buttonUnpress()
    {
        this.GetComponent<SpriteRenderer>().sprite = spriteUnpressed;
    }

}
