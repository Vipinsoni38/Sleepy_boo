using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHolder : MonoBehaviour
{
    Transform Player;
    Vector2 position;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        position.x = Player.position.x;
        this.transform.position = position;
    }  
}
