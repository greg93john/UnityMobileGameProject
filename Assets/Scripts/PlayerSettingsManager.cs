using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSettingsManager : MonoBehaviour {
    const string playerHealthKey = "PLAYER_HEALTH";

    public static void SetPlayersMaxHealth(int amount) {
        PlayerPrefs.SetInt(playerHealthKey, amount);
    }

    public static int GetPlayersMaxHealth() {
        return PlayerPrefs.GetInt(playerHealthKey);
    }
}
