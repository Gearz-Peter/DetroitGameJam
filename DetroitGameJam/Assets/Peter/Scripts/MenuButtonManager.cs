using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtonManager : MonoBehaviour
{
    void StartGame()
    {
        SceneManager.LoadScene("PeterScene", LoadSceneMode.Additive);
    }

    void ExitGame()
    {

    }
}
