using UnityEngine;

public class Fruits : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y<-6f){
            Destroy(gameObject);
        }
    }
}
