using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour {

    public TextMeshProUGUI runScoreText;
    [Space]
    public int scoreForEachClick;
    [HideInInspector]
    public int runScoreAmount;

    public delegate void RunStart();
    public static event RunStart RunStartEvents;

    private void Start()
    {
        runScoreText.text = "0 $";
    }

    public void SetRunScore()
    {
        runScoreAmount += scoreForEachClick;
        runScoreText.text = runScoreAmount.ToString() + " $";
    }

    public void RestartTheGame()
    {
        SceneManager.LoadScene(0);
    }

}
