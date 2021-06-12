using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class SlimeCard : MonoBehaviour
{
    [SerializeField] SpriteRenderer character;
    [SerializeField] TMP_Text valueTMP;

    public SlimeItem SlimeItem;
    public PRS originPRS;


    public void Setup(SlimeItem EnemyItem)
    {
        this.SlimeItem = EnemyItem;

        character.sprite = this.SlimeItem.sprite;
        valueTMP.text = this.SlimeItem.value.ToString();
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
