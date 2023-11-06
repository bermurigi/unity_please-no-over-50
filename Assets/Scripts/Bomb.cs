using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class Bomb : MonoBehaviour
{
    public GameObject P_ParticleBomb;
    private void OnDisable()
    {
        if (GameManager.Instance.isGameOver)
            return;
        if(!GameManager.Instance)
            return;
        
        
        GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");
        for (int i = 0; i < balls.Length; i++)
        { 
            balls[i].SetActive(false);
        }
        GameManager.Instance.ChangeBackgroundColorWithFade();
        var particleBomb = GameManager.Instance.pool.Get(4);
        particleBomb.transform.position = transform.position;
        particleBomb.transform.localScale = transform.localScale * 2.5f;
        particleBomb.transform.SetParent(GameManager.Instance.particleGroup);
        GameManager.Instance.GetScore(5);
        int r = Random.Range(0, 2);
        if (r == 0)
        {
            GameManager.Instance.S_Bomb1.Play();
        }
        else
        {
            GameManager.Instance.S_Bomb2.Play();
        }
        
    }

}
