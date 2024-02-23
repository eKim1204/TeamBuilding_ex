using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TextTMPViewer : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI textPlayerHP;

    
    [SerializeField]
    private TextMeshProUGUI textTimer; // 타이머를 표시할 TextMeshProUGUI

    private float timer; // 경과 시간을 추적할 변수
    [SerializeField]
    private TextMeshProUGUI textPlayerGold;
    [SerializeField]
    private TextMeshProUGUI textWave;

    [SerializeField]
    private TextMeshProUGUI textEnemyCount;
    
    
    [SerializeField]
    private TextMeshProUGUI textPlayerMP;

    [SerializeField]
    private PlayerHP playerHP;

    [SerializeField]
    private PlayerGold playerGold;

    [SerializeField]
    private PlayerMP playerMP;

    [SerializeField]
    private WaveSystem waveSystem;

    [SerializeField]
    private EnemySpawner enemySpawner;
    

    // Start is called before the first frame update
    void Start()
    {
        timer = 0f; // 타이머 초기화
    }

    // Update is called once per frame
    void Update()
    {
        
          textPlayerHP.text = playerHP.CurrentHealth + "/" + playerHP.MaxHealth;
          textPlayerGold.text = playerGold.CurrentGold.ToString();
          textPlayerMP.text = playerMP.CurrentMP.ToString();
          textWave.text = waveSystem.CurrentWave + "/" + waveSystem.MaxWave;
          textEnemyCount.text = enemySpawner.CurrentEnemyCount + "/" + enemySpawner.MaxEnemyCount;
           timer += Time.deltaTime; // 경과 시간 업데이트
        UpdateTimerDisplay(); // 타이머 표시 업데이트
         

    }

     private void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(timer / 60); // 분
        int seconds = Mathf.FloorToInt(timer % 60); // 초
        textTimer.text = string.Format("{0:00}:{1:00}", minutes, seconds); // 시:분 형식으로 포맷
    }

     
}
