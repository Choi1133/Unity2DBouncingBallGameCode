using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncingBall : MonoBehaviour
{
    Rigidbody2D rig;
    public MoveBlock mb;
    float reaction = 300.0f;
    public int count = 1;

    void Start()
    {
        rig = gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(this.transform.position.y < - 20 || this.transform.position.y > 40)
        {
            GameManager.instance.gameState = GameManager.GameState.GameOver;   
            mb.isMove = false;
            gameObject.SetActive(false); //지정된 영역을 벗어나면(게임오버 되면) 객체 비활성화
        }        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Below")
        {
            DownBouncing();
        }

        //if(collision.collider.tag == "blockRight")
        //{
        //    BouncingPlus();
        //}

        //if (collision.collider.tag == "blockLeft")
        //{
        //    BouncingMinus();
        //}
    }

    //바닥에 닿으면 한번만 AddForce
    void DownBouncing()
    {
        count--;

        if (count == 0)
        {
            count = -1;

            rig.AddForce(new Vector2(500, 0));
        }
    }

    ////그 외 벽에 닿으면 진행 방향의 반대로 진행되도록
    //void BouncingPlus()
    //{
    //    Vector2 vecDir = new Vector2(1, 1);

    //    vecDir.x *= -reaction;
    //    vecDir.y *= -reaction;

    //    rig.AddForce(vecDir);
    //}

    //void BouncingMinus()
    //{
    //    Vector2 vecDir = new Vector2(-1, -1);

    //    vecDir.x *= -reaction;
    //    vecDir.y *= -reaction;

    //    rig.AddForce(vecDir);
    //}
}
