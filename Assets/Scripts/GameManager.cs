using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour {

    public TextMeshProUGUI runScore;
    public int scoreForCurrectClick;

    private void Start()
    {
        runScore.text = "0 $";
    }

    public void SetRunScore()
    {
        runScore.text = PlayerPrefs.GetInt("RunScore").ToString() + " $";
    }
}
