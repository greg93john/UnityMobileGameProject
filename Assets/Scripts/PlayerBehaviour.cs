using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(HealthBehavior))]
[RequireComponent(typeof(OffenseBehavior))]
public class PlayerBehaviour : MonoBehaviour {
    public ItemBehaviour[] items;
    public Animator playerAnimator;
    public Slider healthBar;

    private bool abilitySpent = false;
    private EnemyBehaviour enemy;
    private HealthBehavior myHealth;
    private OffenseBehavior offense;

    // will expand upon Status ailments later
    private Status currentStatus = Status.none;

    private void Start() {
        myHealth = GetComponent<HealthBehavior>();
        offense = GetComponent<OffenseBehavior>();
        myHealth.ResetHealth();
        UpdateHealthBar();
        if (items[0]) { SetItemUsesToMax(); }
    }

    private void Update() {
        if (!enemy) { FindEnemy(); }
    }

    public void UpdateHealthBar() {
        if (healthBar) {
            healthBar.value = (float)myHealth.GetCurrentHealth()/(float)myHealth.maxHealth;
        } else { Debug.Log("Warning, Healthbar is not assigned in scene view."); }
    }

    public void Attack() {
        if(myHealth.GetCurrentHealth() > 0){
            AttackAnimations();
            offense.AttackTarget();
        }
    } public void AttackAnimations() {
        playerAnimator.SetTrigger("Attack");
        Debug.Log("Player Attack Animation has Triggered!");
    }


    private void FindEnemy() {
        enemy = GameObject.FindObjectOfType<EnemyBehaviour>();
        if (enemy) {
            offense.SetAttackTarget(enemy.gameObject);
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
}
