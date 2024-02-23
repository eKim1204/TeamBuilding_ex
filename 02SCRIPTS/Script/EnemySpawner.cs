using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
   //[SerializeField]
  // private GameObject enemyPrefab; // 적 프리펩
   [SerializeField]
    private WaveSystem waveSystem; // WaveSystem 참조 추가
  private int spawnedEnemyCount; // 생성된 적의 수를 추적

   [SerializeField]
   private GameObject enemyHPSliderPrefab;
   [SerializeField]
   private Transform canvasTransform;
    [SerializeField]
    private GameObject rewardSelectionUI; // 보상 선택 UI 참조

  // [SerializeField]
   //private float spawnTime; //적생성주기
   [SerializeField]
   private Transform[] wayPoints; //현재 스테이지의 이동경로
   [SerializeField]
   private PlayerHP playerHP;

   [SerializeField]
   private PlayerGold playerGold;
   private Wave currentWave;
   private int currentEnemyCount;

   [SerializeField]
   private PlayerMP playerMP;
   private List<Enemy> enemyList; //현재 맵에 존재하는 모든 적의 정보

//현재 웨이브의 남아 있는 적 ,최대 적 숫자
   public int CurrentEnemyCount => currentEnemyCount;
   public int MaxEnemyCount => currentWave.maxEnemyCount;

   public List<Enemy>  EnemyList => enemyList;

   private void Awake()
   {
    enemyList = new List<Enemy> ();
    //적 생성 코루틴 함수 호출
   // StartCoroutine("SpawnEnemy");

   }

   public void StartWave(Wave wave)
   {
    currentWave = wave;
    currentEnemyCount = currentWave.maxEnemyCount;
    StartCoroutine("SpawnEnemy");
      spawnedEnemyCount = 0; // 생성된 적의 수 초기화
   }
 private IEnumerator SpawnEnemy()
{
    int spawnEnemyCount = 0;

    while (spawnEnemyCount < currentWave.maxEnemyCount)
    {
        int enemyIndex = Random.Range(0, currentWave.enemyPrefabs.Length);
        GameObject clone = Instantiate(currentWave.enemyPrefabs[enemyIndex]);
        Enemy enemy = clone.GetComponent<Enemy>();
        enemy.Setup(this, wayPoints);
        enemyList.Add(enemy);

        SpawnEnemyHPSlider(clone);

        spawnEnemyCount++;
        spawnedEnemyCount++; // 여기로 이동

        yield return new WaitForSeconds(currentWave.spawnTime);
    }
}

 public void StartButton()
    {
        // 보상 선택 UI 활성화
        rewardSelectionUI.SetActive(true);
    }



   public void DestroyEnemy(EnemyDestroyType type, Enemy enemy, int gold, int MP)
   {
    if (type == EnemyDestroyType.Arrive)
    {
        playerHP.TakeDamage(1);
    }
    else if (type == EnemyDestroyType.kill)
    {
        playerGold.CurrentGold += gold;
        playerMP.CurrentMP += MP;
    }

     currentEnemyCount--; // 적이 사망할 때마다 현재 웨이브 감소
        enemyList.Remove(enemy); // 리스트에서 사망하는 적 정보 삭제
        Destroy(enemy.gameObject); // 적 오브젝트 삭제

         
   }
   
    private void SpawnEnemyHPSlider(GameObject enemy)
    {
        GameObject sliderClone = Instantiate(enemyHPSliderPrefab);
        sliderClone.transform.SetParent(canvasTransform);
        sliderClone.transform.localScale = Vector3.one;
        sliderClone.GetComponent<SliderPositionAutoSetter>().Setup(enemy.transform);
       sliderClone.GetComponent<EnemyHPViewer>().Setup(enemy.GetComponent<EnemyHP>());

    }

public bool AllEnemiesSpawnedForWave()
{
    return spawnedEnemyCount >= currentWave.maxEnemyCount;
}

   
}