using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(HealthBehavior))]
[RequireComponent(typeof(OffenseBehavior))]
public class EnemyBehaviour : MonoBehaviour {
    public int winningsValue = 1;
    public float attackRateInSeconds;
    public Animator enemyAnimator;
    public Text nameLabel;

    private GameObject player;
    private HealthBehavior myHealth, playerHealth;
    private OffenseBehavior myOffense;
    private Status currentStatus = Status.none;
    
	void Start () {
        ResetEnemy();
        FindPlayer();
        if (nameLabel) { nameLabel.text = gameObject.name; }
    }

    public int GetWinningsValue() {
        return winningsValue;
    }

    public void ResetEnemy() {
        myHealth = GetComponent<HealthBehavior>();
        myOffense = GetComponent<OffenseBehavior>();
        myHealth.ResetHealth();
        myHealth.UpdateHealthBar();
        Invoke("AttackCyclePrimer", attackRateInSeconds);
    } private void AttackCyclePrimer() {
        StartCoroutine(AttackCycle());
    }

    public void SetAttackDamage(int amount) {
        myOffense.SetAttackPower(amount);
    }

    public void SetAttackRate(float seconds) {
        attackRateInSeconds = seconds;
    }

    public void SetMaxHealth(int amount) {
        myHealth.SetMaxHealth(amount);
    }

    IEnumerator AttackCycle() {
        while (true) {
            if (myHealth.GetCurrentHealth() > 0 && playerHealth.GetCurrentHealth() > 0) {
                Debug.Log("Enemy Attacked for damage!");
                Attack();
                yield return new WaitForSeconds(attackRateInSeconds);
            } else { break; }
        }
        Debug.Log("Enemy Attack Cycle has ended");
    } private void Attack() {
        if (myOffense.CanAttackStatus()) {
            enemyAnimator.SetTrigger("Attack");
        }
        myOffense.AttackTarget(myOffense.GetAttackPower());
    }

    public void SetMyAnimator(string clipName) {
        enemyAnimator.SetTrigger(clipName);
    }

    public void EnemyDeathAnimation() {
        StopAllCoroutines();
        enemyAnimator.SetTrigger("Die");
        Debug.Log("Enemy Dying");
    }

    public void CompleteDeath() {
        Debug.Log("Enemy Dead and Gone.");
        Destroy(gameObject);
    }

    private void FindPlayer() {
        player = GameObject.FindObjectOfType<PlayerBehaviour>().gameObject;
        if (player) {
            playerHealth = player.gameObject.GetComponent<HealthBehavior>();
            myOffense.SetAttackTarget(player);
        } else { Debug.Log("Enemy did not find Player game object in scene."); }
    }
}
