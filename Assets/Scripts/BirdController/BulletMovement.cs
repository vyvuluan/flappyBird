using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bulletMovement();
    }
    void bulletMovement()
    {
        Vector3 temp = transform.position;
        temp.x += 4 * Time.deltaTime;
        transform.position = temp;
        //float worldHeight = Camera.main.orthographicSize * 2f;

        //float worldWidth = worldHeight * Screen.width / Screen.height;
        //if (transform.position.x > worldWidth / 2)
        //{
        //    Destroy(gameObject);
        //}

    }
}
