using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetExplotion : MonoBehaviour {
    // Start is called before the first frame update
    public float timeToExplode = 3.0f; 
    public Player player;
    public bool playerHanged = false; 
  

    private void Start () {
        player = GameObject.Find("PlayerHolder").GetComponent<Player>();
    }

    public IEnumerator StartExplotion(){   
        yield return new WaitForSeconds(timeToExplode);
        Explode();  
    }

    void Explode () {
        if(playerHanged) player.stopGrap();      
        this.gameObject.SetActive(false); 
    }

    void OnDisable(){ 
        playerHanged = false; 
    }

}