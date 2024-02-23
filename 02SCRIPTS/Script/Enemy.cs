using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum EnemyDestroyType { kill = 0, Arrive } //적이 어떻게 죽었는지 체크
public class Enemy : MonoBehaviour
{
    private int wayPointCount; //이동경로 개수
    private Transform[] wayPoints; //이동경로 정보
    private int currentIndex = 0; // 현재 목표지점 인덱스
    private Movement2D movement2D; //오브젝트 이동제어
    private EnemySpawner enemySpawner; //적의 삭제를 본이이 하지 않고 에너미 스포너에 알려서 삭제

    [SerializeField]
    private int gold = 10; 
    [SerializeField]
    private int MP = 10; //적 사망시 획득 mp 
      [SerializeField]
    private GameObject[] deathEffects; // 죽을 때 생성될 이펙트 프리팹 배열

    public void Setup(EnemySpawner enemySpawner, Transform[] wayPoints)
    {
        movement2D = GetComponent<Movement2D>();
        this.enemySpawner = enemySpawner;
        //적 이동 경로 WayPoints 정보 설정

        wayPointCount = wayPoints.Length;
        this.wayPoints = new Transform[wayPointCount];
        this.wayPoints = wayPoints;
        //적의 위치를 첫번째 waypoint 위치로설정

        transform.position = wayPoints[currentIndex].position;
        //적 이동/목표지점 설정 코루틴 함수 시작
        StartCoroutine("OnMove");
    }

    private IEnumerator OnMove()
    {
        //다음이동 방향설정
        NextMoveTo();
        
        while (true)
        {
            //적 오브젝트 회전
           // transform.Rotate(Vector3.forward * 10);

        //적의 현재위치와 목표위치의 거리가 0.02 * 무브스피드보다 작을 때 if 조건문 실행
            if ( Vector3.Distance(transform.position, wayPoints[currentIndex].position) < 0.01f * movement2D.MoveSpeed)
            {
                NextMoveTo();
            }
            yield return null;
        }

    
    }
   private void NextMoveTo()
{
    if (currentIndex < wayPointCount - 1)
    {
        currentIndex++;
        Vector3 direction = (wayPoints[currentIndex].position - transform.position).normalized;
        movement2D.MoveTo(direction);
    }
    else
    {
        // 도착한 경우 처리
        gold = 0;
        MP = 0;
        OnDie(EnemyDestroyType.Arrive);
    }
}


     public void OnDie(EnemyDestroyType type)
    {
        // 이펙트 생성 로직 추가
        if (deathEffects.Length > 0)
        {
            int index = Random.Range(0, deathEffects.Length); // 랜덤한 이펙트 선택
            Instantiate(deathEffects[index], transform.position, Quaternion.identity);
             enemySpawner.DestroyEnemy(type, this, gold, MP);
        }

        // 기존의 OnDie 로직...
        enemySpawner.DestroyEnemy(type, this, gold, MP);
    }

    public void DestroyByBug()
    {
        OnDie(EnemyDestroyType.kill); // "Bug" 버튼에 의한 적 제거 처리
    }
}
