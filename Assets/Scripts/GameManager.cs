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

    [Header("Levels")]
    [SerializeField] private System.Collections.Generic.List<LVLSO> lvlsList;

    private void Awake()
    {
        if (Singleton == null) Singleton = this;
    }

    private void Start()
    {
        SpawnLVL(0);
    }

    private void SpawnLVL(int index)
    {
        LVLSO lvl = lvlsList[index];

        // spawn gabbage
        Field.singleton.cabbageCount = lvl.GabbageCount;
        Field.singleton.Seeding();

        // spawn dogs
        for (int i = 0; i < lvl.DogCount; i++)
            enemyCellSpawn.SpawningMovingUnit(dogPrefab, enemyStartedList);

        // spawn fermers
        for (int i = 0; i < lvl.FermerCount; i++)
            enemyCellSpawn.SpawningMovingUnit(fermerPrebab, enemyStartedList);

    }

    public void StartGame()
    {
        KAP.Pool.Pool.DespawnAll();
        pigSpawnCell.SpawningMovingUnit(pigPrefab, pigStartedList);
    }

    public void EndGame() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
}
