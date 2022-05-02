using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public SceneController sceneController;
    public GameObject Engine_fire;

    public int score=0;
    public int bestScore;
    public int coins=1;
    public int ammo=0;
    public float fuel=2000;
    public Text scoreString;
    public Text bestScoreString;
    public Text fuelString;
    public Text coinString;
    public Text ammoString;

    private Rigidbody2D rb;
    private bool endGame;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coins = PlayerPrefs.GetInt("coins");
        bestScore = PlayerPrefs.GetInt("bestScore");
        endGame = false;
        coinString.text = "" + coins;
        ammoString.text = "" + ammo;
    }

    // Update is called once per frame
    void Update()
    {
        //Score control
        if (endGame == false)
        {
            //Movemente control and visibility of booster jetpack's
            if (Input.GetKey("space") && fuel > 0)
            {
                if (rb.gravityScale > -1)
                {
                    rb.gravityScale -= 1;
                }
                Engine_fire.SetActive(true);
                fuelString.text = "Fuel: " + fuel;
                fuel -= 1;
            }
            else
            {
                if (rb.gravityScale < 1)
                {
                    rb.gravityScale += 1;
                }
                Engine_fire.SetActive(false);
            }
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
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Coin"))
        {
            coins = coins + 5;
            coinString.text = "" + coins;
            PlayerPrefs.SetInt("coins", coins);
        }else if (collision.CompareTag("Shield"))
        {

        }else if (collision.CompareTag("ClearWave"))
        {

        }else if (collision.CompareTag("Fuel"))
        {
            fuel += 1500;
            if (fuel >= 5000) fuel = 5000;
        }
        else if (collision.CompareTag("Game") || (collision.CompareTag("Enemie")))
        {
            sceneController.LoseGame();
            endGame = true;
        }
    }
}
