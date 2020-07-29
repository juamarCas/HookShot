using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform topPos;
    public Transform target;

   

    [Range(0.0f, 10.0f)]
    public float smoothSpeed = 0.125f;

    private void FixedUpdate()
    {
        Vector3 tarPos = new Vector3 (0f, target.position.y, -10.0f);
        Vector3 newPos = new Vector3(0f, transform.position.y, -10f); 
        //Vector3 smoothedPos = Vector3.Lerp(newPos, tarPos, smoothSpeed*Time.deltaTime);
        
        if(target.transform.position.y >= topPos.transform.position.y)
        {
            transform.position = Vector3.Slerp(newPos, tarPos, smoothSpeed*Time.deltaTime);
        }
        else
        {
            transform.position = new Vector3(0f, transform.position.y, -10f); 
        }
    }

}
