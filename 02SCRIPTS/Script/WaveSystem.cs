using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WaveSystem : MonoBehaviour
{
    [SerializeField]
    private Wave[] waves;
    [SerializeField]
    private EnemySpawner enemySpawner;
    private int currentWaveIndex = -1;
    public GameObject rewardSelectionUI; // 보상 선택 UI
    public RewardManager rewardManager; // RewardManager 스크립트 참조

    public int CurrentWave => currentWaveIndex + 1;
    public int MaxWave => waves.Length;

    public void StartWave()
    {
        if (currentWaveIndex >= waves.Length - 1)
        {
            return; // 배열 범위를 벗어날 경우 메소드 종료
        }

        if (enemySpawner.EnemyList.Count == 0)
        {
            currentWaveIndex++;
            if (currentWaveIndex < waves.Length)
            {
                enemySpawner.StartWave(waves[currentWaveIndex]);
            }
        }
    }

  private void Update()
{
    Debug.Log("LateUpdate 호출됨");

    if (IsWaveCompleted())
    {
        Debug.Log("웨이브 완료됨");

        if (currentWaveIndex < waves.Length - 1)
        {
            Debug.Log("보상창 활성화");
            rewardSelectionUI.SetActive(true);
            currentWaveIndex++; // 다음 웨이브로 인덱스 업데이트
            Debug.Log("현재 웨이브 인덱스: " + currentWaveIndex);
        }
        else if (currentWaveIndex == waves.Length - 1)
        {
            Debug.Log("마지막 웨이브 완료됨 - 클리어 씬으로 이동");
            SceneManager.LoadScene("clear"); // 실제 클리어 씬 이름으로 교체
        }
    }
    else
    {
        Debug.Log("웨이브 아직 완료되지 않음");
    }
}



    public void EnemyDestroyed()
    {
        // 적이 파괴됐을 때 필요한 처리를 여기에 작성
        // 예: 웨이브 완료 여부 확인, 다음 웨이브 준비 등
    }

    private bool IsWaveCompleted()
    {
        return currentWaveIndex >= 0 && enemySpawner.AllEnemiesSpawnedForWave() && enemySpawner.EnemyList.Count == 0;
    }
}

[System.Serializable]
public struct Wave
{
    public float spawnTime;
    public int maxEnemyCount;
    public GameObject[] enemyPrefabs;
}
