using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdSelect : MonoBehaviour
{
    [SerializeField] private GameObject blueBird;
    [SerializeField] private GameObject redBird;
    [SerializeField] private GameObject yellowBird;
    private int birdColor;
    public static Bird bird;
    // Start is called before the first frame update
    void Start()
    {
        birdColor = PlayerPrefs.GetInt("color bird");
        choseBird();
    }
    void choseBird()
    {
        if (birdColor == (int)ColorBird.RED)
        {
            Bird.Instance.birdGameObject = Instantiate(redBird, redBird.transform.position, Quaternion.identity);
            bird = new RedBird();
        }
        if (birdColor == (int)ColorBird.BLUE)
        {
            Bird.Instance.birdGameObject = Instantiate(blueBird, blueBird.transform.position, Quaternion.identity);
            bird = new BlueBird();
        }
        if (birdColor == (int)ColorBird.YELLOW)
        {
            Bird.Instance.birdGameObject = Instantiate(yellowBird, yellowBird.transform.position, Quaternion.identity);
            bird = new YellowBird();
        }
    }
}
