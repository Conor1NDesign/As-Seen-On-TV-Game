using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNameHolder : MonoBehaviour
{
    public string playerName = "Speeve";

    public void UpdatePlayerName(string newName)
    {
        playerName = newName;
        Debug.Log(playerName);
    }
}
