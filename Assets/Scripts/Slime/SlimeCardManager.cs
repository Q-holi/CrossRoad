using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using DG.Tweening;
using TMPro;

public class SlimeCardManager : MonoBehaviour
{
    public static SlimeCardManager Inst { get; private set; }
    void Awake() => Inst = this;

    [SerializeField] ItemSO itemSO;//플레리어 카드 형식 클래스
    [SerializeField] SlimeItemSO slimeItemSO;//슬라임카드 형식의 클래스 
    [SerializeField] GameObject cardPrefab;//사용자 카드 게임 오브젝트
    [SerializeField] GameObject slimecardPrefab;//몬스터 카드 게임 오브젝트
    [SerializeField] List<CardonlySlime> myCards;
    [SerializeField] List<SlimeCard> otherCards;
    [SerializeField] Transform cardSpawnPoint;
    [SerializeField] Transform otherCardSpawnPoint;
    [SerializeField] Transform myCardLeft;
    [SerializeField] Transform myCardRight;
    [SerializeField] GameObject TargetPicker;
    [SerializeField] ECardState eCardState;
    [SerializeField] TMP_Text mytrunCost;

    List<Item> itemBuffer;
    List<SlimeItem> slimeItemBuffer;
    CardonlySlime selectCard;
    Entity targetpicker;
    bool isMyCardDrag;
    bool onMyCardArea;
    bool ExistTargetPick => targetpicker != null;
    enum ECardState { Nothing, CanMouseOver, CanMouseDrag }
    int MaxCost = 7;
    int myTurnCost;

    public Item PopItem()//플레이어 카드 형식의 카드 뽑기 
    {
        if (itemBuffer.Count == 0)
            SetupItemBuffer();

        Item item = itemBuffer[0];
        itemBuffer.RemoveAt(0);
        return item;
    }
    public SlimeItem EPopItem()//몬스터 카드 형식의 카드 뽑기
    {
        if (slimeItemBuffer.Count == 0)
            SetupEnemyItemBuffer();

        SlimeItem enemyitem = slimeItemBuffer[0];
        slimeItemBuffer.RemoveAt(0);
        return enemyitem;
    }

    void SetupItemBuffer()
    {
        itemBuffer = new List<Item>(100);
        for (int i = 0; i < itemSO.items.Length; i++)
        {
            Item item = itemSO.items[i];
            for (int j = 0; j < item.percent; j++)
                itemBuffer.Add(item);
        }

        for (int i = 0; i < itemBuffer.Count; i++)
        {
            int rand = Random.Range(i, itemBuffer.Count);
            Item temp = itemBuffer[i];
            itemBuffer[i] = itemBuffer[rand];
            itemBuffer[rand] = temp;
        }
    }//플레이어 카드 클래스 형식의 정렬(셔플)
    void SetupEnemyItemBuffer()
    {
        slimeItemBuffer = new List<SlimeItem>(100);
        for (int i = 0; i < slimeItemSO.Slimeitems.Length; i++)
        {
            SlimeItem enemyitem = slimeItemSO.Slimeitems[i];
            for (int j = 0; j < enemyitem.percent; j++)
                slimeItemBuffer.Add(enemyitem);
        }

        for (int i = 0; i < slimeItemBuffer.Count; i++)
        {
            int rand = Random.Range(i, slimeItemBuffer.Count);
            SlimeItem temp = slimeItemBuffer[i];
            slimeItemBuffer[i] = slimeItemBuffer[rand];
            slimeItemBuffer[rand] = temp;
        }
    }// 몬스터 카드 클래스 형식의 정렬(셔플)

    void Start()
    {
        SetupItemBuffer();//게임 시작과 동시에 플레이어 카드 덱 리스트 셔플 호출
        SetupEnemyItemBuffer();//게임 시작과 동시에 몬스터 카드 덱 리스트 셔플 호출
        TurnManager.OnAddCard += AddCard;
        TurnManager.OnTurnStarted += OnTurnStarted;
    }

