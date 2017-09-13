using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBehavior : MonoBehaviour {
    public int maxHealth, defense;
    public Text changeDisplayText, healthValueText;
    public Color damageDisplayColor, healingDisplayColor;
    public Transform indicatorPositionTransform;
    public Transform[] damageImagePositions;
    public GameObject damageImage, healImage, defenseEffectPrefab;
    public Slider healthBar;

    private int currentHealth, dividerValue = 1, chosenDamagePosition = -1, absorbAmount = 0;
    private bool blockDamageEnabled = false, isInvulnerable = false, absorbingDamage = false;
    private GameObject activeDefenseEffect;

    private void Start() {
        if (defenseEffectPrefab) { 
            activeDefenseEffect = Instantiate(defenseEffectPrefab, transform.position, Quaternion.identity);
            activeDefenseEffect.transform.parent = transform;
            activeDefenseEffect.transform.position = transform.position;
            activeDefenseEffect.SetActive(false);
        }
    }

    public void IncreaseHealth(int healing) {
        if (healing >= 0) {
            currentHealth += healing;
            currentHealth = (int)Mathf.Clamp(currentHealth, 0, maxHealth);
            ShowChange(healing, healingDisplayColor);
        } else { ReduceHealth(-healing); }
    } 

    public void ReduceHealth(int damage) {
        if (!absorbingDamage) {
            if (blockDamageEnabled) { damage = damage / dividerValue; }
            if (damage >= 0) {
                damage = ReduceDamage(damage);
                if (currentHealth > 0 && !isInvulnerable) {
                    currentHealth -= damage;
                    currentHealth = (int)Mathf.Clamp(currentHealth, 0, maxHealth);
                    ShowChange(damage, damageDisplayColor);
                    Debug.Log("current health of " + gameObject + ": " + currentHealth);
                }
            } else { IncreaseHealth(-damage); }
        } else { IncreaseHealth(absorbAmount); }
    }

    private void HandlePlayerBehavior() {
        PlayerBehaviour player = GetComponent<PlayerBehaviour>();
        if(currentHealth <= 0) { player.PlayerDeath(); }
    } private void HandleEnemyBehavior() {
        EnemyBehaviour enemy = GetComponent<EnemyBehaviour>();
        if(currentHealth <= 0) { enemy.EnemyDeathAnimation(); }
    }
    
    private void ShowChange(int changeVal, Color changeValColor) {
        Text spawning = Instantiate(changeDisplayText, Vector3.zero, Quaternion.identity) as Text;
        spawning.text = changeValColor==damageDisplayColor? "-"+changeVal.ToString() : "+"+changeVal.ToString();
        spawning.color = changeValColor;
        spawning.transform.SetParent(GameObject.FindGameObjectWithTag("GameOverlay").transform, false);
        spawning.transform.position = indicatorPositionTransform.position;

        if(changeValColor == damageDisplayColor && damageImage) { DamageImageDisplay(); } 
        else if (changeVal > 0) { HealAnimationDisplay(); }

        if(healthBar) { UpdateHealthBar(); }

        if (GetComponent<PlayerBehaviour>()) { HandlePlayerBehavior(); } 
        else if (GetComponent<EnemyBehaviour>()) { HandleEnemyBehavior(); }
    } private void DamageImageDisplay() {
        int newChosenPosition;

        if (damageImagePositions.Length > 1) {
            newChosenPosition = Random.Range(0, damageImagePositions.Length);
            if (newChosenPosition == chosenDamagePosition) {
                if (newChosenPosition == damageImagePositions.Length - 1) { newChosenPosition--; } else { newChosenPosition++; }
            }
        } else { newChosenPosition = 0; }

        chosenDamagePosition = newChosenPosition;

        GameObject dmgImg = Instantiate(damageImage, damageImagePositions[chosenDamagePosition].position, Quaternion.identity);
        dmgImg.transform.parent = damageImagePositions[chosenDamagePosition];
        dmgImg.transform.position = damageImagePositions[chosenDamagePosition].position;
    } private void HealAnimationDisplay() {
        GameObject effObj = Instantiate(healImage, transform.position, Quaternion.identity);
        effObj.transform.parent = transform;
        effObj.transform.position = transform.position;

    }

    public void SetMaxHealth(int max) {
        maxHealth = max;
    }

    public int GetCurrentHealth() {
        return currentHealth;
    }

    public void ResetHealth() {
        currentHealth = maxHealth;
    }

    public void UpdateHealthBar() {
        if (healthBar) { healthBar.value = (float)currentHealth / (float)maxHealth; }
        if (healthValueText) { healthValueText.text = currentHealth + "/" + maxHealth; }
    }


    // Defense Functions

    private int ReduceDamage(int incomingDamage) {
        int calculatedDamage = (int)Mathf.Clamp(incomingDamage - defense, 1, incomingDamage);
        return calculatedDamage;
    }

    public void RaiseDefense(int raiseVal) {
        defense += raiseVal;
    }

    public void LowerDefense(int lowerVal) {
        defense -= lowerVal;
    }

    public int GetDefenseValue() {
        return defense;
    }
    public void SetDefenseValue(int setVal) {
        defense = setVal;
    }

    public void EnableBlockDamage(bool enableVal) {
        blockDamageEnabled = enableVal;
        activeDefenseEffect.SetActive(enableVal);
    } public void SetDividerValue(int setVal) {
        dividerValue = setVal;
    }

    public void SetAbsorbAttack(bool setVal, int setAbsorbAmount) {
        absorbingDamage = setVal;
        absorbAmount = setAbsorbAmount;
    }

    public void SetInvulnerability(bool setVal) {
        isInvulnerable = setVal;
    } public bool GetInvulnerabilityStatus() {
        return isInvulnerable;
    }
}
