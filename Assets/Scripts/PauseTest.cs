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
        //exitButton.gameObject.GetComponent<SpriteRenderer>().color = new Color(210f / 255f, 198f / 255f, 140f / 255f);
    }

    // Update is called once per frame
    void Update()
    {
        if (paused)
        {
            pauseButton.transform.position = new Vector3(100.0f, 100.0f, 0.0f);
            if (playButton.GetComponent<PausePlay>().Clicked)
            {
                //playButton.gameObject.GetComponent<SpriteRenderer>().color = new Color(210f / 255f, 198f / 255f, 140f / 255f);
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
            pauseButton.transform.position = new Vector3(-2.68f, -1.2f, 0.0f);
            //playButton.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
            if (pauseButton.GetComponent<PausePlay>().Clicked)
            {
                //playButton.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
                paused = true;
                pauseMenu.transform.position = new Vector3(0.09000015f, -0.06900012f, 1);
                playButton.transform.position = new Vector3(1.255f, 0.75f, 1);
                exitButton.transform.position = new Vector3(1.305f, -0.2470001f, 1);
                pauseButton.GetComponent<PausePlay>().Clicked = false;
                //playButton.GetComponent<BoxCollider2D>().transform.position = new Vector3(-0.269f, -0.276f, 1);
                //exitButton.GetComponent<BoxCollider2D>().transform.position = new Vector3(0.342f, -0.276f, 1);
            }
        }
    }
}
