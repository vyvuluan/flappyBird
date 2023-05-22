using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerPipe : MonoBehaviour
{
    [SerializeField] private GameObject pipeHolder;
    private int maxPipes = 2; 
    private float pipeSpacing = 3f; 
    private float resetPositionX; 
    void Start()
    {
        SpawnPipes();
    }
    void Awake()
    {
        float worldHeight = Camera.main.orthographicSize * 2f;
        float worldWidth = worldHeight * Screen.width / Screen.height;
        resetPositionX = worldWidth / 2;
    }
    void SpawnPipes()
    {        
        float startPositionX = resetPositionX;
        for (int i = 0; i < maxPipes; i++)
        {
            Instantiate(pipeHolder, new Vector3(startPositionX, Random.Range(-2.0f, 2.0f), 0f), Quaternion.identity);
            startPositionX += pipeSpacing;
        }
    }

}
