using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetUpPlayer : MonoBehaviour {

    // Use this for initialization
    private void Awake() {
        SetUpPlayerStats();
        SetUpItems();
        SetUpAbilities();
    }

    void SetUpPlayerStats() {
        HealthBehavior playerHealth = GameObject.FindObjectOfType<PlayerBehaviour>().GetComponent<HealthBehavior>();
        playerHealth.SetMaxHealth(10);
        playerHealth.SetDefenseValue(0);
        playerHealth.GetComponent<OffenseBehavior>().SetAttackPower(1);
    }

    void SetUpItems() {
        ItemBehaviour[] itemSlots = GameObject.FindObjectsOfType<ItemBehaviour>();
        for(int i = 0; i< itemSlots.Length; i++) {
            switch (PlayerStatMeta.GetItemName(i)) {
                case "DAMAGE": { itemSlots[i].SetItemType(ItemType.damage); itemSlots[i].SetMaxReserve(3); itemSlots[i].SetPotency(5); }
                    break;
                case "HEAL": { itemSlots[i].SetItemType(ItemType.heal); itemSlots[i].SetMaxReserve(3); itemSlots[i].SetPotency(3); }
                    break;
                case "BUFF": { itemSlots[i].SetItemType(ItemType.buff); itemSlots[i].SetMaxReserve(3); itemSlots[i].SetPotency(1); }
                    break;
            }
        }
    }
	
    void SetUpAbilities() {
        AbilityBehaviour[] abiliySlots = new AbilityBehaviour[3];
        abiliySlots[0] = GameObject.Find("Ability 0").GetComponent<AbilityBehaviour>();
        abiliySlots[1] = GameObject.Find("Ability 1").GetComponent<AbilityBehaviour>();
        abiliySlots[2] = GameObject.Find("Ability 2").GetComponent<AbilityBehaviour>();

        string abilityName;

        for(int a = 0; a < abiliySlots.Length; a++) {
            abilityName = PlayerStatMeta.GetAbilityName(a);
            Debug.Log("Setting an ability to be: " + abilityName);

            switch (abilityName.ToUpper()) {
                case "DEFEND": { abiliySlots[a].SetAbilityType(AbilityType.defend); abiliySlots[a].SetCoolDownTime(PlayerStatMeta.GetAbilityCoolDown(a)); abiliySlots[a].SetPotency(1); }
                    break;
                case "DODGE": { abiliySlots[a].SetAbilityType(AbilityType.dodge); abiliySlots[a].SetCoolDownTime(PlayerStatMeta.GetAbilityCoolDown(a)); abiliySlots[a].SetPotency(1); }
                    break;
                case "STUN": { abiliySlots[a].SetAbilityType(AbilityType.stun); abiliySlots[a].SetCoolDownTime(PlayerStatMeta.GetAbilityCoolDown(a)); abiliySlots[a].SetPotency(1); }
                    break;
            }
        }
    }
    
}
