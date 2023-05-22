using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BirdTest : MonoBehaviour
{
    private static BirdTest instance;
    public static BirdTest Instance { get => instance; private set => instance = value; }

    [SerializeField] private GameObject blueBird;
    [SerializeField] private GameObject redBird;
    [SerializeField] private GameObject yellowBird;
    [SerializeField] private GameObject spawner;
    [SerializeField] private AudioClip flyClip, dieClip, pointClip;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private Image imageSkillDash;
    [SerializeField] private Image imageSkillShoot;
    [SerializeField] private Image imageSkillSlow;
    private int birdColor;
    private IBird bird;
    private bool isAlive;
    private bool didFlap;
    private float verticalVelocity = 0f;
    private Vector3 previousPosition;

    public bool checkPoint;
    GameObject birdGameObject;
    public float bounceForce;
    public bool checkSkill;
    public Text countdownTextSlow;
    public Text countdownTextDash;
    public float gravitationalForce = 9.8f;
    public int score = 0;
    public float flag = 0;
    public GameObject bulletYellow;
    public Vector3 currentPosition;
    public  GameObject[] pipeHolder;
    void Start()
    {
        birdColor = PlayerPrefs.GetInt("color bird") ;
        choseBird();
        pipeHolder = GameObject.FindGameObjectsWithTag("pipe holder");
        Debug.Log("ban dau 0 " + pipeHolder[0].transform.position.y);
        Debug.Log("ban dau 1 " + pipeHolder[1].transform.position.y);
        previousPosition = transform.position;
    }
    void choseBird()
    {
        if(birdColor == (int) ColorBird.RED)
        {
            //bird = gameObject.AddComponent<RedBird>();
            birdGameObject =  bird.choseBird(redBird);
            imageSkillDash.gameObject.SetActive(true);
            imageSkillShoot.gameObject.SetActive(false);
            imageSkillSlow.gameObject.SetActive(false);
        }
        if (birdColor == (int) ColorBird.BLUE)
        {
            //bird = gameObject.AddComponent<BlueBird>();
            birdGameObject = bird.choseBird(blueBird);
            imageSkillDash.gameObject.SetActive(false);
            imageSkillShoot.gameObject.SetActive(false);
            imageSkillSlow.gameObject.SetActive(true);
        }
        if (birdColor == (int) ColorBird.YELLOW)
        {
            //bird = gameObject.AddComponent<YellowBird>();
            birdGameObject = bird.choseBird(yellowBird);
            imageSkillDash.gameObject.SetActive(false);
            imageSkillShoot.gameObject.SetActive(true);
            imageSkillSlow.gameObject.SetActive(false);
        }
    }
    void Awake()
    {
        checkSkill = false;
        isAlive = true;
        checkPoint = false;
        if (instance == null)
        {
            instance = this;
        }
        
    }
    void Update()
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
        currentPosition = birdGameObject.transform.position;
        Vector3 previousFramePosition = previousPosition;
        Debug.Log("ban sau 0 " + pipeHolder[0].transform.position.y);
        Debug.Log("ban sau 1 " + pipeHolder[1].transform.position.y);
        if (isAlive)
        {
            if (didFlap)
            {
                didFlap = false;
                verticalVelocity = bounceForce;
                audioSource.PlayOneShot(flyClip);
            }    
            verticalVelocity -= gravitationalForce * Time.deltaTime;
            if (birdGameObject.transform.position.y > -4.0f)
            {
                Vector3 newPosition = birdGameObject.transform.position + Vector3.up * verticalVelocity * Time.deltaTime;
                newPosition.x = birdGameObject.transform.position.x; 
                birdGameObject.transform.position = newPosition;
            }
            if (birdGameObject.transform.position.y < -4.0f)
            {
                birdDie();
            }
            //transform.Translate(Vector3.up * verticalVelocity * Time.deltaTime);
            if (previousFramePosition.y < currentPosition.y)
            {
                float angel = Mathf.Lerp(0, 90, verticalVelocity / 10);
                birdGameObject.transform.rotation = Quaternion.Euler(0, 0, angel);
            }
            else
            {
                float angel = Mathf.Lerp(0, -90, -verticalVelocity / 10);
                birdGameObject.transform.rotation = Quaternion.Euler(0, 0, angel);
            }

            float distanceFromBirdToPipe1 = Mathf.Abs(birdGameObject.transform.position.x - pipeHolder[0].transform.position.x);
            float distanceFromBirdToPipe2 = Mathf.Abs(birdGameObject.transform.position.x - pipeHolder[1].transform.position.x);
            Transform child1;
            Transform child2 ;
            if (distanceFromBirdToPipe1 < distanceFromBirdToPipe2 )
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
            if (birdGameObject.transform.position.x > objectBoundsMin.x && birdGameObject.transform.position.x < objectBoundsMax.x)
            {
                Debug.Log(checkSkill);
                if (birdGameObject.transform.position.y <= objectBoundsMax.y && !checkSkill || birdGameObject.transform.position.y >= objectBoundsMin1.y && !checkSkill)
                {
                    birdDie();
                }
                if (birdGameObject.transform.position.y > objectBoundsMax.y && birdGameObject.transform.position.y < objectBoundsMin1.y && checkPoint == false)
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
            if (birdGameObject.transform.position.x > objectBoundsMax.x)
            {
                checkPoint = false;
                checkSkill = false;
                gravitationalForce = 9.8f;
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                bird.skill();
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
    public void FlapButton()
    {
        didFlap = true;
    }
    public void skillButton()
    {
        bird.skill();
    }
      
}
