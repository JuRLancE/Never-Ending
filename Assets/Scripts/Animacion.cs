using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animacion : MonoBehaviour
{
    public GameObject Object;
    public Transform StartAnimation;
    public Transform EndAnimation;
    public int Type;
    public float alternancia;

    private Vector3 Movement;


    // Start is called before the first frame update
    void Start()
    {
        Movement = EndAnimation.position;
    }

    // Update is called once per frame
    void Update()
    {
        Object.transform.position = Vector3.MoveTowards(Object.transform.position, Movement, alternancia * Time.deltaTime);

        switch (Type)
        {
            case 0://Solo de inicio a fin       
                if (Object.transform.position == EndAnimation.position)
                {
                    Object.transform.position = StartAnimation.position;
                }

                if (Object.transform.position == StartAnimation.position)
                {
                    Movement = EndAnimation.position;
                }
                break;

            case 1://De inicio a fin y vuelta
                if (Object.transform.position == EndAnimation.position)
                {
                    Movement = StartAnimation.position;
                }

                if (Object.transform.position == StartAnimation.position)
                {
                    Movement = EndAnimation.position;
                }
                break;
        }
    }
}