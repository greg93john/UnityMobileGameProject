using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour {

    public List<GameObject> Menus;

    private int activeMenu = 0;

	// Use this for initialization
	private void Start () {
        SetInitialActiveMenu();
    } private void SetInitialActiveMenu() {
        for (int i = 0; i < Menus.Count; i++) { DeactivateMenu(i); }
        Menus[0].SetActive(true);
        activeMenu = 0;
    }

    public void ChangeToMenu(int choice) {
        DeactivateMenu(activeMenu);
        Menus[choice].SetActive(true);
        activeMenu = choice;
    } private void DeactivateMenu(int choice) {
        Menus[choice].SetActive(false);
    }
}
