using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;



public class GameUI : MonoBehaviour {
    public GameObject Logo;
    public GameObject StartBtn;
    public GameObject ScoreBtn;
    public GameObject Ready;
    public GameObject HowToPlay;
    public GameObject Score;
    public GameObject Over;
    public Sprite[] metals;

    public void UpdateGameState(GameState state)
    {
        switch (state)
        {
            case GameState.Init:
                ToInit();
                break;
            case GameState.Ready:
                ToReady();
                break;
            case GameState.Play:
                ToPlay();
                break;
            case GameState.Over:
                ToOver();
                break;
            default:
                break;
        }
    }

     void ToInit()
    {
        Logo.SetActive(true);
        StartBtn.SetActive(true);
        ScoreBtn.SetActive(true);
        Ready.SetActive(false);
        HowToPlay.SetActive(false);
        Score.SetActive(false);
        Over.SetActive(false);
    }
     void ToReady()
    {
        Logo.SetActive(false);
        StartBtn.SetActive(false);
        ScoreBtn.SetActive(false);
        Ready.SetActive(true);
        HowToPlay.SetActive(true);
        Score.SetActive(false);
        Over.SetActive(false);
        //panel面板位置重置
        PanelReset();
    }
     void ToPlay()
    {
        Logo.SetActive(false);
        StartBtn.SetActive(false);
        ScoreBtn.SetActive(false);
        Ready.SetActive(false);
        HowToPlay.SetActive(false);
        Score.SetActive(true);
        Over.SetActive(false);
    }
     void ToOver()
    {
        Logo.SetActive(false);
        StartBtn.SetActive(false);
        ScoreBtn.SetActive(false);
        Ready.SetActive(false);
        HowToPlay.SetActive(false);
        Score.SetActive(false);
        Over.SetActive(true);
        //添加面板出场动画
        PanelMove();
    }

    /// <summary>
    /// 更新显示score文本框中的分数(实时)
    /// </summary>
    /// <param name="score"></param>
    public void UpdateCurrentScore(int score)
    {
        Score.GetComponent<Text>().text = score.ToString() ;
    }

    /// <summary>
    /// 对over面板中的结果进行显示，需要通过Game获得Saver的引用
    /// </summary>
    public void SetResultScore()
    {
        int currentScore = Game.Instance.saver.Score;
        int bestScore = Game.Instance.saver.Best;
        bool isNew = Game.Instance.saver.isNew;
        Sprite metal = null;
        if (currentScore == 0)
        {
            metal = null;
        }
        else if (currentScore > 0 && currentScore <= 5)
        {
            metal = metals[0];
        }
        else if (currentScore <= 10)
        {
            metal = metals[1];
        }
        else if (currentScore <= 15)
        {
            metal= metals[2];
        }
        else
        {
            metal = metals[3];
        }
        Over.transform.Find("Panel/Score").GetComponent<Text>().text = currentScore.ToString();
        Over.transform.Find("Panel/BestScore").GetComponent<Text>().text = bestScore.ToString();
        Over.transform.Find("Panel/IsNew").gameObject.SetActive(isNew);
       
        Image image = Over.transform.Find("Panel/Metal").GetComponent<Image>();
        if (metal == null)
        {
            Color c = image.color;
            c.a = 0;
            image.sprite = metal;
            image.color = c;
        }
        else
        {
            image.sprite = metal;
            Color c = image.color;
            c.a = 1;
            image.color = c;
        }
    }

    private void PanelMove()
    {
        Over.transform.Find("Panel").DOLocalMove(new Vector3(0, 91, 0), 0.5f);
    }

    private void PanelReset()
    {
        Over.transform.Find("Panel").localPosition = new Vector3(0,-369,0);
    }

}
