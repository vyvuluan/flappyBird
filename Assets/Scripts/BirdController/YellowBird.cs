using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class YellowBird : Bird
{
    private List<GameObject> bulletPool;
    public int poolSize = 7;
    public GameObject choseBird(GameObject bird)
    {
        return Instantiate(bird, bird.transform.position, Quaternion.identity);
    }


    public override void Skill()
    {
        //Instantiate(BirdTest.Instance.bulletYellow, BirdTest.Instance.currentPosition, Quaternion.identity);
        foreach (GameObject bullet in bulletPool)
        {
            if (!bullet.activeInHierarchy)
            {
                bullet.transform.position = BirdTest.Instance.currentPosition;
                bullet.SetActive(true);
                break;
            }
        }
    }
    public override void Start()
    {
        base.Start();
        bulletPool = new List<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject bullet = Instantiate(BirdTest.Instance.bulletYellow);
            bullet.SetActive(false);
            bulletPool.Add(bullet);
        }
    }
    public override void Update()
    {
        base.Update();
        float worldHeight = Camera.main.orthographicSize * 2f;
        float worldWidth = worldHeight * Screen.width / Screen.height;
        foreach (GameObject bullet in bulletPool)
        {
            if (bullet.activeInHierarchy && bullet.transform.position.x > worldWidth/2)
            {
                bullet.SetActive(false);
            }
        }
    }
   
}
