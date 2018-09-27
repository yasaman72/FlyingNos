using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OfflineRewardManager : MonoBehaviour
{
    public int initialRewardAmountForEachMinute;
    [Tooltip("don't change this manualy!")]
    public int offlineRewardAmountForEachMinute;
    public int maxMinutesToCalculateReward;
    private int coinsToAddToPlayer;
    public GameObject offlineRewardsPopup;
    public GameManager gameManager;
    public TextMeshProUGUI rewardAmountText;


    System.DateTime startTime;
    System.DateTime exitTime;

    int minutesPassed;

    private void Start()
    {
        startTime = System.DateTime.Now;
        Debug.Log("Application started at " + startTime);

        if (PlayerPrefs.HasKey("exitTime"))
        {
            minutesPassed = ((DateTimeToUnixTimestamp(startTime) - PlayerPrefs.GetInt("exitTime")));

            Debug.Log("Application minutes passed: " + minutesPassed);

            CalculateOfflineRewards();
        }
        else
        {
            Debug.Log("no data on exit time");
        }
    }

    private void OnApplicationQuit()
    {
        exitTime = System.DateTime.Now;
        Debug.Log("Application ended at " + exitTime);

        PlayerPrefs.SetInt("exitTime", DateTimeToUnixTimestamp(exitTime));

        Debug.Log("Application ended at " + DateTimeToUnixTimestamp(exitTime) + " minutes long time.");
    }

    public static int DateTimeToUnixTimestamp(System.DateTime dateTime)
    {
        return (int)(dateTime - new System.DateTime(1970, 1, 1)).TotalMinutes;
    }

    private void CalculateOfflineRewards()
    {

        if (minutesPassed > maxMinutesToCalculateReward)
        {
            coinsToAddToPlayer = maxMinutesToCalculateReward * offlineRewardAmountForEachMinute;
        }
        else
        {
            coinsToAddToPlayer = minutesPassed * offlineRewardAmountForEachMinute;
        }

        if (minutesPassed < 5)
        {
            offlineRewardsPopup.SetActive(true);
        }

        Debug.Log("Coins to add to player: " + coinsToAddToPlayer);

    }


    public void AddOfflineRewards(bool watchedAds)
    {

        if (watchedAds)
        {
            rewardAmountText.text = (coinsToAddToPlayer * 2).ToString();
            gameManager.AddCoinToPlayer(coinsToAddToPlayer * 2);
        }
        else
        {
            rewardAmountText.text = coinsToAddToPlayer.ToString();
            gameManager.AddCoinToPlayer(coinsToAddToPlayer);
        }

    }
}
