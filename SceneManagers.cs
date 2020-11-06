using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneManagers : MonoBehaviour
{
    //게임 오브젝트에서 레벨이 증가 할 때 마다(GameState = StageClear가 될 때 마다)
    //다음 씬으로 이동 하고 싶다.
    //매니저는 게임당 하나밖에 없어서 이것도 싱글톤으로 만들어본다.
    //필요 속성 : GameManager.instacne.gameState, GameManager.Instace.Level

    public static SceneManagers sceneInstance;

    void Awake()
    {
        if (sceneInstance == null)
        {
            sceneInstance = this;
        }
        else if (sceneInstance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        LoadNextScene();
        LoadStageOne();
    }

    void LoadNextScene()
    {
        if(GameManager.instance.gameState == GameManager.GameState.StageClear)
        {
            //현재 씬 정보를 가져옴
            Scene scene = SceneManager.GetActiveScene();

            //현재 씬의 빌드 인덱스 가져옴
            int currSceneBuildIndex = scene.buildIndex;

            //다음 씬을 빌드 하기위해 현재 씬 빌드 인덱스에서 +1해준다
            int nextScene = currSceneBuildIndex + 1;

            //다음 씬을 불러온다.
            SceneManager.LoadScene(nextScene);

            //다음 씬을 불러오면 GameState상태를 StageStart로 바꿔준다.
            GameManager.instance.gameState = GameManager.GameState.StageStart;
        }        
    }

    void LoadStageOne() 
    {
        if(GameManager.instance.gameState == GameManager.GameState.GameOver)
        {
            //플레이어가 죽으면 처음부터 다시 시작 하도록 레벨을 1로 되돌려 놓는다.
            GameManager.instance.level = 1;

            //그 후 1스테이지 씬을 로드
            SceneManager.LoadScene("Stage1");
        }
    }
}
