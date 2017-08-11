using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempGameObject : MonoBehaviour {
    [Tooltip("Make value '0' or less, if you don't want it to dissapear")]
    public float timeTilGone = 1f;
    public bool isMoving = false;
    [Tooltip("negative moves object in the oposite direction specified.")]
    public float movementSpeed = 1f;
    [Range(-1,1)]public int xAxisMovementDirection = 1, yAxisMovementDirection = 1;
	// Use this for initialization
	void Start () {
        if (timeTilGone > 0) {
            Invoke("Gone", timeTilGone);
        }
	}

    private void Update() {
        if (isMoving) { transform.Translate(new Vector2(xAxisMovementDirection, yAxisMovementDirection) * movementSpeed * Time.deltaTime); }
    }

    private void Gone() {
        Destroy(gameObject);
    }
}
