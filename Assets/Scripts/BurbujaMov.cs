using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurbujaMov : MonoBehaviour
{
    Vector3 target;
    float speed;
    private float degreesPerSecond = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, Time.smoothDeltaTime * speed);
        transform.Rotate(new Vector3(0f, Time.deltaTime * degreesPerSecond, 0f), Space.World);
        // Debug.Log($"Burbuja moviendo en posicion {transform.position}");
        if (transform.position.y > 16)
        {
            Destroy(gameObject);
        }
    }

    public void SetVars(Vector3 newTarget, float newSpeed)
    {
        target = newTarget;
        speed = newSpeed;
    }
}
