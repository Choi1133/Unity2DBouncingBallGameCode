using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//시간을 나타내는 클래스 추후 점수로 집계된다.

public class TimeText : MonoBehaviour
{
    public Text timeTxt;
    double time;
    double score;

    void Update()
    {
        if(!GameManager.instance.isDie)
        {
            time += Time.deltaTime;

            double upTime = System.Math.Truncate(time * 100) / 100;
            score = upTime;

            timeTxt.text = "Score : " + upTime.ToString();
        }
        else
        {
            timeTxt.transform.position = new Vector3(335, 121, 0);
            timeTxt.fontSize = 30;
            timeTxt.text = timeTxt.text = "Score : " + score.ToString();
        }
    }
}
