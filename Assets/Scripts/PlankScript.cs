using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlankScript : MonoBehaviour
{

    float Amp = 1.5f, freq = 2;
    Vector2 pos;
    Transform child;
    // Start is called before the first frame update
    void Start()
    {
        pos = Vector2.zero;
        child = transform.Find("smallPlank");
    }

    // Update is called once per frame
    void Update()
    {
        pos = new Vector2(Mathf.Sin(Time.time * freq) * Amp,0);
        child.localPosition = pos;   
    }
}
