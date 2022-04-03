using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{

    public GameObject Enemy1;
    public GameObject Enemy2;
    public GameObject Enemy3;
    public GameObject Enemy4;
    public float Altura;

    private float MaxTime;
    private int numEnemy = 1;
    private float InitialTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        numEnemy = Random.Range(1, 5);
        if (InitialTime > MaxTime)
        {
            if (numEnemy == 1)
            {
                GameObject NewEnemy = Instantiate(Enemy1);
                NewEnemy.transform.position = transform.position + new Vector3(0, Random.Range(-Altura, Altura), 0);
                MaxTime = Random.Range(100, 500); 
            }
            else if (numEnemy == 2)
            {
                GameObject NewEnemy = Instantiate(Enemy2);
                NewEnemy.transform.position = transform.position + new Vector3(0, Random.Range(-Altura, Altura), 0);
                MaxTime = Random.Range(100, 600);
            }
            else if (numEnemy == 3)
            {
                GameObject NewEnemy = Instantiate(Enemy3);
                NewEnemy.transform.position = transform.position + new Vector3(0, Random.Range(-Altura, Altura), 0);
                MaxTime = Random.Range(100, 700);
            }
            else if (numEnemy == 4)
            {
                GameObject NewEnemy = Instantiate(Enemy4);
                NewEnemy.transform.position = transform.position + new Vector3(0, Random.Range(-Altura, Altura), 0);
                MaxTime = Random.Range(100, 800);
            }
            InitialTime = 0;
        }
        else
        {
            InitialTime ++;
        }
    }
}
