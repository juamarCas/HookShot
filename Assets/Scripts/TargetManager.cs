using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManager : MonoBehaviour
{
    public Vector2 chanceToMove;
    bool isMovable = false;



    [Header("Movement")]
    public int numTargets;
    public Vector2[] targets;
    public float d_x;
    public float d_y;
    public float moveSpeed;

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
        float rnd = Random.Range(chanceToMove[0], chanceToMove[1]);
        float chance = Random.Range(0,100);
        if(rnd < chance)
        {
            isMovable = true;
            for (int i = 0; i< numTargets; i++)
            {
                Vector2 targetPos = new Vector2(transform.position.x + Random.Range(-d_x, d_x), transform.position.y + Random.Range(-d_y, d_y));
                targets[i] = targetPos;
            }
        }
    }

    void move()
    {
         
        if(Vector2.Distance(transform.position, targets[0])  >= 0.1f)
        { 
            transform.position = Vector2.Lerp(transform.position,targets[0], moveSpeed * Time.deltaTime);
        }
        else if (Vector2.Distance(transform.position, targets[1]) >= 0.1f)
        { 
            transform.position = Vector2.Lerp(transform.position, targets[1], moveSpeed * Time.deltaTime);
        }
    }
}
