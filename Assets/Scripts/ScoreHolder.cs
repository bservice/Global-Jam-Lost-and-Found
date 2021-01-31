using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Need to add this for "Text" class.
using UnityEngine.SceneManagement; // Need to add this to check the scene name.

public class ScoreHolder : MonoBehaviour
{
    private int finalScore;
    private Text textScore;

    // Start is called before the first frame update
    void Start()
    {        
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        //Destroy score holder on main menu so it resets for game over
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            Destroy(this);
            Destroy(this.gameObject);
        }

        textScore = FindObjectOfType<Text>();
        if (SceneManager.GetActiveScene().name == "GameOver")
        {
            textScore.text = "" + finalScore;
        }
    }

    public void GetScore(int score)
    {
        finalScore = score;
    }
}
