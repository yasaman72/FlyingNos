using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour {

    #region Variables
    public NumbersManager numbersManager;

    [Space, Header("Coin System Variables")]
    public TextMeshProUGUI runCoinText;
    public TextMeshProUGUI playerCoinsText;
    public int coinForEachClick;
    [Space]
    public BettingController bettingController;
    [HideInInspector]
    public int runCoinAmount;

    [Space]
    public Animator mainCanvasAnimator;
    #endregion

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

        //Counts rounds played
        if (PlayerPrefs.HasKey("RunCount"))
        {
            PlayerPrefs.SetInt("RunCount", PlayerPrefs.GetInt("RunCount") + 1);
            Debug.Log("Run Count: " + PlayerPrefs.GetInt("RunCount"));
        }
        else
        {
            PlayerPrefs.SetInt("RunCount", 0);
            Debug.Log("Run Count: " + PlayerPrefs.GetInt("RunCount"));
        }
    }

    public void FinishedARun()
    {
        //recording number of passed buttons
        if (PlayerPrefs.HasKey("BestPassed"))
        {
            if (PlayerPrefs.GetInt("BestPassed") < (numbersManager.numberToSelect - numbersManager.startingNumber))
            {
                PlayerPrefs.SetInt("BestPassed", numbersManager.numberToSelect - numbersManager.startingNumber);
            }
                Debug.Log("Best Passed: " + PlayerPrefs.GetInt("BestPassed"));
        }
        else
        {
            PlayerPrefs.SetInt("BestPassed", numbersManager.numberToSelect - numbersManager.startingNumber);
            Debug.Log("Best Passed: " + PlayerPrefs.GetInt("BestPassed"));
        }

        bettingController.EndGameBetResult(numbersManager.numberToSelect);
        AddCoinToPlayer(runCoinAmount);
    }

    public void SetupGameAfterFinishingARun()
    {
        mainCanvasAnimator.SetTrigger("EndGame");


    }


    public void AddCoinToPlayer(int coinAmount)
    {
        PlayerPrefs.SetInt("PlayerCoins", PlayerPrefs.GetInt("PlayerCoins") + coinAmount);
        playerCoinsText.text = PlayerPrefs.GetInt("PlayerCoins").ToString();
        if (PlayerPrefs.GetInt("PlayerCoins") < 0)
            PlayerPrefs.SetInt("PlayerCoins", 0);
    }
}