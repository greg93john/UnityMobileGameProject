using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Click : MonoBehaviour {
    PlayerBehaviour player;

    private void Start() {
        player = GetComponent<PlayerBehaviour>();
    }

    public void ClickNoise() {
        Debug.Log("Click!");
        //player.Attack();
    }
}
