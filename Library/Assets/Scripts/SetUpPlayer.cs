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
                case "DAMAGE": { itemSlots[i].SetItemType(ItemType.damage); }
                    break;
                case "HEAL": { itemSlots[i].SetItemType(ItemType.heal); }
                    break;
                case "BUFF": { itemSlots[i].SetItemType(ItemType.buff); }
                    break;
            }
            itemSlots[i].SetPotency(1);
            itemSlots[i].SetMaxReserve(3);
        }
    }
	
    void SetUpAbilities() {
        AbilityBehaviour[] abiliySlots = GameObject.FindObjectsOfType<AbilityBehaviour>();
        for(int a = 0; a < abiliySlots.Length; a++) {
            switch (PlayerStatMeta.GetAbilityName(a)) {
                case "DEFEND": { abiliySlots[a].SetAbilityType(AbilityType.defend); abiliySlots[a].SetCoolDownTime(0); }
                    break;
                case "DODGE": { abiliySlots[a].SetAbilityType(AbilityType.dodge); abiliySlots[a].SetCoolDownTime(0); }
                    break;
                case "STUN": { abiliySlots[a].SetAbilityType(AbilityType.stun); abiliySlots[a].SetCoolDownTime(6f); }
                    break;
            }
            abiliySlots[a].SetPotency(1);
        }
    }
    
}
