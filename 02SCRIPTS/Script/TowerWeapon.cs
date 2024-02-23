using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.XR;

public enum WeaponType { Cannon = 0, Laser, Slow, Buff }
public enum WeaponState { SearchTarget = 0, TryAttackCannon, TryAttackLaser,  } //열거형 공격대상 탐색,대상을 공격하는 함수정의 
public class TowerWeapon : MonoBehaviour
{
  [Header("Commons")]

    [SerializeField]
    private TowerTemplate towerTemplate;

    [SerializeField]
    private Transform spawnPoint;
    [SerializeField]
    private WeaponType weaponType;
   
    [Header("Cannon")]
    [SerializeField]
   private GameObject projectilePrefab;

   [Header("Laser")]
   [SerializeField] 
   private LineRenderer lineRenderer;
   [SerializeField]
   private Transform hitEffect;
   [SerializeField]
   private LayerMask tragetLayer;
   [SerializeField]
private float AddedDamage; // AddedDamage 변수 추가

[SerializeField]
private LayerMask targetLayer; // targetLayer 변수 추가
  private Tile ownerTile;
   [SerializeField]
private int numberOfProjectiles = 1; // 발사할 프로젝타일 수
[SerializeField]
private float projectileSpreadAngle = 10f; // 프로젝타일 사이의 각도

  
  

  private int level =0;
  private PlayerGold playerGold;
  private SpriteRenderer spriteRenderer;
  
  
  
  private WeaponState weaponState = WeaponState.SearchTarget; //타워 무기의 상태
  private Transform attackTarget = null; //공격 대상
  private EnemySpawner enemySpawner; //게임에 존재하는 적 정보 획득용

 public Sprite TowerSprite => towerTemplate.weapon[level].sprite;

    public float Damage => towerTemplate.weapon[level].damage;
    public float Rate => towerTemplate.weapon[level].rate;
    public float Range => towerTemplate.weapon[level].range;
   // public int UpgradeCost => Level < MaxLevel ? towerTemplate.weapon[level+1].cost : 0;
    public int SellCost => towerTemplate.weapon[level].sell;
    public int UpgradeCost => Level < MaxLevel ? towerTemplate.weapon[level + 1].cost :0;
    

  

 public int Level => level + 1;
 public int MaxLevel => towerTemplate.weapon.Length;

 public float Slow => towerTemplate.weapon[level].slow;
 public float Buff => towerTemplate.weapon[level].buff;

 public WeaponType WeaponType => weaponType;

 private TowerSpawner towerSpawner;

 private int buffLevel;

  
  public float addedDamage
  {
    set => AddedDamage = Mathf.Max(0, value);
    get => AddedDamage;
  }
  public int BuffLevel
  {
    set => buffLevel = Mathf.Max(0, value);
    get => buffLevel;
  }

  public void Setup ( TowerSpawner towerSpawner, EnemySpawner enemySpawner, PlayerGold playerGold, Tile ownerTile)
  {
    
    spriteRenderer = GetComponent<SpriteRenderer>();
    this.towerSpawner = towerSpawner;
    this.enemySpawner = enemySpawner;
    this.playerGold = playerGold;
    this.ownerTile = ownerTile;
   
    //ChangeState(WeaponState.SearchTarget);

    if (weaponType == WeaponType.Cannon || weaponType == WeaponType.Laser)
    {
        ChangeState(WeaponState.SearchTarget);
    }
  }

  public void ChangeState(WeaponState newState)
  {
    StopCoroutine(weaponState.ToString());//이전에 재생중이던 상태 종료
    weaponState = newState; // 상태 변경
    StartCoroutine(weaponState.ToString()); //새로운 상태 재생
  }

private void Update()
    {
        
        if (attackTarget != null)
        {
            RotateToTarget();
        }
    }
    private void RotateToTarget()
    {
        float dx = attackTarget.position.x - transform.position.x;
         float dy = attackTarget.position.y - transform.position.y;

        float degree = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, degree);
    }


  private Transform FindClosestAttackTarget()
{
    Transform closestTarget = null;
    float closestDistSqr = Mathf.Infinity;

    for (int i = 0; i < enemySpawner.EnemyList.Count; ++i)
    {
        float distance = Vector3.Distance(enemySpawner.EnemyList[i].transform.position, transform.position);

        if (distance <= towerTemplate.weapon[level].range && distance < closestDistSqr)
        {
            closestDistSqr = distance;
            closestTarget = enemySpawner.EnemyList[i].transform;
        }
    }

    return closestTarget;
}
private bool IsPossibleToAttackTarget()
    {
        if (attackTarget == null)
        {
            return false;
        }

        float distance = Vector3.Distance(attackTarget.position, transform.position);
        if (distance > towerTemplate.weapon[level].range)
        {
            attackTarget = null;
            return false;
        }
        return true;
    }

