using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatMeta : MonoBehaviour {

    static protected PlayerStatMeta s_instance;
    static public PlayerStatMeta instance { get { return s_instance; } }

    static private string[] abilityNames;
    static private string saveSceneTriggerName;

    private void Awake() {
        if(s_instance != null) { Destroy(gameObject); return; }
        s_instance = this; abilityNames = new string[] { "DEFEND", "DODGE", "STUN" }; DontDestroyOnLoad(gameObject);
    }

    // Player Stats

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

    // Ability Stats

    const string abilityOneKey = "ABILITY1", abilityTwoKey = "ABILITY2", abilityThreeKey = "ABILITY3";

    public static void SetAbilityName(int slot, string abilityName) {
        switch (slot) {
            case 0: { abilityNames[slot] = abilityName; }
                break;
            case 1: { abilityNames[slot] = abilityName; }
                break;
            case 2: { abilityNames[slot] = abilityName; }
                break;
        }
    } public static string GetAbilityName(int slot) {
        switch (slot) {
            case 0: { return abilityNames[slot]; }
            case 1: { return abilityNames[slot]; }
            case 2: { return abilityNames[slot]; }
            default: { Debug.LogWarning("Warning, ability slot index out of range: " + slot); return ""; }
        }
    }

    public static void SetAbilityPotency(int slot, int potency) {
        switch (slot) {
            case 0: { PlayerPrefs.SetInt(abilityOneKey, potency); }
                break;
            case 1: { PlayerPrefs.SetInt(abilityTwoKey, potency); }
                break;
            case 2: { PlayerPrefs.SetInt(abilityThreeKey, potency); }
                break;
        }
    }
    public static int GetAbilityPotency(int slot) {
        switch (slot) {
            case 0: { return PlayerPrefs.GetInt(abilityOneKey); }
            case 1: { return PlayerPrefs.GetInt(abilityTwoKey); }
            case 2: { return PlayerPrefs.GetInt(abilityThreeKey); }
            default: { Debug.LogWarning("Warning, ability slot index out of range: " + slot); return -1; }
        }
    }

    public static void SetAbilityCoolDown(int slot, float coolDownTime) {
        switch (slot) {
            case 0: { PlayerPrefs.SetFloat(abilityOneKey, coolDownTime); }
                break;
            case 1: { PlayerPrefs.SetFloat(abilityTwoKey, coolDownTime); }
                break;
            case 2: { PlayerPrefs.SetFloat(abilityThreeKey, coolDownTime); }
                break;
        }
    }
    public static float GetAbilityCoolDown(int slot) {
        switch (slot) {
            case 0: { return PlayerPrefs.GetFloat(abilityOneKey); }
            case 1: { return PlayerPrefs.GetFloat(abilityTwoKey); }
            case 2: { return PlayerPrefs.GetFloat(abilityThreeKey); }
            default: { Debug.LogWarning("Warning, ability slot index out of range: " + slot); return 0f; }
        }
    }

    // Items

    const string itemOneKey = "ITEM1", itemTwoKey = "ITEM2", itemThreeKey = "ITEM3";

    public static void SetItemName(int slot, string itemName) {
        switch (slot) {
            case 0: { PlayerPrefs.SetString(itemOneKey, itemName); }
                break;
            case 1: { PlayerPrefs.SetString(itemTwoKey, itemName); }
                break;
            case 2: { PlayerPrefs.SetString(itemThreeKey, itemName); }
                break;
        }
    }
    public static string GetItemName(int slot) {
        switch (slot) {
            case 0: { return PlayerPrefs.GetString(itemOneKey); }
            case 1: { return PlayerPrefs.GetString(itemTwoKey); }
            case 2: { return PlayerPrefs.GetString(itemThreeKey); }
            default: { Debug.LogWarning("Warning, item slot index out of range: " + slot); return ""; }
        }
    }

    public static void SetItemPotency(int slot, int potency) {
        switch (slot) {
            case 0: { PlayerPrefs.SetInt(itemOneKey, potency); }
                break;
            case 1: { PlayerPrefs.SetInt(itemTwoKey, potency); }
                break;
            case 2: { PlayerPrefs.SetInt(itemThreeKey, potency); }
                break;
        }
    }
    public static int GetItemPotency(int slot) {
        switch (slot) {
            case 0: { return PlayerPrefs.GetInt(itemOneKey); }
            case 1: { return PlayerPrefs.GetInt(itemTwoKey); }
            case 2: { return PlayerPrefs.GetInt(itemThreeKey); }
            default: { Debug.LogWarning("Warning, item slot index out of range: " + slot); return -1; }
        }
    }

    public static void SetItemCoolDown(int slot, float coolDownTime) {
        switch (slot) {
            case 0: { PlayerPrefs.SetFloat(itemOneKey, coolDownTime); }
                break;
            case 1: { PlayerPrefs.SetFloat(itemTwoKey, coolDownTime); }
                break;
            case 2: { PlayerPrefs.SetFloat(itemThreeKey, coolDownTime); }
                break;
        }
    }
    public static float GetItemCoolDown(int slot) {
        switch (slot) {
            case 0: { return PlayerPrefs.GetFloat(itemOneKey); }
            case 1: { return PlayerPrefs.GetFloat(itemTwoKey); }
            case 2: { return PlayerPrefs.GetFloat(itemThreeKey); }
            default: { Debug.LogWarning("Warning, item slot index out of range: " + slot); return 0f; }
        }
    }

    public static void SetItemMaxReserve(int slot, int maxReserve) {
        switch (slot) {
            case 0: { PlayerPrefs.SetInt(itemOneKey, maxReserve); }
                break;
            case 1: { PlayerPrefs.SetInt(itemTwoKey, maxReserve); }
                break;
            case 2: { PlayerPrefs.SetInt(itemThreeKey, maxReserve); }
                break;
        }
    }
    public static int GetItemMaxReserve(int slot) {
        switch (slot) {
            case 0: { return PlayerPrefs.GetInt(itemOneKey); }
            case 1: { return PlayerPrefs.GetInt(itemTwoKey); }
            case 2: { return PlayerPrefs.GetInt(itemThreeKey); }
            default: { Debug.LogWarning("Warning, item slot index out of range: " + slot); return 0; }
        }
    }

    // Currency

    const string currencyKey = "CURRENCY";

    public static int GetCurrencyReserveAmount() {
        return PlayerPrefs.GetInt(currencyKey);
    }

    public static void AddToCurrencyReserve(int incomingAmount) {
        Debug.Log("Triggered adding this amount to currency meta " + incomingAmount);
        int totalReserve = incomingAmount + GetCurrencyReserveAmount();
        PlayerPrefs.SetInt(currencyKey, totalReserve);
    }
    public static void ReduceFromCurrencyReserve(int reductionValue) {
        int totalReserve = GetCurrencyReserveAmount() - reductionValue;
        PlayerPrefs.SetInt(currencyKey, totalReserve);
    }

    public static void HardSetCurrencyReserveAmountTo(int total) {
        PlayerPrefs.SetInt(currencyKey, total);
    }

    // Reset All

    public static int defaultPlayerAttack = 1, defaultPlayerDefense = 1, defaultPlayerMaxHealth = 10;

    public void ResetAllMeta() {
        SetPlayerAttackPowerStat(defaultPlayerAttack);
        SetPlayerDefenseStat(defaultPlayerDefense);
        SetPlayerMaxHealthStat(defaultPlayerMaxHealth);
        HardSetCurrencyReserveAmountTo(0);
    }
}
