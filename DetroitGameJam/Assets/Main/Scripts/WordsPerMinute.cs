using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WordsPerMinute : MonoBehaviour
{
    int Entries=0;

    int Index=0;
    [SerializeField] int[] WPMList;

    float GrossWPM;

    float CurrentTime;

    [SerializeField] Text WPMTextUI;


    private void Update()
    {
        if(Input.anyKeyDown )
        {
            Entries++;       
        }

        CurrentTime += Time.deltaTime / 60;
        GrossWPM = (Entries / 5) / CurrentTime;
        int textword = (int)GrossWPM;
        WPMTextUI.text = "WPM " + textword.ToString();
    }


    public void WordPassed()
    {
        WPMList[Index] = (int)GrossWPM;
        Index++;
    }
    public void WordFail(int amoutChar)
    {
        Entries -= amoutChar;
    }

    private void OnEnable()
    {
        CurrentTime = 0;
         Entries = 0;
    }

    private void OnDisable()
    {
        WPMList[Index] = (int)GrossWPM;
        Index++;
    }

}
