using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnExitButton : MonoBehaviour
{
    public void OnClickExitButton()
    {
        GameManager.instance.gameState = GameManager.GameState.GameExit;
    }
}
