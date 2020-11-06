using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//블록을 이동하고 싶다.
//필요속성 : 이동 속도

public class MoveBlock : MonoBehaviour
{
    //블록을 이동하고 싶다.
    //필요속성 : 이동 속도

    public float moveSpeed;
    public bool isMove = true;

    void Update()
    {
        //사용자의 입력에 따라 
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        //방향을 구하고
        Vector3 dir = new Vector3(h, v, 0);

        //이동한다.
        if(isMove)
        {
            this.transform.position += dir * moveSpeed * Time.deltaTime;           
        }

        GameOver(); //게임오버 상태일시
    }

    void GameOver()
    {
        if (GameManager.instance.gameState == GameManager.GameState.GameOver)
        {
            isMove = false;
        }
        else
        {
            isMove = true;
        }
    }
}
