using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public Text BestScoreText, ScoreText, NewRecordText,ballCountText,FinalScoreText,GameOverReasonText;
    public GameObject GameOverPanel, OptionPanel, HCPPanel;
    public AudioSource S_GameOver,S_Ball,S_Ball2,S_Ball3,S_Bomb1, S_Bomb2, S_Trap, S_BallBounce, S_ButtonClick, S_NewRecord;
    
    
    public int score;
    public bool isNewRecord;


    public Transform particleGroup;

    public GameObject bg;

    public static GameManager Instance;
    public PoolManager pool;

    public bool isGameOver;
    public bool isPaused;
    
    
    public List<Color> backgroundColors = new List<Color>();
    public Camera mainCamera;
    public float fadeDuration = 1.0f; // 페이드 인/아웃 지속 시간

    private Coroutine currentFadeCoroutine;
    
    

    //리스타트 버튼
    public void Restart()
    {
        GameManager.Instance.S_ButtonClick.Play();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        CheckScore();
        BestScoreText.text = PlayerPrefs.GetInt("BestScore").ToString();
    }

    public void Start()
    {
        //공 레이어끼리 충돌못하게 하기
        Physics2D.IgnoreLayerCollision(7,7);
        ChangeBackgroundColor(); // 시작 시 배경 색상 변경
        
        bool FirstPlay = PlayerPrefs.GetInt("FirstPlay", 0) == 0;
        if (FirstPlay)
        {
            HCPPanelButton();
            PlayerPrefs.SetInt("FirstPlay", 1);
        }

    }
   

    void CheckScore()
    {
        ScoreText.text = score.ToString();
        if(PlayerPrefs.GetInt("BestScore", 0) < score)
        {
            PlayerPrefs.SetInt("BestScore", score);
            BestScoreText.text = PlayerPrefs.GetInt("BestScore").ToString();
            BestScoreText.color=Color.green;
            isNewRecord = true;
        }
    }

    public void GetScore(int num)
    {
        score += num;
        ScoreText.text = score.ToString();
    }

    public void GameOver(int reason)
    {
    
        switch (reason)
        {
            case 1: //볼50개 넘겨서
                GameOverReasonText.text = "50개 넘었어ㅠ";
                break;
            case 2: //트랩눌러서
                GameOverReasonText.text = "빨간공은 위험해";
                break;
            
        }
        
        CheckScore();
        isGameOver = true;
        GameOverPanel.SetActive(true);
        FinalScoreText.text = "최종점수 : " + score.ToString();
        if (isNewRecord)
        {
            NewRecordText.gameObject.SetActive(true);
            S_NewRecord.Play();
        }
        Camera.main.GetComponent<Animator>().SetTrigger("shake");
        S_GameOver.Play();
    }

    private void ChangeBackgroundColor()
    {
        if (backgroundColors.Count > 0)
        {
            // 랜덤하게 배경 색상 선택
            
            Color currentColor = mainCamera.backgroundColor;
            Color targetColor;
            do
            {
                targetColor = backgroundColors[Random.Range(0, backgroundColors.Count)];
            } while (currentColor == targetColor);
           

            // 선택한 배경 색상을 카메라에 적용
            mainCamera.backgroundColor = targetColor;
        }
    }
    public void ChangeBackgroundColorWithFade()
    {
        if (backgroundColors.Count > 0)
        {
            if (currentFadeCoroutine != null)
            {
                StopCoroutine(currentFadeCoroutine);
            }
            
            // 현재 색상을 가져옴
            Color currentColor = mainCamera.backgroundColor;
            Color targetColor;

            // 현재 색상과 동일한 경우, 다시 무작위로 선택
            do
            {
                targetColor = backgroundColors[Random.Range(0, backgroundColors.Count)];
            } while (currentColor == targetColor);

            currentFadeCoroutine = StartCoroutine(FadeBackgroundColor(targetColor));
        
        }
    }
    
    private IEnumerator FadeBackgroundColor(Color targetColor)
    {
        float elapsedTime = 0f;
        Color startColor = mainCamera.backgroundColor;
        

        while (elapsedTime < fadeDuration)
        {
            mainCamera.backgroundColor = Color.Lerp(startColor, targetColor, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        mainCamera.backgroundColor = targetColor;
        currentFadeCoroutine = null;
    }

    public void OptionPanelButton()
    {
        S_ButtonClick.Play();
        if (!isPaused)
        {
            OptionPanel.SetActive(true);
            Time.timeScale = 0;
        }
        else if (isPaused)
        {
            OptionPanel.SetActive(false);
            Time.timeScale = 1;
        }
        isPaused = !isPaused;
    }

    public void OptionApply()
    {
        S_ButtonClick.Play();
        OptionPanel.SetActive(false);
        Time.timeScale = 1;
        isPaused = !isPaused;
        
    }

    public void HCPPanelButton()
    {
        S_ButtonClick.Play();
        if (!isPaused)
        {
            HCPPanel.SetActive(true);
            Time.timeScale = 0;
        }
        else if (isPaused)
        {
            HCPPanel.SetActive(false);
            Time.timeScale = 1;
        }
        isPaused = !isPaused;
    }
    public void HCPCancle()
    {
        S_ButtonClick.Play();
        HCPPanel.SetActive(false);
        Time.timeScale = 1;
        isPaused = !isPaused;
        
    }
    
}


