using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditorInternal;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum GameState { GameOver, Pause, Continue, StageStart, StageClear, GameExit }

    public GameState gameState;
    public static GameManager instance;
    public int stagelevel = 1; //씬이 넘어갈 때 마다 하나씩 증가
    public bool isDie;

    bool isPause = true;

    void Awake()
    {
        if(instance == null) //instance가 없다면 하나 만들기
        {
            instance = this;
        }
        else if(instance != this) //씬에 instance가 더 있다면 파괴
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject); //이 객체는 파괴되지 않습니다.
    }

    // Start is called before the first frame update
    void Start()
    {
        gameState = GameState.StageStart;
    }

    // Update is called once per frame
    void Update()
    {
        switch(gameState)
        {
            case GameState.GameOver:
                GameOver();
                break;

            case GameState.Pause:
                Pause();
                break;

            case GameState.StageClear:
                StageClear(stagelevel);
                break;

            case GameState.StageStart:
                StageStart();
                break;

            case GameState.Continue:
                Continue();
                break;

            case GameState.GameExit:
                GameExit();
                break;
        }
    }

    void Pause()
    {
        if (!isPause)
        {            
            Time.timeScale = 0;
            isPause = true;
        }
        else if (isPause)
        {
            Time.timeScale = 1;
            gameState = GameState.StageStart;
            isPause = false;
        }           
    }

    void Continue()
    {
        if (isDie)
        {                    
           SceneManagers.sceneInstance.LoadStageOne();
           gameState = GameState.StageStart;
        }
    }

    void GameOver()
    {
        isDie = true;
        
        if(Input.GetKeyDown(KeyCode.C))
        {
            gameState = GameState.Continue;
        }
    }

    void StageStart()
    {
        isDie = false;

        if (Input.GetKeyDown(KeyCode.P))
        {
            gameState = GameState.Pause;
        }
    }

    public int StageClear(int level)
    {
        level++;
        gameState = GameState.StageStart;
        return level;
    }

    void GameExit()
    {
        Application.Quit();
    }

    public void OnExitButton()
    {
        gameState = GameState.GameExit;    
    }
}
