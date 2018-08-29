using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class NumbersManager : MonoBehaviour
{
    public int startingNumber;
    public int tableCellsCount;
    public GameObject dollarEffect;
    [Space]
    public GameManager gameManager;
    [Space]
    public BalloonTimerCtrler balloonTimerCtrler;
    public float addedtimerAmount;
    [Space]
    public Numbers[] number;
    public Color[] textHintColor;

    private List<int> Lnumbers = new List<int>();
    private int lastClickedNumber;
    private int iterationCounter = 1;

    private void Start()
    {
        lastClickedNumber = startingNumber - 1;
        Lnumbers.Add(-1);
        //number = new Numbers[12];

        for (int i = 0; i < tableCellsCount; i++)
        {
            //Debug.Log("Added child number: " + i);
            number[i].numberGameObject = gameObject.transform.GetChild(i).gameObject;

            int randomNumber;
            randomNumber = Random.Range(startingNumber, startingNumber + tableCellsCount);

            while (Lnumbers.Contains(randomNumber))
            {
                randomNumber = Random.Range(startingNumber, startingNumber + tableCellsCount);
            }
            //Debug.Log("Final radom number is: " + randomNumber);
            Lnumbers.Add(randomNumber);

            number[i].digit = randomNumber;
            number[i].numberGameObject.transform.GetComponentInChildren<TextMeshProUGUI>().text = randomNumber.ToString();

            ChangeNumbersColor(i);
        }

        //Lnumbers.Sort();
        //for (int i = 0; i < Lnumbers.Count; i++) Debug.Log(Lnumbers[i] + "\n");

        //changing list contents to 1 to 12
        Lnumbers.Clear();
        for (int i = 0; i < tableCellsCount; i++)
        {
            Lnumbers.Add(i);
        }

        //Lnumbers.Sort();
        //for (int i = 0; i < Lnumbers.Count; i++) Debug.Log(Lnumbers[i] + "\n");
    }

    public void CheckClickedNumber(int btnIndex)
    {
        //correct number selected
        if (number[btnIndex].digit - 1 == lastClickedNumber)
        {
            lastClickedNumber++;

            int randomNumber;
            randomNumber = Random.Range(0, tableCellsCount);
            while (!Lnumbers.Contains(randomNumber))
            {
                randomNumber = Random.Range(0, tableCellsCount);
            }
            //Debug.Log("random number is: " + randomNumber);

            number[btnIndex].digit = randomNumber + (startingNumber + (iterationCounter * tableCellsCount));
            number[btnIndex].numberGameObject.transform.GetComponentInChildren<TextMeshProUGUI>().text = number[btnIndex].digit.ToString();
            ChangeNumbersColor(btnIndex);

            #region scoringEffects
            //effects
            number[btnIndex].numberGameObject.GetComponent<Animator>().SetTrigger("ShowNewNo");
            dollarEffect.GetComponent<RectTransform>().position = number[btnIndex].numberGameObject.GetComponent<RectTransform>().position;
            dollarEffect.GetComponent<ParticleSystem>().Play();
            #endregion

            //slider effects
            balloonTimerCtrler.timerSlider.size += addedtimerAmount;

            //giving score
            gameManager.SetRunScore();

            Lnumbers.Remove(randomNumber);
            //filling the numbers list when it gets empty
            if (Lnumbers.Count == 0)
            {
                //Debug.Log("Filling the number list");
                iterationCounter++;
                for (int i = 0; i < tableCellsCount; i++)
                {
                    Lnumbers.Add(i);
                }
                //    for (int j = 0; j < Lnumbers.Count; j++)
                //        Debug.Log(Lnumbers[j] + "\n");
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

