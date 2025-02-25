using UnityEngine;
using UnityEngine.SceneManagement;

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

    public GameObject gameOverPanel; // UI Panel
    public GameObject tapText;
    private bool gameStarted = false;
    private bool isPaused = false;

    void Start()
    {
        InvokeRepeating(nameof(IncreaseBombSpawnRate), 10f, 10f);
        gameOverPanel.SetActive(false); // Hide game over panel
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !gameStarted)
        {
            StartSpawning();
            gameStarted = true;
            tapText.SetActive(false);
        }
    }

    private void StartSpawning()
    {
        InvokeRepeating(nameof(SpawnFruit), 1f, spawnInterval);
        InvokeRepeating(nameof(SpawnBomb), bombSpawnInterval, bombSpawnInterval);
    }

    private void SpawnFruit()
    {
        if (fruitPrefab.Length == 0) return;

        int randomIndex = Random.Range(0, fruitPrefab.Length);
        float randomX = Random.Range(minX, maxX);
        Vector3 spawnPosition = new Vector3(randomX, spawnY, 0);
        GameObject fruit = Instantiate(fruitPrefab[randomIndex], spawnPosition, Quaternion.identity);
        Rigidbody2D rb = fruit.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = new Vector2(0, -fallSpeed); // Set falling speed
        }
    }

    private void SpawnBomb()
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
        CancelInvoke(nameof(SpawnBomb));
        InvokeRepeating(nameof(SpawnBomb), bombSpawnInterval, bombSpawnInterval);
    }

    public void GameOver()
    {
        TogglePause();
    }

    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0f;
            gameOverPanel.SetActive(true);
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
