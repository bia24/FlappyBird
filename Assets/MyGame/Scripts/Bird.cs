using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Bird : MonoBehaviour {
    //小鸟可视化开关
	public bool Isvisible
    {
        get { return gameObject.activeInHierarchy; }
        private set { gameObject.SetActive(value); }
    }
    //小鸟重力开关
    public bool UseGravity
    {
        get { return GetComponent<Rigidbody2D>().gravityScale == 1; }
        private set { GetComponent<Rigidbody2D>().gravityScale = value ? 1 : 0; }
    }
    //小鸟速度控制
    public float jumpSpeed = 6f;

    //小鸟死亡和穿越管道事件定义
    public event Action<GameObject> OnHit=null;
    public event Action OnDie=null;

    //小鸟初始位置记录
    private Vector3 defaultPosition;

    //小鸟死亡图片
    public Sprite BirdDieSprite;

    private void Start()
    {
        defaultPosition = GetComponent<Transform>().position;
       
    }
    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        /*下面调用OnDie和OnHit两个事件*/
        if(collision.gameObject.tag== "Pipe"||collision.gameObject.tag=="Land")
        {
            if (OnDie != null)
            {
                if (Game.Instance.State != GameState.Over)
                {
                    OnDie();
                    Sound.Instance.PlaySound("sfx_die");
                }
            }
            else
            {
                Debug.LogError("OnDie事件未绑定");
            }
        }
        if (collision.gameObject.tag == "Hit")
        {
            if (OnHit != null)
            {
                OnHit(collision.gameObject);
            }
            else
            {
                Debug.LogError("OnHit事件未绑定");
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Star")
        {
            if (OnHit != null)
            {
                OnHit(collision.gameObject);
            }
        }
    }


    public void UpdateBirdVisible(GameState state)
    {
        switch (state)
        {
            case GameState.Init:
                Isvisible = false;
                break;
            case GameState.Ready:
                Isvisible = true;
                break;
            case GameState.Play:
                Isvisible = true;
                break;
            case GameState.Over:
                Isvisible = true;
                break;
            default:
                Isvisible = false;
                break;
        }
    }


    public void UseBirdGravity(GameState state)
    {
        switch (state)
        {
            case GameState.Init:
                UseGravity = false;
                break;
            case GameState.Ready:
                UseGravity = false;
                break;
            case GameState.Play:
                UseGravity = true;
                break;
            case GameState.Over:
                UseGravity = true;
                break;
            default:
                UseGravity = false;
                break;
        }
    }

  

    /// <summary>
    /// 小鸟跳跃函数
    /// </summary>
    public void Jump()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector3.up * jumpSpeed;
        Sound.Instance.PlaySound("sfx_wing");
    }
    /// <summary>
    ///  小鸟重置
    /// </summary>
    public void Reset(GameState state)
    {
        if (state == GameState.Ready)
        {
            GetComponent<Transform>().position = defaultPosition;
            GetComponent<Animator>().enabled = true;
        }
    }
    /// <summary>
    /// 设置小鸟死亡
    /// </summary>
    public void SetDieBirdSprite()
    {
        GetComponent<SpriteRenderer>().sprite = BirdDieSprite;
        GetComponent<Animator>().enabled = false;
    }
  

    
}
