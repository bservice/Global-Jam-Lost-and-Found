using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseTest : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject playButton;
    public GameObject exitButton;
    public GameObject pauseButton;

    private bool paused;

    //Property to access the paused bool
    public bool Paused
    {
        get { return paused; }
    }

    // Start is called before the first frame update
    void Start()
    {
        paused = false;
        pauseMenu.transform.position = new Vector3(100.0f, 100.0f, 0.0f);
        playButton.transform.position = new Vector3(100.0f, 100.0f, 0.0f);
        exitButton.transform.position = new Vector3(100.0f, 100.0f, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (paused)
        {
            //Move pause button out of the way when paused, open book
            pauseButton.transform.position = new Vector3(100.0f, 100.0f, 0.0f);
            //Play game when button clicked
            if (playButton.GetComponent<PausePlay>().Clicked)
            {
                //Move items out of the way when not paused
                pauseMenu.transform.position = new Vector3(100.0f, 100.0f, 0.0f);
                playButton.transform.position = new Vector3(100.0f, 100.0f, 0.0f);
                exitButton.transform.position = new Vector3(100.0f, 100.0f, 0.0f);
                paused = false;
                playButton.GetComponent<PausePlay>().Clicked = false;
            }
            //Change scene if exit is pressed
            if(exitButton.GetComponent<PausePlay>().Clicked)
            {
                exitButton.GetComponent<PausePlay>().Clicked = false;
                SceneManager.LoadScene("MainMenu");
            }
        }
        else
        {
            //Move pause button back to desk
            pauseButton.transform.position = new Vector3(-2.68f, -1.2f, 0.0f);
            //Pause when button clicked
            if (pauseButton.GetComponent<PausePlay>().Clicked)
            {
                //When paused, move items back to position
                paused = true;
                pauseMenu.transform.position = new Vector3(0.09000015f, -0.06900012f, 1);
                playButton.transform.position = new Vector3(1.255f, 0.75f, 1);
                exitButton.transform.position = new Vector3(1.305f, -0.2470001f, 1);
                pauseButton.GetComponent<PausePlay>().Clicked = false;
            }
        }
    }
}
