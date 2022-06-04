using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public GameObject UI_Pause;
    public GameObject UI_NewGame;
    public GameObject UI_Credits;
    public GameObject UI_Controls;
    public GameObject UI_Shop;
    public GameObject EnemyCreator;
    public GameObject ObjectCreator;
    private GameObject player;
    private GameObject AudioPlay;
    private GameObject AudioMenu;


    public GameObject BN_Jetpack;
    public GameObject BN_Belt;
    public GameObject BN_Purse;
    public GameObject B_Jetpack;
    public GameObject B_Belt;
    public GameObject B_Purse;
    public int a_jetpack;
    public int a_belt = 1;
    public int a_purse;

    private int score = 0;
    private int bestScore;
    public Text scoreString;
    public Text bestScoreString;

    private bool paused;
    private bool shopping;

    private int inxMenu = 0;
    private float TempoDec = 0;

    void Start()
    {
        bestScore = PlayerPrefs.GetInt("bestScore");
        player = GameObject.FindGameObjectWithTag("Player");
        AudioPlay = GameObject.FindGameObjectWithTag("AudioPlay");
        AudioMenu = GameObject.FindGameObjectWithTag("AudioMenu");
        a_belt = PlayerPrefs.GetInt("belt");
        a_jetpack = PlayerPrefs.GetInt("jetpack");
        a_purse = PlayerPrefs.GetInt("purse");
        Time.timeScale = 1;
        TempoDec = 0;
    }

    void Update()
    {
        if (!paused)
        {
            if (TempoDec >= .1f)
            {
                TempoDec = 0;

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
                TempoDec = (TempoDec + Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.Escape))
            {
                Menu_PauseGame();
            }
            AudioPlay.SetActive(true);
            AudioMenu.SetActive(false);
            
        }
        else
        {
            AudioPlay.SetActive(false);
            AudioMenu.SetActive(true);
            PlayerPrefs.SetInt("bestScore", bestScore);

            if (shopping) Hide_Buttons();
        }
    }
    private void Hide_Buttons()
    {
        shopping = false;

        if (a_belt == 1)
        {
            BN_Belt.SetActive(false);
            B_Belt.SetActive(true);
        }
        else
        {
            BN_Belt.SetActive(true);
            B_Belt.SetActive(false);
        }

        if (a_jetpack == 1)
        {
            BN_Jetpack.SetActive(false);
            B_Jetpack.SetActive(true);
        }
        else
        {
            BN_Jetpack.SetActive(true);
            B_Jetpack.SetActive(false);
        }

        if (a_purse == 1)
        {
            BN_Purse.SetActive(false);
            B_Purse.SetActive(true);
        }
        else
        {
            BN_Purse.SetActive(true);
            B_Purse.SetActive(false);
        }
    }
    public void Buy_Jetpack()
    {
        if (player.GetComponent<PlayerMovement>().coins >= 250)
        {
            a_jetpack = 1;
            PlayerPrefs.SetInt("jetpack", a_jetpack);
            Hide_Buttons();
            player.GetComponent<PlayerMovement>().coins -= 250;
        }
    }
    public void Buy_Belt()
    {
        if (player.GetComponent<PlayerMovement>().coins >= 250)
        {
            a_belt = 1;
            PlayerPrefs.SetInt("belt", a_belt);
            Hide_Buttons();
            player.GetComponent<PlayerMovement>().coins -= 250; 
        }
    }

    public void Buy_Purse()
    {
        if (player.GetComponent<PlayerMovement>().coins >= 500)
        {
            a_purse = 1;
            PlayerPrefs.SetInt("purse", a_purse);
            Hide_Buttons();
            player.GetComponent<PlayerMovement>().coins -= 500;
        }
    }
    public void Menu_PauseGame()
    {
        UI_NewGame.SetActive(false);
        UI_Pause.SetActive(true);
        UI_Credits.SetActive(false);
        UI_Controls.SetActive(false);
        UI_Shop.SetActive(false);
        EnemyCreator.SetActive(false);
        ObjectCreator.SetActive(false);
        Time.timeScale = 0;
        paused = true;
        player.GetComponent<PlayerMovement>().PauseGame();
    }
    public void Menu_NewGame() 
    {
        UI_NewGame.SetActive(true);
        UI_Pause.SetActive(false);
        UI_Credits.SetActive(false);
        UI_Controls.SetActive(false);
        UI_Shop.SetActive(false);
        EnemyCreator.SetActive(false);
        ObjectCreator.SetActive(false);
        Time.timeScale = 0;
        paused = true;
        player.GetComponent<PlayerMovement>().PauseGame();
    }
    public void Menu_Controls() 
    {
        UI_NewGame.SetActive(false);
        UI_Pause.SetActive(false);
        UI_Credits.SetActive(false);
        UI_Controls.SetActive(true);
        UI_Shop.SetActive(false);
    }
    public void Menu_Credits() 
    {
        UI_NewGame.SetActive(false);
        UI_Pause.SetActive(false);
        UI_Credits.SetActive(true);
        UI_Controls.SetActive(false);
        UI_Shop.SetActive(false);
    }
    public void Menu_Shop()
    {
        UI_NewGame.SetActive(false);
        UI_Pause.SetActive(false);
        UI_Credits.SetActive(false);
        UI_Controls.SetActive(false);
        UI_Shop.SetActive(true);

        shopping = true;
    }
    public void Action_NewGame() 
    {
        SceneManager.LoadScene(0);
    }
    public void Action_Continue() 
    {
        UI_NewGame.SetActive(false);
        UI_Pause.SetActive(false);
        UI_Credits.SetActive(false);
        UI_Controls.SetActive(false);
        UI_Shop.SetActive(false);
        EnemyCreator.SetActive(true);
        ObjectCreator.SetActive(true);
        Time.timeScale = 1;
        paused = false;
        player.GetComponent<PlayerMovement>().ResumeGame();
    }
    public void Action_PreviousMenu()
    {
        if (inxMenu == 1) Menu_PauseGame();
        else if (inxMenu == 2) Menu_NewGame();
    }
    public void FromPause()
    {
        inxMenu = 1;
    }
    public void FromNewGame()
    {
        inxMenu = 2;
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void DeleteGame()
    {
        PlayerPrefs.DeleteAll();
    }

}
