using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class Entity : MonoBehaviour
{
    [SerializeField] Item item;
    [SerializeField] SpriteRenderer entity;
    [SerializeField] SpriteRenderer character;
    [SerializeField] TMP_Text nameTMP;
    [SerializeField] TMP_Text attackTMP;
    [SerializeField] TMP_Text costTMP;
    [SerializeField] TMP_Text healthTMP;
    [SerializeField] TMP_Text shieldTMP;
    [SerializeField] GameObject shieldIMG;

    public int attack;
    public int cost;
    public int health;
    public bool isMine;
    public bool isDie;
    public bool isBossOrEmpty;
    public bool attackable;
    public Vector3 originPos;

    void Start()
    {
        TurnManager.OnTurnStarted += OnTurnStarted;
    }

    void OnDestroy()
    {
        TurnManager.OnTurnStarted -= OnTurnStarted;
    }

    void OnTurnStarted(bool myTurn)
    {
        if (isBossOrEmpty)
            return;
    }

    public void Setup(Item item)
    {
        attack = item.attack;
        cost = item.cost;

        this.item = item;
        character.sprite = this.item.sprite;
        nameTMP.text = this.item.name;
        attackTMP.text = attack.ToString();
        costTMP.text = cost.ToString();
    }

    void OnMouseDown()
    {
        if (isMine)
            EntityManager.Inst.EntityMouseDown(this);
    }

    void OnMouseUp()
    {
        if (isMine)
            EntityManager.Inst.EntityMouseUp();
    }

    void OnMouseDrag()
    {
        if (isMine)
            EntityManager.Inst.EntityMouseDrag();
    }
    public bool DefenceShield(int value)
    {
        int shield = int.Parse(shieldTMP.text);
        shield += value;
        shieldIMG.SetActive(true);
        shieldTMP.text = shield.ToString();
        return true;
    }
    public void KillCharacter()
    {
        healthTMP.text = "1";
        shieldTMP.text = "0";
    }

    public bool Damaged(int damage)
    {
        int health = int.Parse(healthTMP.text);
        int shield = int.Parse(shieldTMP.text);
        if (shield <= 0)
        {
            health -= damage;
            healthTMP.text = health.ToString();
            shieldIMG.SetActive(false);
        }
        else
        {
            shield -= damage;
            Debug.Log(damage + "데미지 들어감");
            int Remainder = shield;
            Debug.Log(Remainder + "잔여 쉴드량");
            if (Remainder < 0)
            {
                health += Remainder;
                healthTMP.text = health.ToString();
                shieldTMP.text = "0";
                shieldIMG.SetActive(false);

            }
            int RemainderShield = int.Parse(shieldTMP.text);
            if (RemainderShield != 0)
                shieldTMP.text = Remainder.ToString();
        }
        if (health <= 0)
        {
            isDie = true;
            return true;
        }
        return false;
    }

    public void MoveTransform(Vector3 pos, bool useDotween, float dotweenTime = 0)
    {
        if (useDotween)
            transform.DOMove(pos, dotweenTime);
        else
            transform.position = pos;
    }
}
