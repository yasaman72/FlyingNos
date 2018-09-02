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
    public int startingNumber;
    [Range(1, 20)]
    public int startingTableSize;
    [Range(1, 20)]
    public int maxTableSize;
    [Tooltip("Don't set it to 0!!")]
    public int newCellInterval;

    [Space]
    public GameObject numbersBtnPrefab;
    public GameObject dollarEffect;
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
    private int numberToSelect;

    private int iterationCounter = 0;
    #endregion

    private void Start()
    {
        MygridLayoutGroup = gameObject.GetComponent<GridLayoutGroup>();

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

        Lnumbers.Remove(Lnumbers[randomNumber]);
    }

    private void AddItemsToNumberList()
    {
        //Debug.Log("adding new Items to list ...");
        for (int i = startingNumber + (iterationCounter * startingTableSize); i < startingNumber + (iterationCounter * startingTableSize) + startingTableSize; i++)
        {
            Lnumbers.Add(i);
        }

        for (int i = 0; i < Lnumbers.Count; i++)
        {
            Debug.Log(i + " number: " + Lnumbers[i]);
        }
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
            dollarEffect.GetComponent<RectTransform>().position = number[btnIndex].numberGameObject.GetComponent<RectTransform>().position;
            dollarEffect.GetComponent<ParticleSystem>().Play();

            //slider effects
            balloonTimerCtrler.timerSlider.size += addedtimerAmount;

            //giving score
            gameManager.SetRunScore();
            #endregion

            //Adding a new cell to table
            if (tableSize != maxTableSize && numberToSelect % newCellInterval == 0)
            {
                CreateNewNumberGameObject(tableSize);
                tableSize++;
                if(tableSize > 12)
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

