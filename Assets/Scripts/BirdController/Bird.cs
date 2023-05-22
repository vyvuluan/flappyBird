using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Bird : MonoBehaviour
{

    private static Bird instance;
    public static Bird Instance { get => instance; private set => instance = value; }


    [SerializeField] private GameObject spawner;
    [SerializeField] private AudioClip flyClip, dieClip, pointClip;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private Image imageSkillDash;
    [SerializeField] private Image imageSkillShoot;
    [SerializeField] private Image imageSkillSlow;
    private int birdColor;
    public bool didFlap;
    public bool isAlive;
    private float verticalVelocity = 0f;
    private Vector3 previousPosition;

    public bool checkPoint;
    public GameObject birdGameObject;
    public float bounceForce;
    public bool checkSkill;
    public Text countdownTextSlow;
    public Text countdownTextDash;
    public float gravitationalForce = 9.8f;
    public int score = 0;
    public float flag = 0;
    public GameObject bulletYellow;
    public Vector3 currentPosition;
    public GameObject[] pipeHolder;
    public virtual void Start()
    {
        birdColor = PlayerPrefs.GetInt("color bird");

        pipeHolder = GameObject.FindGameObjectsWithTag("pipe holder");
        previousPosition = transform.position;
    }
    public virtual void Awake()
    {
        checkSkill = false;
        isAlive = true;
        checkPoint = false;
        if (instance == null)
        {
            instance = this;
        }

    }
    public virtual void Update()
    {
        BirdMoveMent();
    }
    void birdDie()
    {
        flag = 1;
        if (isAlive)
        {
            isAlive = false;
            Destroy(spawner);
            audioSource.PlayOneShot(dieClip);
            int bestScore = PlayerPrefs.GetInt("high score");
            if (score > bestScore)
            {
                PlayerPrefs.SetInt("high score", score);
            }
            GamePlayController.Instance.setTextBestScore(PlayerPrefs.GetInt("high score"));
            Time.timeScale = 0;
            // anim.SetTrigger("Died");
            GamePlayController.Instance.setMedal(score);
            GamePlayController.Instance.enableGameOverPanel();
        }
    }
    void BirdMoveMent()
    {
        currentPosition = transform.position;
        Vector3 previousFramePosition = previousPosition;
        if (isAlive)
        {
            if (didFlap)
            {
                didFlap = false;
                verticalVelocity = bounceForce;
                audioSource.PlayOneShot(flyClip);
            }
            verticalVelocity -= gravitationalForce * Time.deltaTime;
            if (transform.position.y > -4.0f)
            {
                Vector3 newPosition = transform.position + Vector3.up * verticalVelocity * Time.deltaTime;
                newPosition.x = transform.position.x;
                transform.position = newPosition;
            }
            if (transform.position.y < -4.0f)
            {
                birdDie();
            }
            //transform.Translate(Vector3.up * verticalVelocity * Time.deltaTime);
            if (previousFramePosition.y < currentPosition.y)
            {
                float angel = Mathf.Lerp(0, 90, verticalVelocity / 10);
                transform.rotation = Quaternion.Euler(0, 0, angel);
            }
            else
            {
                float angel = Mathf.Lerp(0, -90, -verticalVelocity / 10);
                transform.rotation = Quaternion.Euler(0, 0, angel);
            }

            float distanceFromBirdToPipe1 = Mathf.Abs(transform.position.x - pipeHolder[0].transform.position.x);
            float distanceFromBirdToPipe2 = Mathf.Abs(transform.position.x - pipeHolder[1].transform.position.x);
            Transform child1;
            Transform child2;
            if (distanceFromBirdToPipe1 < distanceFromBirdToPipe2)
            {
                child1 = pipeHolder[0].transform.GetChild(0);
                child2 = pipeHolder[0].transform.GetChild(1);
            }
            else
            {
                child1 = pipeHolder[1].transform.GetChild(0);
                child2 = pipeHolder[1].transform.GetChild(1);
            }
            Renderer objectRenderer1 = child1.GetComponent<Renderer>();
            Renderer objectRenderer2 = child2.GetComponent<Renderer>();
            //pipe bottom
            Vector3 objectBoundsMin = objectRenderer1.bounds.min;
            Vector3 objectBoundsMax = objectRenderer1.bounds.max;
            //pipe top
            Vector3 objectBoundsMin1 = objectRenderer2.bounds.min;
            Debug.Log(objectBoundsMax.y);
            if (transform.position.x > objectBoundsMin.x && transform.position.x < objectBoundsMax.x)
            {
                Debug.Log(checkSkill);
                if (transform.position.y <= objectBoundsMax.y && !checkSkill || transform.position.y >= objectBoundsMin1.y && !checkSkill)
                {
                    birdDie();
                }
                if (transform.position.y > objectBoundsMax.y && transform.position.y < objectBoundsMin1.y && checkPoint == false)
                {
                    checkPoint = true;
                    IncreaseScore();
                }
                else if (checkPoint == false && checkSkill == true)
                {
                    checkPoint = true;
                    IncreaseScore();
                }
            }
            if (transform.position.x > objectBoundsMax.x)
            {
                checkPoint = false;
                checkSkill = false;
                gravitationalForce = 9.8f;
            }

        }
        previousPosition = currentPosition;

    }
    public void IncreaseScore()
    {
        score++;
        GamePlayController.Instance.setTextScore(score);
        audioSource.PlayOneShot(pointClip);
    }

    public abstract void Skill();
    
}
