using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMP : MonoBehaviour
{
    [SerializeField]
    private int currentMP = 50; // 백업 필드

    public int CurrentMP
    {
        set => currentMP = Mathf.Max(0, value); // 백업 필드를 설정
        get => currentMP; // 백업 필드를 반환
    }
}
