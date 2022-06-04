using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public float difficult = 0;
    public Text difficultString;

    private float MaxTime;
    private int InitialTime;
    private int numObject = 1;
    private float Tempo = 0;
    private float TempoDec = 0;

    // Start is called before the first frame update
    void Start()
    {
        difficult = 0;
        Tempo = 0;
        TempoDec = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Tempo >= 1)
        {
            Tempo = 0;

            switch (Type)
            {
                case 0://tiempo random de respawn de objetos (para enemigos)
                    if (difficult <= 19)
                    {
                        difficult += .1f;
                    }
                    break;
            }
        }
        else
        {
            Tempo = (Tempo + Time.deltaTime);
        }

        if (TempoDec >= .1f)
        {
            TempoDec = 0;

            switch (Type)
            {
                case 0://tiempo random de respawn de objetos (para enemigos)
                    if (difficult <= 19)
                    {
                        MaxTime = (20 - difficult);
                    }

                    if (difficult < 5)
                    {
                        difficultString.text = "Difficulty: Easy";
                    }
                    else if (difficult >= 5 && difficult < 10)
                    {
                        difficultString.text = "Difficulty: Medium";
                    }
                    else if (difficult >= 10 && difficult < 15)
                    {
                        difficultString.text = "Difficulty: Hard";
                    }
                    else if (difficult >= 15 && difficult <= 19)
                    {
                        difficultString.text = "Difficulty: Extreme";
                    }
                    else
                    {
                        difficultString.text = "Difficulty: Impossible";
                    }
                    break;

                case 1://tiempo fijo de respawn de objetos (para items)
                    MaxTime = 10;
                    break;
            }

            if (InitialTime > MaxTime)
            {
                numObject = Random.Range(0, 101);

                if (percent1 > numObject)
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
                else if (percent3 <= numObject)
                {
                    GameObject NewEnemy = Instantiate(Spawn4);
                    NewEnemy.transform.position = transform.position + new Vector3(0, Random.Range(-Altura, Altura), 0);
                }
                InitialTime = 0;
            }
            else
            {
                InitialTime += 1;
            }
        }
        else
        {
            TempoDec = (TempoDec + Time.deltaTime);
        }
    }
}