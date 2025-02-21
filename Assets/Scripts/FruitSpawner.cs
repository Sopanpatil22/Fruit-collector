using UnityEngine;


public class FruitSpawner : MonoBehaviour
{
    public GameObject fruitPrefab;
    public float maxX;
    public Transform Fruitspawner;
    public float SpawnRate;

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
        InvokeRepeating("spawnblock",0.5f,SpawnRate);
    }


    private void spawnblock(){
        Vector3 spawnPos =Fruitspawner.position;

        spawnPos.x=Random.Range(-maxX,maxX);

        Instantiate(fruitPrefab,spawnPos,Quaternion.identity);

       

        

    }
}
