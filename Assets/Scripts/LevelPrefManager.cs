using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPrefManager : MonoBehaviour {
    const string enemyReserveKey = "ENEMY_RESERVE", playerHealthKey = "PLAYER_HEALTH";

    public static void SetEnemyReserveAmountForLevel(int amount) {
        PlayerPrefs.SetInt(enemyReserveKey, amount);
    }
}
