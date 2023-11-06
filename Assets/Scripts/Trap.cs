using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public GameObject P_ParticleTrap;
    private void OnDisable()
    {
        if (GameManager.Instance.isGameOver)
            return;
        if(!GameManager.Instance)
            return;
        var particleTrab = GameManager.Instance.pool.Get(5);
        particleTrab.transform.position = transform.position;
        particleTrab.transform.localScale = transform.localScale * 2.5f;
        particleTrab.transform.SetParent(GameManager.Instance.particleGroup);
        

    }

    private void OnMouseDown()
    {
        GameManager.Instance.S_Trap.Play();
        if (GameManager.Instance.isPaused)
            return;
        GameManager.Instance.GameOver(2);
    }
}
