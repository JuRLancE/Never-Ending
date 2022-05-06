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


    private int score = 0;
    private int bestScore;
    public Text scoreString;
    public Text bestScoreString;

    private bool paused;

    private int inxMenu = 0;

    void Start()
    {
        bestScore = PlayerPrefs.GetInt("bestScore");
        player = GameObject.FindGameObjectWithTag("Player");
        AudioPlay = GameObject.FindGameObjectWithTag("AudioPlay");
        AudioMenu = GameObject.FindGameObjectWithTag("AudioMenu");
        Time.timeScale = 1;
    }

    void Update()
    {
        if (!paused)
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                Menu_PauseGame();
            }
            AudioPlay.SetActive(true);
            AudioMenu.SetActive(false);
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
            AudioPlay.SetActive(false);
            AudioMenu.SetActive(true);
            PlayerPrefs.SetInt("bestScore", bestScore);
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
    public void Quit()
    {
        PlayerPrefs.DeleteAll();
    }

}
