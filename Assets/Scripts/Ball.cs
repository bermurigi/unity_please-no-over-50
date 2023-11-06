using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Ball : MonoBehaviour
{
    public GameObject P_ParticleBall;
    

    private void OnDisable()
    {
        if (GameManager.Instance.isGameOver)
            return;
        if (!GameManager.Instance)
            return;
        GameManager.Instance.GetScore(1);
        var particleBall = GameManager.Instance.pool.Get(3);
        particleBall.transform.position = transform.position;
        particleBall.transform.localScale = transform.localScale* 2.5f;
        particleBall.transform.SetParent(GameManager.Instance.particleGroup);

        int r = Random.Range(0, 3);
        if (r == 0)
        {
            GameManager.Instance.S_Ball.Play();
        }
        else if(r==1)
        {
            GameManager.Instance.S_Ball2.Play();
        }
        else
        {
            GameManager.Instance.S_Ball3.Play();
        }
    }

   
    

    
    
    
}
