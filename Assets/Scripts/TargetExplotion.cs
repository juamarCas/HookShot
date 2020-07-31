using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetExplotion : MonoBehaviour {
    // Start is called before the first frame update
    public float explotionStartingTime = 4.0f;
    private float explotionCounter;
    private bool explode = false;
    private bool hasExplode = false;
    

    private void Start () {

        explotionCounter = explotionStartingTime;
    }

    private void Update () {
        if (!hasExplode) {
            if (explotionCounter <= 0) {
                //explode
                Debug.Log ("Explodee");
                Explode(); 
                hasExplode = true;
            } else if (explode) {
                explotionCounter -= Time.deltaTime;
            }
        }

    }

    public void StartExplotion () {
        explode = true;
    }

    void Explode () {
        Player p = GameObject.Find("PlayerHolder").GetComponent<Player>(); 
        p.DeletePreviousJoint(); 
        this.gameObject.SetActive(false); 
    }

}