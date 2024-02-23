using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    [SerializeField]
    private float maxHP;
    private float currentHP;
    private bool isDie = false;
    private SpriteRenderer spriteRenderer;
    private Enemy enemy; // Enemy 컴포넌트를 참조하기 위한 변수 추가

   public float MaxHP => maxHP;
    public float CurrentHP => currentHP; // 'currentHP' 변수 이름을 'CurrentHP'로 수정

    private void Awake()
    {
        currentHP = maxHP;
        enemy = GetComponent<Enemy>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        
    }

    public void TakeDamage(float damage)
    {
        if (isDie == true ) return; // 이미 사망한 경우 함수 종료

        currentHP -= damage;

        StopCoroutine("HitAlphaAnimation");
        StartCoroutine("HitAlphaAnimation");

        if (currentHP <= 0 )
        {
            isDie = true;
            AudioManager.Instance.PlaySFX("Die");
             AudioManager.Instance.PlayRandomDeathSFX(); // 랜덤 죽음 사운드 재생

            enemy.OnDie(EnemyDestroyType.kill);
            //enemy.OnDie(EnemyDestroyType.kill);
        }
    }

    private IEnumerator HitAlphaAnimation()
    {
        Color color = spriteRenderer.color;

        // 적을 깜빡이는 효과를 위한 코드 추가
        color.a = 0.5f;
        spriteRenderer.color = color;

        yield return new WaitForSeconds(0.05f);

        // 깜빡이는 시간 후 원래 알파값으로 복원
        color.a = 1.0f;
        spriteRenderer.color = color;
    }
}
