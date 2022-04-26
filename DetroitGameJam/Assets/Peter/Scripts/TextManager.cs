using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextManager : MonoBehaviour
{
    [SerializeField] private Image textImage;
    [SerializeField] private TextMeshProUGUI TMP;

    [SerializeField] private Image nameImage;
    [SerializeField] private TextMeshProUGUI nameTMP;

    // Start is called before the first frame update
    void Start()
    {
        nameImage.enabled = false;
        nameTMP.text = "";
        textImage.enabled = false;
        TMP.text = "";
    }

    public void DisplayText(string text, string name)
    {
        if (name != "")
        {
            nameImage.enabled = true;
            nameTMP.text = name;
        }
        textImage.enabled = true;
        TMP.text = text;
    }

    public void RemoveText()
    {
        nameImage.enabled = false;
        nameTMP.text = "";
        textImage.enabled = false;
        TMP.text = "";
    }
}
