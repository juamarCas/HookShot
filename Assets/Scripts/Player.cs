using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [Header ("Joint properties")]
    [Range (0, 1)]
    public float jointDampingRatio = 0.5f;
    public LayerMask whatCanWeGrapple;
    public float distanceBetweenObjects = 10.0f;
    public float grappleVel = 8.0f;
    public int maxDistanceGrapple = 10;
    public float impulseTrh = 1f;
    public float impulseForce = 2.0f;

    [Header ("Weapon")]
    public GameObject weapon;
    public LineRenderer line;
  

    private Vector3 grapplePoint;
    private Vector3 startPosition;
    private Vector3 currentGrapplePosition;
    private Rigidbody2D rb;

    private SpringJoint2D joint;
    private TargetExplotion explotion; 
    

  
    private bool isHanged = false;

    private void Awake () {
        //  joint = GetComponent<SpringJoint2D> ();
        rb = GetComponent<Rigidbody2D> ();
        line = GetComponent<LineRenderer> ();
        line.enabled = false;
      
    }
    // Update is called once per frame
    void Update () {

        HookShot ();

    }
    void LateUpdate () {
        DrawRope ();
    }

    private void HookShot () {
        #region WinCompiler
#if UNITY_EDITOR_WIN
        if (Input.GetMouseButtonDown (0)) {
            startGrap (Input.mousePosition);
        } else if (Input.GetMouseButtonUp (0)) {
            if(explotion != null){
                explotion.playerHanged = false; 
                explotion = null; 
            }
            stopGrap ();
        }

#endif
        #endregion

        #region Android
#if UNITY_ANDROID
        if (Input.touchCount > 0) {
            Touch touch = Input.GetTouch (0);

            switch (touch.phase) {
                case TouchPhase.Began:
                    Vector3 pos; //touc.position returns a Vector2, so i addapted it as Vector3                
                    pos = new Vector3 (touch.position.x, touch.position.y, 0f);
                    startPosition = pos;
                    startGrap (pos);
                    break;

                case TouchPhase.Moved: //impulse
                    Vector3 newPos = new Vector3 (touch.position.x, touch.position.y, 0f);
                    float swipeValue = startPosition.x - newPos.x;
                    float nwimfrce = 0;

                    if (Mathf.Abs (swipeValue) > Mathf.Abs (impulseTrh)) {
                        if (swipeValue > 0.1) nwimfrce = -impulseForce;
                        else if (swipeValue < -0.1) nwimfrce = impulseForce * 1.35f;

                        rb.AddForce (Vector2.right * nwimfrce, ForceMode2D.Impulse);
                    }

                    startPosition = newPos;
                    break;

                case TouchPhase.Ended:
                    // releas target 
                    stopGrap ();
                    break;
            }
        }
#endif
        #endregion

    }

    private void startGrap (Vector3 input) {
        RaycastHit2D hit;
        Vector3 shootDirection;
        shootDirection = Camera.main.ScreenToWorldPoint (input);
        shootDirection = shootDirection - this.gameObject.transform.position;
        shootDirection.z = 0;
        hit = Physics2D.Raycast (weapon.transform.position, shootDirection, maxDistanceGrapple, whatCanWeGrapple);
        try {
            if (hit != null && hit.collider.gameObject.GetComponent<Rigidbody2D> () != null) {
                grapplePoint = hit.point;
                joint = this.gameObject.AddComponent<SpringJoint2D> ();
                joint.autoConfigureConnectedAnchor = false;
                joint.connectedBody = hit.collider.gameObject.GetComponent<Rigidbody2D> ();
                joint.connectedAnchor = hit.point - new Vector2 (hit.collider.transform.position.x, hit.collider.transform.position.y);

                joint.distance = distanceBetweenObjects;
                joint.dampingRatio = jointDampingRatio;
                isHanged = true;
                line.positionCount = 2;
                currentGrapplePosition = weapon.transform.position;
                hit.collider.GetComponent<TargetManager> ().GivePoint ();
                if (hit.collider.tag == "ExplodeTarget") {
                    explotion = hit.collider.gameObject.GetComponent<TargetExplotion> ();
                    explotion.playerHanged = true; 
                    explotion.StartCoroutine (explotion.StartExplotion ());
       
                }
            }

        } catch (NullReferenceException ex) {
            Debug.Log ("Catched exception: " + ex);
            return;
        }

    }

    //this function is used to avoid destroying the new joint if the player has sticked to another target 
    public void stopGrap () {
        line.positionCount = 0;
        line.enabled = false;
        isHanged = false;
        Destroy (joint);
    }

    private void DrawRope () {
        if (!joint) return;
        line.enabled = true;
        currentGrapplePosition = Vector3.Lerp (currentGrapplePosition, grapplePoint, grappleVel * Time.deltaTime);
        line.SetPosition (0, weapon.transform.position);
        line.SetPosition (1, grapplePoint);
    }

    private void OnTriggerEnter2D (Collider2D other) {
        if (other.tag == "Danger") {
            Debug.Log ("Dead");
            GameManager.Instance.GameOver ();
        }

        if (other.tag == "Coin") {
            Debug.Log ("Coin Collected");
            other.gameObject.SetActive (false);
            GameManager.Instance.AddCoin ();
        }
    }

}