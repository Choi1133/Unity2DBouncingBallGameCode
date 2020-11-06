using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum GameState { GameOver, Pause, Continue, StageStart, StageClear, Restart }

    public GameState gameState;
    public GameObject ball;
    public GameObject block;
    public BouncingBall bouncingBall;
    public static GameManager instance;
    public int level = 0; //씬이 넘어갈 때 마다 하나씩 증가
    public bool isDie;
    public bool isAddLevel = false;

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
        ball = gameObject.GetComponent<GameObject>();
        block = gameObject.GetComponent<GameObject>();
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

            case GameState.Restart:
                Restart();
                break;

            case GameState.StageClear:
                StageClear();
                break;

            case GameState.StageStart:
                StageStart();
                break;

            case GameState.Continue:
                Continue();
                break;
        }
    }

    void Restart()
    {
       if(Input.GetKeyDown(KeyCode.R))
       {
           ball.transform.position = new Vector3(0, 0, 0);
           block.transform.position = new Vector3(0, -8.6f, 0);
           bouncingBall.count = 1;
           gameState = GameState.Restart;
       }
    }

    void Pause()
    {
       if(Input.GetKeyDown(KeyCode.P))
        {
            if(!isPause)
            {
                isPause = true;
                Time.timeScale = 0;
            }
            else if(isPause)
            {
                isPause = false;
                Time.timeScale = 1;
            }

            gameState = GameState.Pause;
        }
              
    }

    void Continue()
    {
        if (isDie && Input.GetKeyDown(KeyCode.C))
        {                    
           level = 1;
           gameState = GameState.Continue;
        }
    }

    void GameOver()
    {
        isDie = true;
        gameState = GameState.Continue;
    }

    void StageStart()
    {
        isAddLevel = false;

        if(Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.Space))
        {
            isPause = false;
            Time.timeScale = 1;
        }
    }

    void StageClear()
    {
        level++;
        isAddLevel = true;
        gameState = GameState.StageStart;
    }
}
