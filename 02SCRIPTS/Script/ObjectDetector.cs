using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ObjectDetector : MonoBehaviour
{
   [SerializeField]
   private TowerSpawner towerSpawner;
   [SerializeField]
   private TowerDataViewer towerDataViewer;

   private Camera mainCamera;
   private Ray ray;
   private RaycastHit hit;
   private Transform hitTransform = null;

   private void Awake()

   {
    mainCamera = Camera.main;
   }

   private void Update()
   {
    

    if (EventSystem.current.IsPointerOverGameObject() == true )
    {
        return;
    }
    //마우스 왼쪽 버튼눌렀을때
    if (Input.GetMouseButton(0) )
    {

        //카메라 위치에서 화면의 마우스 위치를 관통하는 광선 생성
        ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if( Physics.Raycast(ray, out hit, Mathf.Infinity ) ) 
        {
                hitTransform = hit.transform;
            //광선에 부딪히는 오브젝트를 검출하는 hit에 저장
            if ( hit.transform.CompareTag("Tile"))
            {

                //타워를 생성하는 스폰타워 호출 
                towerSpawner.SpawnTower(hit.transform);
            }

            else if ( hit.transform.CompareTag("Tower"))
            {
                towerDataViewer.OnPanel(hit.transform);
            }
        }
    }
    else if ( Input.GetMouseButtonUp(0))
    {
        if ( hitTransform == null || hitTransform.CompareTag("Tower") == false)
        {
            towerDataViewer.OffPanel();
        }
        hitTransform = null;
    }
   }
   }
    

