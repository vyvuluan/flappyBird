using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private const string HIGH_SCORE = "high score";
    private const string COLOR_BIRD = "color bird";
    private void Awake()
    {   
        PlayerPrefs.SetInt(HIGH_SCORE, 0);
        PlayerPrefs.SetString(COLOR_BIRD, "red");
    }
    

}
