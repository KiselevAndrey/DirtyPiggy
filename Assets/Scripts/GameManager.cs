using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Singleton { get; private set; }

    [Header("Pig")]
    [SerializeField] private GameObject pigPrefab;
    [SerializeField] private Cell pigSpawnCell;
    [SerializeField] private System.Collections.Generic.List<Cell> pigStartedList;

    [Header("Enemy")]
    [SerializeField] private GameObject dogPrefab;
    [SerializeField] private GameObject fermerPrebab;
    [SerializeField] private Cell enemyCellSpawn;
    [SerializeField] private System.Collections.Generic.List<Cell> enemyStartedList;

    private void Awake()
    {
        if (Singleton == null) Singleton = this;
    }

    private void Start()
    {
        enemyCellSpawn.SpawningMovingUnit(dogPrefab, enemyStartedList);
        enemyCellSpawn.SpawningMovingUnit(fermerPrebab, enemyStartedList);
    }

    public void StartGame()
    {
        pigSpawnCell.SpawningMovingUnit(pigPrefab, pigStartedList);
    }

    public void EndGame() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
}
