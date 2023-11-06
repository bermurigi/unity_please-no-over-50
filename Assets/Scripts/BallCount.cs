using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCount : MonoBehaviour
{
    public string targetTag = "Ball"; // 검색할 태그 지정
    public int ballCount;
    private float backGroundColor;
    private void Update()
    {
        if (GameManager.Instance.isGameOver)
            return;
            // 해당 영역에 포함된 "ball" 태그를 가진 오브젝트를 모두 찾습니다.
        GameObject[] balls = GameObject.FindGameObjectsWithTag(targetTag);
        // "ball" 태그를 가진 오브젝트의 개수를 출력합니다.
        
        ballCount = balls.Length;
        if (ballCount < 10)
            GameManager.Instance.ballCountText.color = new Color(204f / 255f, 226f / 255f, 203f / 255f);
        else if(ballCount<25)
            GameManager.Instance.ballCountText.color = new Color(255f / 255f, 174f / 255f, 165f / 255f);
        else if (ballCount < 40)
            GameManager.Instance.ballCountText.color = new Color(255f / 255f, 100f / 255f, 138f / 255f);
        GameManager.Instance.ballCountText.text = ballCount.ToString() + "/50";
        backGroundColor = Mathf.Clamp01(ballCount * 5  / 255.0f);
        GameManager.Instance.bg.GetComponent<SpriteRenderer>().color = new Color(
            GameManager.Instance.bg.GetComponent<SpriteRenderer>().color.r,
            GameManager.Instance.bg.GetComponent<SpriteRenderer>().color.g,
            GameManager.Instance.bg.GetComponent<SpriteRenderer>().color.b, backGroundColor);
        if (ballCount > 50)
        {
            GameManager.Instance.GameOver(1);
            
        }
    }
    
}
