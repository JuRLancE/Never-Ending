using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed;
    public float movility;

    public Animator animator;
    public GameObject player;
    public GameObject trail;
    public Rigidbody2D rb;


    private bool dead = false;
    private float InitialTime = 0;

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
       
        if (dead)
        {
            trail.SetActive(false);
            CC2D.enabled = false;
            if (InitialTime > 1)
            {
                Destroy(gameObject);
                InitialTime = 0;
            }
            else
            {
                InitialTime += Time.deltaTime;
            }
        }
        else
        {
            trail.SetActive(true);
            if (player.transform.position.y > transform.position.y)
            {
                if (rb.rotation >= -20f)
                {
                    rb.rotation += -movility;
                }
            }
            else if (player.transform.position.y < transform.position.y)
            {
                if (rb.rotation <= 20f)
                {
                    rb.rotation += movility;
                }
            }
            rb.velocity = -(transform.right * speed);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemie") || (collision.CompareTag("Game")))
        {
            animator.SetBool("dead", true);
            dead = true;
        }
    }
}
