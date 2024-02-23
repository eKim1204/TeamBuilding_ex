using UnityEngine;
using TMPro; // TextMeshPro를 사용하기 위한 네임스페이스

public class GameTimer : MonoBehaviour
{
    public TextMeshProUGUI timerText; // 타이머를 표시할 TextMeshProUGUI

    private float elapsedTime; // 경과 시간을 추적할 변수

    private void Start()
    {
        elapsedTime = 0f; // 타이머 초기화
    }

    private void Update()
    {
        
        elapsedTime += Time.deltaTime; // 매 프레임마다 경과 시간을 더함
        UpdateTimerDisplay(); // 타이머 표시 업데이트
    }

    private void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(elapsedTime / 60); // 분
        int seconds = Mathf.FloorToInt(elapsedTime % 60); // 초
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds); // 타이머를 텍스트로 표시
    }
}
