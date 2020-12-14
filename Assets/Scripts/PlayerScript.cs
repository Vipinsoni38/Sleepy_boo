using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    public GameObject ninjaCardObj;
    float x,y;
    float horVel = 6, verVel = 2, CheckPointPos = 0;
    Rigidbody2D Player;
    float lastTime = 0;
    public SpriteRenderer Power; 
    AudioManager audioManager;
    bool isPaused = true, gameover = false, inDash = false, isInvincible = false, triggerInvincibleOff = false,
         bufferingTimeForCards = false, isWon = false;
    SceneHolder sceneHolder;
    CameraSelfScript cameraSelfScript;
    int coins = 0, ninjaCard = 0;
    Color PowerColor;
    public GameObject CardsValueHolder;
    public Text CardsText;
    Animator CardValueHolderAnimator;
    
    // Start is called before the first frame update
    void Start()
    {
        CheckPointPos = PlayerPrefs.GetFloat("CheckPointPos", 0);        
        Player = GetComponent<Rigidbody2D>();
        PowerColor = Power.color;
        audioManager = FindObjectOfType<AudioManager>();
        CardValueHolderAnimator = CardsValueHolder.GetComponent<Animator>();
        sceneHolder = FindObjectOfType<SceneHolder>();
        cameraSelfScript = FindObjectOfType<CameraSelfScript>();
        coins = PlayerPrefs.GetInt("Coins", 0);
        if(CheckPointPos > 0){
            Player.transform.position = new Vector2(CheckPointPos, Player.transform.position.y);
        }
        CheckpointScript[] cps = GameObject.FindObjectsOfType<CheckpointScript>();
        foreach(CheckpointScript cp in cps){
            if(cp.transform.position.x < CheckPointPos){
                cp.colorIt();
            }
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        if(isPaused || gameover || isWon){
            return;
        }
        if(triggerInvincibleOff){
            PowerColor.a = Mathf.Sin(Time.time * 5);
            Power.color = PowerColor;
        }                
        x = Input.GetAxis("Horizontal") * horVel;
        y = Input.GetAxis("Vertical") * horVel;
        if(x == 0 && y == 0){
            return;
        }  
        if(x == 0){
            x = Player.velocity.x;
        }
        if(y == 0){
            y = Player.velocity.y;
        }
      
        Player.velocity = new Vector2(x,y);
    }
    void Dash(){
        Player.velocity = new Vector2(0,1);
        horVel *= 8;
        inDash = true;
        Invoke("RemoveDashing", 0.2f);
        cameraSelfScript.CameraShake(0.2f, 0.1f);
    }    
    void RemoveDashing(){
        horVel /= 8;
        inDash = false;
    }
    public void IsGamePaused(bool p){
        isPaused = p;
    }
    void GameOver(){
        if(gameover){
            return;
        }
        audioManager.PlaySound("ouch");
        gameover = true;
        sceneHolder.Gameover();
    }
    void OnCollisionEnter2D(Collision2D collisionInfo){
        if(collisionInfo.gameObject.tag.Equals("Enemy")){
            if(triggerInvincibleOff){
                return;
            }
            if(isInvincible){
                audioManager.PlaySound("ouch");
                triggerInvincibleOff = true;                
                //this.gameObject.GetComponent<CapsuleCollider2D>().isTrigger = true;
                Invoke("InvincibleOff", 2);
                return;
            }            
            GameOver();
        }
    }    
    void InvincibleOff(){
        triggerInvincibleOff = false;
        isInvincible = false;
        this.gameObject.GetComponent<CapsuleCollider2D>().isTrigger = false;    
        this.transform.Find("Power").gameObject.SetActive(false);    
    }
    void OnTriggerEnter2D(Collider2D collisionInfo){
        if(collisionInfo.gameObject.tag.Equals("Pillow")){
            Destroy(collisionInfo.gameObject);
            isInvincible = true;
            this.transform.Find("Power").gameObject.SetActive(true);
            PowerColor.a = 1;
            Power.color = PowerColor;
            audioManager.PlaySound("pillow");
        }
        if(collisionInfo.gameObject.tag.Equals("NinjaCards")){
            Destroy(collisionInfo.gameObject);
            ninjaCard = 10;
            CardsText.text = "X "+ninjaCard;
            CardsValueHolder.SetActive(true);
            //ShowNinjaCardsValue();
        }
        if(collisionInfo.gameObject.tag.Equals("Finish") && !isWon){                        
            Player.gravityScale = 0;
            Player.velocity = Vector2.zero;
            Player.transform.position = collisionInfo.transform.Find("Target").position;
            isWon = true;
            sceneHolder.Won();
            Player.transform.parent = collisionInfo.transform.Find("Blanket");
            Player.transform.localPosition = new Vector2(-0.4f, 2.3f);
            audioManager.PlaySound("vic");
        }
    }
    public void GetLocationToThrowCards(Vector2 pos){
        if(ninjaCard > 0 && !bufferingTimeForCards){
            bufferingTimeForCards = true;
            Invoke("RemoveBuffer", 0.3f);
            throwCard(pos);
        }
    }

    void RemoveBuffer(){
        bufferingTimeForCards = false;
    }
    void throwCard(Vector2 pos){
        ninjaCard--;
        audioManager.PlaySound("fire");
        CardsText.text = "X "+ninjaCard;
        ShowNinjaCardsValue();
        GameObject g = Instantiate(ninjaCardObj);
        Destroy(g,3);
        g.transform.position = transform.position;        
        g.GetComponent<Rigidbody2D>().velocity = (pos - (Vector2)transform.position).normalized * 15;
    }

    void ShowNinjaCardsValue(){
        //CardsValueHolder.SetActive(true);
        Invoke("HideCardValueHolder", 1);
    }

    void HideCardValueHolder(){
        //CardsValueHolder.SetActive(false);
        CardValueHolderAnimator.Play("CardDisplayAnim",-1,0f);
    }
}
