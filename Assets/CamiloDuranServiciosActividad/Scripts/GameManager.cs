using UnityEngine;
using System.Threading.Tasks;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public float scorePerSecond = 10f;
    public float gameTime { get; private set; }
    public bool IsPlaying { get; private set; }

    private float score;
    public float Score => score;
    public int attempts = 0;

    private int objectsCaught = 0;
    private int objectsMissed = 0;

    [HideInInspector] public string playerName = "Player";

    public Spawner spawner;
    public UIManager uiManager;
    public FirebaseManager firebaseManager;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Update()
    {
        if (!IsPlaying) return;

        gameTime += Time.deltaTime;
        score += scorePerSecond * Time.deltaTime;

        uiManager.UpdateTime(gameTime);
        uiManager.UpdateScore(Mathf.FloorToInt(score));
    }

    public void StartGame(string player)
    {
        playerName = player;
        attempts++;
        score = 0f;
        gameTime = 0f;
        objectsCaught = 0;
        objectsMissed = 0;
        IsPlaying = true;

        spawner.StartSpawning();
        uiManager.ShowHUD();
    }

    public async void PlayerDied()
    {
        if (!IsPlaying) return;

        IsPlaying = false;
        spawner.StopSpawning();

        uiManager.ShowEndScreen();

        // Save data
        int finalScore = Mathf.FloorToInt(score);
        await firebaseManager.SaveHighscore(playerName, finalScore, gameTime, attempts, objectsCaught, objectsMissed);

        // Load data
        var top = await firebaseManager.GetTopHighscores(10);
        uiManager.PopulateLeaderboard(top);
    }

    public void ObjectCaught() => objectsCaught++;
    public void ObjectMissed() => objectsMissed++;

    public void Retry()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
}
