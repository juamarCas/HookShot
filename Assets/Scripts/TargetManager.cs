using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManager : MonoBehaviour
{
    public float prob; 
    bool isMovable = false;
    private bool gavePoint = false; //este bloque ya dio un punto?



    [Header("Movement")]
    public int numTargets;
    public Vector2[] targets;
    public float d_x;
    public float d_y;
    public float moveSpeed;
    private Vector2 goingTo; 

    void Start()
    { 
        initializeMovement();
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isMovable)
            move();
    }

    public void initializeMovement()
    {
        
        float chance = Random.Range(0,100);
        
        if(chance < prob)
        {
            isMovable = true;
            for (int i = 0; i < numTargets; i++)
            {
                Vector2 targetPos = new Vector2(Random.Range(-d_x, d_x), this.transform.position.y /*+ Random.Range(-d_y, d_y)*/);
                targets[i] = targetPos;
                goingTo = targets[0]; 
            }
        }
    }

    void move()
    {
         
        if(Vector2.Distance(transform.position, targets[0])  >= 0.1f && goingTo == targets[0])
        { 
            transform.position = Vector2.Lerp(transform.position,targets[0], moveSpeed * Time.deltaTime);
        }else if(Vector2.Distance(transform.position, targets[0]) <= 0.1f && goingTo == targets[0])
        {
            goingTo = targets[1]; 
        }

        if (Vector2.Distance(transform.position, targets[1]) >= 0.1f && goingTo == targets[1])
        { 
            transform.position = Vector2.Lerp(transform.position, targets[1], moveSpeed * Time.deltaTime);
        }else if (Vector2.Distance(transform.position, targets[1]) <= 0.1f && goingTo == targets[1])
        {
            goingTo = targets[0];
        }
    }

    public void GivePoint()
    {
        if (!this.gavePoint)
        {
            GameManager.Instance.AddPoint();
            this.gavePoint = true;
        }
    }
}
