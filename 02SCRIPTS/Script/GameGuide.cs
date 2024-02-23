using UnityEngine;

public class GameGuide : MonoBehaviour
{
    public GameObject guidePanel;
    public GameObject guideButton; // 가이드 버튼 참조
    public string panelOpenSoundName = "PanelOpen"; // 패널 열릴 때 사운드 이름

    void Start()
    {
        guidePanel.SetActive(false); // 시작 시 가이드 패널 숨김
        guideButton.SetActive(true); // 시작 시 가이드 버튼 보임
    }

    public void ToggleGuide()
    {
        bool isPanelActive = guidePanel.activeSelf;

        guidePanel.SetActive(!isPanelActive); // 패널 활성화 상태 토글

        // 패널 상태에 따라 게임 일시 정지 또는 재개
        if (!isPanelActive)
        {
            // 패널이 열릴 때
            Time.timeScale = 0f; // 게임 일시 정지
            AudioManager.Instance.PlaySFX(panelOpenSoundName); // 사운드 재생
        }
        else
        {
            // 패널이 닫힐 때
            Time.timeScale = 1.0f; // 게임 재개
        }
    }
}
