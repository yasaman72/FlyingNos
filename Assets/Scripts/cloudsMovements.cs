using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cloudsMovements : MonoBehaviour
{

    public RectTransform[] clouds;
    public float movementSpeed;

    private void Start()
    {
        for (int i = 0; i < clouds.Length; i++)
        {
            clouds[i].localPosition = (new Vector3(clouds[i].rect.width * i,
                    0,
                    0));
        }
    }


    // Update is called once per frame
    void Update()
    {

        for (int i = 0; i < clouds.Length; i++)
        {
            clouds[i].transform.Translate(Vector3.left * movementSpeed * Time.deltaTime);

            //move object to the end of chain
            if (clouds[i].localPosition.x < -clouds[i].rect.width)
            {
                clouds[i].localPosition = (new Vector3(clouds[i].rect.width * (clouds.Length - 1) - 10,
                    0,
                    0));

                //Camera.main.ScreenToViewportPoint(clouds[i].position).x < -Camera.main.ScreenToViewportPoint(Vector3.one * clouds[i].rect.width).x
            }
        }


    }
}
