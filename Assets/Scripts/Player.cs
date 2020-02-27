using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{


    public GameObject weapon;
    public float speed = 7.5f;
    public float offset = 0.5f;
    public LayerMask mask;
    public LineRenderer line;
    public float distanceJoint = 0;
    public Transform hitTransform;


    private Vector3 HookShotPosition;  
    DistanceJoint2D joint; 

    void Start()
    {       
        joint = GetComponent<DistanceJoint2D>();
        joint.enabled = false;
        line.enabled = false; 
    }

    // Update is called once per frame
    void Update()
    {

        if(joint.distance > distanceJoint)
        {
            joint.distance -= 0.2f;
        }            
         HookShot();
        
    }

    private void HookShot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 shootDirection;
            shootDirection = Input.mousePosition;
            shootDirection = Camera.main.ScreenToWorldPoint(shootDirection);
            shootDirection = shootDirection - this.transform.position;
            shootDirection.z = 0.0f;
            RaycastHit2D hit = Physics2D.Raycast(weapon.transform.position, shootDirection, Mathf.Infinity);
            line.enabled = true;
           
            if (hit.collider != null && hit.collider.gameObject.GetComponent<Rigidbody2D>() != null)
            {
                line.SetPosition(1, hit.point); 
                line.SetPosition(0, weapon.transform.position);
                
                HookShotPosition = hit.point; // punto donde colisionó el collider
                joint.enabled = true;
                joint.connectedBody = hit.collider.gameObject.GetComponent <Rigidbody2D>();
                joint.connectedAnchor = hit.point - new Vector2(hit.collider.transform.position.x, hit.collider.transform.position.y);
                joint.distance = Vector2.Distance(this.transform.position, HookShotPosition);
            }

           

        }
        if (Input.GetMouseButton(0))
        {
           line.SetPosition(0, weapon.transform.position);

        }

        if (Input.GetMouseButtonUp(0))
        {
            line.SetPosition(1, weapon.transform.position);
            joint.enabled = false;
            line.enabled = false; 
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Danger")
        {
            Debug.Log("Dead"); 
        }

        if(other.tag == "Coin")
        {
            Debug.Log("Coin Collected");
            Destroy(other.gameObject); 
        }
    }
}
