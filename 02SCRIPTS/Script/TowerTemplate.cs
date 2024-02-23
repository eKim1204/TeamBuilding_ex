using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class TowerTemplate : ScriptableObject
{
    public GameObject towerPrefab;
    public GameObject followTowerPrefab;
    public Weapon[] weapon;

    [System.Serializable]
    public struct Weapon
    {
        public Sprite sprite;
        public float damage;
        public float slow; // 감속 퍼센트
        public float buff; // 버프 퍼센트
        public float rate;
        public float range;
        public int cost;
        public int sell;
        public Vector3 scale; // 스케일
        public AudioClip firingSound; // 발사음 추가
    }
}
