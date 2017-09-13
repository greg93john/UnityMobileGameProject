using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrencyDisplay : MonoBehaviour {
    Text displayText;
	// Use this for initialization
	void Start () {
        displayText = GetComponent<Text>();
        UpdateCurrencyDisplay();
	}

    private void Update() {
        UpdateCurrencyDisplay();
    }

    public void UpdateCurrencyDisplay() {
        displayText.text = "Currency: " + PlayerStatMeta.GetCurrencyReserveAmount();
    }
}
