using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class PlayerMovement : MonoBehaviour
{
    public SceneController sceneController;
    public GameObject Bullet;
    public AudioSource Sound;
    private Rigidbody2D rb;
    private GameObject Engine_fire;
    private GameObject Shield;
    private GameObject ClearWave;
    private GameObject SpawnShoot;

    //Internal atributes
    private bool paused;
    private float fuel = 2000;
    public int coins = 1000;
    private int ammo = 0;
    private int valueAmmo = 1;
    private float fireRate = 0.5f;
    private float fireTime = 0.5f;
    public int shields;
    private bool shield;
    private float shieldTime = 0f;
    public int clearWaves;
    private bool clearWave;
    private float clearWaveTime = 0f;
    private float TempoDec = 0;


    //Text
    public Text t_fuel;
    public Text t_coin;
    public Text t_ammo;
    public Text t_shield;
    public Text t_clearwave;

    //abilities
    private int a_jetpack;
    private int a_belt;
    private int a_purse;

    //others
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
        t_coin.text = "" + coins;
        t_ammo.text = "" + ammo;
        a_belt = PlayerPrefs.GetInt("belt");
        a_jetpack = PlayerPrefs.GetInt("jetpack");
        a_purse = PlayerPrefs.GetInt("purse");
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

            if (Input.GetKey(KeyCode.Q) && !shield)
            {
                ShieldActive();
            }

            if (Input.GetKey(KeyCode.E) && !clearWave)
            {
                ClearWaveActive();
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

            if (TempoDec > 0.01f)
            {
                TempoDec = 0;

                if (Input.GetKey(KeyCode.Space) && fuel >= 0)
                {
                    if (rb.gravityScale > -.25)
                    {
                        if (a_belt == 1) rb.gravityScale -= 1f;
                        else rb.gravityScale -= .1f;
                    }
                    Engine_fire.SetActive(true);
                    fuel -= 1;
                    fuel = (int)fuel;
                    t_fuel.text = "Fuel: " + fuel;
                }
                else
                {
                    if (rb.gravityScale < .25)
                    {
                        if (a_jetpack == 1) rb.gravityScale += 1f;
                        else rb.gravityScale += .1f;
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
            }
            else
            {
                TempoDec += Time.deltaTime;
            }
        }
    }

    private void ShieldActive()
    {
        if (shields > 0)
        {
            shield = true;
            shieldTime = Time.time;
            shields -= 1;
            t_shield.text = " : " + shields;
        }
    }

    private void ClearWaveActive()
    {
        if (clearWaves > 0)
        {
            clearWave = true;
            clearWaveTime = Time.time;
            clearWaves -= 1;
            t_clearwave.text = " : " + clearWaves;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Coin"))
        {
            if (a_purse == 1) coins = coins + 2;
            else coins = coins + 1;
            t_coin.text = "" + coins;
            PlayerPrefs.SetInt("coins", coins);
        }
        else if (collision.CompareTag("Shield_PU"))
        {
            shields += 1;
            t_shield.text = " : " + shields;
        }
        else if (collision.CompareTag("ClearWave_PU"))
        {
            clearWaves += 1;
            t_clearwave.text = " : " + clearWaves;
        }
        else if (collision.CompareTag("Fuel"))
        {
            fuel += 1500;
            if (fuel >= 5000) fuel = 5000;
        }
        else if (collision.CompareTag("Game") || ((collision.CompareTag("Enemie")) && !shield)){
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
            t_ammo.text = "" + ammo;
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
            t_coin.text = "" + coins;
            PlayerPrefs.SetInt("coins", coins);
            t_ammo.text = "" + ammo;
            PlayerPrefs.SetInt("ammo", ammo);
        }
    }
}
