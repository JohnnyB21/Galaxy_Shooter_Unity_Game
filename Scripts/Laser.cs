using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private float _speed = 8.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Translate laser up
        transform.Translate(Vector3.up * _speed * Time.deltaTime);

        // If laser position is greater than 8 on the y
        // destroy the object

        if (transform.position.y > 6.5f)
        {
            // Check if object has a parent
            // destroy parent too
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            Destroy(gameObject);
        }

    }
}
