using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BettingController : MonoBehaviour
{
    #region Variables
    [Tooltip("Show the bet popup after each \"Bet Offer Interval\" rounds.\n Should not be 0!")]
    public int startBettingAfterThisScore;
    public int betOfferInterval;
    public float betNumberMultiple;
    [Tooltip("Enter a negative number.")]
    public int numberToHitLess;
    public int numberToHitMore;

    [Space]
    public GameManager gameManager;
    public NumbersManager numbersManager;
    [Space, Header("Betting Popup")]
    public GameObject bettingPopup;
    public TextMeshProUGUI numberText, rewardCoinText, loseCoinText;
    [Space, Header("End Game Bet")]
    public GameObject betResultPopup;
    public GameObject endGameMenu;
    public TextMeshProUGUI endRewardText;
    public TextMeshProUGUI endPenaltyText;
    public GameObject endGameWon, endGameLose;

    private float betNumberToHit, BetRewardAmount, BetPenaltyAmount;
    private bool acceptedTheBet;
    #endregion

    public void CheckForABetAndStart()
    {

        if (PlayerPrefs.GetInt("BestPassed") > startBettingAfterThisScore &&
            PlayerPrefs.GetInt("RunCount") % betOfferInterval == 0)
        {
            //selecting the bet number
            int randomNumber = Random.Range(numberToHitLess, numberToHitMore);
            //finds the bet number which is multipleable to the betNumberMultiple
            betNumberToHit = ((Mathf.Round((PlayerPrefs.GetInt("BestPassed") + numbersManager.startingNumber + randomNumber) / betNumberMultiple)) * betNumberMultiple);
            numberText.text = betNumberToHit.ToString();

            //selecting the bet reward -----------------------change this!
            BetRewardAmount = Random.Range(10, 100);
            rewardCoinText.text = BetRewardAmount.ToString();

            //selecting the bet penalty -----------------------change this!
            BetPenaltyAmount = -Random.Range(10, 80);
            loseCoinText.text = BetPenaltyAmount.ToString();

            bettingPopup.SetActive(true);
        }
        else
        {
            gameManager.StartARun();
        }
    }

    public void AcceptTheBet()
    {
        gameManager.StartARun();
        acceptedTheBet = true;
        Debug.Log("Accepted the bet! ;)");
    }

    public void DeclineTheBet()
    {
        gameManager.StartARun();
        acceptedTheBet = false;
        Debug.Log("Declined the bet! :(");
    }


    public void EndGameBetResult(int lastNumber)
    {
        if (acceptedTheBet)
        {
            betResultPopup.SetActive(true);
            if (lastNumber >= betNumberToHit)
            {
                gameManager.AddCoinToPlayer((int)BetRewardAmount);
                endRewardText.text = BetRewardAmount.ToString();
                endGameWon.SetActive(true);
            }
            else
            {
                gameManager.AddCoinToPlayer((int)BetPenaltyAmount);
                endPenaltyText.text = BetPenaltyAmount.ToString();
                endGameLose.SetActive(true);
            }
        }
        else
        {
            endGameMenu.SetActive(true);
        }
    }
}
