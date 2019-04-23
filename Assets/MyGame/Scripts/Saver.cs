using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Saver : MonoBehaviour {
    //存储当前分数
    private int score=0;
    public int Score
    {
        get { return score; }
        set
        {
            score = value;
            //触发事件
            if(OnScoreChanged!= null)
            {
                OnScoreChanged(value);
            }
            else
            {
                Debug.LogError("OnScoreChanged 事件未绑定");
            }
        }
    }

    //存储最高分数
    public int Best
    {
        get { return PlayerPrefs.GetInt("Best",0); }
        set { PlayerPrefs.SetInt("Best",value); }
    }

    //score改变的时候触发
    public event Action<int> OnScoreChanged;

    //是否更新了最高成绩
    public bool isNew = false;

    //最高分设置回调函数，向OnScoreChanged注册
    public void SetBest(int score)
    {
        if (score > Best)
        {
            Best = score;
            isNew = true;
        }
    }
    //增加分数，向Hit方法中注册
    public void AddScore(GameObject obj)
    {
        if (obj.tag == "Hit")
        {
            Score=Score+(int)(Game.Instance.obstacleLoop.speed/2);
        }
        if (obj.tag == "Star")
        {
            Score += 2*(int)(Game.Instance.obstacleLoop.speed / 2);
            Sound.Instance.PlaySound("sfx_point");         
        }
        
    }

   

    public void ScoreChange(GameState state)
    {
        if (state == GameState.Play)
        {
            Score = 0;
            isNew = false;
        }
    }

    public void RestBestScore()
    {
        Best = 0;
    }

    


}
