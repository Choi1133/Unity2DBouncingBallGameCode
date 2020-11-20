using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneManagers : MonoBehaviour
{
    //게임 오브젝트에서 레벨이 증가 할 때 마다(GameState = StageClear가 될 때 마다)
    //다음 씬으로 이동 하고 싶다.
    //매니저는 게임당 하나밖에 없어서 이것도 싱글톤으로 만들어본다.
    //필요 속성 : GameManager.instacne.gameState, GameManager.Instace.stagelevel

    public int currSceneBuildIndex;

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

    public void LoadNextScene()
    {
       //현재 씬 정보를 가져옴
       Scene scene = SceneManager.GetActiveScene();
       
       //현재 씬의 빌드 인덱스 가져옴
       currSceneBuildIndex = scene.buildIndex;
                  
       //다음 씬을 빌드 하기위해 현재 씬 빌드 인덱스에서 +1해준다
       GameManager.instance.stagelevel = GameManager.instance.StageClear(currSceneBuildIndex);
       
       //다음 씬을 불러온다.
       SceneManager.LoadScene(GameManager.instance.stagelevel);             
    }

    public void LoadStageOne() 
    {
        if(GameManager.instance.gameState == GameManager.GameState.Continue)
        {
            SceneManager.LoadScene("Stage1");
        }     
    }
}
