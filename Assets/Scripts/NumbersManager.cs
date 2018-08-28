using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class NumbersManager : MonoBehaviour
{
    public Numbers[] number;
    public Color[] textHintColor;
    public GameManager gameManager;

    private List<int> Lnumbers = new List<int>();
    private int lastClickedNumber = -1;
    private int interationCounter = 1;

    private void Start()
    {
        Lnumbers.Add(-1);
        //number = new Numbers[12];

        for (int i = 0; i < number.Length; i++)
        {
            //Debug.Log("Added child number: " + i);
            number[i].numberGameObject = gameObject.transform.GetChild(i).gameObject;

            int randomNumber;
            randomNumber = Random.Range(0, number.Length);

            while (Lnumbers.Contains(randomNumber))
            {
                randomNumber = Random.Range(0, number.Length);
            }
            //Debug.Log("Final radom number is: " + randomNumber);
            Lnumbers.Add(randomNumber);

            number[i].digit = randomNumber;
            number[i].numberGameObject.transform.GetComponentInChildren<TextMeshProUGUI>().text = randomNumber.ToString();
            ChangeNumbersColor(i);
        }

        Lnumbers.Remove(-1);
        Lnumbers.Remove(number.Length);
        Lnumbers.Sort();
        //for (int i = 0; i < Lnumbers.Count; i++) Debug.Log(Lnumbers[i] + "\n");
    }

    public void CheckClickedNumber(int btnIndex)
    {
        if (number[btnIndex].digit - 1 == lastClickedNumber)
        {
            lastClickedNumber++;

            int randomNumber;
            randomNumber = Random.Range(0, number.Length);
            while (!Lnumbers.Contains(randomNumber))
            {
                randomNumber = Random.Range(0, number.Length);
            }
            Debug.Log("random number is: " + randomNumber);

            number[btnIndex].digit = randomNumber + (number.Length * interationCounter);
            number[btnIndex].numberGameObject.transform.GetComponentInChildren<TextMeshProUGUI>().text = number[btnIndex].digit.ToString();
            ChangeNumbersColor(btnIndex);

            Lnumbers.Remove(randomNumber);
            //filling the numbers list when it gets empty
            if (Lnumbers.Count == 0)
            {
                Debug.Log("Filling the number list");
                interationCounter++;
                for (int i = 0; i < number.Length; i++)
                {
                    Lnumbers.Add(i);
                }
            }

            if(PlayerPrefs.HasKey("RunScore"))
            {
                PlayerPrefs.SetInt("RunScore", 
                    PlayerPrefs.GetInt("RunScore") + gameManager.scoreForCurrectClick);
                gameManager.SetRunScore();
            }
            else
            {
                PlayerPrefs.SetInt("RunScore", gameManager.scoreForCurrectClick);
                gameManager.SetRunScore();
            }
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
            Debug.Log(secondDigit);
            Debug.Log(numberString[numberString.Length - 1]);
        }

        number[numberIndex].numberGameObject.transform.GetComponentInChildren<TextMeshProUGUI>().color = textHintColor[secondDigit];
    }

}

