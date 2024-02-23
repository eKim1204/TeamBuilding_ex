using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHP : MonoBehaviour

  
{
    [SerializeField]
   // private Image imageScreen; gpt 수정전 
    private UnityEngine.UI.Image imageScreen;

   

    [SerializeField]
    private float MaxHP = 20;
    private float currentHP; // 오타 수정

    public float MaxHealth => MaxHP; // MaxHP를 가져오는 프로퍼티
    public float CurrentHealth => currentHP; // currentHP를 가져오는 프로퍼티
    
    private void Awake()
    {
        currentHP = MaxHP;
    }

    public void TakeDamage(float damage)
    {
        currentHP -= damage;

        StopCoroutine("HitAlphaAnimation");
        StartCoroutine("HitAlphaAnimation");
        
        if (currentHP <= 0)
        {
            // 여기에 플레이어가 사망했을 때의 처리를 추가할 수 있습니다.
            SceneManager.LoadSceneAsync("gameover");
        }
    }
    private IEnumerator HitAlphaAnimation()
{
    Color color = imageScreen.color;
    color.a = 0.4f; // 초기 알파 값 설정
    imageScreen.color = color;

    while (color.a >= 0.0f)
    {
        color.a -= Time.deltaTime; // 여기서 오타 수정됨
        imageScreen.color = color;

        yield return null;
    }
}
}
