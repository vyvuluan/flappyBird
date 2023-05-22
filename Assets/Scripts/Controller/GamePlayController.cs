using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GamePlayController : MonoBehaviour
{
    private static GamePlayController instance;
    public static GamePlayController Instance { get => instance; private set => instance = value; }

    [SerializeField] private Button tapToStart;
    [SerializeField] private Text scroreText, scrorePanel, bestScore;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private Image medalGold, medalSilver, medalBronze;
    [SerializeField] private Text timeCountDownStartText;
    [SerializeField] private GameObject pausePanel;
    private void Awake()
    {
        Time.timeScale = 0;
        if(instance == null)
        {
            instance = this;
        }
        
    }

    public void startGame()
    {

        timeCountDownStartText.gameObject.SetActive(true);
        
        tapToStart.gameObject.SetActive(false);
        StartCoroutine(countDownToStartGame());
    }
    public void setTextScore(int scrore)
    {
        scroreText.text =  scrore.ToString();
        scrorePanel.text = scrore.ToString();
    }
    public void setTextBestScore(int scrore)
    {
        bestScore.text = scrore.ToString();
    }

    public void setMedal(int scrore)
    {
        if(scrore > 10)
        {
            medalGold.gameObject.SetActive(true);
            medalSilver.gameObject.SetActive(false);
            medalBronze.gameObject.SetActive(false);
        }
        else if(scrore < 10 && scrore > 5 )
        {
            medalGold.gameObject.SetActive(false);
            medalSilver.gameObject.SetActive(true);
            medalBronze.gameObject.SetActive(false);
        }
        else
        {
            medalGold.gameObject.SetActive(false);
            medalSilver.gameObject.SetActive(false);
            medalBronze.gameObject.SetActive(true);
        }
    }

    public void enableGameOverPanel()
    {
        gameOverPanel.SetActive(true);
    }
    public void homeButton()
    {
        Debug.Log("menu");
        SceneManager.LoadScene("MainMenu");
    }
    public void resumButton()
    {
        SceneManager.LoadScene("GamePlay");
    }
    IEnumerator countDownToStartGame()
    {
        int countDownTime = 1;
        while (countDownTime > 0)
        {
            timeCountDownStartText.text = countDownTime.ToString();
            yield return new WaitForSecondsRealtime(1.0f);
            countDownTime--;
        }
        
        Time.timeScale = 1;
        timeCountDownStartText.gameObject.SetActive(false);

    }
    public void pauseGame()
    {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
    }
    public void resumeGame()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
    }


}
