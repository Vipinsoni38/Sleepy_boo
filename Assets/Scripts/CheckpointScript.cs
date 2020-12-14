using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointScript : MonoBehaviour
{
    AudioManager audioManager;
    bool IscheckpointMarked = false;
    void OnTriggerEnter2D(Collider2D collisionInfo){
        if(collisionInfo.gameObject.tag.Equals("Player") && !IscheckpointMarked){            
            audioManager = FindObjectOfType<AudioManager>();
            audioManager.PlaySound("Checkpoint");
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
