using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(GridLayoutGroup))]
public class NumbersManager : MonoBehaviour
{
    #region variables
    [Header("Table Settings:")]
    public GameObject numbersBtnPrefab;
    public int startingNumber;
    [Range(1, 20)]
    public int startingTableSize;
    [Range(1, 20)]
    public int maxTableSize;
    [Tooltip("Don't set it to 0!!")]
    public int newCellInterval;
    [Space]
    public int coinMinInterval;
    public int coinMaxInterval;

    [Space]
    public ObjectPooler objectPooler;
    [Space]
    public GameObject coinParticle;

    [Space]
    public GameManager gameManager;
    [Space]
    public BalloonTimerCtrler balloonTimerCtrler;
    public float addedtimerAmount;
    [Space]
    public List<Numbers> number;
    public Color[] textHintColor;


    private GridLayoutGroup MygridLayoutGroup;
    private int tableSize;
    private List<int> Lnumbers;
    [HideInInspector]
    public int numberToSelect;

    private int coinMakerChecker;

    private int iterationCounter = 0;
    #endregion

    private void Start()
    {
        MygridLayoutGroup = gameObject.GetComponent<GridLayoutGroup>();

        coinMakerChecker = Random.Range(coinMinInterval, coinMaxInterval);

        numberToSelect = startingNumber;
        tableSize = startingTableSize;

        number = new List<Numbers>();
        Lnumbers = new List<int>();

        if (tableSize > 12)
        {
            MygridLayoutGroup.constraintCount = 4;
        }
    }

    public void PopulateTheTable()
    {
        //creating button objects
        for (int i = 0; i < startingTableSize; i++)
        {
            CreateNewNumberGameObject(i);
        }
    }

    private void CreateNewNumberGameObject(int i)
    {
        GameObject numberGameObject = Instantiate(numbersBtnPrefab, gameObject.transform);
        numberGameObject.GetComponent<NumberGameObjectsManager>().buttonIndex = i;

        Numbers temp = new Numbers();
        number.Add(temp);

        number[i].numberGameObject = gameObject.transform.GetChild(i).gameObject;
        AddNumberToObjects(i);
    }

    private void AddNumberToObjects(int i)
    {
        if (Lnumbers.Count == 0)
        {
            AddItemsToNumberList();
            iterationCounter++;
        }

        int randomNumber = Random.Range(0, Lnumbers.Count);

        number[i].digit = Lnumbers[randomNumber];
        //number[i].digit = randomNumber + (startingNumber + (iterationCounter * tableSize));
        number[i].numberGameObject.transform.GetComponentInChildren<TextMeshProUGUI>().text = number[i].digit.ToString();

        ChangeNumbersColor(i);
        AddCoinToObjects(i);

        Lnumbers.Remove(Lnumbers[randomNumber]);

    }

    private void AddCoinToObjects(int i)
    {
        if (coinMakerChecker <= 0)
        {
            number[i].hasCoinOnIt = true;
            number[i].numberGameObject.GetComponent<NumberGameObjectsManager>().myCoinGameObject.SetActive(true);
            coinMakerChecker = Random.Range(coinMinInterval, coinMaxInterval);
        }
        else
        {
            number[i].numberGameObject.GetComponent<NumberGameObjectsManager>().myCoinGameObject.SetActive(false);
            coinMakerChecker--;
        }
    }

    private void AddItemsToNumberList()
    {
        //Debug.Log("adding new Items to list ...");
        for (int i = startingNumber + (iterationCounter * startingTableSize); i < startingNumber + (iterationCounter * startingTableSize) + startingTableSize; i++)
        {
            Lnumbers.Add(i);
        }

        //for (int i = 0; i < Lnumbers.Count; i++)
        //{
        //    Debug.Log(i + " number: " + Lnumbers[i]);
        //}
    }

    public void CheckClickedNumber(int btnIndex)
    {
        //correct number selected
        if (number[btnIndex].digit == numberToSelect)
        {
            numberToSelect++;
            AddNumberToObjects(btnIndex);

            #region otherEffects
            //effects
            number[btnIndex].numberGameObject.GetComponent<Animator>().SetTrigger("ShowNewNo");

            //create the click effect
            GameObject ClickEffect = objectPooler.GetPooledObject();
            if (ClickEffect != null)
            {
                ClickEffect.GetComponent<RectTransform>().position = number[btnIndex].numberGameObject.GetComponent<RectTransform>().position;
                ClickEffect.SetActive(true);
                ClickEffect.GetComponent<Animator>().SetTrigger("Clicked");
            }

            //slider effects
            balloonTimerCtrler.timerSlider.size += addedtimerAmount;

            //giving coin
            if (number[btnIndex].hasCoinOnIt == true)
            {
                gameManager.SetRunCoin();
                coinParticle.GetComponent<RectTransform>().position = number[btnIndex].numberGameObject.GetComponent<RectTransform>().position;
                coinParticle.GetComponent<ParticleSystem>().Play();
            }
            #endregion

            //Adding a new cell to table
            if (tableSize != maxTableSize && numberToSelect % newCellInterval == 0)
            {
                CreateNewNumberGameObject(tableSize);
                tableSize++;
                if (tableSize > 12)
                {
                    MygridLayoutGroup.constraintCount = 4;
                }
            }
        }
        else
        {
            number[btnIndex].numberGameObject.GetComponent<Animator>().SetTrigger("WrongNo");
        }
    }

    private void ChangeNumbersColor(int numberIndex)
    {
        string numberString;
        int secondDigit = 0;
        numberString = number[numberIndex].digit.ToString();

        if (numberString.Length > 1)
        {
            secondDigit = int.Parse(numberString[numberString.Length - 2].ToString());
            //Debug.Log(secondDigit);
            //Debug.Log(numberString[numberString.Length - 1]);
        }

        number[numberIndex].numberGameObject.GetComponent<Image>().color = textHintColor[secondDigit];
    }

}

