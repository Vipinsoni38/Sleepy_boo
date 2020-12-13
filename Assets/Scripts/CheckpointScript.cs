using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointScript : MonoBehaviour
{
    bool IscheckpointMarked = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    void OnTriggerEnter2D(Collider2D collisionInfo){
        if(collisionInfo.gameObject.tag.Equals("Player") && !IscheckpointMarked){            
            colorIt();
            PlayerPrefs.SetFloat("CheckPointPos", this.transform.position.x + 1);
        }
    }

    public void colorIt()
    {
        IscheckpointMarked = true;
        transform.Find("Base").GetComponent<SpriteRenderer>().color = Color.yellow;
    }
}
