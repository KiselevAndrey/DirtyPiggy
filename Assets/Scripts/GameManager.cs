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
    [SerializeField] private System.Collections.Generic.List<UnityEngine.UI.Button> lvlsBtnsList;

    [Header("Menu")]
    [SerializeField] private GameObject menuUI;
    [SerializeField] private GameObject startBtnsMenu;
    [SerializeField] private GameObject selectLVLsMenu;
    [SerializeField] private GameObject gameUI;
    [SerializeField] private GameObject endGameUI;
    [SerializeField] private UnityEngine.UI.Text endGameText;

    private int _currentLVL;

    private void Awake()
    {
        if (Singleton == null) Singleton = this;
    }

    private void Start()
    {
        ActivateMenu();
    }

    private void SpawnLVL(int index)
    {
        LVLSO lvl = lvlsList[index];

        // spawn cabbage
        Field.Singleton.CabbageCount = lvl.CabbageCount;
        Field.Singleton.Seeding();

        // spawn dogs
        for (int i = 0; i < lvl.DogCount; i++)
            enemyCellSpawn.SpawningMovingUnit(dogPrefab, enemyStartedList);

        // spawn fermers
        for (int i = 0; i < lvl.FermerCount; i++)
            enemyCellSpawn.SpawningMovingUnit(fermerPrebab, enemyStartedList);

    }

    public void StartGame(int lvlIndex)
    {
        Cleaning();
        pigSpawnCell.SpawningMovingUnit(pigPrefab, pigStartedList);
        SpawnLVL(lvlIndex);
        _currentLVL = lvlIndex;
        ActivateGameUI();
    }

    /// <summary> Clearing the Field of Units & start time </summary>
    private void Cleaning()
    {
        KAP.Pool.Pool.DespawnAll();
        Field.Singleton.Cleaning();
        Time.timeScale = 1f;
    }

    #region End Game
    public void EndGame(string text)
    {
        Time.timeScale = 0f;
        gameUI.SetActive(false);
        endGameUI.SetActive(true);
        endGameText.text = text;
    }

    public void LoseGame() => EndGame("You LOSE!\n\nTap to continue");

    public void WinGame()
    {
        EndGame("You WIN!\n\nTap to continue");

        int maxLVL = PlayerPrefs.GetInt("Max LVL");
        PlayerPrefs.SetInt("Max LVL", Mathf.Max(maxLVL, _currentLVL));
    }
    #endregion

    public void Exit() => Application.Quit();

    #region Menu
    public void ActivateMenu()
    {
        Cleaning();
        menuUI.SetActive(true);
        startBtnsMenu.SetActive(true);
        selectLVLsMenu.SetActive(false);
        gameUI.SetActive(false);
        endGameUI.SetActive(false);
        SpawnLVL(0);
    }

    public void ActivateGameUI()
    {
        menuUI.SetActive(false);
        gameUI.SetActive(true);
    }

    public void SelectLVL()
    {
        startBtnsMenu.SetActive(false);
        selectLVLsMenu.SetActive(true);

        int maxLVL = PlayerPrefs.GetInt("Max LVL");
        for (int i = 0; i <= maxLVL; i++)
            lvlsBtnsList[i].interactable = true;
        for (int i = maxLVL + 1; i < lvlsBtnsList.Count; i++)
            lvlsBtnsList[i].interactable = false;
    }
    #endregion
}
