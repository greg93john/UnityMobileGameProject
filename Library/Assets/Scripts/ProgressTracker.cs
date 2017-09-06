using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressTracker : MonoBehaviour {
    const string enemyReserveKey = "ENEMY_RESERVE";
    public Transform enemyParentTransform;
    public Text resultText, winningsText;
    public GameObject postMatchMenu;

    private int reservedEnemies, totalWinnings = 0;
    private bool sameRound = false, gameActive = true;
    private Vector3 enemyPosition;
    private GameObject enemy;
    private EnemyPool enemyPool;
    private EnemyBehaviour enemyAi;
    private OffenseBehavior enemyOffenseStats;
    private HealthBehavior enemyHealthStats;

	void Start () {
        totalWinnings = 0;
        AddToWinnings(0);

        resultText.text = "";

        enemyPool = GetComponent<EnemyPool>();
        InitializeReservedEnemiesCount(enemyPool.enemyPool.Length - 1);
        FindEnemy();
        enemyPosition = enemyParentTransform.position;
        ShowPostMatchMenu(false);
	}
	
	void Update () {
        if (!enemy) {
            Debug.Log("No Enemy Found.");
            if (sameRound) {
                Debug.Log("Enemy Eliminated. Starting next round.");
                sameRound = false;
                CheckProgress();
            } else { FindEnemy(); }
        }
	}

    public void CheckProgress() {
        if(reservedEnemies > 0) {
            NextRound();
        } else {
            LevelComplete();
        }
    }

    private void FindEnemy() {
        Debug.Log("Looking for enemy");
        enemy = GameObject.FindGameObjectWithTag("Enemy");
        if (enemy) {
            Debug.Log("Enemy Found. Starting Round.");
            sameRound = true;
        }
    }

    private void NextRound() {
        SpawnNextEnemy();
        PrimeNewEnemy();
        sameRound = true;
        Debug.Log("Next Round! " + reservedEnemies + " more enemies to go!");
    } private void SpawnNextEnemy() {
        enemy = Instantiate(enemyPool.GetNextEnemy(), enemyPosition, Quaternion.identity);
        enemy.gameObject.name = enemy.gameObject.name.Replace("(Clone)", ""); // make a list of names and grab random.range from the list
        enemy.transform.parent = enemyParentTransform;
        enemy.transform.localScale = new Vector3(1, 1, 1);
        reservedEnemies--;
    } private void PrimeNewEnemy() {
        enemyOffenseStats = enemy.GetComponent<OffenseBehavior>();
        enemyOffenseStats.SetAttackPower(enemyOffenseStats.GetAttackPower());

        enemyHealthStats = enemy.GetComponent<HealthBehavior>();
        enemyHealthStats.SetMaxHealth(enemyHealthStats.maxHealth);

        enemyAi = enemy.GetComponent<EnemyBehaviour>();
        enemyAi.SetAttackRate(enemyAi.attackRateInSeconds);
        enemyAi.ResetEnemy();
    }

    private void LevelComplete() {
        Debug.Log("Level Complete!");
        resultText.text = "You Win!";
        gameActive = false;
        ShowPostMatchMenu(true);
    }

    public void PlayerLost() {
        resultText.text = "You Lose!";
        ShowPostMatchMenu(true);
        gameActive = false;
    }

    public void ShowPostMatchMenu(bool answer) {
        PlayerStatMeta.AddToCurrencyReserve(totalWinnings);
        postMatchMenu.SetActive(answer);
    }

    public int GetReservedEnemiesCount() {
        return reservedEnemies;
    }

    public void InitializeReservedEnemiesCount(int amount) {
        reservedEnemies = amount;
    }
    
    public bool GameStillInSession() {
        return gameActive;
    }

    public void AddToWinnings(int addAmount) {
        totalWinnings += addAmount;
        winningsText.text = "Winnings: " + totalWinnings;
    }
}
