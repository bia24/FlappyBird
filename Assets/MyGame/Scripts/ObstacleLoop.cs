using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleLoop : MonoBehaviour {
    public const float LEFT = -7.3f;
    public const float Right = 7.28f;
    public const float OVERFLOW = -0.01f;
    public const float MAX = 2.66f;
    public const float MIN = -2.98f;
    public const float STARMAX = 1.71f;
    public const float STARMIM = -0.44f;
    //额外的星星出现概率
    public float extraPointProbability = 0.5f;
    //障碍物对象
    public GameObject[] obstacles;
    //移动速度
    public float speed=6f;
    public float startSpeed;
    //控制是否移动
    public bool isMove = true;
    //控制是否延迟显示
    public bool visibleSoon = false;
    //设置所有障碍物的可见性
    public void SetObstaclesVisible(bool isVisible)
    {
        foreach (var t in obstacles)
        {
            SetObstacleVisible(t,isVisible);
        }
    }
    //设置一个障碍物的可见性
    public void SetObstacleVisible(GameObject obstacle, bool isVisible)
    {
        obstacle.transform.Find("Pipes").gameObject.SetActive(isVisible);
    }
    //设置一个障碍物中的管道随机高度&&额外分数星星的出现
    public void SetPipesWithRandomHeight(GameObject obstacle)
    {
        Transform[] transforms = obstacle.GetComponentsInChildren<Transform>();
        foreach (Transform t in transforms)
        {
            if(t.name=="FirstPipe"|| t.name == "TwicePipe")
            {
                Vector3 postion = t.localPosition;
                postion.y = Random.Range(MIN,MAX);
                t.localPosition = postion;
                Transform star = t.transform.Find("HitCollider/Point");
                if (Random.Range(0f, 1.0f) <= extraPointProbability)
                {
                    star.gameObject.SetActive(true);
                    star.GetComponent<SpriteRenderer>().enabled = true;
                }
                else
                {
                    star.gameObject.SetActive(false);
                }
                postion = star.localPosition;
                postion.y = Random.Range(STARMIM,STARMAX);
                star.localPosition = postion;
            }
        }
    }
    //关闭所有管道的碰撞器
    public void OffPipesCollider()
    {
        foreach(var o in obstacles)
        {
            Transform pipes = o.transform.Find("Pipes");
            BoxCollider2D[] bx2ds = pipes.GetComponentsInChildren<BoxCollider2D>();
            foreach(var b in bx2ds)
            {
                b.enabled = false;
            }
        }
    }
    //开启所有管道的碰撞器
    public void OnPipesCollider()
    {
        foreach (var o in obstacles)
        {
            Transform pipes = o.transform.Find("Pipes");
            BoxCollider2D[] bx2ds = pipes.GetComponentsInChildren<BoxCollider2D>();
            foreach (var b in bx2ds)
            {
                b.enabled = true;
            }
        }
    }

    private void Start()
    {
        startSpeed = speed;
    }


    private void Update()
    {
        if (isMove)
        {
            for (int i=0;i<obstacles.Length;i++)
            {
                GameObject thisObstacle = obstacles[i];
                thisObstacle.transform.Translate(Vector3.left*speed*Time.deltaTime);
                Vector3 thislocalPosition = thisObstacle.transform.localPosition;
                if (thislocalPosition.x <= LEFT)
                {
                    SetObstacleVisible(thisObstacle, visibleSoon);
                    thisObstacle.transform.localPosition = new Vector3(Right,thislocalPosition.y,thislocalPosition.z);
                    if (visibleSoon)
                    {
                        SetPipesWithRandomHeight(thisObstacle);
                    }
                    obstacles[i == 0 ? 1 : 0].transform.localPosition = new Vector3(OVERFLOW, thislocalPosition.y, thislocalPosition.z);
                    break;
                }
            }
            if (visibleSoon)
            {
                speed += 0.02f* Time.deltaTime;
            }
        }
    }

    public void OffStarActive(GameObject obj)
    {
        if (obj.tag == "Star")
        {
            obj.SetActive(false);
        }
    }


    public void SetObstaclesState(GameState state)
    {
        switch (state)
        {
            case GameState.Init:
                isMove = true;
                SetObstaclesVisible(false);
                break;
            case GameState.Ready:
                isMove = true;
                SetObstaclesVisible(false);
                OnPipesCollider();
                break;
            case GameState.Play:
                isMove = true;
                visibleSoon = true;
                speed = startSpeed;
                break;
            case GameState.Over:
                isMove = false;
                visibleSoon = false;
                break;
            default:
                break;
        }

    }


}
