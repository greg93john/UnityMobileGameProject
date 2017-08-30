using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DivisionsMenu : MonoBehaviour {

    public Text lightText, midText, heavyText;
    public Color lightTextOriginalColor, midTextOriginalColor, heavyTextOriginalColor;

    private void OnEnable() {
        SetAllTextToOriginalColor();
        HighlightChosenDivision();
    }

    private void SetAllTextToOriginalColor() {
        lightText.color = lightTextOriginalColor;
        midText.color = midTextOriginalColor;
        heavyText.color = heavyTextOriginalColor;
    }

    private void HighlightChosenDivision() {
        string activeDivision = GameObject.FindObjectOfType<LevelSceneInfo>().GetDivisionChoice();

        switch (activeDivision) {
            case "light": { lightText.color = Color.white; }
                break;
            case "mid": { midText.color = Color.white; }
                break;
            case "heavy": { heavyText.color = Color.white; }
                break;
            default: { Debug.Log("No division is currently selected"); }
                break;
        }
    }
}
