using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOScript : MonoBehaviour
{
    private float frontOfScene = 200.0f;
    private float backOfScene = -5.0f;
    private float speed = 20.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
        if (transform.position.z > frontOfScene || transform.position.z <  backOfScene)
        {
            Destroy(gameObject);
        }
    }
}
