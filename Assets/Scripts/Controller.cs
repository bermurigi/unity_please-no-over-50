using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    void Update()
    {
        if (GameManager.Instance.isPaused)
            return;
        // 모든 터치 입력을 확인
        for (int i = 0; i < Input.touchCount; i++)
        {
            // 터치 입력을 가져옴
            Touch touch = Input.GetTouch(i);

            // 터치가 시작되었을 때
            if (touch.phase == TouchPhase.Began)
            {
                // 터치 위치를 화면 좌표에서 월드 좌표로 변환
                Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);

                // 터치한 위치에서 레이를 쏴서 충돌한 오브젝트를 찾음
                RaycastHit2D hit = Physics2D.Raycast(touchPosition, Vector2.zero);

                // 충돌한 오브젝트가 있을 경우
                if (hit.collider != null)
                {
                    var hitObj = hit.collider.gameObject;
                    if (hitObj.layer == LayerMask.NameToLayer("Ball"))
                    {
                        hitObj.gameObject.SetActive(false);
                    }
                    // 터치한 오브젝트의 이름을 출력
                    Debug.Log("Touched Object: " + hit.collider.gameObject.name);
                }
            }
        }
        
        // 마우스 왼쪽 버튼이 클릭되었는지 확인
        if (Input.GetMouseButtonDown(0))
        {
           
            // 마우스 클릭 위치를 화면 좌표에서 월드 좌표로 변환
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // 클릭한 위치에서 레이를 쏴서 충돌한 오브젝트를 찾음
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);
            // 충돌한 오브젝트가 있을 경우
            if (hit.collider != null)
            {
                var hitObj = hit.collider.gameObject;
                if (hitObj.layer == LayerMask.NameToLayer("Ball"))
                {
                    hitObj.gameObject.SetActive(false);

                    
                }
                // 클릭한 오브젝트의 이름을 출력
                Debug.Log("Clicked Object: " + hit.collider.gameObject.name);
            }
        }
    }
}



