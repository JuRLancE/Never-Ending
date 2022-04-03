using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public SceneController sceneController;
    public GameObject Engine_fire;

    private int score;
    private int bestScore;
    public Text scoreString;
    public Text bestScoreString;

    private Rigidbody2D rb;
    private bool endGame;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bestScore = PlayerPrefs.GetInt("bestScore");
        endGame = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Score control
        if (endGame == false)
        {
            if (score > bestScore)
            {
                bestScore = score;
            }

            scoreString.text = "Score: " + score;
            bestScoreString.text = "Best score: " + bestScore;
            score += 1;
        }
        else
        {
            PlayerPrefs.SetInt("bestScore", bestScore);
        }

        //Movemente control and visibility of booster jetpack's
        if (Input.GetKey("space"))
        {
            if (rb.gravityScale > -1)
            {
                rb.gravityScale -= 1;
            }
            Engine_fire.SetActive(true);
        }
        else
        {
            if(rb.gravityScale < 1)
            {
                rb.gravityScale += 1;
            }
            Engine_fire.SetActive(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        sceneController.LoseGame();
        endGame = true; 
    }
}
