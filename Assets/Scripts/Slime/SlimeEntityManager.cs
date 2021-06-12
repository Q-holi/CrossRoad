using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SlimeEntityManager : MonoBehaviour
{
	public static SlimeEntityManager Inst { get; private set; }
	void Awake() => Inst = this;

	[SerializeField] GameObject entityPrefab;
	[SerializeField] GameObject cardPrefab;
	[SerializeField] GameObject damagePrefab;
	[SerializeField] List<SlimeEntity> myEntities;
	[SerializeField] List<SlimeEntity> otherEntities;
	[SerializeField] GameObject TargetPicker;
	[SerializeField] SlimeEntity myEmptyEntity;
	[SerializeField] SlimeEntity myBossEntity;
	[SerializeField] SlimeEntity otherBossEntity;

	bool ExistTargetPickEntity => targetPickEntity != null;
	bool ExistMyEmptyEntity => myEntities.Exists(x => x == myEmptyEntity);
	int MyEmptyEntityIndex => myEntities.FindIndex(x => x == myEmptyEntity);
	bool CanMouseInput => TurnManager.Inst.myTurn && !TurnManager.Inst.isLoading;

	SlimeEntity selectEntity;
	SlimeEntity targetPickEntity;
	WaitForSeconds delay1 = new WaitForSeconds(1);
	WaitForSeconds delay2 = new WaitForSeconds(2);



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
		AttackableReset(myTurn);

		if (!myTurn)
			StartCoroutine(AICo());
	}

	void Update()
	{
		ShowTargetPicker(ExistTargetPickEntity);
	}

	IEnumerator AICo()
	{
		SlimeCardManager.Inst.TryPutCard(false);
		yield return delay1;

		// attackable이 true인 모든 otherEntites를 가져와 순서를 섞는다
		var attackers = new List<SlimeEntity>(otherEntities.FindAll(x => x.attackable == true));
		for (int i = 0; i < attackers.Count; i++)
		{
			int rand = Random.Range(i, attackers.Count);
			SlimeEntity temp = attackers[i];
			attackers[i] = attackers[rand];
			attackers[rand] = temp;
		}

		// 보스를 포함한 myEntities를 랜덤하게 시간차 공격한다
		foreach (var attacker in attackers)
		{
			var defenders = new List<SlimeEntity>(myEntities);
			defenders.Add(myBossEntity);
			int rand = Random.Range(0, defenders.Count);
			//Attack(attacker, defenders[rand]);

			if (TurnManager.Inst.isLoading)
				yield break;

			yield return delay2;
		}
		TurnManager.Inst.EndTurn();
	}
	public void RemoveMyEmptyEntity()
	{
		if (!ExistMyEmptyEntity)
			return;

		myEntities.RemoveAt(MyEmptyEntityIndex);
	}

	public bool TryPutCardCheck(bool isMine)
	{
		return true;
	}

	public void EntityMouseDown(SlimeEntity entity)
	{
		if (!CanMouseInput)
			return;

		selectEntity = entity;
	}

	public void EntityMouseUp()
	{
		if (!CanMouseInput)
			return;

		selectEntity = null;
		targetPickEntity = null;
	}

	public void EntityMouseDrag()
	{
		if (!CanMouseInput || selectEntity == null)
			return;

		// other 타겟엔티티 찾기
		bool existTarget = false;
		foreach (var hit in Physics2D.RaycastAll(Utils.MousePos, Vector3.forward))
		{
			SlimeEntity entity = hit.collider?.GetComponent<SlimeEntity>();
			if (entity != null && !entity.isMine && selectEntity.attackable)
			{
				targetPickEntity = entity;
				existTarget = true;
				break;
			}
		}
		if (!existTarget)
			targetPickEntity = null;
	}

	IEnumerator CheckBossDie()
	{
		yield return delay2;

		if (myBossEntity.isDie)
			StartCoroutine(GameManager.Inst.GameOver(false));

		if (otherBossEntity.isDie)
			StartCoroutine(GameManager.Inst.GameOver(true));
	}
	public void BoosDamage(bool isMine, int option, int value)//ItemSO에서는 attack라고 되어있음 이부분은 수정이 필요)
	{
		if (isMine == true)
		{
			if (option == 0)
			{
				myBossEntity.DefenceShield(value);
				StartCoroutine(CheckBossDie());
			}
			else if (option == 1)
			{
				otherBossEntity.Damaged(value);
				StartCoroutine(CheckBossDie());
			}
			else
			{
				for (int i = 0; i < value; i++)
					SlimeCardManager.Inst.AddCard(true);
			}
		}
		else
		{
			if (option == 0)
			{
				otherBossEntity.DefenceShield(value);
				StartCoroutine(CheckBossDie());
			}
			else if (option == 11)
            {
				SlimeCardManager.Inst.OneHandCardDestory();
            }
            else
			{
				myBossEntity.Damaged(value);
				StartCoroutine(CheckBossDie());
			}


		}
	}


	public void Cheatmode(bool isMine)
	{
		var targetBossEntity = isMine ? myBossEntity : otherBossEntity;
		if (isMine == true)
			myBossEntity.KillCharacter();
		else
			otherBossEntity.KillCharacter();

		StartCoroutine(CheckBossDie());
	}// 캐릭터 피1로 만들기

	void ShowTargetPicker(bool isShow)
	{
		TargetPicker.SetActive(isShow);
		if (ExistTargetPickEntity)
			TargetPicker.transform.position = targetPickEntity.transform.position;
	}

	void SpawnDamage(int damage, Transform tr)
	{
		if (damage <= 0)
			return;

		var damageComponent = Instantiate(damagePrefab).GetComponent<Damage>();
		damageComponent.SetupTransform(tr);
		damageComponent.Damaged(damage);
	}

	public void AttackableReset(bool isMine)
	{
		var targetEntites = isMine ? myEntities : otherEntities;
		targetEntites.ForEach(x => x.attackable = true);
	}

}
