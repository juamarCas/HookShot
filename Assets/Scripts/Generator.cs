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
    public int distanceBtwDanger = 0;

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
                do
                {
                    rnd = Random.Range(-3, 4);
                    if (rnd >= -3 && rnd <= -2)
                    {
                        region = 1;
                    }
                    else if (rnd >= -1 && rnd <= 1)
                    {
                        region = 2;
                    }
                    else if (rnd >= 2 && rnd <= 3)
                    {
                        region = 3;
                    }

                } while (region == preRegion);
                Debug.Log(region);
                //Instantiate(Object, generatorPos.transform.position, generatorPos.transform.rotation, null);
                GameObject newObj = objectPool.GetPooledObject(); 
                generatorPos.transform.position = new Vector3(rnd, generatorPos.transform.position.y + distanceBtwDanger, 0f);
                newObj.transform.position = generatorPos.transform.position;
                newObj.transform.rotation = generatorPos.transform.rotation;
                newObj.SetActive(true); 
                preRegion = region;
            }
           
        }
    }
}
