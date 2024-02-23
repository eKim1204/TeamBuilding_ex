using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayerMP playerMP; // PlayerMP 스크립트 참조
    public int skillMPCost = 40; // 스킬 사용에 필요한 MP

    public PlayerGold playerGold; // PlayerGold 스크립트 참조
    public int skillGoldCost = 30; // 골드 변환 스킬 사용에 필요한 MP
    public SystemTextViewer systemTextViewer; // SystemTextViewer 스크립트 참조
     [SerializeField]
    private AudioClip freezeSound; // Freeze 효과음
    private AudioSource audioSource;


    private void Start()
    {
        // 참조 체크
        if (playerMP == null || playerGold == null || systemTextViewer == null)
        {
            Debug.LogError("PlayerMP, PlayerGold, or SystemTextViewer reference is not set in GameManager.");
        }
         audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("AudioSource component not found on GameManager GameObject.");
        }
    }

    public void ActivateFreezeEnemiesSkill()
    {
        if (TryUseMP(skillMPCost))
        {
            Enemy[] enemies = FindObjectsOfType<Enemy>();
            foreach (Enemy enemy in enemies)
            {
                Movement2D movement = enemy.GetComponent<Movement2D>();
                if (movement != null)
                {
                    movement.MoveSpeed = 0; // 이동 속도를 0으로 설정
                }
            }

            StartCoroutine(ResetEnemiesSpeed(enemies, 3f));
             if (audioSource != null && freezeSound != null)
            {
                audioSource.PlayOneShot(freezeSound);
            }
        }
    }

    public void ActivateGoldChangeSkill()
    {
        if (TryUseMP(skillGoldCost))
        {
            int goldChange = 30;
            bool isLoss = false;
            int result = Random.Range(1, 5);

            switch (result)
            {
                case 1:
                    goldChange = 0; // 모든 골드 잃음
                    isLoss = true;
                    break;
                case 2:
                    goldChange /= 2; // 반만 잃음
                    isLoss = true;
                    break;
                case 3:
                    goldChange *= 2; // 2배로 증가
                    break;
                case 4:
                    goldChange *= 3; // 3배로 증가
                    break;
            }

            playerGold.CurrentGold += goldChange;
            systemTextViewer.PrintGoldChangeMessage(goldChange, isLoss);
        }
    }


    private bool TryUseMP(int mpCost)
    {
        if (playerMP.CurrentMP >= mpCost)
        {
            playerMP.CurrentMP -= mpCost;
            return true;
        }
        else
        {
            systemTextViewer.PrintText(SystemType.MP);
            Debug.Log("Not enough MP to use the skill");
            return false;
        }
    }

    private IEnumerator ResetEnemiesSpeed(Enemy[] enemies, float delay)
    {
        yield return new WaitForSeconds(delay);

        foreach (Enemy enemy in enemies)
        {
            if (enemy != null)
            {
                Movement2D movement = enemy.GetComponent<Movement2D>();
                if (movement != null)
                {
                    movement.ResetMoveSpeed();
                }
            }
        }
    }
    void Update()
{
    
    if (Input.GetKeyDown(KeyCode.W))
    {
        ActivateFreezeEnemiesSkill();
    }

 
    if (Input.GetKeyDown(KeyCode.Q))
    {
        ActivateGoldChangeSkill();
    }
}
public void SetGameSpeed(float speed)
{
    Time.timeScale = speed;
}
public void OnSpeedChange(float speed)
{
    SetGameSpeed(speed);
    // UI에 배속 상태 업데이트
    // 예: speedDisplayText.text = speed + "x";
}


}
