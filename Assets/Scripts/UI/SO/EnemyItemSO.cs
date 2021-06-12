using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyItem
{
    public string name;//card name
    public int value;
    public Sprite sprite;//card img
    public float percent;//card drop%
    public int option; //몬스터 카드 공격 또는 방어인지 판별 0은 방어 1이 공격
}

[CreateAssetMenu(fileName = "EnemyItemSO", menuName = "Scriptable Object/EnemyItemSO")]
public class EnemyItemSO : ScriptableObject
{
    public EnemyItem[] Enemyitems;
}

