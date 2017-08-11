using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType { healing, buff, damage }

public class ItemBehaviour : MonoBehaviour {

    public ItemType itemType;
    public int potency = 1, maxInReserve = 3;

    private int currentReserve;
    private PlayerBehaviour player;
    private EnemyBehaviour enemy;

    public void UseItem() {
        if (currentReserve > 0) {
            switch (itemType) {
                case ItemType.healing: {
                        player = GameObject.FindObjectOfType<PlayerBehaviour>();
                        if (player) {
                            HealthBehavior playerHealth =  player.GetComponent<HealthBehavior>();
                            playerHealth.IncreaseHealth(potency);
                            DecreaseCurrentReserveBy(1);
                        }
                    }
                    break;

                case ItemType.buff: {
                        player = GameObject.FindObjectOfType<PlayerBehaviour>();
                        if (player) {
                            OffenseBehavior playerAtk = player.GetComponent<OffenseBehavior>();
                            playerAtk.SetAttackPower(playerAtk.GetAttackPower() + potency);
                            DecreaseCurrentReserveBy(1);
                        }
                    }
                    break;

                case ItemType.damage: {
                        enemy = GameObject.FindObjectOfType<EnemyBehaviour>();
                        HealthBehavior enemyHealth = enemy.GetComponent<HealthBehavior>();
                        enemyHealth.ReduceHealth(potency);
                        DecreaseCurrentReserveBy(1);
                    }
                    break;

                default: { Debug.Log("Item Type for this item was not specified."); }
                    break;
            }
        } else { Debug.Log("No more left in reserve."); }
    }

    public int GetCurrentReserveAmount() {
        return currentReserve;
    }

    public int GetMaxReserveAmount() {
        return maxInReserve;
    }

    public void SetMaxReserve(int maxSet) {
        maxInReserve = maxSet;
    }

    public void IncreaseCurrentReserveBy(int amount) {
        currentReserve += amount;
        currentReserve = (int)Mathf.Clamp(currentReserve, 0, maxInReserve);
    } private void DecreaseCurrentReserveBy(int amount) {
        currentReserve -= amount;
        currentReserve = (int)Mathf.Clamp(currentReserve, 0, maxInReserve);
        Debug.Log(currentReserve + " left in reserve for this item.");
    }
}
