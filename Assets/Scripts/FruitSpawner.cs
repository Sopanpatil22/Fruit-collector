using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class FruitSpawner : MonoBehaviour
{
    public GameObject[] fruitPrefab;
    public GameObject bombPrefab;
    public float maxX, minX;
    public float spawnInterval = 1.5f;
    public float spawnY = 6f;
    public float fallSpeed = 3f;
    public float bombSpawnInterval = 5f;
    public float bombSpawnAcceleration = 0.1f;
    
    private int score = 0;
    private int highScore;
    public TMP_Text scoreText;
    public TMP_Text highScoreText;
    
    public GameObject gameOverPanel; // UI Panel
    public TMP_Text finalScoreText;
    public TMP_Text finalHighScoreText;

    bool gameStarted = false;
    public GameObject taptext;
    private bool isPaused = false;

    void Start()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        UpdateHighScoreText();
        InvokeRepeating(nameof(IncreaseBombSpawnRate), 10f, 10f);
        gameOverPanel.SetActive(false); // Hide game over panel
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !gameStarted)
        {
            startSpawning();
            gameStarted = true;
            taptext.SetActive(false);
        }
    }

    private void startSpawning()
    {
        InvokeRepeating(nameof(spawnFruit), 1f, spawnInterval);
        InvokeRepeating(nameof(spawnBomb), bombSpawnInterval, bombSpawnInterval);
    }

    private void spawnFruit()
    {
        if (fruitPrefab.Length == 0) return;

        int randomIndex = Random.Range(0, fruitPrefab.Length);
        float randomX = Random.Range(minX, maxX);
        Vector3 spawnPosition = new Vector3(randomX, spawnY, 0);
        GameObject fruit = Instantiate(fruitPrefab[randomIndex], spawnPosition, Quaternion.identity);
        Rigidbody2D rb = fruit.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = new Vector2(0, -fallSpeed);
        }
    }

    private void spawnBomb()
    {
        if (bombPrefab == null) return;

        float randomX = Random.Range(minX, maxX);
        Vector3 spawnPosition = new Vector3(randomX, spawnY, 0);
        GameObject bomb = Instantiate(bombPrefab, spawnPosition, Quaternion.identity);
        Rigidbody2D rb = bomb.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = new Vector2(0, -fallSpeed);
        }
    }

    private void IncreaseBombSpawnRate()
    {
        bombSpawnInterval = Mathf.Max(0.5f, bombSpawnInterval - bombSpawnAcceleration);
        CancelInvoke(nameof(spawnBomb));
        InvokeRepeating(nameof(spawnBomb), bombSpawnInterval, bombSpawnInterval);
    }

    public void AddScore(int points)
    {
        score += points;
        scoreText.text = "Score: " + score;
        
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
            PlayerPrefs.Save();
            UpdateHighScoreText();
        }
    }

    private void UpdateHighScoreText()
    {
        highScoreText.text = "High Score: " + PlayerPrefs.GetInt("HighScore", 0);
    }

    public void GameOver()
    {
        if (score > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", score);
            PlayerPrefs.Save();
        }
        TogglePause();
    }

    public void TogglePause()
    {
        isPaused = !isPaused;
        
        if (isPaused)
        {
            Time.timeScale = 0f;
            gameOverPanel.SetActive(true);
            finalScoreText.text = "Score: " + score;
            finalHighScoreText.text = "High Score: " + PlayerPrefs.GetInt("HighScore", 0);
        }
        else
        {
            Time.timeScale = 1f;
            gameOverPanel.SetActive(false);
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
