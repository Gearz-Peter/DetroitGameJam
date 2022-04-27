using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{

    [SerializeField] GameObject PausemenuObj;
    [SerializeField] Slider volume;
    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            PausemenuObj.SetActive(!PausemenuObj.activeSelf);
        }
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void ChangeVolume()
    {
        AudioListener.volume = volume.value;
    }
}
