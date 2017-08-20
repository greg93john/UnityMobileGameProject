using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMeta {
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
    } public static string GetItemName(int slot) {
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
    } public static int GetItemPotency(int slot) {
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
    } public static float GetItemCoolDown(int slot) {
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
    } public static int GetItemMaxReserve(int slot) {
        switch (slot) {
            case 0: { return PlayerPrefs.GetInt(itemOneKey); }
            case 1: { return PlayerPrefs.GetInt(itemTwoKey); }
            case 2: { return PlayerPrefs.GetInt(itemThreeKey); }
            default: { Debug.LogWarning("Warning, item slot index out of range: " + slot); return 0; }
        }
    }
}
