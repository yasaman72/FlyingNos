using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour {

    #region Variables
    [Space]
    public NumbersManager numbersManager;
    public UpgradesManager upgradesManager;
    public GameObject groundGameObject;
    public TextMeshProUGUI bestScoreText;
    public GameObject coinParticle;

    public GameObject backgroundSky, foregroundSky;

    [Space]
    public AudioSource gameManagerAudioSource;
    public AudioClip balloonUpAudioClip;

    [Space]
    public GameObject[] activateTheseGameObjectsWhenStart;
    public GameObject[] deactiveTheseGameObjectsWhenStart;

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

        if (PlayerPrefs.HasKey("PlayerCoins"))
        {
            playerCoinsText.text = PlayerPrefs.GetInt("PlayerCoins").ToString();
        }
        else
        {
            PlayerPrefs.SetInt("PlayerCoins", 0);
            playerCoinsText.text = PlayerPrefs.GetInt("PlayerCoins").ToString();
        }

        //showing best score
        if (PlayerPrefs.HasKey("BestPassed"))
        {
            bestScoreText.text = PlayerPrefs.GetInt("BestPassed").ToString();
        }


        for (int i = 0; i < activateTheseGameObjectsWhenStart.Length; i++)
        {
            activateTheseGameObjectsWhenStart[i].SetActive(true);
        }
        for (int i = 0; i < deactiveTheseGameObjectsWhenStart.Length; i++)
        {
            deactiveTheseGameObjectsWhenStart[i].SetActive(false);
        }

    }

    public void SetRunCoin(Transform transform)
    {
        runCoinAmount += coinForEachClick;
        runCoinText.text = runCoinAmount.ToString();

        coinParticle.GetComponent<RectTransform>().position = transform.GetComponent<RectTransform>().position;
        coinParticle.GetComponent<ParticleSystem>().Play();
    }

    public void SetRunCoin(int moneyToAdd, Transform transform)
    {
        runCoinAmount += moneyToAdd;
        runCoinText.text = runCoinAmount.ToString();

        coinParticle.GetComponent<RectTransform>().position = transform.GetComponent<RectTransform>().position;
        coinParticle.GetComponent<ParticleSystem>().Play();
    }


    public void StartARun ()
    {
        gameManagerAudioSource.clip = balloonUpAudioClip;
        gameManagerAudioSource.Play();

        runCoinText.text = "0";
        runCoinAmount = 0;

        mainCanvasAnimator.SetTrigger("StartGame");

        backgroundSky.SetActive(true);
        foregroundSky.SetActive(true);

        //Counts rounds played
        if (PlayerPrefs.HasKey("RunCount"))
        {
            PlayerPrefs.SetInt("RunCount", PlayerPrefs.GetInt("RunCount") + 1);
            //Debug.Log("Run Count: " + PlayerPrefs.GetInt("RunCount"));
        }
        else
        {
            PlayerPrefs.SetInt("RunCount", 0);
            Debug.Log("Run Count: " + PlayerPrefs.GetInt("RunCount"));
        }
    }

    public void FinishedARun()
    {
        backgroundSky.SetActive(false);
        foregroundSky.SetActive(false);
        groundGameObject.SetActive(true);

        numbersManager.clearTheTable();      

        //recording number of passed buttons
        if (PlayerPrefs.HasKey("BestPassed"))
        {
            if (PlayerPrefs.GetInt("BestPassed") < (numbersManager.numberToSelect))
            {
                PlayerPrefs.SetInt("BestPassed", numbersManager.numberToSelect);
                bestScoreText.text = PlayerPrefs.GetInt("BestPassed").ToString();
            }
                //Debug.Log("Best Passed: " + PlayerPrefs.GetInt("BestPassed"));
                bestScoreText.text = PlayerPrefs.GetInt("BestPassed").ToString();
        }
        else
        {
            PlayerPrefs.SetInt("BestPassed", numbersManager.numberToSelect);
            //Debug.Log("Best Passed: " + PlayerPrefs.GetInt("BestPassed"));
        }

        bettingController.EndGameBetResult(numbersManager.numberToSelect - 1);
        AddCoinToPlayer(runCoinAmount);
        upgradesManager.CheckAvailableUpgrades();
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