using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreMenu : MonoBehaviour {

    public GameObject[] subMenus;

    private int activeSubMenu = 0, selectedItemSlot, selectedAbilitySlot;
    private string[] itemName, abilityName;

    private void OnEnable() {
        DisplayInitialSubMenu();
        ResetAbilityAndItemSelections();
    } private void DisplayInitialSubMenu() {
        for(int i = 0; i < subMenus.Length; i++) { DeactivateSubMenu(i); }
        subMenus[0].SetActive(true);
        activeSubMenu = 0;
    } private void ResetAbilityAndItemSelections() {
        selectedItemSlot = -1;
        selectedAbilitySlot = -1;
        itemName = new string[3];
        abilityName = new string[3];
    }

    public void ChangeToSubMenu(int choice) {
        DeactivateSubMenu(activeSubMenu);
        subMenus[choice].SetActive(true);
        activeSubMenu = choice;
        InitializeActiveSubScene();
    } private void DeactivateSubMenu(int pick) {
        subMenus[pick].SetActive(false);
    }
    private void InitializeActiveSubScene() {
        switch (activeSubMenu) {
            case 0: { ResetAbilityAndItemSelections(); }
                break;
            case 1: { DisplayChosenSlot("Selected Item Slot"); }
                break;
            case 2: { DisplayChosenSlot("Selected Ability Slot"); }
                break;
            default: { Debug.LogWarning("Unknown Scene Active"); }
                break;
        }
    }

    private void DisplayChosenSlot(string slotParentGameObjectName) {
        Transform selectedParentTransform = GameObject.Find(slotParentGameObjectName).transform;
        GameObject[] selectionImages = new GameObject[selectedParentTransform.childCount];
        for(int b = 0; b < selectionImages.Length; b++) {
            selectionImages[b] = selectedParentTransform.GetChild(b).gameObject;
            selectionImages[b].SetActive(false);
        }
        if (slotParentGameObjectName.Equals("Selected Item Slot")) { selectionImages[selectedItemSlot].SetActive(true); }
        else if(slotParentGameObjectName.Equals("Selected Ability Slot")) { selectionImages[selectedAbilitySlot].SetActive(true); }
    }


    public void SelectItemSlot(int ItenNo) {
        selectedItemSlot = ItenNo;
    }

    public void SelectAbilitySlot(int abilityNo) {
        selectedAbilitySlot = abilityNo;
    }

    public void SetInItemSlot(string item) {
        itemName[selectedItemSlot] = item;
    }

    public void SetAbilitySlot(string ability) {
        abilityName[selectedAbilitySlot] = ability;
    }

    public void ConfirmItem() {
        ConfirmItemName();
    } private void ConfirmItemName() {
        PlayerStatMeta.SetItemName(selectedItemSlot, itemName[selectedItemSlot]);
    }

    public void ConfirmAbility() {
        ConfirmAbilityName();
    } private void ConfirmAbilityName() {
        PlayerStatMeta.SetAbilityName(selectedAbilitySlot, abilityName[selectedAbilitySlot]);
    }
}
