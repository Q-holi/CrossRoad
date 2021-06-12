using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class EntityManager : MonoBehaviour
{
	public static EntityManager Inst { get; private set; }
	public Animator camAnim;
	void Awake() => Inst = this;
	[SerializeField] GameObject entityPrefab;
	[SerializeField] GameObject cardPrefab;
	[SerializeField] GameObject damagePrefab;
	[SerializeField] List<Entity> myEntities;
	[SerializeField] List<Entity> otherEntities;
	[SerializeField] GameObject TargetPicker;
	[SerializeField] Entity myEmptyEntity;
	[SerializeField] Entity myBossEntity;
	[SerializeField] Entity otherBossEntity;
	[SerializeField] Transform mybossTrasnsform;
	[SerializeField] Transform otherBossTrasnsform;

	bool ExistTargetPickEntity => targetPickEntity != null;
	bool ExistMyEmptyEntity => myEntities.Exists(x => x == myEmptyEntity);
	int MyEmptyEntityIndex => myEntities.FindIndex(x => x == myEmptyEntity);
	bool CanMouseInput => TurnManager.Inst.myTurn && !TurnManager.Inst.isLoading;

	Entity selectEntity;
	Entity targetPickEntity;
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
		CardManager.Inst.TryPutCard(false);
		yield return delay1;

		// attackable이 true인 모든 otherEntites를 가져와 순서를 섞는다
		var attackers = new List<Entity>(otherEntities.FindAll(x => x.attackable == true));
		for (int i = 0; i < attackers.Count; i++)
		{
			int rand = Random.Range(i, attackers.Count);
			Entity temp = attackers[i];
			attackers[i] = attackers[rand];
			attackers[rand] = temp;
		}

		// 보스를 포함한 myEntities를 랜덤하게 시간차 공격한다
		foreach (var attacker in attackers)
		{
			var defenders = new List<Entity>(myEntities);
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

	public void EntityMouseDown(Entity entity)
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
			Entity entity = hit.collider?.GetComponent<Entity>();
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
				CamShake();
				otherBossEntity.Damaged(value);
				SpawnDamage(value, otherBossTrasnsform);
				StartCoroutine(CheckBossDie());
			}
			else
			{
				for (int i = 0; i < value; i++)
					CardManager.Inst.AddCard(true);
			}
		}
		else
		{
			if (option == 0)
			{
				otherBossEntity.DefenceShield(value);
				StartCoroutine(CheckBossDie());
			}
			else if(option == 11)
            {
				CardManager.Inst.OneHandCardDestory();
			}
			else
			{
				CamShake();
				myBossEntity.Damaged(value);
				SpawnDamage(value, mybossTrasnsform);
				StartCoroutine(CheckBossDie());
			}

		}
	}
	public void CamShake()
	{
		camAnim.SetTrigger("shake");
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
