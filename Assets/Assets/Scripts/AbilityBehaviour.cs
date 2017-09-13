using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum AbilityType { none, defend, dodge, stun }

public class AbilityBehaviour : MonoBehaviour {

    public AbilityType abilityType;
    public int potency = 1;
    [Tooltip("Set value to '0' if the ability does not cool down")]
    public float coolDownTime = 5f;
    public Text abilityLabelText;
    public Color originalLabelTextColor;

    private bool abilityActive = false, isCoolingDown = false;
    private PlayerBehaviour player;
    private EnemyBehaviour enemy;

    private void Start() {
        player = GameObject.FindObjectOfType<PlayerBehaviour>();
        UpdateLabels();
        if(abilityType == AbilityType.defend) { SetCoolDownTime(0); }
    } public void UpdateLabels() {
        switch (abilityType) {
            case AbilityType.defend: { abilityLabelText.text = "Defend"; }
                break;
            case AbilityType.dodge: { abilityLabelText.text = "Dodge"; }
                break;
            case AbilityType.stun: { abilityLabelText.text = "Stun"; }
                break;
            default: { abilityLabelText.text = "N/A"; }
                break;
        }
    }

    public void UseAbility() {
        if (!abilityActive && GameObject.FindObjectOfType<ProgressTracker>().GameStillInSession() && !player.GetAbilityUseStatus()) {
            switch (abilityType) {

                case AbilityType.defend: {
                        abilityActive = true;
                        HealthBehavior playerHealth = player.GetComponent<HealthBehavior>();
                        playerHealth.SetDividerValue(potency+1);
                        playerHealth.EnableBlockDamage(true);
                        player.AbilityInUse(true);
                        player.SetMyAnimator("StartDefense");

                        DisableAbilityLabels();
                }
                    break;

                case AbilityType.dodge: {
                        abilityActive = true;
                        HealthBehavior playerHealth = player.GetComponent<HealthBehavior>();
                        playerHealth.SetAbsorbAttack(true,potency);
                        player.AbilityInUse(true);
                        player.SetMyAnimator("Dodge");
                        DisableAbilityLabels();
                }
                    break;

                case AbilityType.stun: {
                        abilityActive = true;
                        OffenseBehavior playerOffense = player.GetComponent<OffenseBehavior>();
                        playerOffense.AbilityAttack(abilityType,potency);
                        player.SetMyAnimator("stunAttack");
                        DisableAbilityLabels();
                        coolDownTime = Mathf.Clamp(coolDownTime + 1f, 1f, 10f);
                }
                    break;

                default: { Debug.Log("Ability was not specified. cannot be used."); }
                    break;
            }

            if (coolDownTime > 0) {
                StartCoroutine(CoolDownAbility(coolDownTime));
            }
        }
    } public void ManualAbilityCancel() {
        if (abilityActive && !isCoolingDown) {
            switch (abilityType) {
                case AbilityType.defend: {
                        abilityActive = false;
                        HealthBehavior playerHealth = player.GetComponent<HealthBehavior>();
                        playerHealth.EnableBlockDamage(false);
                        player.AbilityInUse(false);
                        player.SetMyAnimator("EndDefense");
                        EnableAbilityLabels();
                    }
                    break;

                case AbilityType.stun: {
                        abilityActive = false;
                        EnableAbilityLabels();
                    }
                    break;
                case AbilityType.dodge: {

                    }
                    break;
            }

        }
    } public void SpecialAbilityCancel() {
        if (abilityActive) {
            switch (abilityType) {
                case AbilityType.dodge: {
                        if (!isCoolingDown) {
                            abilityActive = false;
                            EnableAbilityLabels();
                        }
                        HealthBehavior playerHealth = player.GetComponent<HealthBehavior>();
                        playerHealth.SetAbsorbAttack(false, 0);
                        player.AbilityInUse(false);
                    }
                    break;
            }
        }
    }

    private IEnumerator CoolDownAbility(float waitValue) {
        abilityActive = true;
        isCoolingDown = true;
        DisableAbilityLabels();
        {
            yield return new WaitForSeconds(waitValue);
        }
        isCoolingDown = false;
        switch (abilityType) {
            case AbilityType.defend: { ManualAbilityCancel(); } break;
            case AbilityType.dodge: { SpecialAbilityCancel(); } break;
            case AbilityType.stun: { ManualAbilityCancel(); } break;
        }
        EnableAbilityLabels();
    } private void DisableAbilityLabels() {
        Color disabledColor = Color.gray;
        disabledColor.a = 0.75f;
        abilityLabelText.color = disabledColor;
    } private void EnableAbilityLabels() {
        abilityLabelText.color = originalLabelTextColor;
    }
    public void SetAbilityType(AbilityType setVal) {
        abilityType = setVal;
    }

    public void SetPotency(int setVal) {
        potency = setVal;
    }

    public void SetCoolDownTime(float setVal) {
        coolDownTime = setVal;
    }
}
