using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SlimeItem
{ 
    public string name;//card name
    public int value;
    public Sprite sprite;//card img
    public float percent;//card drop%
    public int option; //몬스터 카드 공격 또는 방어인지 판별 0은 방어 1이 공격
}

[CreateAssetMenu(fileName = "SlimeItemSO", menuName = "Scriptable Object/SlimeItemSO")]
public class SlimeItemSO : ScriptableObject
{
    public SlimeItem[] Slimeitems;
}
