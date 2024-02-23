using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardManager : MonoBehaviour
{
    public PlayerGold playerGold;
    public PlayerMP playerMP;
    public GameObject rewardSelectionUI; // 보상 선택 UI 참조
    public WaveSystem waveSystem; // WaveSystem 참조 추가

    public void ChooseGoldReward()
    {
        playerGold.CurrentGold += 300;
        ProceedToNextWave();
    }

    public void ChooseMPReward()
    {
        playerMP.CurrentMP += 200;
        ProceedToNextWave();
    }

    private void ProceedToNextWave()
    {
        CloseRewardUI();
        waveSystem.StartWave(); // 다음 웨이브 시작
    }

    private void CloseRewardUI()
    {
        // 보상 선택 UI 비활성화
        rewardSelectionUI.SetActive(false);
    }
}

