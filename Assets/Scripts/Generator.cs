using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    public Transform generatorPos;
    public Transform playerPos;
  
    public float spwnProb; //probabilidad de spawnear
    
    public float spawningDist;
    int rnd;
    private int prePos = -99999; 
    public int distanceBtwDanger = 0;
    public bool needRndPos = true; // falso = solo hace spawn en el lugar donde está el generador
    private float originalPos_x;
    public int minTS; //min target selector
    public int maxTS; //max target selector
    private int targetSelector; 


    public ObjectPooler[] objectPool; 


    void Start()
    {

        originalPos_x = transform.position.x;
        
    }


    void Update()
    {
        float yDist = generatorPos.position.y - playerPos.position.y;    
        //Debug.Log("Distance: " + yDist); 
        if (yDist < spawningDist)
        {
            float spwn = Random.Range(0, 100);
            
            if(spwn <=  spwnProb)
            {
                if (needRndPos)
                {
                    do
                    {
                        rnd = Random.Range(-3, 4);
                      
                    } while (rnd == prePos);
                }
                targetSelector = Random.Range(minTS, maxTS + 1); 
                prePos = rnd; 
                GameObject newObj = objectPool[targetSelector].GetPooledObject();
                generatorPos.transform.position = new Vector2(generatorPos.transform.position.x + rnd, generatorPos.transform.position.y + distanceBtwDanger);
                newObj.transform.position = generatorPos.transform.position;
                newObj.transform.rotation = generatorPos.transform.rotation;
                newObj.SetActive(true);
                generatorPos.transform.position = new Vector2(originalPos_x, generatorPos.transform.position.y);
            }
            else
            {
                generatorPos.transform.position = new Vector2(generatorPos.transform.position.x, generatorPos.transform.position.y + distanceBtwDanger);
            }
           
        }
    }

    
}
