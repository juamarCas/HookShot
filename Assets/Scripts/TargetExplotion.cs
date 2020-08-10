using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetExplotion : MonoBehaviour {
    // Start is called before the first frame update
    public float explotionStartingTime = 4.0f;
    private float explotionCounter;
    private bool explode = false;
    private bool hasExplode = false;
    public Player player;

    public bool playerHanged = false; 
  

    private void Start () {

        explotionCounter = explotionStartingTime;
        player = GameObject.Find("PlayerHolder").GetComponent<Player>();
    }

    private void Update () {}

    public IEnumerator StartExplotion(){
      
        yield return new WaitForSeconds(5);
        Explode(); 
        hasExplode = true; 
    }

    void Explode () {
        if(playerHanged) player.stopGrap();      
        this.gameObject.SetActive(false); 
    }

    void OnDisable(){
        explode = false; 
        hasExplode = false; 
    }

}