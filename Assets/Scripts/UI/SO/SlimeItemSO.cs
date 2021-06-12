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
    public int option; //���� ī�� ���� �Ǵ� ������� �Ǻ� 0�� ��� 1�� ����
}

[CreateAssetMenu(fileName = "SlimeItemSO", menuName = "Scriptable Object/SlimeItemSO")]
public class SlimeItemSO : ScriptableObject
{
    public SlimeItem[] Slimeitems;
}
