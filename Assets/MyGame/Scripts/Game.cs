using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Game : MonoBehaviour {
    #region 全局单例
    private static Game m_instance = null;
    public static Game Instance { get { return m_instance; } }
    private void Awake() {m_instance = this; }
    #endregion

    #region 状态改变事件定义及state变量定义
    public event Action<GameState> OnGameStateChanged;//定一个事件供别人注册，当游戏状态发生改变时触发。 
    private GameState mState = GameState.Init;
    public GameState State
    {
       get { return mState; }
       private set
        {
            //当state进行赋值改变的时候，触发事件
            mState = value;
            if (OnGameStateChanged != null)
            {
                OnGameStateChanged(mState);
            }
        }
    }
    #endregion
    #region 持有游戏所有要使用的组件的引用
    public GameUI gameUI = null;
    public Bird bird = null;
    public Background background = null;
    public InputControl inputControl = null;
    public ObstacleLoop obstacleLoop = null;
    public Saver saver = null;
    
    #endregion
    private void Start()
    {
        OnGameStateChanged += gameUI.UpdateGameState; //GameUI:update ui组件切换方法注册
        OnGameStateChanged += bird.UpdateBirdVisible;//Bird:update 显示方法注册
        OnGameStateChanged += bird.UseBirdGravity;//Bird:使用重力方法注册
        OnGameStateChanged += bird.Reset;//Bird:重置方法注册
        OnGameStateChanged += background.ChangeBackground;//background 改变方法注册
        OnGameStateChanged += inputControl.UseControl;//InputControl 开启控制方法注册
        OnGameStateChanged += saver.ScoreChange;//saver分数改变方法注册
        bird.OnDie += GoToOver;//Bird 死亡事件注册over方法
        bird.OnDie += obstacleLoop.OffPipesCollider;//Bird死亡注册关闭碰撞器方法
        bird.OnDie += bird.SetDieBirdSprite;//Bird死亡注册更改死亡图片
        bird.OnDie += gameUI.SetResultScore;//Bird死亡设置over面板分数
        OnGameStateChanged += obstacleLoop.SetObstaclesState;//ObstacleLoop障碍物状态设置方法注册
        saver.OnScoreChanged += saver.SetBest;//saver:最高分更新方法注册
        saver.OnScoreChanged += gameUI.UpdateCurrentScore;//gameui当前分数更新方法注册
        bird.OnHit += obstacleLoop.OffStarActive;//关闭star对象
        bird.OnHit += saver.AddScore;//saver 增加分数方法注册
        
        GoToInit();
    }
    private void Update()
    {
        if (State == GameState.Ready)
        {
            if (Input.GetMouseButtonDown(0))
            {
                GoToPlay();
            }
        }
    }

    #region 给外界提供改变state的接口
    public void GoToInit()
    {
        this.State = GameState.Init;
    }

    public void GoToReady()
    {
        this.State = GameState.Ready;
    }

    public void GoToPlay()
    {
        this.State = GameState.Play;
    }
    public void GoToOver()
    {
        this.State = GameState.Over;
    }
    #endregion



}
