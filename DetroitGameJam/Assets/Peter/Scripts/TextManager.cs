using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextManager : MonoBehaviour
{
    [SerializeField] private Image textImage;
    [SerializeField] private TextMeshProUGUI TMP;

    // Start is called before the first frame update
    void Start()
    {
        textImage.enabled = false;
        TMP.text = "";
    }

    public void DisplayText(string text)
    {
        textImage.enabled = true;
        TMP.text = text;
    }

    public void RemoveText()
    {
        textImage.enabled = false;
        TMP.text = "";
    }
}
