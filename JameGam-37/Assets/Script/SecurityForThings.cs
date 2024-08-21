using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class SecurityForThings : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector3 initialPosition;
    void Start()
    {
        print(transform.position);
        initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        if((gameObject.transform.position.x < -15 || gameObject.transform.position.x > 15) || (gameObject.transform.position.y < -10 || gameObject.transform.position.y > 10))
        {
            gameObject.transform.position = initialPosition;
            Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
            rb.velocity = new Vector3(0, 0, 0);
        }
        
    }
}
