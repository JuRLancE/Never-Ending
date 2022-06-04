using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class EnemyMovement : MonoBehaviour
{
    public float speed;
    public float movility;

    public Animator animator;
    public GameObject player;
    public GameObject trail;
    public Rigidbody2D rb;

    public AudioSource Sound;

    private bool dead = false;
    private float InitialTime = 0;
    private float deltaTime;

    private CapsuleCollider2D CC2D;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        CC2D = GetComponent<CapsuleCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        deltaTime = Time.deltaTime;

        if (dead)
        {
            trail.SetActive(false);
            CC2D.enabled = false;
            if (Time.time > InitialTime + 1f)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            trail.SetActive(true);
            if (player.transform.position.y > transform.position.y)
            {
                if (rb.rotation >= -15f)
                {
                    rb.rotation += -movility * deltaTime;
                }
            }
            else if (player.transform.position.y < transform.position.y)
            {
                if (rb.rotation <= 15f)
                {
                    rb.rotation += movility * deltaTime;
                }
            }
            rb.velocity = -(transform.right * speed * deltaTime);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemie") || (collision.CompareTag("Game")) || (collision.CompareTag("Bullet")) || (collision.CompareTag("Shield")) || (collision.CompareTag("ClearWave")) || (collision.CompareTag("Player")))
        {
            animator.SetBool("dead", true);
            dead = true;
            InitialTime = Time.time;
            Sound.Play();
        }
    }
}
