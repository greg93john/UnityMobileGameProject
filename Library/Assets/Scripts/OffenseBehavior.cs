using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Status { none, soft, weak, poison, blind }
public enum Element { none, fire, water, earth }

public class OffenseBehavior : MonoBehaviour
{
    public int attackPower = 1;
    public GameObject buffObject;

    private Element elementType = Element.none;
    private GameObject target = null;
    private GameObject activeBuffObject;
    private bool canAttack = true;

    public void AttackTarget(int atkAmount) {
        if (target && canAttack) {
            target.GetComponent<HealthBehavior>().ReduceHealth(atkAmount);
        } else { Debug.Log("There is no target to attack"); }
    }

    public void SetAttackTarget(GameObject enemy) {
        target = enemy;
    }

    public Element GetElementType() {
        return elementType;
    }
    public void SetElementType(Element typeVal) {
        elementType = typeVal;
    }

    public void BuffAttackPowerBy(int boost, float timeLength) {
        StartCoroutine(BuffTimeFrame(boost, timeLength));
        ShowBuffAnimation();
    }
    IEnumerator BuffTimeFrame(int atkBoost, float waitTime) {
        attackPower += atkBoost;
        yield return new WaitForSeconds(waitTime);
        attackPower -= atkBoost;
        EndBoost();
    }
    private void EndBoost() {
        activeBuffObject.GetComponent<Animator>().SetTrigger("End");
        activeBuffObject = null;
    }
    private void ShowBuffAnimation() {
        activeBuffObject = Instantiate(buffObject, transform.position, Quaternion.identity);
        activeBuffObject.transform.parent = transform;
        activeBuffObject.transform.position = transform.position;
    }

    public int GetAttackPower() {
        return attackPower;
    }
    public void SetAttackPower(int power) {
        attackPower = power;
    }

    public void AbilityAttack(AbilityType abilityMove, int dmgStrength) {
        switch (abilityMove) {
            case AbilityType.stun: {
                    HealthBehavior theirHealth = target.GetComponent<HealthBehavior>();
                    if (theirHealth)
                        target.GetComponent<OffenseBehavior>().StunThisOffense(target.GetComponent<EnemyBehaviour>().attackRateInSeconds * 1.1f);
                    int multiplier = (int)Mathf.Clamp(dmgStrength / 3, 0, 1);
                    AttackTarget(attackPower * multiplier);
                }
                break;
        }
    }
    public void StunThisOffense(float stunTime) {
        HealthBehavior myHealth = GetComponent<HealthBehavior>();
        if(!myHealth.GetInvulnerabilityStatus()){
            StartCoroutine(IAmStunned(stunTime));
        }
    } IEnumerator IAmStunned(float waitTime) {
        canAttack = false;
        EnemyBehaviour enemy = GetComponent<EnemyBehaviour>();
        if (enemy) { enemy.SetMyAnimator("stunned"); } 
        else {
            PlayerBehaviour player = GetComponent<PlayerBehaviour>();
            if (player) {
                player.SetMyAnimator("stunned");
            } else { Debug.LogWarning("Stunning an object that is unknown."); }
        }

        yield return new WaitForSeconds(waitTime);

        canAttack = true;
        if (enemy) { enemy.SetMyAnimator("stunEnd"); } 
        else {
            PlayerBehaviour player = GetComponent<PlayerBehaviour>();
            if (player) {
                player.SetMyAnimator("stunEnd");
            } else { Debug.LogWarning("Ending an object that is unknown."); }
        }
    }

    public bool CanAttackStatus() {
        return canAttack;
    }
}