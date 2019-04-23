using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour {
    public Sprite[] sprites;
    private bool isFisrtIntoGame = true;
    private void BackgroundRandomShow()
    {
        int index=Random.Range(0,sprites.Length);
        Sprite sprite = sprites[index];
        this.GetComponent<SpriteRenderer>().sprite = sprite;
    }
    /// <summary>
    /// 对BackgroundRandomShow的封装，注册到GameStateChange事件中去 
    /// </summary>
    /// <param name="state"></param>
    public void ChangeBackground(GameState state)
    {
        switch (state)
        {
            case GameState.Init:
                BackgroundRandomShow();
                break;
            case GameState.Ready:
                if (isFisrtIntoGame)
                {
                    isFisrtIntoGame = false;
                    break;
                }
                BackgroundRandomShow();
                break;
            default:
                break;
        }
    }
}
