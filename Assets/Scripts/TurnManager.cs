using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;
using TMPro;

public class TurnManager : MonoBehaviour
{
	public static TurnManager Inst { get; private set; }
	void Awake() => Inst = this;

	[Header("Develop")]
	[SerializeField] [Tooltip("시작 턴 모드를 정합니다")] ETurnMode eTurnMode;
	[SerializeField] [Tooltip("카드 배분이 매우 빨라집니다")] bool fastMode;
	[SerializeField] [Tooltip("시작 카드 개수를 정합니다")] int startCardCount;
	[SerializeField] TMP_Text mymanaCost;

	[Header("Properties")]
	public bool isLoading; // 게임 끝나면 isLoading을 true로 하면 카드와 엔티티 클릭방지
	public bool myTurn;

	enum ETurnMode { Random, My, Other }
	WaitForSeconds delay05 = new WaitForSeconds(0.5f);
	WaitForSeconds delay07 = new WaitForSeconds(0.7f);

	public static Action<bool> OnAddCard;
	public static event Action<bool> OnTurnStarted;


	void GameSetup()//게임 모드 첫턴이 누구에게 시작을 할 것인지 
	{
		if (fastMode)
			delay05 = new WaitForSeconds(0.05f);

		switch (eTurnMode)
		{
			case ETurnMode.Random:
				myTurn = Random.Range(0, 2) == 0;
				break;
			case ETurnMode.My:
				myTurn = true;
				break;
			case ETurnMode.Other:
				myTurn = false;
				break;
		}
	}

	public IEnumerator StartGameCo()
	{
		GameSetup();
		isLoading = true;

		for (int i = 0; i < startCardCount; i++)
		{
			yield return delay05;
			OnAddCard?.Invoke(true);
		}
		OnAddCard?.Invoke(false);
		StartCoroutine(StartTurnCo());
	}

	IEnumerator StartTurnCo()
	{
		isLoading = true;
		if (myTurn)
			GameManager.Inst.Notification("My Trun");

		yield return delay07;
		OnAddCard?.Invoke(myTurn);
		isLoading = false;
		OnTurnStarted?.Invoke(myTurn);
	}

	public void EndTurn()
	{
		myTurn = !myTurn;
		mymanaCost.text = "7";
		StartCoroutine(StartTurnCo());
	}
}
