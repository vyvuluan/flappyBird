using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GScaler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        Vector3 tempScale = transform.localScale;

        //lấy chiều cao và rộng của hình background
        float width = sr.bounds.size.x;
        //của màn hình
        float worldHeight = Camera.main.orthographicSize * 2f;
        float worldWidth = worldHeight * Screen.width / Screen.height;
        Debug.Log(worldHeight + "x" + worldWidth);


        //tempScale.y = worldHeight / height;
        tempScale.x = worldWidth / width;


        transform.localScale = tempScale;
    }

   
}