private IEnumerator SearchTarget()
{
    while (true)
    {
        attackTarget = FindClosestAttackTarget();

        if (attackTarget != null)
        {
            if (weaponType == WeaponType.Cannon)
            {
                ChangeState(WeaponState.TryAttackCannon);
            }
            else if (weaponType == WeaponType.Laser)
            {
                ChangeState(WeaponState.TryAttackLaser);
            }
        }
        yield return null;
    }
}


    private IEnumerator TryAttackCannon()
    {
        while (true)
        {
           

            if ( IsPossibleToAttackTarget() == false )
            { 
                
                ChangeState(WeaponState.SearchTarget);
                break;
            }
            

           yield return new WaitForSeconds(towerTemplate.weapon[level].rate); // 수정된 부분

            SpawnProjectile();
        }

    }

    private IEnumerator TryAttackLaser()
    {
        EnableLaser();
        while ( true )
        {
            if ( IsPossibleToAttackTarget() == false )
            {
                DisableLaser();
                ChangeState(WeaponState.SearchTarget);
                break;
            }
            SpawnLaser();

            yield return null;
        }
    }
     private void EnableLaser()
    {
        lineRenderer.gameObject.SetActive(true);
        hitEffect.gameObject.SetActive(true);
    }
    private void DisableLaser()
    {
        lineRenderer.gameObject.SetActive(false);
        hitEffect.gameObject.SetActive(false);
    }
    private void SpawnLaser()
    {
        Vector3 direction = attackTarget.position - spawnPoint.position;
        RaycastHit2D[] hit = Physics2D.RaycastAll(spawnPoint.position, direction, towerTemplate.weapon[level].range, targetLayer);

        for ( int i = 0; i < hit.Length; ++ i)
        {
            if ( hit[i].transform == attackTarget )
            {
                lineRenderer.SetPosition(0, spawnPoint.position);
                lineRenderer.SetPosition(1, new Vector3(hit[i].point.x, hit [i].point.y,0 )+ Vector3.back);
                hitEffect.position = hit[i].point;
                float damage = towerTemplate.weapon[level].damage + AddedDamage;
                attackTarget.GetComponent<EnemyHP>().TakeDamage(damage * Time.deltaTime);
            }
        }
    }
public bool Upgrade()
{
    if (playerGold.CurrentGold < towerTemplate.weapon[level + 1].cost)
    {
        return false;
    }

    level++;
    spriteRenderer.sprite = towerTemplate.weapon[level].sprite;

    playerGold.CurrentGold -= towerTemplate.weapon[level].cost;

    transform.localScale = towerTemplate.weapon[level].scale;

    towerSpawner.OnBuffAllBuffTowers();

    return true;
}










      private void SpawnProjectile()
    {
        List<Transform> targetsInRange = FindTargetsInRange();

        for (int i = 0; i < numberOfProjectiles; i++)
        {
            if (targetsInRange.Count == 0) break;

            Transform target = targetsInRange[i % targetsInRange.Count]; // 여러 적 중 하나 선택
            float angle = projectileSpreadAngle * (i - numberOfProjectiles / 2);
            Quaternion rotation = Quaternion.Euler(0, 0, angle);

            GameObject clone = Instantiate(projectilePrefab, spawnPoint.position, spawnPoint.rotation * rotation);
            clone.GetComponent<Projectile>().Setup(target, towerTemplate.weapon[level].damage);
            float damage = towerTemplate.weapon [level].damage + AddedDamage;
        }
         audioSource.PlayOneShot(towerTemplate.weapon[level].firingSound);
    }

    private List<Transform> FindTargetsInRange()
    {
        List<Transform> targetsInRange = new List<Transform>();
        
        foreach (var enemy in enemySpawner.EnemyList)
        {
            if (Vector3.Distance(transform.position, enemy.transform.position) <= towerTemplate.weapon[level].range)
            {
                targetsInRange.Add(enemy.transform);
            }
        }

        return targetsInRange;
    }


    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
public void Sell()
{
    playerGold.CurrentGold += towerTemplate.weapon[level].sell;
    ownerTile.IsBuildTower = false;
    Destroy(gameObject);
}

public void OnBuffAroundTower()
{
    GameObject[] towers = GameObject.FindGameObjectsWithTag("Tower");
    
    for ( int i = 0; i < towers.Length; ++ i )
    {
        TowerWeapon weapon = towers[i].GetComponent<TowerWeapon>();

        if ( weapon.buffLevel > Level )
        {
            continue;
        }
        
        if ( Vector3.Distance(weapon.transform.position, transform.position) <= towerTemplate.weapon[level].range )
        {
            if (weapon.WeaponType == WeaponType.Cannon || weapon.WeaponType == WeaponType.Laser )
            {
                weapon.AddedDamage = weapon.Damage * (towerTemplate.weapon[level].buff);
                weapon.BuffLevel = level;
            }
        }
    }
}
}
