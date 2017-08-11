using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBehavior : MonoBehaviour {
    public int maxHealth;
    public Text changeDisplayText;
    public Color damageDisplayColor, healingDisplayColor;
    public Transform indicatorPositionTransform;
    public Transform[] damageImagePositions;
    public GameObject damageImage, healImage;

    private int currentHealth;
    private int chosenDamagePosition = -1;

    public void IncreaseHealth(int healing) {
        if (healing >= 0) {
            currentHealth += healing;
            currentHealth = (int)Mathf.Clamp(currentHealth, 0, maxHealth);
            ShowChange(healing, healingDisplayColor);
        } else { ReduceHealth(healing); }
    } 

    public void ReduceHealth(int damage) {
        if (damage >= 0) {
            if (currentHealth > 0) {
                currentHealth -= damage;
                currentHealth = (int)Mathf.Clamp(currentHealth, 0, maxHealth);
                ShowChange(damage, damageDisplayColor);
                Debug.Log("current health of " + gameObject + ": " + currentHealth);
            }
        } else { IncreaseHealth(damage); }
    }

    private void HandlePlayerBehavior() {
        PlayerBehaviour player = GetComponent<PlayerBehaviour>();
        player.UpdateHealthBar();
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
        else if(changeValColor == healingDisplayColor && healImage) { HealImageDisplay(); }

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
    } private void HealImageDisplay() {
        Vector3 healingImageOrigin = transform.position;

        GameObject healImg = Instantiate(healImage, transform.position, Quaternion.identity);
        healImg.transform.parent = transform;
        healImg.transform.position = transform.position; ;
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
}
