using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour {
    private static Sound _instance;
    public static Sound Instance
    {
        get { return _instance; }
    }
    private void Awake()
    {
        _instance = this;
    }

    public AudioClip[] audioClips;

    public void PlaySound(string name)
    {
        foreach (var c in audioClips)
        {
            if (c.name == name)
            {
                AudioSource.PlayClipAtPoint(c,transform.position);
                break; 
            }
        }
    }



}
