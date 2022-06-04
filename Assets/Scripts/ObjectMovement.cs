using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class ObjectMovement : MonoBehaviour
{

    public Animator animator;
    public Rigidbody2D rb;
    public AudioSource Sound;

    public float speed;

    private BoxCollider2D CC2D;

    private bool collected = false;
    private float InitialTime = 0;
    private float deltatime;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        CC2D = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        deltatime = Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        deltatime = Time.deltaTime;
        if (collected)
        {
            CC2D.enabled = false;
            if ( Time.time > InitialTime + 0.5f)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            rb.velocity = -(transform.right * speed * deltatime);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            animator.SetBool("collected", true);
            collected = true;
            InitialTime = Time.time;
            Sound.Play();
        }

        if (collision.CompareTag("Player"))
        {
            animator.SetBool("collected", true);
            collected = true;
            InitialTime = Time.time;
        }
    }
}
