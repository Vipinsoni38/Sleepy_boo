using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHolder : MonoBehaviour
{
    public GameObject Menu, PausedMenu, GameRestart, PauseButton, GameWon;
    bool isPaused = false, gameStarted = false, gameover = false;
    int lifeLeft = 0;
    // Start is called before the first frame update
    PlayerScript playerScript;
    void Start()
    {
        Time.timeScale = 0;
        ShowMenu(0);
        playerScript = FindObjectOfType<PlayerScript>();
        lifeLeft = PlayerPrefs.GetInt("lifeLeft", 3);
    }

    // Update is called once per frame
    void Update()
    {
        if(gameover){
            return;
        }
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(gameStarted){
               if(isPaused){
                   Play();
               } else{
                   Pause();
               }
            }
        }
    }

    public void Play(){
        PauseButton.SetActive(true);
        Time.timeScale = 1;
        ShowMenu(-1);        
        gameStarted = true;
        isPaused = false;
        playerScript.IsGamePaused(false);
    }
    public void Pause(){
        PauseButton.SetActive(false);
        Time.timeScale = 0;
        isPaused = true;
        ShowMenu(1);
        playerScript.IsGamePaused(true);
    }

    public void Gameover()
    {
        PauseButton.SetActive(false);
        gameover = true;
        PlayerPrefs.SetInt("lifeLeft", PlayerPrefs.GetInt("lifeLeft", 3)-1);
        ShowMenu(2);
        playerScript.IsGamePaused(true);
    }

    public void Restart(){      
        PlayerPrefs.SetInt("lifeLeft", 3);
        PlayerPrefs.SetFloat("CheckPointPos", 0);         
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void RestartFromCheckPoint(){      
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    void ShowMenu(int a){
        Menu.SetActive(false);
        PausedMenu.SetActive(false);
        if(a == 0){
            Menu.SetActive(true);
        }
        else if(a == 1){
            PausedMenu.SetActive(true);
        }
        else if(a == 2){
            GameRestart.SetActive(true);
            lifeLeft = PlayerPrefs.GetInt("lifeLeft", 3);
            if(lifeLeft == 0){
                GameRestart.transform.Find("checkpoint").gameObject.SetActive(false);
            }
            for(int i=(lifeLeft+1);i<=3;i++){
                GameRestart.transform.Find("LifeHolder").Find(""+i).Find("Image").gameObject.SetActive(true);
            }
        }
    }
    public void Won(){
        PauseButton.SetActive(false);
        gameover = true;
        ShowMenu(-1);
        Invoke("ShowWonScreen", 2);
    }
    void ShowWonScreen(){
        GameWon.SetActive(true);
    }
}
