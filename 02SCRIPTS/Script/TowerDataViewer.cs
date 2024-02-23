using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

//타워 정보 활성화 비활성화 타워 정보 연동
public class TowerDataViewer : MonoBehaviour
{
    [SerializeField]
    private SystemTextViewer systemTextViewer;
    [SerializeField]
    private Image imageTower;
    [SerializeField]
    private TextMeshProUGUI textDamage;
    [SerializeField]
    private TextMeshProUGUI textRate;
    [SerializeField]
    private TextMeshProUGUI textRange;
    [SerializeField]
    private TextMeshProUGUI textLevel; // 추가된 필드
    [SerializeField]
    private TowerAttackRange towerAttackRange;
    [SerializeField] 
    private TextMeshProUGUI textUpgradeCost;

    private TowerWeapon currentTower; // 수정된 타입

    [SerializeField]
    private Button buttonUpgrade;
    

    public void Awake()
    {
        OffPanel();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OffPanel();
        }
    }

  public void OnPanel(Transform towerWeapon)
{
    currentTower = towerWeapon.GetComponent<TowerWeapon>();
    gameObject.SetActive(true); // 타워 정보 패널 활성화
    UpdateTowerData();
    towerAttackRange.OnAttackRange(currentTower.transform.position, currentTower.Range); // 수정된 라인
    Debug.Log("OnPanel 메서드 호출됨");
}




    public void OffPanel()
    {
        
        gameObject.SetActive(false);
        towerAttackRange.OffAttackRange();

        // 디버그 로그 추가
        Debug.Log("OffPanel 메서드 호출됨");
    }

    private void UpdateTowerData()
    {
        if ( currentTower.WeaponType == WeaponType.Cannon || currentTower.WeaponType == WeaponType.Laser)
        {
            imageTower.rectTransform.sizeDelta = new Vector2(88, 59);
            textDamage.text = " Damage : " + currentTower.Damage + "+" + "<color=red>" + currentTower.addedDamage.ToString("F1") + "</color>";
        }
        else
        {
            imageTower.rectTransform.sizeDelta = new Vector2 (59, 59 );

            if ( currentTower.WeaponType == WeaponType.Slow)
            {
            textDamage.text = "Slow : " + currentTower.Slow * 100 + "%";
            }
            else if ( currentTower.WeaponType == WeaponType.Buff)
            {
                textDamage.text = "Buff : " + currentTower.Buff * 100 + "%";
            }

        }
        imageTower.sprite = currentTower.TowerSprite;
        //textDamage.text = "Damage : " + currentTower.Damage;
        textRate.text = "Rate : " + currentTower.Rate;
        textRange.text = "Range : " + currentTower.Range;
        textLevel.text = "Level : " + currentTower.Level; // 이 부분을 사용하려면 textLevel 필드를 정의해야 합니다.
        buttonUpgrade.interactable = currentTower.Level < currentTower.MaxLevel ? true : false;
        textUpgradeCost.text = currentTower.UpgradeCost.ToString();

        // 디버그 로그 추가
        Debug.Log("UpdateTowerData 메서드 호출됨");
    }

    public void OnclickEventTowerUpgrade()
    {
        bool isSuccess = currentTower.Upgrade();

        if (isSuccess)
        {
            UpdateTowerData(); // 업그레이드 성공 시 타워 정보 업데이트
            towerAttackRange.OnAttackRange(currentTower.transform.position, currentTower.Range);

            // 디버그 로그 추가
            Debug.Log("OnclickEventTowerUpgrade 메서드 호출됨");
        }
        else
        {
            // 실패 시 처리
            systemTextViewer.PrintText(SystemType.Money);

            // 디버그 로그 추가
            Debug.Log("OnclickEventTowerUpgrade 실패");
        }
    }

    public void OnclickEventTowerSell()
    {
        currentTower.Sell();
        OffPanel();

        // 디버그 로그 추가
        Debug.Log("OnclickEventTowerSell 메서드 호출됨");
    }
}
