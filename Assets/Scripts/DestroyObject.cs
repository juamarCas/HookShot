using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    public Transform playerPos;
    public Transform thisPosition;
    public float maxSepDistance;
    MeshRenderer Renderer;
    void Start()
    {
        playerPos = FindObjectOfType<Player>().GetComponent<Transform>();
        Renderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float yPos = playerPos.transform.position.y - thisPosition.transform.position.y;
        if(yPos > maxSepDistance)
        {
            this.gameObject.SetActive(false); 
        }
    }
}
