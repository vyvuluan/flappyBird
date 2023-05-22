using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BirdController : MonoBehaviour
{
    
    private  Bird bird;
    
    // Start is called before the first frame update

    void Start()
    {
        bird = BirdSelect.bird;
    }

    // Update is called once per frame
    void Update()
    {
        bird.Update();
    }

    
    public void FlapButton()
    {
        Debug.Log("nhan");
        bird.didFlap = true;
    }

}