    void OnDestroy()
    {
        TurnManager.OnAddCard -= AddCard;
        TurnManager.OnTurnStarted -= OnTurnStarted;
    }

    void OnTurnStarted(bool myTurn)
    {
        if (myTurn)
            mytrunCost.text = "7";
    }

    void Update()
    {
        if (isMyCardDrag)
            CardDrag();

        DetectCardArea();
        SetECardState();
        ShowTargetPicker(ExistTargetPick);
    }
    private void ShowTargetPicker(bool isShow)
    {
        TargetPicker.SetActive(isShow);
        if (ExistTargetPick)
            TargetPicker.transform.position = targetpicker.transform.position;
    }

    public void AddCard(bool isMine)
    {
        if (isMine == true)
        {
            var cardObject = Instantiate(cardPrefab, cardSpawnPoint.position, Utils.QI);
            var card = cardObject.GetComponent<CardonlySlime>();
            card.Setup(PopItem(), isMine);
            myCards.Add(card);
        }
        else
        {
            var enemycardObject = Instantiate(slimecardPrefab, otherCardSpawnPoint.position, Utils.QI);
            var enemycard = enemycardObject.GetComponent<SlimeCard>();
            enemycard.Setup(EPopItem());
            otherCards.Add(enemycard);
        }
        SetOriginOrder(isMine);
        CardAlignment(isMine);
    }

    void SetOriginOrder(bool isMine)
    {
        int count = myCards.Count;
        for (int i = 0; i < count; i++)
        {
            var targetCard = myCards[i];
            targetCard?.GetComponent<Order>().SetOriginOrder(i);
        }
    }

    void CardAlignment(bool isMine)
    {
        List<PRS> originCardPRSs = new List<PRS>();
        if (isMine)
            originCardPRSs = RoundAlignment(myCardLeft, myCardRight, myCards.Count, 0.5f, Vector3.one * 1.9f);

        if (isMine == true)
        {
            var targetCards = myCards;
            for (int i = 0; i < targetCards.Count; i++)
            {
                var targetCard = targetCards[i];
                targetCard.originPRS = originCardPRSs[i];
                targetCard.MoveTransform(targetCard.originPRS, true, 0.7f);
            }
        }
    }

    List<PRS> RoundAlignment(Transform leftTr, Transform rightTr, int objCount, float height, Vector3 scale)
    {
        float[] objLerps = new float[objCount];
        List<PRS> results = new List<PRS>(objCount);

        switch (objCount)
        {
            case 1: objLerps = new float[] { 0.5f }; break;
            case 2: objLerps = new float[] { 0.27f, 0.73f }; break;
            case 3: objLerps = new float[] { 0.1f, 0.5f, 0.9f }; break;
            default:
                float interval = 1f / (objCount - 1);
                for (int i = 0; i < objCount; i++)
                    objLerps[i] = interval * i;
                break;
        }

        for (int i = 0; i < objCount; i++)
        {
            var targetPos = Vector3.Lerp(leftTr.position, rightTr.position, objLerps[i]);
            var targetRot = Utils.QI;
            if (objCount >= 4)
            {
                float curve = Mathf.Sqrt(Mathf.Pow(height, 2) - Mathf.Pow(objLerps[i] - 0.5f, 2));
                curve = height >= 0 ? curve : -curve;
                targetPos.y += curve;
                targetRot = Quaternion.Slerp(leftTr.rotation, rightTr.rotation, objLerps[i]);
            }
            results.Add(new PRS(targetPos, targetRot, scale));
        }
        return results;
    }
    public void OneHandCardDestory()
    {
        Destroy(myCards[0].gameObject);
        myCards.RemoveAt(0);
        CardAlignment(true);
    }

