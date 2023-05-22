using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{

    private int count = 1;
    [SerializeField] private GameObject redBird;
    [SerializeField] private GameObject blueBird;
    [SerializeField] private GameObject yellowBird;
    private void Start()
    {
        showBird();
    }
    public void NextButton()
    {
        count++;
        if (count > 3) count = 1;
        showBird();
    }
    public void showBird()
    {
        switch (count)
        {
            case 1:
                {
                    redBird.SetActive(true);
                    blueBird.SetActive(false);
                    yellowBird.SetActive(false);
                }
                break;
            case 2:
                {
                    redBird.SetActive(false);
                    blueBird.SetActive(true);
                    yellowBird.SetActive(false);
                }
                break;
            case 3:
                {
                    redBird.SetActive(false);
                    blueBird.SetActive(false);
                    yellowBird.SetActive(true);
                }
                break;
        }
    }    
    public void BackButton()
    {
        count--;
        if (count <= 0) count = 3;
        showBird();
    }
    public void StartGameButton()
    {
        switch (count)
        {
            case 1:
                {
                    PlayerPrefs.SetInt("color bird",(int) ColorBird.RED);
                    SceneManager.LoadScene("GamePlay");
                }
                break;
            case 2:
                {
                    PlayerPrefs.SetInt("color bird", (int)ColorBird.BLUE);
                    SceneManager.LoadScene("GamePlay");
                }
                break;
            case 3:
                {
                    PlayerPrefs.SetInt("color bird", (int)ColorBird.YELLOW);
                    SceneManager.LoadScene("GamePlay");
                }
                break;
        }
    }


    
}
