using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempGameObject : MonoBehaviour {
    [Tooltip("Make value '0' or less, if you don't want it to dissapear from a timer")]
    public float timeTilGone = 1f;
    public bool isMoving = false;
    [Tooltip("negative moves object in the oposite direction specified.")]
    public float movementSpeed = 1f;
    [Range(-1,1)]public int xAxisMovementDirection = 1, yAxisMovementDirection = 1;
    private Animator buffAnimator;
	// Use this for initialization
	void Start () {
        if (timeTilGone > 0) {
            Invoke("Gone", timeTilGone);
        }
	}

    private void Update() {
        if (isMoving) { transform.Translate(new Vector2(xAxisMovementDirection, yAxisMovementDirection) * movementSpeed * Time.deltaTime); }
    }

    public void EndBuffAnimation() {
        buffAnimator = GetComponent<Animator>();
        buffAnimator.SetTrigger("End");
    }

    private void Gone() {
        Destroy(gameObject);
    }
}
