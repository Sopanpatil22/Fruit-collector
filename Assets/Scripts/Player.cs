using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Player : MonoBehaviour
{
    private bool isDragging = false;
    private Vector3 targetPosition;
    public float smoothSpeed = 10f;
    private Rigidbody2D rb;

    private float minX, maxX, minY, maxY;

    public Text scoreText;
    private int score = 0;
    
    public int lives = 3; 
    public Text livesText;

    public Camera mainCamera;

    public GameObject RgameOverPanel;
    public Text finalScoreText;
    public Text highScoreText;
    public Text LasthighScore;
    private bool isShaking = false; // Prevents shake after Game Over

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.gravityScale = 0;
        rb.freezeRotation = true;

        Vector3 screenBottomLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
        Vector3 screenTopRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

        minX = screenBottomLeft.x + 0.5f;
        maxX = screenTopRight.x - 0.5f;
        minY = screenBottomLeft.y + 0.5f;
        maxY = screenTopRight.y - 0.5f;

        UpdateScoreText();
        UpdateLivesUI();
        LoadHighScore();
    }

    void Update()
    {
        if (isDragging)
        {
            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            targetPosition = new Vector3(touchPosition.x, touchPosition.y, transform.position.z);

            targetPosition.x = Mathf.Clamp(targetPosition.x, minX, maxX);
            targetPosition.y = Mathf.Clamp(targetPosition.y, minY, maxY);
        }

        rb.linearVelocity = (targetPosition - transform.position) * smoothSpeed;
    }

    private void OnMouseDown()
    {
        isDragging = true;
    }

    private void OnMouseUp()
    {
        isDragging = false;
        rb.linearVelocity = Vector2.zero;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Fruit"))
        {
            Destroy(collision.gameObject);
            IncreaseScore();
        }
        else if (collision.gameObject.CompareTag("Bomb"))
        {
            Destroy(collision.gameObject);
            lives--;
            UpdateLivesUI();
            
            if (lives <= 0)
            {
                GameOver();
            }
            else
            {
                StartCoroutine(ShakeCamera(0.2f, 0.3f));
            }
        }
    }

    IEnumerator ShakeCamera(float duration, float magnitude)
    {
        if (isShaking) yield break; // Prevent shake if already shaking
        isShaking = true;

        Vector3 originalPos = mainCamera.transform.position;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;
            mainCamera.transform.position = new Vector3(originalPos.x + x, originalPos.y + y, originalPos.z);
            elapsed += Time.deltaTime;
            yield return null;
        }

        mainCamera.transform.position = originalPos;
        isShaking = false;
    }

    void GameOver()
    {
        CancelInvoke(); // Stop spawning fruits and bombs
        Time.timeScale = 0f; // Pause game
        RgameOverPanel.SetActive(true);
        finalScoreText.text = "Score: " + score;

        SaveHighScore();
    }

    void IncreaseScore()
    {
        score++;
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        scoreText.text = " " + score;
    }

    void UpdateLivesUI()
    {
        livesText.text = "Lives: " + lives;
    }

    void LoadHighScore()
    {
        int highScore = PlayerPrefs.GetInt("HighScore", 0);
        highScoreText.text = "High Score: " + highScore;
    }

    void SaveHighScore()
    {
        int highScore = PlayerPrefs.GetInt("HighScore", 0);
        if (score > highScore)
        {
            PlayerPrefs.SetInt("HighScore", score);
            PlayerPrefs.Save();
            highScoreText.text = "High Score: " + score;


            // Show high score on Game Over panel
            int lasthighScore = PlayerPrefs.GetInt("HighScore");
            LasthighScore.text = "High Score: " + lasthighScore;
        
        }
    }
}
