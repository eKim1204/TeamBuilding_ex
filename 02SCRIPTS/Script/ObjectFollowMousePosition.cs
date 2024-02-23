using UnityEngine;

public class ObjectFollowMousePosition : MonoBehaviour
{
    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        
        // 마우스의 스크린 좌표를 가져옴
        Vector3 mousePos = Input.mousePosition;

        // 스크린 좌표를 월드 좌표로 변환 (Z 축은 카메라와 오브젝트 사이의 거리를 사용)
        mousePos.z = Mathf.Abs(mainCamera.transform.position.z - transform.position.z);
        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(mousePos);

        // 오브젝트의 위치를 마우스의 월드 좌표로 업데이트
        transform.position = new Vector3(worldPosition.x, worldPosition.y, transform.position.z);
    }
}
