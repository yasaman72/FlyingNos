using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameStartAnimationEndEvent : MonoBehaviour {


    public BalloonTimerCtrler balloonTimerCtrler;

    public void LetTheGameStarts()
    {
        balloonTimerCtrler.StartTimer();
    }
}
