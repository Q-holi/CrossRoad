using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class KillBossEndText : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 2f;
    public Text TalkText;
    private int index = 0;
    private int i = 0;
    public float speed;
    public Image background;
    public Sprite bgSprite0;
    public Sprite bgSprite1;
    public Sprite bgSprite2;
    public Sprite bgSprite3;
    public Sprite bgSprite4;
    public Sprite bgSprite5;
    public GameObject EndCursor;
    string[] text = {"인간들에게 배신을 당했어도 나는 인간들을 수호해야한다. \n 나는 그러한 존재이기 때문이다. 다짐을 하였다.",
        "나는 천천히 마왕의 앞으로 다가갔다.","이 끝없이 영원한 전쟁을 나의 손으로 끝낸다.","","나는 마왕의 가면을 두 조각으로 부숴버렸다.\n 이제 마왕의 마력은 " +
            "소실되고 소멸하겠지."," ","그렇게 나는 마왕의 소멸시키고 인간들의 세계로 다시 내려가는듯 하였다.","......... "," ","어디서부터 잘못이었을까?",
            "어떻게 내가 마왕이었던걸까?","나는 너무 혼란스러운 상황에 어디선가 들려오는 목소리를 들었다.","...............",
    "그렇다 나 또한 타락하며 인간들을 배신하여 마왕이 되었던 것이다.\n 이제서야 깨달았다.",".....","'끝없이 영원한 전쟁'은 막을 수 없다."};

    private void Start()
    {
        StartCoroutine(Wait());
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            index++;
            if (index >= 15)
            {

                StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex - 13));
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
            case 2:
                background.sprite = bgSprite1;
                break;
            case 4:
                background.sprite = bgSprite2;
                break;
            case 6:
                background.sprite = bgSprite3;
                break;
            case 7:
                background.sprite = bgSprite0;
                break;
            case 8:
                background.sprite = bgSprite4;
                break;
            case 9:
                background.sprite = bgSprite0;
                break;
            case 12:
                background.sprite = bgSprite5;
                break;
            case 14:
                background.sprite = bgSprite0;
                break;
            default:
                break;
        }
    }
}
