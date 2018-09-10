using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameStartAnimationEndEvent : MonoBehaviour {


    public BalloonTimerCtrler balloonTimerCtrler;
    public GameObject groundGameObject;

    public void LetTheGameStarts()
    {
        balloonTimerCtrler.StartTimer();
        groundGameObject.SetActive(false);
    }
}
