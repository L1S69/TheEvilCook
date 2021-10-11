using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void PlayButtonClick()
    {
        SceneManager.LoadScene("Game");
    }

    public void SettingsButtonClick()
    {
        SceneManager.LoadScene("Settings");
    }
}
