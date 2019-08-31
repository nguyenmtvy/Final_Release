using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat : MonoBehaviour
{
    private bool isArrived = false;
    private bool isEnd = false;
    [SerializeField] public int arrivedWait = 20;
    [SerializeField] public int endWait = 300;
    [SerializeField] Transform end;
    [SerializeField] Transform spawn;
    [SerializeField] Transform arrived;
    [SerializeField] public float speed;

    private int arrivedTimer = 0;
    private int endTimer = 0;
    private void Start()
    {
        InvokeRepeating("boatArrived", 1, 1);
        InvokeRepeating("boatPeriod", 1, 1);
    }

    public bool getArrived()
    {
        return isArrived;
    }

    void boatArrived()
    {
        if (isArrived)
        {
            if(arrivedTimer < arrivedWait)
            {                
                arrivedTimer++;                
            }
            else
            {                
                arrivedTimer = 0;
                isArrived = false;                                
                Move();
            }
        }
    }

    void boatPeriod()
    {
        if (isEnd)
        {
            if(endTimer < endWait)
            {
                endTimer++;                
            }
            else
            {
                endTimer = 0;
                //spawn
                this.transform.position = spawn.transform.position;                
                isEnd = false;
                Move();
            }
        }
    }

    private void Update()
    {
        Move();

        //pass end -> spawn back
        if (this.transform.position.x > end.position.x)
        {
            isEnd = true;
        }
        else if ((this.transform.position.x > arrived.transform.position.x - 0.01) && (this.transform.position.x <= arrived.transform.position.x + 0.01))
        {
            isArrived = true;
        }
    }

    private void Move()
    {
        if (!isArrived && !isEnd)
        {
            this.transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
        else if (isEnd)
        {
            this.transform.Translate(0, 0, 0);
        }
        else if (isArrived)
        {
            this.transform.Translate(0, 0, 0);
        }
    }
}
