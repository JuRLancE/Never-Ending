using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMovement : MonoBehaviour
{
    public float speed;

    public Animator animator;
    public Rigidbody2D rb;


    private bool collected = false;
    private float InitialTime = 0;

    private BoxCollider2D CC2D;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        CC2D = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        if (collected)
        {
            CC2D.enabled = false;
            if (InitialTime > 0.5f)
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
            rb.velocity = -(transform.right * speed);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            animator.SetBool("collected", true);
            collected = true;
        }
    }
}
