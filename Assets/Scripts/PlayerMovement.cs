using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class PlayerMovement : MonoBehaviour
{
    public SceneController sceneController;
    private GameObject Engine_fire;
    private GameObject Shield;
    private GameObject ClearWave;
    private GameObject SpawnShoot;
    public GameObject Bullet;

    public AudioSource Sound;

    private int coins=1;
    private int ammo=0;
    private int valueAmmo = 1;
    public float fireRate = 0.5f;
    public float fireTime = 0.5f;
    private float fuel=2000;
    public Text fuelString;
    public Text coinString;
    public Text ammoString;

    private Rigidbody2D rb;
    private bool paused;
    public bool shield;
    private float shieldTime = 0f;
    public bool clearWave;
    private float clearWaveTime = 0f;
    private float scaleX = 1f;
    private float scaleY = 1f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coins = PlayerPrefs.GetInt("coins");
        ammo = PlayerPrefs.GetInt("ammo");
        Engine_fire = GameObject.FindGameObjectWithTag("Engine_fire");
        Shield = GameObject.FindGameObjectWithTag("Shield");
        ClearWave = GameObject.FindGameObjectWithTag("ClearWave");
        SpawnShoot = GameObject.FindGameObjectWithTag("SpawnShoot");
        paused = false;
        coinString.text = "" + coins;
        ammoString.text = "" + ammo;
    }

    void Update()
    {
        if (!paused)
        {
            if (Input.GetMouseButtonDown(0) && ammo > 0 && Time.time > fireTime)
            {
                fireTime = Time.time + fireRate;
                Shoot();
            }
            if (Input.GetKey(KeyCode.Space) && fuel >= 0)
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

            if (shield)
            {
                Shield.SetActive(true);
                if (Time.time > shieldTime + 10f)
                {
                    shield = false;
                }
            }
            else
            {
                Shield.SetActive(false);
            }
            if (clearWave)
            {
                ClearWave.SetActive(true);
                ClearWave.transform.localScale = new Vector3(scaleX, scaleY, 1f);
                if (Time.time > clearWaveTime + 3f)
                {
                    clearWave = false;
                    scaleX = 1f;
                    scaleY = 1f;
                }
                else
                {
                    scaleX += 0.15f;
                    scaleY += 0.15f;
                }
            }
            else
            {
                ClearWave.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Coin")){
            coins = coins + 1;
            coinString.text = "" + coins;
            PlayerPrefs.SetInt("coins", coins);
        }else if (collision.CompareTag("Shield_PU")){
            shield = true;
            shieldTime = Time.time;
        }else if (collision.CompareTag("ClearWave_PU")){
            clearWave = true;
            clearWaveTime = Time.time;
        }else if (collision.CompareTag("Fuel")){
            fuel += 1500;
            if (fuel >= 5000) fuel = 5000;
        }else if (collision.CompareTag("Game") || ((collision.CompareTag("Enemie")) && !shield)){
            Engine_fire.SetActive(false);
            sceneController.Menu_NewGame();
        }
    }
    public void PauseGame()
    {
        paused = true;
    }
    public void ResumeGame()
    {
        paused = false;
    }
    public void Shoot()
    {
        if (ammo > 0)
        {
            Instantiate(Bullet, SpawnShoot.transform.position, Quaternion.identity);
            ammo--;
            ammoString.text = "" + ammo;
            PlayerPrefs.SetInt("ammo", ammo);
            Sound.Play();
        }
    }
    public void BuyAmmo()
    {
        if (coins > 0)
        {
            coins = coins - valueAmmo;
            ammo = ammo + 1;
            coinString.text = "" + coins;
            PlayerPrefs.SetInt("coins", coins);
            ammoString.text = "" + ammo;
            PlayerPrefs.SetInt("ammo", ammo);
        }
    }
}
