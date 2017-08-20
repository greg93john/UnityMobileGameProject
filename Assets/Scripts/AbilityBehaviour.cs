using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum AbilityType { none, defense, dodge, stun }

public class AbilityBehaviour : MonoBehaviour {

    public AbilityType abilityType;
    public int potency = 1;
    [Tooltip("Set value to '0' if the ability does not cool down")]
    public float coolDownTime = 5f;
    public GameObject abilityEffectPrefab;
    public Text abilityLabelText;
    public Color originalLabelTextColor;

    private bool abilityActive = false, isCoolingDown = false;
    private PlayerBehaviour player;
    private EnemyBehaviour enemy;
    private GameObject activeAbilityEffect;

    private void Start() {
        player = GameObject.FindObjectOfType<PlayerBehaviour>();
        UpdateLabels();
    } public void UpdateLabels() {
        switch (abilityType) {
            case AbilityType.defense: { abilityLabelText.text = "Defend"; }
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

                case AbilityType.defense: {
                        abilityActive = true;
                        HealthBehavior playerHealth = player.GetComponent<HealthBehavior>();
                        playerHealth.SetDividerValue(potency+1);
                        playerHealth.EnableBlockDamage(true);
                        player.AbilityInUse(true);
                        player.SetMyAnimator("StartDefense");

                        if (abilityEffectPrefab) {
                            activeAbilityEffect = Instantiate(abilityEffectPrefab, player.transform.position, Quaternion.identity);
                            activeAbilityEffect.transform.parent = player.transform;
                            activeAbilityEffect.transform.position = player.transform.position;
                        }

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
    } public void CancelAbility(string cancelType) {
        if (abilityActive && !isCoolingDown) {
            switch (abilityType) {
                case AbilityType.defense: {
                        abilityActive = false;
                        HealthBehavior playerHealth = player.GetComponent<HealthBehavior>();
                        playerHealth.EnableBlockDamage(false);
                        player.AbilityInUse(false);
                        player.SetMyAnimator("EndDefense");
                        EnableAbilityLabels();
                    }
                    break;
                case AbilityType.dodge: {
                        if (cancelType.Equals("dodge")) {
                            abilityActive = false;
                            HealthBehavior playerHealth = player.GetComponent<HealthBehavior>();
                            playerHealth.SetAbsorbAttack(false,0);
                            player.AbilityInUse(false);
                            EnableAbilityLabels();
                        }
                    }
                    break;
                case AbilityType.stun: {
                        abilityActive = false;
                        OffenseBehavior playerOffense = player.GetComponent<OffenseBehavior>();
                        EnableAbilityLabels();
                    }
                    break;
            }

            if (activeAbilityEffect) { Destroy(activeAbilityEffect); }
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
        CancelAbility("");
        EnableAbilityLabels();
    } private void DisableAbilityLabels() {
        Color disabledColor = Color.gray;
        disabledColor.a = 0.75f;
        abilityLabelText.color = disabledColor;
    } private void EnableAbilityLabels() {
        abilityLabelText.color = originalLabelTextColor;
    }

    public void SetPotency(int setVal) {
        potency = setVal;
    }
}
