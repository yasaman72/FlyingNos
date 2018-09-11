using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyElementsController : MonoBehaviour
{

    [Header("Flying Money Setting")]
    public GameObject flyingMoney;
    public GameManager gameManager;
    public Transform flyingNumberStartingPosition;
    public float moneyMovementSpeed;
    public int moneyFromFlyingNumber;
    [Range(0, 100)]
    public float flyingMoneyReleaseChance;
    private int numbersClickedOnFLyingNumber;


    public void ClickedFlyingMoney()
    {
        flyingMoney.GetComponent<Animator>().SetTrigger("Clicked");
        numbersClickedOnFLyingNumber++;
        if (numbersClickedOnFLyingNumber > 3)
        {
            //Debug.Log("got flying money money");
            gameManager.SetRunCoin(moneyFromFlyingNumber, flyingMoney.transform);
            flyingMoney.SetActive(false);
        }
    }

    public void CheckAndReleaseFlyingMoney()
    {
        if (flyingMoney.activeInHierarchy)
            return;

        float randomNumber = Random.Range(0, 101);
        if (randomNumber < flyingMoneyReleaseChance)
        {
            //Debug.Log("generated flying money");

            flyingMoney.transform.position = flyingNumberStartingPosition.position;

            numbersClickedOnFLyingNumber = 0;
            flyingMoney.SetActive(true);

            StartCoroutine("FlyingTheMoney");
        }
    }

    IEnumerator FlyingTheMoney()
    {
        while (true)
        {
            flyingMoney.transform.Translate(Vector3.left * moneyMovementSpeed * Time.deltaTime);

            if (Camera.main.WorldToScreenPoint(flyingMoney.transform.position).x < 0)
            {
                //Debug.Log("Stoped the pig!");
                flyingMoney.SetActive(false);
                StopCoroutine("FlyingTheMoney");
            }

            yield return new WaitForSeconds(0.01f);
        }



    }
}
