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
    public int option; //���� ī�� ���� �Ǵ� ������� �Ǻ� 0�� ��� 1�� ����
}

[CreateAssetMenu(fileName = "EnemyItemSO", menuName = "Scriptable Object/EnemyItemSO")]
public class EnemyItemSO : ScriptableObject
{
    public EnemyItem[] Enemyitems;
}

