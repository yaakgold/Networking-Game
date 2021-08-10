using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

/// <summary>
/// Player name input field. Let the user input his name, will appear above the player in the game.
/// </summary>
[RequireComponent(typeof(TMP_InputField))]
public class PlayerNameInputField : MonoBehaviour
{
    #region Private Constants

    //Store the PlayerPref Key to avoid typos
    const string playerNamePrefKey = "PlayerName";

    #endregion

    #region MonoBehavior Callbacks

    private void Start()
    {
        string defaultName = string.Empty;
        TMP_InputField _inputField = GetComponent<TMP_InputField>();
        if(_inputField != null)
        {
            if(PlayerPrefs.HasKey(playerNamePrefKey))
            {
                defaultName = PlayerPrefs.GetString(playerNamePrefKey);
                _inputField.text = defaultName;
            }
        }

        PhotonNetwork.NickName = defaultName;
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Sets the name of the player, and save it in the PlayerPrefs for future sessions.
    /// </summary>
    /// <param name="val">The name of the Player</param>
    public void SetPlayerName(string val)
    {
        //#Important
        if(string.IsNullOrEmpty(val))
        {
            Debug.LogError("Player Name is null or empty");
            return;
        }

        PhotonNetwork.NickName = val;

        PlayerPrefs.SetString(playerNamePrefKey, val);
    }

    #endregion
}
