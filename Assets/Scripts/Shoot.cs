using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public float speed;
    private float timeShoot;
    public Rigidbody2D rb;


    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        timeShoot = Time.time;

}

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(speed,0f);
        if(Time.time > timeShoot + 5f)
        {
            Destroy(gameObject);
        }
    }
}
