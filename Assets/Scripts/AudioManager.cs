using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static AudioClip ouch, Checkpoint, fire, vic, click;
    static AudioSource audiosource;
    private static AudioManager instance;
    void Start()
    {
        if(instance == null){
            instance = this;
        } else {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this);
        audiosource = GetComponent<AudioSource>();
        ouch = Resources.Load<AudioClip>("ouch");
        Checkpoint = Resources.Load<AudioClip>("Checkpoint");
        fire = Resources.Load<AudioClip>("fire");
        click = Resources.Load<AudioClip>("click");
        vic = Resources.Load<AudioClip>("vic");
    }
    public void PlaySound(string s){
        audiosource = GetComponent<AudioSource>();
        switch (s)
        {
            case "ouch" : audiosource.PlayOneShot(ouch,1); break;
            case "Checkpoint" : audiosource.PlayOneShot(Checkpoint,1); break;
            case "fire" : audiosource.PlayOneShot(fire,1); break;
            case "click" : audiosource.PlayOneShot(click,1); break;
            case "vic" : audiosource.PlayOneShot(vic,1); break;
        }
    }
}
