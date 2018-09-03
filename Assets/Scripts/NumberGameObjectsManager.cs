using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberGameObjectsManager : MonoBehaviour {

    public int buttonIndex;
    public GameObject myCoinGameObject;
    private NumbersManager numbersManager;

    private void Start()
    {
        numbersManager = GameObject.FindWithTag("NumberManager").GetComponent<NumbersManager>();
    }

    public void NumberClicked()
    {
        numbersManager.CheckClickedNumber(buttonIndex);
    }


}
