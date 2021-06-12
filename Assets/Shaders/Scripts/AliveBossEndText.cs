using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AliveBossEndText : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 4f;
    public Text TalkText;
    private int index = 0;
    private int i = 0;
    public float speed;
    public Image background;
    public Sprite bgSprite0;
    public Sprite bgSprite1;
    public Sprite bgSprite2;
    public Sprite bgSprite3;
    public GameObject EndCursor;
    string[] text = {"내가 과연 이 마왕을 죽여서 끝없이 영원한 전쟁을 멈출 수 있을까?\n" +
            "잠시 생각 속으로 들어간 나는 결심을 내렸다.",
        "치명상을 입고 체력을 다 소모한 마왕 앞에서 나는 빛의 주문으로 마왕을 치유하기 시작했다.","왜인지 웃음이 나온다.\n" +
            "복수에 대한 기대감인가? ",
        "금방 체력은 회복한 마왕은 어리둥절한 모습으로 나를 바라본다.","나의 가장 깊은 곳에서 소리가 들려온다.",
        "복....수.....\n 고.....통.......","늦었다 나는 이미 인간에 대한 증오심으로 뒤덮여있다.","인류의 방패인 나를 버린 인간들에게 자비는 없다.",
        "이렇게 나는 마왕가 인간 세상으로 내려가 배신에 대한 복수를 이룰 것이니.",""
       };

    private void Start()
    {
        StartCoroutine(Wait());
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            index++;
            if (index > 8)
            {

                StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex - 14));
            }
            TalkText.text = "";
            StartCoroutine(Wait());
        }
        
    }

    IEnumerator Wait()
    {
        BackgroundIndex();
        EndCursor.SetActive(false);
        TalkText.text = "";
        for (i = 0; i < text[index].Length; i++)
        {
            TalkText.text += text[index][i].ToString();
            yield return new WaitForSeconds(speed);
        }
        EndCursor.SetActive(true);
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");
        //대기
        yield return new WaitForSeconds(transitionTime);
        //오프닝 씬
        SceneManager.LoadScene(levelIndex);
    }

    private void BackgroundIndex()
    {
        switch (index)
        {
            case 1:
                background.sprite = bgSprite1;
                break;
            case 2:
                background.sprite = bgSprite2;
                break;
            case 4:
                background.sprite = bgSprite0;
                break;
            case 6:
                background.sprite = bgSprite3;
                break;
            default:
                break;
        }
    }
}
