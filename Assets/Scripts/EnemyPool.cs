using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour {
    public GameObject[] enemyTypes, enemyPool;

    private int enemyIndex = 0;

    private void Start() {
        //enemyPool = new GameObject[GameObject.FindObjectOfType<ProgressTracker>().GetReservedEnemiesCount()];
        InitializeEnemyPool();
    }

    private void InitializeEnemyPool() {
        for(int i = 0; i < enemyPool.Length; i++) {
            int choice = (int)Random.Range(0, enemyTypes.Length - 1);
            enemyPool[i] = enemyTypes[choice];
        }
    }

    public GameObject GetNextEnemy() {
        int index = enemyIndex;
        enemyIndex++;
        enemyIndex = (int)Mathf.Clamp(enemyIndex, 0, enemyPool.Length - 1);
        return enemyPool[index];
    }
}
