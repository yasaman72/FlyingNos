using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BalloonTimerCtrler : MonoBehaviour
{
    public GameObject endGameMenu;

    [Space, Header("Slider")]
    public  Scrollbar timerSlider;
    public float sliderValueReduction;
    public float reductionWaitingTime;
    [Header("Slide Handle Section")]
    public Image sliderHandle;
    public Color defaultColor, dangerColor;

    public void StartTimer()
    {
        timerSlider.size = 1;
        StartCoroutine("TimerReduction");
    }

    IEnumerator TimerReduction()
    {
        while (timerSlider.size > 0)
        {
            timerSlider.size -= sliderValueReduction;

            //showing danger sign
            if (timerSlider.size < 0.45f)
            {
                sliderHandle.color = dangerColor;
                timerSlider.GetComponent<Animator>().SetBool("DangerSign", true);
            }
            else
            {
                sliderHandle.color = defaultColor;
                timerSlider.GetComponent<Animator>().SetBool("DangerSign", false);
            }

            if (timerSlider.size <= 0)
            {
                sliderHandle.color = defaultColor;
                timerSlider.GetComponent<Animator>().SetBool("DangerSign", false);

                endGameMenu.SetActive(true);

                StopCoroutine("TimerReduction");
            }
            yield return new WaitForSeconds(reductionWaitingTime);
        }
    }

}
