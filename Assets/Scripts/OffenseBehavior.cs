using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Status { none, soft, weak, poison, blind }
public enum Element { none, fire, water, earth }

public class OffenseBehavior : MonoBehaviour {
    public int attackPower = 1;

    private Element elementType = Element.none;
    private GameObject target = null;

    public void AttackTarget() {
        if (target) {
            // Will expand on damage calculation later
            target.GetComponent<HealthBehavior>().ReduceHealth(attackPower);
        } else { Debug.Log("There is no target to attack"); }
    }

    public void SetAttackTarget(GameObject enemy) {
        target = enemy;
    }

    public Element GetElementType() {
        return elementType;
    } public void SetElementType(Element typeVal) {
        elementType = typeVal;
    }

    public int GetAttackPower() {
        return attackPower;
    } public void SetAttackPower(int power) {
        attackPower = power;
    }
}
