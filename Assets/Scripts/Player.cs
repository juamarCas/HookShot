using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour {

    [Header ("Joint properties")]
    [Range (0, 1)]
    public float jointDampingRatio = 0.5f;
    public LayerMask whatCanWeGrapple;
    public float distanceBetweenObjects = 10.0f;
    public float grappleVel = 8.0f;
    public int maxDistanceGrapple = 10;

    [Header ("Weapon")]
    public GameObject weapon;
    public LineRenderer line;

    private Vector2 hitpoint;
    private Vector3 grapplePoint;
    private Vector3 HookShotPosition;
    private Vector3 currentGrapplePosition;

    private SpringJoint2D joint;

    private bool hasTouched = false; // detectar si está tocando la pantalla

    private void Awake () {
        //  joint = GetComponent<SpringJoint2D> ();
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
            stopGrap ();
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
                //joint.anchor = weapon.transform.position; 
                joint.connectedBody = hit.collider.gameObject.GetComponent <Rigidbody2D>();
                joint.connectedAnchor = hit.point - new Vector2(hit.collider.transform.position.x, hit.collider.transform.position.y);

                joint.distance = distanceBetweenObjects;
                joint.dampingRatio = jointDampingRatio;

                line.positionCount = 2;
                currentGrapplePosition = weapon.transform.position;
                 hit.collider.GetComponent<TargetManager>().GivePoint(); 
            }

        } catch (NullReferenceException ex) {
            Debug.Log("Catched exception"); 
            return; 
        }

    }

    private void stopGrap () {
        line.positionCount = 0;
        line.enabled = false;
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
        Debug.Log ("Triggered");
        if (other.tag == "Danger") {
            Debug.Log ("Dead");
            hasTouched = false;
            GameManager.Instance.GameOver ();
        }

        if (other.tag == "Coin") {
            Debug.Log ("Coin Collected");
            other.gameObject.SetActive (false);
            GameManager.Instance.AddCoin ();
        }
    }

}