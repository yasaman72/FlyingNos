using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour {

    [Space, Header("Coin System Variables")]
    public TextMeshProUGUI runCoinText;
    public TextMeshProUGUI playerCoinsText;
    public int coinForEachClick;
    [HideInInspector]
    public int runCoinAmount;

    [Space]
    public Animator mainCanvasAnimator;

    [Space]
    public GameObject endGameMenu;

    //public delegate void RunStart();
    //public static event RunStart RunStartEvents;

    private void Start()
    {
        runCoinText.text = "0";

        if (PlayerPrefs.HasKey("PlayerCoins"))
        {
            playerCoinsText.text = PlayerPrefs.GetInt("PlayerCoins").ToString();
            return;
        }
        else
        {
            PlayerPrefs.SetInt("PlayerCoins", 0);
            playerCoinsText.text = PlayerPrefs.GetInt("PlayerCoins").ToString();
        }
    }

    public void SetRunCoin()
    {
        runCoinAmount += coinForEachClick;
        runCoinText.text = runCoinAmount.ToString();
    }

    public void StartARun ()
    {
        mainCanvasAnimator.SetTrigger("StartGame");

    }

    public void RestartTheGame()
    {
        SceneManager.LoadScene(0);
    }

    public void FinishedARun()
    {
        endGameMenu.SetActive(true);
        AddCoinToPlayer(runCoinAmount);
    }

    public void AddCoinToPlayer(int coinAmount)
    {
        PlayerPrefs.SetInt("PlayerCoins", PlayerPrefs.GetInt("PlayerCoins") + coinAmount);
        playerCoinsText.text = PlayerPrefs.GetInt("PlayerCoins").ToString();
    }
}