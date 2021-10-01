using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Singleton { get; private set; }

    private void Awake()
    {
        if (Singleton == null) Singleton = this;
    }

    public void EndGame() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
}
