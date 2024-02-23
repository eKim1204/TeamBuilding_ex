using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class TowerSpawner : MonoBehaviour
{
    [SerializeField]
    private TowerTemplate[] towerTemplate;
  // [SerializeField]
   //private GameObject towerPrefab;
   //[SerializeField]
   //private int towerBuildGold = 50;

   [SerializeField]
   private EnemySpawner enemySpawner; //현재 맵에 존재하는 적 리스트 정보를 얻기 위해..
   [SerializeField]
   private PlayerGold playerGold;
   
   [SerializeField]
   private SystemTextViewer systemTextViewer;

   private bool isOnTowerButton = false;
   private GameObject followTowerClone = null;
   private int towerType;

   public void ReadyToSpawnTower(int type)
   {
    towerType = type;
    if (isOnTowerButton == true)
    {
        return;
    }
    if (towerTemplate[towerType].weapon[0].cost > playerGold.CurrentGold)
    {
        systemTextViewer.PrintText(SystemType.Money);
        return;
    }
    isOnTowerButton = true;

    followTowerClone = Instantiate(towerTemplate[towerType].followTowerPrefab);
    StartCoroutine("OnTowerCancelSystem");
   }

   public void SpawnTower(Transform tileTransform)
   {
    if ( isOnTowerButton == false)
    {
        return;
    }
   // if ( towerBuildGold > playerGold.CurrentGold )
  // if (towerTemplate.weapon[0].cost > playerGold.CurrentGold)
    //{
       // systemTextViewer.PrintText(SystemType.Money);
       // return;
    //}
    Tile tile = tileTransform.GetComponent<Tile>();
    //타워 건설 가능 여부 확인
    if ( tile.IsBuildTower == true)
    {
        systemTextViewer.PrintText(SystemType.Build);
        return;
    }
    isOnTowerButton = false;
    tile.IsBuildTower = true;
   // playerGold.CurrentGold -= towerBuildGold;
   playerGold.CurrentGold -= towerTemplate[towerType].weapon[0].cost;

    Vector3 position = tileTransform.position + Vector3.back;
    //선택한타일의 위치에 건설
    //GameObject clone = Instantiate(towerPrefab, tileTransform.position, Quaternion.identity);
    GameObject clone = Instantiate(towerTemplate[towerType].towerPrefab, position, Quaternion.identity);
    //타워 무기에 enemySpawner 정보 전달
    clone.GetComponent<TowerWeapon>().Setup(this, enemySpawner, playerGold, tile );

    //Instantiate(towerPrefab, tileTransform.position, Quaternion.identity);
    OnBuffAllBuffTowers();
    
    Destroy(followTowerClone);

    StopCoroutine("OnTowerCancelSystem");
   }
    private IEnumerator OnTowerCancelSystem()
{
    while (true)
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButtonDown(1))
        {
            isOnTowerButton = false;
            if (followTowerClone != null)
            {
                Destroy(followTowerClone);
            }
            break;
        }
        yield return null;
    }
}
public void OnBuffAllBuffTowers()
{
    GameObject[] towers = GameObject.FindGameObjectsWithTag("Tower");
    
    for ( int i = 0; i < towers.Length; ++ i )
    {
        TowerWeapon weapon = towers[i].GetComponent<TowerWeapon>();
        if( weapon.WeaponType == WeaponType.Buff)
        {
            weapon.OnBuffAroundTower();
        }
    }
}

}
