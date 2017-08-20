using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(HealthBehavior))]
[RequireComponent(typeof(OffenseBehavior))]
public class PlayerBehaviour : MonoBehaviour {
    public ItemBehaviour[] items;
    public Animator playerAnimator;

    private bool usingAbility = false;
    private EnemyBehaviour enemy;
    private HealthBehavior myHealth;
    private OffenseBehavior myOffense;
    private ProgressTracker gameState;

    // will expand upon Status ailments later
    private Status currentStatus = Status.none;

    private void Start() {
        myHealth = GetComponent<HealthBehavior>();
        myOffense = GetComponent<OffenseBehavior>();
        gameState = GameObject.FindObjectOfType<ProgressTracker>();
        myHealth.ResetHealth();
        myHealth.UpdateHealthBar();
        if (items[0]) { SetItemUsesToMax(); }
    }

    private void Update() {
        if (!enemy) { FindEnemy(); }
    }

    public void Attack() {
        if(!usingAbility && gameState.GameStillInSession()){
            AttackAnimations();
            myOffense.AttackTarget(myOffense.GetAttackPower());
        }
    } public void AttackAnimations() {
        playerAnimator.SetTrigger("Attack");
        Debug.Log("Player Attack Animation has Triggered!");
    }


    private void FindEnemy() {
        enemy = GameObject.FindObjectOfType<EnemyBehaviour>();
        if (enemy) {
            myOffense.SetAttackTarget(enemy.gameObject);
        }
    }

    public void PlayerDeath() {
        playerAnimator.SetTrigger("Die");
        GameObject.FindObjectOfType<ProgressTracker>().PlayerLost();
    }

    private void SetItemUsesToMax() {
        for (int i = 0; i < items.Length; i++) {
            items[i].IncreaseCurrentReserveBy(items[i].GetMaxReserveAmount());
        }
    }

    public Animator GetPlayerAnimator() {
        return playerAnimator;
    }

    public void SetMyAnimator(string clipName) {
        playerAnimator.SetTrigger(clipName);
    }

    public void AbilityInUse(bool setVal) {
        usingAbility = setVal;
    } public bool GetAbilityUseStatus() {
        return usingAbility;
    }

    public void EndDodgeStatus() {
        AbilityBehaviour[] abilities = GameObject.FindObjectsOfType<AbilityBehaviour>();
        for(int i = 0; i < abilities.Length; i++) {
            if(abilities[i].abilityType == AbilityType.dodge) {
                abilities[i].CancelAbility("dodge");
                break;
            }
        }
    }
}
