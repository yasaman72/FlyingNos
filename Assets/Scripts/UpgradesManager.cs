using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class UpgradeObject
{
    public string upgradeName;

    [Space]
    public int currentLevel;

    [Header("Level Costs")]
    public int minLevelCost;
    public float levelCostMultiplier;
    [HideInInspector]
    public int currentLevelCost;

    [Header("Bonuses")]
    public int minBonus;
    public float BonusMultiplier;
    [HideInInspector]
    public int currentBonus;

    [Space]
    public TextMeshProUGUI upgradeCostText, upgradeLevelText, upgradeBonusText;

}

public class UpgradesManager : MonoBehaviour
{


    public NumbersManager numbersManager;
    public GameManager gameManager;
    public BalloonTimerCtrler balloonTimerCtrler;
    [Space]
    public List<UpgradeObject> upgrades;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("Upgrade0"))
        {
            PlayerPrefs.SetInt("Upgrade0", 1);

            PlayerPrefs.SetInt("Upgrade1", 1);

            PlayerPrefs.SetInt("Upgrade2", 1);

            PlayerPrefs.SetInt("Upgrade3", 1);

            PlayerPrefs.SetInt("Upgrade4", 1);
        }

        for (int i = 0; i < upgrades.Count; i++)
        {
            SetUpgradeLevel();

            SettingTheLevelCost(i);

            SetUpgradeBonus(i);

            SetUpgradeEffects(i);
        }
    }

    public void NextLevelUpgrade(int i)
    {
        if (upgrades[i].currentLevelCost <= PlayerPrefs.GetInt("PlayerCoins"))
        {
            gameManager.AddCoinToPlayer(-upgrades[i].currentLevelCost);

            //setting the level
            switch (i)
            {
                case 0:
                    PlayerPrefs.SetInt("Upgrade0", PlayerPrefs.GetInt("Upgrade0") + 1);
                    break;
                case 1:
                    PlayerPrefs.SetInt("Upgrade1", PlayerPrefs.GetInt("Upgrade1") + 1);
                    break;
                case 2:
                    PlayerPrefs.SetInt("Upgrade2", PlayerPrefs.GetInt("Upgrade2") + 1);
                    break;
                case 3:
                    PlayerPrefs.SetInt("Upgrade3", PlayerPrefs.GetInt("Upgrade3") + 1);
                    break;
                case 4:
                    PlayerPrefs.SetInt("Upgrade4", PlayerPrefs.GetInt("Upgrade4") + 1);
                    break;
                default:
                    break;
            }

            SetUpgradeLevel();

            SettingTheLevelCost(i);

            SetUpgradeBonus(i);

            SetUpgradeEffects(i);

        }
    }

    private void SetUpgradeLevel()
    {
        upgrades[0].currentLevel = PlayerPrefs.GetInt("Upgrade0");
        upgrades[0].upgradeLevelText.text = PlayerPrefs.GetInt("Upgrade0").ToString();

        upgrades[1].currentLevel = PlayerPrefs.GetInt("Upgrade1");
        upgrades[1].upgradeLevelText.text = PlayerPrefs.GetInt("Upgrade1").ToString();

        upgrades[2].currentLevel = PlayerPrefs.GetInt("Upgrade2");
        upgrades[2].upgradeLevelText.text = PlayerPrefs.GetInt("Upgrade2").ToString();

        upgrades[3].currentLevel = PlayerPrefs.GetInt("Upgrade3");
        upgrades[3].upgradeLevelText.text = PlayerPrefs.GetInt("Upgrade3").ToString();

        upgrades[4].currentLevel = PlayerPrefs.GetInt("Upgrade4");
        upgrades[4].upgradeLevelText.text = PlayerPrefs.GetInt("Upgrade4").ToString();

    }

    private void SetUpgradeBonus(int i)
    {
        upgrades[i].currentBonus = upgrades[i].minBonus * Mathf.CeilToInt(Mathf.Pow(upgrades[i].BonusMultiplier, upgrades[i].currentLevel - 1));

        upgrades[i].upgradeBonusText.text = "+ " + upgrades[i].currentBonus.ToString();
    }

    private void SettingTheLevelCost(int i)
    {
        //cost based on level calculation formula
        upgrades[i].currentLevelCost =
            Mathf.CeilToInt(upgrades[i].minLevelCost *
            (Mathf.Pow((upgrades[i].levelCostMultiplier), upgrades[i].currentLevel - 1)));

        upgrades[i].upgradeCostText.text = upgrades[i].currentLevelCost.ToString();
    }

    private void SetUpgradeEffects(int i)
    {
        switch (i)
        {
            case 0:
                //Number Money
                gameManager.coinForEachClick = Mathf.CeilToInt(upgrades[i].currentLevel * upgrades[i].BonusMultiplier) - 1;
                upgrades[i].upgradeBonusText.text = gameManager.coinForEachClick.ToString();
                break;
            case 1:
                //offline reward
                break;
            case 2:
                //timer time
                balloonTimerCtrler.sliderValueReduction -= upgrades[i].currentLevel * 0.0001f;
                break;
            case 3:
                //flying money
                break;
            case 4:
                //starting number
                numbersManager.startingNumber = Mathf.CeilToInt(upgrades[i].currentLevel * upgrades[i].BonusMultiplier) - 1;
                upgrades[i].upgradeBonusText.text = numbersManager.startingNumber.ToString();
                break;
            default:
                break;
        }
    }
}


