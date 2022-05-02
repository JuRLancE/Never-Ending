using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{

    public GameObject Spawn1;
    public int percent1;
    public GameObject Spawn2;
    public int percent2;
    public GameObject Spawn3;
    public int percent3;
    public GameObject Spawn4;
    public int Type;
    public float Altura;
    private float difficult;

    private float MaxTime;
    private int numObject = 1;
    private float InitialTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        numObject = Random.Range(1, 101);
        if (InitialTime > MaxTime)
        {
            if (numObject < percent1)
            {
                GameObject NewEnemy = Instantiate(Spawn1);
                NewEnemy.transform.position = transform.position + new Vector3(0, Random.Range(-Altura, Altura), 0);
            }
            else if (numObject >= percent1 && numObject <= percent2)
            {
                GameObject NewEnemy = Instantiate(Spawn2);
                NewEnemy.transform.position = transform.position + new Vector3(0, Random.Range(-Altura, Altura), 0);
            }
            else if (numObject > percent2 && numObject < percent3)
            {
                GameObject NewEnemy = Instantiate(Spawn3);
                NewEnemy.transform.position = transform.position + new Vector3(0, Random.Range(-Altura, Altura), 0);
            }
            else if (numObject >= percent3)
            {
                GameObject NewEnemy = Instantiate(Spawn4);
                NewEnemy.transform.position = transform.position + new Vector3(0, Random.Range(-Altura, Altura), 0);
            }
            switch (Type)
            {
                case 0://tiempo random de respawn de objetos (para enemigos)
                    MaxTime = 250 - difficult;
                    difficult++;
                    break;

                case 1://tiempo fijo de respawn de objetos (para items)
                    MaxTime = 500;
                    break;
            }
            InitialTime = 0;
        }
        else
        {
            InitialTime ++;
        }
    }
}