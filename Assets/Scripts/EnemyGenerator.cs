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
    private float difficult = 0;

    private float MaxTime;
    private int numObject = 1;
    private float InitialTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        difficult = 0;
    }

    // Update is called once per frame
    void Update()
    {
        numObject = Random.Range(0, 101);
        if (InitialTime > MaxTime)
        {
            if (percent1 > numObject )
            {
                GameObject NewEnemy = Instantiate(Spawn1);
                NewEnemy.transform.position = transform.position + new Vector3(0, Random.Range(-Altura, Altura), 0);
            }
            else if (percent1 <= numObject && numObject < (percent1 + percent2))
            {
                GameObject NewEnemy = Instantiate(Spawn2);
                NewEnemy.transform.position = transform.position + new Vector3(0, Random.Range(-Altura, Altura), 0);
            }
            else if ((percent1 + percent2) <= numObject && numObject < (percent1 + percent2 + percent3))
            {
                GameObject NewEnemy = Instantiate(Spawn3);
                NewEnemy.transform.position = transform.position + new Vector3(0, Random.Range(-Altura, Altura), 0);
            }
            else if ( percent3 <= numObject)
            {
                GameObject NewEnemy = Instantiate(Spawn4);
                NewEnemy.transform.position = transform.position + new Vector3(0, Random.Range(-Altura, Altura), 0);
            }
            switch (Type)
            {
                case 0://tiempo random de respawn de objetos (para enemigos)
                    if (difficult < 100)
                    {
                        MaxTime = 200 - difficult;
                        difficult++;
                    }
                    break;

                case 1://tiempo fijo de respawn de objetos (para items)
                    MaxTime = 100;
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