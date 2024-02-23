using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SliderPositionAutoSetter : MonoBehaviour
{
    [SerializeField]
    private Vector3 distance = Vector3.down * 20.0f; // 올바른 대괄호 사용 및 Vector3 오타 수정
    private Transform targetTransform;
    private RectTransform rectTransform;

    public void Setup(Transform target)
    {
        //slider ui가 쫓아다닐 target 설정
        targetTransform = target;

        rectTransform = GetComponent<RectTransform>();
    }

    private void LateUpdate()
    {
        
        //적이 파괴되어 쫓아다닐 대상이 사라지면 슬라이더 유아이도 삭제
        if (targetTransform == null)
        {
            Destroy(gameObject);
            return;
        }

        // 여기에 슬라이더의 위치를 설정하는 코드를 추가하세요
        // 예를 들어, rectTransform.position을 조정하여 위치를 설정할 수 있습니다.

        Vector3 screenPosition = Camera.main.WorldToScreenPoint(targetTransform.position);
        rectTransform.position = screenPosition + distance;
    }
}
