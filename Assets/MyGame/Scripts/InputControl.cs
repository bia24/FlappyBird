using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InputControl : MonoBehaviour {

    private bool Enabled { get; set; }//是否启用
    public event Action OnTab; //定义点击事件
    
    
    // Use this for initialization
	void Start () {
        Enabled = false;
        OnTab += Game.Instance.bird.Jump;//注册跳跃事件
	}
	
	// Update is called once per frame
	void Update () {
        if (Enabled && Input.GetMouseButtonDown(0))
        {
            if (OnTab != null)
            {
                OnTab();//事件触发
            }
        }
	}


    public void UseControl(GameState state)
    {
        switch (state)
        {
            case GameState.Play:
                Enabled = true;
                OnTab();
                break;
            default:
                Enabled = false;
                break;
        }
    }
}
