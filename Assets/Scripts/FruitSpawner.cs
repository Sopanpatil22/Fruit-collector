using UnityEngine;


public class FruitSpawner : MonoBehaviour
{
    public GameObject[] fruitPrefab;
    public float maxX,minX;
    public Transform Fruitspawner;
     public float spawnInterval = 1.5f; 

       public float spawnY = 6f; // Height above screen
    public float fallSpeed = 3f;

    bool gameStarted = false;

    public GameObject taptext;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }



    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)&& !gameStarted ){
            startSpawning();
            gameStarted = true;
            
        taptext.SetActive(false);
        }
    }

    private void startSpawning(){

         InvokeRepeating(nameof(spawnblock), 1f, spawnInterval);
    }


    private void spawnblock(){

        if (fruitPrefab.Length == 0) return;


       int randomIndex = Random.Range(0, fruitPrefab.Length);

        // Random X position within screen limits
        float randomX = Random.Range(minX, maxX);

        // Set spawn position at the top of the screen
        Vector3 spawnPosition = new Vector3(randomX, spawnY, 0);

        // Instantiate fruit
        GameObject fruit = Instantiate(fruitPrefab[randomIndex], spawnPosition, Quaternion.identity);

         // Apply downward velocity
        Rigidbody2D rb = fruit.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = new Vector2(0, -fallSpeed); // Falls straight down
        }

        

    }
}
