using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ItemType { healing, buff, damage }

public class ItemBehaviour : MonoBehaviour {

    public ItemType itemType;
    public int potency = 1, maxInReserve = 3;
    public float timeLengthOfEffect = 0f;
    public Text counterText, labelText;
    public Color originalCounterTextColor, originalLabelTextColor;
    public GameObject effectObject;

    private bool effectActive = false;
    private int currentReserve;
    private PlayerBehaviour player;
    private EnemyBehaviour enemy;

    private void Start() {
        UpdateLabels();
    }

    public void UseItem() {
        if (currentReserve > 0 && !effectActive && GameObject.FindObjectOfType<ProgressTracker>().GameStillInSession()) {
            player = GameObject.FindObjectOfType<PlayerBehaviour>();
            HealthBehavior playerHealth = player.GetComponent<HealthBehavior>();
            switch (itemType) {
                case ItemType.healing: {
                        if (player && playerHealth.GetCurrentHealth() > 0) {
                            playerHealth.IncreaseHealth(potency);
                            DecreaseCurrentReserveBy(1);
                            DisplayEffect(player.transform);
                        }
                    }
                    break;

                case ItemType.buff: {
                        if (player && playerHealth.GetCurrentHealth() > 0) {
                            /*int isDeadHash = Animator.StringToHash("Dead");
                            AnimatorStateInfo info = player.GetPlayerAnimator().GetCurrentAnimatorStateInfo(0);
                            Debug.Log("Player's current int state is: " + info.fullPathHash);
                            Debug.Log("Player's dead animation state is: " + isDeadHash);*/
                            OffenseBehavior playerAtk = player.GetComponent<OffenseBehavior>();
                            playerAtk.BuffAttackPowerBy(potency, timeLengthOfEffect);
                            effectActive = true;
                            Invoke("EndOfEffect", timeLengthOfEffect+0.5f);
                            DecreaseCurrentReserveBy(1);
                        }
                    }
                    break;

                case ItemType.damage: {
                        enemy = GameObject.FindObjectOfType<EnemyBehaviour>();
                        HealthBehavior enemyHealth = enemy.GetComponent<HealthBehavior>();
                        if (enemy && playerHealth.GetCurrentHealth() > 0 && enemyHealth.GetCurrentHealth() > 0) {
                            enemyHealth.ReduceHealth(potency);
                            StartCoroutine(DisplayAndTrackEffect(enemy.transform));
                            DecreaseCurrentReserveBy(1);
                        }
                    }
                    break;

                default: { Debug.Log("Item Type for this item was not specified."); }
                    break;
            }
        } else { Debug.Log("Cannot use this Item anymore"); }
    } private void EndOfEffect() {
        effectActive = false;
        UpdateUIDisplay();
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
        if (counterText) {
            UpdateUIDisplay();
        }
    } private void DecreaseCurrentReserveBy(int amount) {
        currentReserve -= amount;
        currentReserve = (int)Mathf.Clamp(currentReserve, 0, maxInReserve);
        if (counterText) {
            UpdateUIDisplay();
        }
        Debug.Log(currentReserve + " left in reserve for this item.");
    }

    public void SetItemType(ItemType typeOfItem) {
        itemType = typeOfItem;
    }

    public void SetEffectObject(GameObject chosenObject) {
        effectObject = chosenObject;
    }

    public void SetPotency(int setVal) {
        potency = setVal;
    } public int GetPotency() {
        return potency;
    }

    private void UpdateUIDisplay() {
        if (effectActive) {
            Color inactiveColor = Color.gray;
            inactiveColor.a = 0.75f;
            counterText.color = inactiveColor;
            labelText.color = inactiveColor;
        } else {
            counterText.color = originalCounterTextColor;
            labelText.color = originalLabelTextColor;
        }
        UpdateReserveText();
    } private void UpdateReserveText() {
        counterText.text = currentReserve.ToString();
    }

    private void DisplayEffect(Transform targetPosition) {
        GameObject effObj = Instantiate(effectObject, transform.position, Quaternion.identity);
        effObj.transform.parent = targetPosition;
        effObj.transform.position = targetPosition.position;
    } private IEnumerator DisplayAndTrackEffect(Transform targetObject) {
        GameObject effObj = Instantiate(effectObject, targetObject.position, Quaternion.identity);
        effObj.transform.parent = targetObject;
        effObj.transform.position = targetObject.position;
        effectActive = true;
        while (effObj) {
            yield return new WaitForSeconds(0.01f);
        }
        EndOfEffect();
    }

    public void UpdateLabels() {
        switch (itemType) {
            case ItemType.buff: { labelText.text = "Buff"; }
                break;
            case ItemType.damage: { labelText.text = "Dmg"; }
                break;
            case ItemType.healing: { labelText.text = "Heal"; }
                break;
            default: { labelText.text = "?"; }
                break;
        }
    }
}
