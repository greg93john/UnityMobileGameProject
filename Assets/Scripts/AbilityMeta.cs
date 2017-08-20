using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityMeta {
    const string abilityOneKey = "ABILITY1", abilityTwoKey = "ABILITY2", abilityThreeKey = "ABILITY3";

    public static void SetAbilityName(int slot, string abilityName) {
        switch (slot) {
            case 0: { PlayerPrefs.SetString(abilityOneKey, abilityName); }
                break;
            case 1: { PlayerPrefs.SetString(abilityTwoKey, abilityName); }
                break;
            case 2: { PlayerPrefs.SetString(abilityThreeKey, abilityName); }
                break;
        }
    } public static string GetAbilityName(int slot) {
        switch (slot) {
            case 0: { return PlayerPrefs.GetString(abilityOneKey); }
            case 1: { return PlayerPrefs.GetString(abilityTwoKey); }
            case 2: { return PlayerPrefs.GetString(abilityThreeKey); }
            default: { Debug.LogWarning("Warning, ability slot index out of range: " + slot); return ""; }
        }
    }

    public static void SetAbilityPotency(int slot, int potency) {
        switch (slot) {
            case 0: { PlayerPrefs.SetInt(abilityOneKey,potency); }
                break;
            case 1: { PlayerPrefs.SetInt(abilityTwoKey, potency); }
                break;
            case 2: { PlayerPrefs.SetInt(abilityThreeKey, potency); }
                break;
        }
    } public static int GetAbilityPotency(int slot) {
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
    } public static float GetAbilityCoolDown(int slot) {
        switch (slot) {
            case 0: { return PlayerPrefs.GetFloat(abilityOneKey); }
            case 1: { return PlayerPrefs.GetFloat(abilityTwoKey); }
            case 2: { return PlayerPrefs.GetFloat(abilityThreeKey); }
            default: { Debug.LogWarning("Warning, ability slot index out of range: " + slot); return 0f; }
        }
    }
} 
