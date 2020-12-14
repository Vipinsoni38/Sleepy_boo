using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonScript : MonoBehaviour
{
    // Start is called before the first frame update
    Transform Player;
    Rigidbody2D Enemy;
    public GameObject Blast;
    PlayerScript playerScript;
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        Enemy = GetComponent<Rigidbody2D>();
        playerScript = Player.GetComponent<PlayerScript>();
    }

    // Update is called once per frame
    void Update()
    {        
        if((Player.transform.position - transform.position).magnitude < 12){            
            Enemy.velocity = (Player.transform.position - transform.position).normalized * 4;
            if((Player.transform.position - transform.position).magnitude < 5){
                playerScript.GetLocationToThrowCards((Vector2)transform.position);
            }            
        }
    }
     void OnCollisionEnter2D(Collision2D collisionInfo){
        if(collisionInfo.gameObject.tag.Equals("NinjaCard")){
            Destroy(collisionInfo.gameObject);
            GameObject g = Instantiate(Blast);
            g.transform.position = transform.position;
            Destroy(g, 1); 
            Destroy(this.gameObject);            
        }
    }    
}
