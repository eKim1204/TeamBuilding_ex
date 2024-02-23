using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum SystemType { Money = 0, Build, MP } 

public class SystemTextViewer : MonoBehaviour
{
    private TextMeshProUGUI textSystem;
    private TMPAlpha tMPAlpha;

    private void Awake()
    {
        textSystem = GetComponent<TextMeshProUGUI>();
        tMPAlpha = GetComponent<TMPAlpha>();
    }

 public void PrintText(SystemType type)
{
    switch (type)
    {
        case SystemType.Money:
            textSystem.text = "Not enough money...";
            break;
        case SystemType.Build:
            textSystem.text = "Invalid build tower...";
            break;
        case SystemType.MP:
            textSystem.text = "Not enough MP...";
            break;
    }
    tMPAlpha.FadeOut();
}
 [SerializeField] private AudioClip winSound;
[SerializeField] private AudioClip loseSound;
[SerializeField] private AudioSource audioSource;

public void PrintGoldChangeMessage(int goldChange, bool isLoss)
{
    string message = isLoss ? "Lost " : "Earned ";
    textSystem.text = "Gold " + message + Mathf.Abs(goldChange) + "!";

    // 효과음 재생
    audioSource.clip = isLoss ? loseSound : winSound;
    audioSource.Play();

    tMPAlpha.FadeOut();
}

}
