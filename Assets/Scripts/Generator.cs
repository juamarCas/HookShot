using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    public Transform generatorPos;
    public Transform playerPos;
    public GameObject Object; //can be a coin or danger
    public float spwnProb; //probabilidad de spawnear
    
    public float spawningDist;
    int rnd;
    private int region = 0; // divisiones del mapa
    private int preRegion = -9999;
    private int prePos = -99999; 
    public int distanceBtwDanger = 0;
    public bool needRndPos = true; // falso = solo hace spawn en el lugar donde está el generador

    public ObjectPooler objectPool; 

    void Start()
    {
        objectPool = GetComponent<ObjectPooler>(); 
    }

    // Update is called once per frame
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
                else
                {
                    rnd = (int)generatorPos.transform.position.x; 
                    Debug.Log(this.transform.name + " Púas en posición: " + rnd); 
                }               
                GameObject newObj = objectPool.GetPooledObject();
                generatorPos.transform.position = new Vector3(rnd, generatorPos.transform.position.y + distanceBtwDanger, 0f);
                newObj.transform.position = generatorPos.transform.position;
                newObj.transform.rotation = generatorPos.transform.rotation;
                newObj.SetActive(true);
                prePos = rnd;
            }
            else
            {
                generatorPos.transform.position = new Vector3(rnd, generatorPos.transform.position.y + distanceBtwDanger, 0f);
            }
           
        }
    }

    
}
