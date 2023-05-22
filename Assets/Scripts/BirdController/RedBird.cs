using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RedBird :  Bird
{

    private bool isCountDown = false;  

    

    public GameObject choseBird(GameObject bird)
    {
        return Instantiate(bird, bird.transform.position, Quaternion.identity);
    }
    public override void Start()
    {
        base.Start();
    }
    public override void Awake()
    {
        base.Awake();
    }
    public override void Update()
    {
        base.Update();
    }
    public override void Skill()
    {
        if (!isCountDown)
        {
            Debug.Log("skill");
            BirdTest.Instance.checkSkill = true;
            BirdTest.Instance.gravitationalForce = 0;
            StartCoroutine(dash());
        }
          
    }
    
    IEnumerator dash()
    {
        GameObject[] pipeHolders = BirdTest.Instance.pipeHolder;
        pipeHolders[0].GetComponent<PipeController>().speed = 8f;
        pipeHolders[1].GetComponent<PipeController>().speed = 8f;
        yield return new WaitForSeconds(0.25f);
        pipeHolders[0].GetComponent<PipeController>().speed = 3f;
        pipeHolders[1].GetComponent<PipeController>().speed = 3f;
        StartCoroutine(Countdown());
    }
    IEnumerator Countdown()
    {
        int countdownTime = 5;
        isCountDown = true;

        while (countdownTime > 0)
        {
            
            BirdTest.Instance.countdownTextDash.text = countdownTime.ToString();
            yield return new WaitForSeconds(1.0f);
            countdownTime--;
        }

        // Kết thúc count down
        isCountDown = false;
        BirdTest.Instance.countdownTextDash.text = "";
    }

}