    public bool TryPutCard(bool isMine)
    {
        if (isMine == true)
        {
            CardonlySlime card = selectCard;
            var targetCards = myCards;
            int Pcost = int.Parse(mytrunCost.text);
            Pcost += card.item.cost;
            if (Pcost <= MaxCost)
            {
                mytrunCost.text = Pcost.ToString();
                targetCards.Remove(selectCard);
                selectCard.transform.DOKill();
                DestroyImmediate(card.gameObject);
                if (isMine)
                {
                    SlimeEntityManager.Inst.BoosDamage(true, card.item.option, card.item.attack);
                    selectCard = null;
                }
                CardAlignment(isMine);
                return true;
            }
            else
            {
                targetCards.ForEach(x => x.GetComponent<Order>().SetMostFrontOrder(false));
                CardAlignment(isMine);
                return false;
            }
        }
        else
        {
            SlimeCard enemyCard = otherCards[0];
            var targetCards = otherCards;
            SlimeEntityManager.Inst.BoosDamage(false, enemyCard.SlimeItem.option, enemyCard.SlimeItem.value);
            targetCards.Remove(enemyCard);
            enemyCard.gameObject.SetActive(false);
            enemyCard.transform.DOKill();
            return true;
        }
    }

    #region MyCard

    public void CardMouseOver(CardonlySlime card)
    {
        if (eCardState == ECardState.Nothing)
            return;

        selectCard = card;
        EnlargeCard(true, card);
    }

    public void CardMouseExit(CardonlySlime card)
    {
        EnlargeCard(false, card);
    }

    public void CardMouseDown()
    {
        if (eCardState != ECardState.CanMouseDrag)
            return;

        isMyCardDrag = true;
    }

    public void CardMouseUp()
    {
        isMyCardDrag = false;

        if (eCardState != ECardState.CanMouseDrag)
            return;

        if (onMyCardArea)
            SlimeEntityManager.Inst.RemoveMyEmptyEntity();
        else
            TryPutCard(true);
    }


    void CardDrag()
    {
        if (eCardState != ECardState.CanMouseDrag)
            return;

        if (!onMyCardArea)
        {
            selectCard.MoveTransform(new PRS(Utils.MousePos, Utils.QI, selectCard.originPRS.scale), false);
        }
    }

    void DetectCardArea()
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(Utils.MousePos, Vector3.forward);
        int layer = LayerMask.NameToLayer("MyCardArea");
        onMyCardArea = Array.Exists(hits, x => x.collider.gameObject.layer == layer);
    }

    void EnlargeCard(bool isEnlarge, CardonlySlime card)
    {
        if (isEnlarge)
        {
            Vector3 enlargePos = new Vector3(card.originPRS.pos.x, -4.8f, -10f);
            card.MoveTransform(new PRS(enlargePos, Utils.QI, Vector3.one * 3.5f), false);
        }
        else
            card.MoveTransform(card.originPRS, false);

        card.GetComponent<Order>().SetMostFrontOrder(isEnlarge);
    }

    void SetECardState()//카드를 몇까지 내게 할수 있는가 
    {
        if (TurnManager.Inst.isLoading)
            eCardState = ECardState.Nothing;

        else if (!TurnManager.Inst.myTurn || myTurnCost >= MaxCost)
            eCardState = ECardState.CanMouseOver;

        else if (TurnManager.Inst.myTurn && myTurnCost < MaxCost)
            eCardState = ECardState.CanMouseDrag;
    }
    public void CardMouseDrag()
    {
        CardonlySlime card = selectCard;
        bool existTarget = false;
        foreach (var hit in Physics2D.RaycastAll(Utils.MousePos, Vector3.forward))
        {
            Entity entity = hit.collider?.GetComponent<Entity>();
            if (entity != null)
            {
                targetpicker = entity;
                existTarget = true;
                break;
            }
        }
        if (!existTarget)
        {
            targetpicker = null;
            selectCard.enabled = true;
        }
    }
    #endregion
}
