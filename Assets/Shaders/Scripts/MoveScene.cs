using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveScene : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 2f;
    public void MainScene()
    {
        SceneManager.LoadScene(0);
    }

    public void OptionScene()
    {
        SceneManager.LoadScene(2);
    }

    public void DictionaryScene()
    {
        SceneManager.LoadScene(3);
    }
    public void NoaniMap()
    {
        SceneManager.LoadScene(4);
    }

    public void MapScene()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 3));
    }
    public void OpenningScene()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");
        //대기
        yield return new WaitForSeconds(transitionTime);
        //오프닝 씬
        SceneManager.LoadScene(levelIndex);
    }
}
