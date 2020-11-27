using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnStartButton : MonoBehaviour
{
    public void OnClickButton()
    {
        SceneManagers.sceneInstance.LoadNextScene();
    }
}
