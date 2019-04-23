using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Logo : MonoBehaviour {

	// Use this for initialization
	void Start () {

        transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
        transform.DOScale(new Vector3(1, 1, 1), 0.5f)
            .SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Yoyo); //Dotween动画

    }


}
