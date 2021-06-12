using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class EnemyCard : MonoBehaviour
{
    [SerializeField] SpriteRenderer character;
    [SerializeField] TMP_Text valueTMP;

    public EnemyItem EnemyItem;
    public PRS originPRS;


    public void Setup(EnemyItem EnemyItem)
    {
        this.EnemyItem = EnemyItem;

        character.sprite = this.EnemyItem.sprite;
        valueTMP.text = this.EnemyItem.value.ToString();
    }

    public void MoveTransform(PRS prs, bool useDotween, float dotweenTime = 0)
    {
        if (useDotween)
        {
            transform.DOMove(prs.pos, dotweenTime);
            transform.DORotateQuaternion(prs.rot, dotweenTime);
            transform.DOScale(prs.scale, dotweenTime);
        }
        else
        {
            transform.position = prs.pos;
            transform.rotation = prs.rot;
            transform.localScale = prs.scale;
        }
    }
}
