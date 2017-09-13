using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressTracker : MonoBehaviour
{
    const string enemyReserveKey = "ENEMY_RESERVE";
    public Transform enemyParentTransform;
    public Text resultText, winningsText;
    public GameObject postMatchMenu;

    private int reservedEnemies, totalWinnings = 0, pendingWinnings = 0, multiplier = 1, difficultyScale = 1;
    private bool gameActive = true;
    private GameObject enemy;
    private EnemyPool enemyPool;
    private EnemyBehaviour enemyAi;
    private OffenseBehavior enemyOffenseStats;
    private HealthBehavior enemyHealthStats;

    void Start() {
        totalWinnings = 0;
        pendingWinnings = 0;
        AddToWinnings(0);

        SetDifficultyScaling();

        resultText.gameObject.SetActive(false);
        
        enemyPool = GetComponent<EnemyPool>();
        FindEnemy();
        if (enemy) { InitializeReservedEnemiesCount(enemyPool.enemyPool.Length - 1); } else { InitializeReservedEnemiesCount(enemyPool.enemyPool.Length); }
        ShowPostMatchMenu(false);
    }

    void Update() {
        if (!enemy) {
            if (gameActive) {
                Debug.Log("No enemy, and starting next round.");
                AddToWinnings(pendingWinnings);
                CheckProgress();
            }
        }
    }

    public void CheckProgress() {
        if (reservedEnemies > 0) {
            NextRound();
        } else {
            gameObject.SetActive(true);
            resultText.text = "You Win!";
            LevelComplete();
        }
    }

    private void FindEnemy() {
        Debug.Log("Looking for enemy");
        enemy = GameObject.FindGameObjectWithTag("Enemy");
        if (enemy) {
            pendingWinnings = enemy.GetComponent<EnemyBehaviour>().winningsValue;
        }
    }

    private void NextRound() {
        SpawnNextEnemy();
        PrimeNewEnemy();
        Debug.Log("Next Round! " + reservedEnemies + " more enemies to go!");
    }
    private void SpawnNextEnemy() {
        enemy = Instantiate(enemyPool.GetNextEnemy(), enemyParentTransform.position, Quaternion.identity);
        enemy.gameObject.name = enemy.gameObject.name.Replace("(Clone)", ""); // make a list of names and grab random.range from the list
        enemy.transform.parent = enemyParentTransform;
        enemy.transform.localScale = new Vector3(1, 1, 1);
        reservedEnemies--;
    }
    private void PrimeNewEnemy() {
        enemyOffenseStats = enemy.GetComponent<OffenseBehavior>();
        enemyOffenseStats.SetAttackPower(enemyOffenseStats.GetAttackPower() + multiplier);

        enemyHealthStats = enemy.GetComponent<HealthBehavior>();
        enemyHealthStats.SetMaxHealth(enemyHealthStats.maxHealth * multiplier);
        enemyHealthStats.SetDefenseValue(enemyHealthStats.GetDefenseValue() + multiplier);

        enemyAi = enemy.GetComponent<EnemyBehaviour>();
        //enemyAi.SetAttackRate(enemyAi.attackRateInSeconds);
        enemyAi.winningsValue *= multiplier;
        pendingWinnings = enemy.GetComponent<EnemyBehaviour>().winningsValue;

        multiplier = (enemyPool.enemyIndex+1) * difficultyScale;
    }

    private void LevelComplete() {
        Debug.Log("Game Ending!");
        gameActive = false;
        ShowPostMatchMenu(true);
    }

    public void PlayerLost() {
        resultText.gameObject.SetActive(true);
        resultText.text = "You Lose!";
        LevelComplete();
    }

    public void ShowPostMatchMenu(bool answer) {
        postMatchMenu.SetActive(answer);
        if (answer == true) {
            PlayerStatMeta.AddToCurrencyReserve(totalWinnings);
        }
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

    private void SetDifficultyScaling() {
        LevelSceneInfo levelInfo = GameObject.FindObjectOfType<LevelSceneInfo>();
        switch (levelInfo.GetDivisionChoice().ToLower()) {
            case "light": { difficultyScale = 1; }
                break;
            case "mid": { difficultyScale = 2; }
                break;
            case "heavy": { difficultyScale = 3; }
                break;
        }
    }
}
