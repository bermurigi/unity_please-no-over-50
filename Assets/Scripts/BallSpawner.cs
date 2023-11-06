using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class BallSpawner : MonoBehaviour
{
    public float ballSpawnTimer;
    public float ballTimer;

    public float bombSpawnTimer;
    public float bombTimer;
    
    public float trabSpawnTimer;
    public float trabTimer;
   
    public GameObject ballPrefab;
    public GameObject bombPrefab;
    public GameObject trabPrefab;
    

    public GameObject[] ballSpawnPoint;
    private int spawnPointArray;
    public Transform ballGroup;

    private float localScaleXY;
    
    //벽도 여기서 관리
    [SerializeField] private GameObject[] WallPat;
    public float wallSpawnTimer;
    public float wallTimer;
    
    
    
    
    
    //밸런스
    private float count;
    private float randLv;
    //공 스폰, pos 공의 방향값
    private void Awake()
    {
        ballSpawnTimer = 1f;
        bombSpawnTimer = 3f;
    }

    public void Launch(Vector3 pos,GameObject prefab)
    {
        int spawnPointArray = Random.Range(0, ballSpawnPoint.Length);
        var ballObj = prefab;
        ballObj.transform.position = ballSpawnPoint[spawnPointArray].transform.position;
            //Instantiate(prefab, ballSpawnPoint[spawnPointArray].transform.position, Quaternion.identity);
        localScaleXY = Random.Range(5f, 10f);
        ballObj.transform.localScale = new Vector3(localScaleXY, localScaleXY, 1f);
        ballObj.GetComponent<Rigidbody2D>().AddForce(pos.normalized * 6000);
        ballObj.transform.SetParent(ballGroup);
    }

    private void Update()
    {
        if (GameManager.Instance.isGameOver)
            return;


        //공 스폰
        ballTimer += Time.deltaTime;
        if (ballTimer > ballSpawnTimer) 
        {
            ballTimer = 0;
            //이렇게하면 -1,0이나와서 무한동력될수도있음
            LvDesign();
            for (int i = 0; i < count; i++)
            {
                float randomX = Random.Range(-1.0f, 1.0f);
                float randomY = Random.Range(-1.0f, 0.0f);
                Vector2 RandomVec = new Vector3(randomX, randomY); 
                Launch(RandomVec, GameManager.Instance.pool.Get(0));
            }
        }
        
        bombTimer += Time.deltaTime;
        if (bombTimer > bombSpawnTimer) 
        {
            bombTimer = 0;
            //이렇게하면 -1,0이나와서 무한동력될수도있음
            
            float randomX = Random.Range(-1.0f, 1.0f);
            float randomY = Random.Range(-1.0f, 0.0f);
            Vector2 RandomVec = new Vector3(randomX, randomY);
            Launch(RandomVec, GameManager.Instance.pool.Get(1));
            bombSpawnTimer = Random.Range(25f, 40f);

        }
        
        trabTimer += Time.deltaTime;
        if (trabTimer > trabSpawnTimer) 
        {
            trabTimer = 0;
            //이렇게하면 -1,0이나와서 무한동력될수도있음
            
            float randomX = Random.Range(-1.0f, 1.0f);
            float randomY = Random.Range(-1.0f, 0.0f);
            Vector2 RandomVec = new Vector3(randomX, randomY);
            Launch(RandomVec, GameManager.Instance.pool.Get(2));
            trabSpawnTimer = Random.Range(15f, 30f);

        }

        wallTimer += Time.deltaTime;
        if (wallTimer > wallSpawnTimer)
        {
            wallTimer = 0;
            int pat = Random.Range(0, WallPat.Length);
            for (int i = 0; i < WallPat.Length; i++)
            {
                WallPat[i].SetActive(i==pat);
            }

            wallSpawnTimer = Random.Range(10, 30);
        }
    }

    void LvDesign()
    {
        
        randLv = Random.Range(0, 24);
        if (GameManager.Instance.score <= 10) count = randLv < 16 ? 1 : 2;
        else if (GameManager.Instance.score <= 20) count = randLv < 8 ? 1 : 2;
        else if (GameManager.Instance.score <= 40) count = randLv < 4 ? 1 : 2;
        else if (GameManager.Instance.score <= 100) count = randLv < 18 ? 2 : 3;
        else count = randLv < 18 ? 3 : 4;
    }

    
}
