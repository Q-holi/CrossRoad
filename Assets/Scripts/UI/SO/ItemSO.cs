using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{
    public string name;//card name
    public int attack;//card attack damage
    public int cost;//card cost
    public Sprite sprite;//card img
    public int option; //�÷��̾� ī�� ���� �Ǵ� ������� �Ǻ� 0�� ��� 1�� ���� 3�� ī�� ��ο�
    public float percent;//card drop%
}

[CreateAssetMenu(fileName = "ItemSO", menuName = "Scriptable Object/ItemSO")]
public class ItemSO : ScriptableObject
{
    public Item[] items;
}
