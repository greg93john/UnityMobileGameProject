using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreMenu : MonoBehaviour {

    public GameObject[] subMenus;

    private int activeSubMenu = 0, selectedItemSlot, selectedAbilitySlot;
    private string[] itemName, abilityName;
    private int[] itemPotency, itemMaxReserve, abilityPotency;
    private float[] itemCoolDown, abilityCoolDown;

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
        itemPotency = new int[3];
        itemCoolDown = new float[3];
        itemMaxReserve = new int[3];

        abilityName = new string[3];
        abilityPotency = new int[3];
        abilityCoolDown = new float[3];
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
    } public void SetItemSlotPotency(int potency) {
        itemPotency[selectedItemSlot] = potency;
    } public void SetItemSlotCoolDown(float coolDown) {
        itemCoolDown[selectedItemSlot] = coolDown;
    } public void SetItemSlotMaxReserve(int maxReserve) {
        itemMaxReserve[selectedItemSlot] = maxReserve;
    }

    public void SetAbilitySlotName(string typeName) {
        abilityName[selectedAbilitySlot] = typeName;
    } public void SetAbilitySlotPotency(int potency) {
        abilityPotency[selectedAbilitySlot] = potency;
    } public void SetAbilitySlotCoolDown(float coolDown) {
        abilityCoolDown[selectedAbilitySlot] = coolDown;
    }

    public void ConfirmItem() {
        ConfirmItemName();
        ConfirmItemPotency();
        ConfirmItemCoolDown();
        ConfirmItemMaxReserve();
    }private void ConfirmItemName() {
        PlayerStatMeta.SetItemName(selectedItemSlot, itemName[selectedItemSlot]);
    }private void ConfirmItemPotency() {
        PlayerStatMeta.SetItemPotency(selectedItemSlot, itemPotency[selectedItemSlot]);
    }private void ConfirmItemCoolDown() {
        PlayerStatMeta.SetItemCoolDown(selectedItemSlot, itemCoolDown[selectedItemSlot]);
    }private void ConfirmItemMaxReserve() {
        PlayerStatMeta.SetItemMaxReserve(selectedItemSlot, itemMaxReserve[selectedItemSlot]);
    }

    public void ConfirmAbility() {
        ConfirmAbilityName();
        ConfirmAbilityPotency();
        ConfirmAbilityCoolDown();
    } private void ConfirmAbilityName() {
        PlayerStatMeta.SetAbilityName(selectedAbilitySlot, abilityName[selectedAbilitySlot]);
    }private void ConfirmAbilityPotency() {
        PlayerStatMeta.SetAbilityPotency(selectedAbilitySlot, abilityPotency[selectedAbilitySlot]);
    }private void ConfirmAbilityCoolDown() {
        PlayerStatMeta.SetAbilityCoolDown(selectedAbilitySlot, abilityCoolDown[selectedAbilitySlot]);
    }
}
