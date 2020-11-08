using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum GameState { GameOver, Pause, Continue, StageStart, StageClear, Restart, GameExit }

    public GameState gameState;
    public GameObject ball;
    public GameObject block;
    public BouncingBall bouncingBall;
    public static GameManager instance;
    public int stagelevel = 0; //씬이 넘어갈 때 마다 하나씩 증가
    public bool isDie;

    bool isPause = false;

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
        ball = gameObject.GetComponent<GameObject>();
        block = gameObject.GetComponent<GameObject>();
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

            case GameState.Restart:
                Restart();
                break;

            case GameState.StageClear:
                StageClear(SceneManagers.sceneInstance.currSceneBuildIndex);
                break;

            case GameState.StageStart:
                StageStart();
                break;

            case GameState.Continue:
                Continue();
                break;
        }
        GameExit();
    }

    void Restart()
    {   
        ball.transform.position = new Vector3(0, 0, 0);
        block.transform.position = new Vector3(0, -8.6f, 0);
        bouncingBall.count = 1;
        gameState = GameState.StageStart;
    }

    void Pause()
    {
        if (!isPause)
        {
            isPause = true;
            Time.timeScale = 0;
        }
        else if (isPause)
        {
            isPause = false;
            Time.timeScale = 1;
            gameState = GameState.StageStart;
        }           
    }

    void Continue()
    {
        if (isDie && Input.GetKeyDown(KeyCode.C))
        {                    
           stagelevel = 1;
        }
    }

    void GameOver()
    {
        isDie = true;
        gameState = GameState.Continue;
    }

    void StageStart()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            gameState = GameState.Pause;
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            gameState = GameState.Restart;
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
        if(Input.GetKeyDown(KeyCode.Escape))
        {
          Application.Quit();          
        }
    }
}
