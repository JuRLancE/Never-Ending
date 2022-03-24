using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed;
    public Animator animator;
    public GameObject trail;

    private CapsuleCollider2D CC2D;
    private bool dead = false;
    private float InitialTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        CC2D = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;
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
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        animator.SetBool("dead", true);
        dead = true;
    }
}
