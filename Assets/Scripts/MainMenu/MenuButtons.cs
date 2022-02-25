using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuButtons : MonoBehaviour
{
    public PlayerNameHolder playerNameHolder;
    public TMPro.TextMeshProUGUI enteredPlayerName;

    public void OnStartButton()
    {
        if (enteredPlayerName.text != "")
        {
            playerNameHolder.playerName = enteredPlayerName.text;
            DontDestroyOnLoad(playerNameHolder);
        }

        SceneManager.LoadScene(1);
    }

    public void OnSettingsButton()
    {
        //Ima do these later... -Conor
    }

    public void OnQuitButton()
    {
        Application.Quit();
    }
}
