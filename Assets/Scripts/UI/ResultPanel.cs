using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class ResultPanel : MonoBehaviour
{
	[SerializeField] TMP_Text resultTMP;
	public Animator transition;
	public float transitionTime = 2f;


	public void Show(string message)
	{
		resultTMP.text = message;
		transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.InOutQuad);
	}

	public void EndShow()
    {
		StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
	}

	public void Restart()
	{
		StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 0));
	}

	IEnumerator LoadLevel(int levelIndex)
	{
		transition.SetTrigger("Start");
		//´ë±â
		yield return new WaitForSeconds(transitionTime);
		//¿ÀÇÁ´× ¾À
		SceneManager.LoadScene(levelIndex);
	}

	void Start() => ScaleZero();

	[ContextMenu("ScaleOne")]
	void ScaleOne() => transform.localScale = Vector3.one;

	[ContextMenu("ScaleZero")]
	public void ScaleZero() => transform.localScale = Vector3.zero;
}
