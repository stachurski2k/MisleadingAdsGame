using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [SerializeField] int winConditions=1;
    [SerializeField] GameObject winMenu;
    [SerializeField] GameObject loseMenu;
    [SerializeField] GameObject pauseMenu;
    public UnityEvent OnGameWon;
    public UnityEvent OnGameLost;
    bool inGame=true;
    bool gameFinished=false;
    int conditions=0;
    void Update()
    {
        if(!gameFinished&&Input.GetKeyDown(KeyCode.Escape)){
            inGame=!inGame;
            SetTime(inGame);
            SetInGame();
        }
    }
    void SetOneActive(GameObject go){
        winMenu.SetActive(false);
        loseMenu.SetActive(false);
        pauseMenu.SetActive(false);
        go.SetActive(true);
    }
    void SetTime(bool active){
        Time.timeScale=(active)?1f:0f;
    }
    void SetInGame(){
        pauseMenu.gameObject.SetActive(!inGame);
    }
    public void WinGame(){
        conditions++;
        if(conditions>=winConditions){
            OnGameWon?.Invoke();
            SetTime(false);
            gameFinished=true;
            SetOneActive(winMenu);
        }
    }
    public void LoseGame(){
        OnGameLost?.Invoke();
        SetTime(false);
        gameFinished=true;
        SetOneActive(loseMenu);
    }
    public void ResumeGame(){
        inGame=true;
        SetTime(true);
        SetInGame();
    }
    public void RestartGame(){
        SetTime(true);
        LevelLoader.LoadActiveScene();
    }
    public void LoadMenu(){
        SetTime(true);
        LevelLoader.LoadMainScene();
    }
}
