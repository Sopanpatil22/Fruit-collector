using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    private bool isDragging = false;
    private Vector3 targetPosition;
    public float smoothSpeed = 20f; // Adjust for smooth movement
    private Rigidbody2D rb;

    private float minX, maxX, minY, maxY; // Screen boundaries

    
    public TextMeshProUGUI scoreText;

    int Score=0;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Ensure Rigidbody2D settings are correct
        rb.gravityScale = 0; // No gravity effect
        rb.freezeRotation = true; // Prevent unwanted rotation

        // Get screen boundaries dynamically
        Vector3 screenBottomLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
        Vector3 screenTopRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

        minX = screenBottomLeft.x;
        maxX = screenTopRight.x;
        minY = screenBottomLeft.y;
        maxY = screenTopRight.y;
    }

    void Update()
    {
        if (isDragging)
        {
            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            targetPosition = new Vector3(touchPosition.x, touchPosition.y, transform.position.z);

            // Clamp position to keep object inside screen boundaries
            targetPosition.x = Mathf.Clamp(targetPosition.x, minX, maxX);
            targetPosition.y = Mathf.Clamp(targetPosition.y, minY, maxY);
        }

        // Apply smooth movement without lag
        rb.linearVelocity = (targetPosition - transform.position) * smoothSpeed;

    }

    private void OnMouseDown()
    {
        isDragging = true;
    }

    private void OnMouseUp()
    {
        isDragging = false;
        rb.linearVelocity = Vector2.zero; // Stop movement smoothly when releasing
    }


    private void OnCollisionEnter2D(Collision2D collision ){
        if(collision.gameObject.tag=="Fruit"){
            Destroy(collision.gameObject);
             Score++;

             scoreText.text=Score.ToString();
        }
        else if(collision.gameObject.tag=="Bomb"){
            Destroy(collision.gameObject);
             FindObjectOfType<FruitSpawner>().GameOver();
        }
    }
}
