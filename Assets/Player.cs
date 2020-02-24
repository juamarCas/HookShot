using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Color color;
    public Transform hitTransform;
    private State state;
    private Vector3 HookShotPosition; 
    private CharacterController chara;
    public GameObject weapon; 
    public float speed = 7.5f;
    public float offset = 0.5f;
    public LayerMask mask;
    public LineRenderer line; 

    DistanceJoint2D joint; 

    private enum State {
        Normal,
    }
    void Start()
    {
        chara = GetComponent<CharacterController>();
        state = State.Normal;
        joint = GetComponent<DistanceJoint2D>();
        joint.enabled = false;
        line.enabled = false; 
    }

    // Update is called once per frame
    void Update()
    {

        if(joint.distance > 1f)
        {
            joint.distance -= 0.2f;
        }
        else
        {
            line.enabled = false;
            joint.enabled = false; 
        }

        switch (state)
        {
            default:
            case State.Normal:
                HookShot();
                break; 
        }

       
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
            Debug.Log(shootDirection);
            RaycastHit2D hit = Physics2D.Raycast(weapon.transform.position, shootDirection, Mathf.Infinity);
            line.enabled = true;
           
            if (hit.collider != null && hit.collider.gameObject.GetComponent<Rigidbody2D>() != null)
            {
               
                line.SetPosition(0, weapon.transform.position);
                line.SetPosition(1, hit.point);
                HookShotPosition = hit.point; // punto donde colisionó el collider
                Debug.Log(HookShotPosition);
                Debug.Log(hit.collider.name);
                joint.enabled = true;
                joint.connectedBody = hit.collider.gameObject.GetComponent <Rigidbody2D>();
                joint.connectedAnchor = hit.point - new Vector2(hit.collider.transform.position.x, hit.collider.transform.position.y);
                joint.distance = Vector2.Distance(this.transform.position, HookShotPosition);
            }
            else
            {
                line.SetPosition(0, weapon.transform.position);
                line.SetPosition(1, Camera.main.ScreenToWorldPoint(Input.mousePosition));
            }

        }

        if (Input.GetMouseButtonUp(0))
        {
            joint.enabled = false;
            line.enabled = false; 
        }
    }
}
