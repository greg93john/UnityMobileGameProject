using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatMeta {
    const string playerMaxHealthKey = "PLAYER_MAX_HEALTH", playerDefenseKey = "PLAYER_DEFENSE", playerAttackPowerKey = "PLAYER_ATTACK_POWER";

    public static void SetPlayerMaxHealthStat(int setValue) {
        PlayerPrefs.SetInt(playerMaxHealthKey, setValue);
    } public static int GetPlayerMaxHealthStat() {
        return PlayerPrefs.GetInt(playerMaxHealthKey);
    }

    public static void SetPlayerDefenseStat(int setValue) {
        PlayerPrefs.SetInt(playerDefenseKey, setValue);
    } public static int GetPlayerDefenseStat() {
        return PlayerPrefs.GetInt(playerDefenseKey);
    }

    public static void SetPlayerAttackPowerStat(int setValue) {
        PlayerPrefs.SetInt(playerAttackPowerKey, setValue);
    } public static int GetPlayerAttackPowerStat() {
        return PlayerPrefs.GetInt(playerAttackPowerKey);
    }
}
