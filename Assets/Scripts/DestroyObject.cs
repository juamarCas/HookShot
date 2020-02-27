using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    public Transform playerPos;
    public Transform thisPosition;
    public float maxSepDistance; 
    void Start()
    {
        playerPos = FindObjectOfType<Player>().GetComponent<Transform>(); 
    }

    // Update is called once per frame
    void Update()
    {
        float yPos = playerPos.transform.position.y - thisPosition.transform.position.y;
        if(yPos > maxSepDistance)
        {
            Destroy(this.gameObject); 
        }
    }
}
