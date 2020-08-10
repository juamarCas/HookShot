using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingTarget : TargetManager
{
    public Transform posX; 
    public Transform negX; 
    public float speed; 
    private Transform currentTarget; 
    
    void Start()
    {
        currentTarget = posX; 
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, currentTarget.position, speed*Time.deltaTime); 

        if(Vector3.Distance(transform.position, currentTarget.position) < 0.001f){
            if(currentTarget = posX){
                currentTarget = negX; 
            }else{
                currentTarget = posX; 
            }
        }
    }
}
