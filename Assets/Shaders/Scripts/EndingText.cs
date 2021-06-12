using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndingText : MonoBehaviour
{
    [SerializeField] GameObject killbossBtn;
    [SerializeField] GameObject savebossBtn;
    [SerializeField] GameObject textPannel;
    public Animator transition;
    public float transitionTime = 2f;
    public Text TalkText;
    private int index = 0;
    private int i = 0;
    public float speed;
    public Image background;
    public Sprite bgSprite1;
    public Sprite bgSprite2;
    public GameObject EndCursor;
    string[] text = {"나는 마왕에게 치명상을 입히고 일격을 가했다.","일격을 맞은 마왕은 그대로 쓰러지고 치열한 전투는 마무리되었다.",
       "마침내 인간과 마족 간의 끝없이 영원한 전쟁을 마무리 지을 수 있다.",
        "전생의 인류의 방패이였던 나는 과연 이 상황에서 무엇을 선택을 하였을까?","마왕의 목숨을 끊고 인류의 평화를 다시 한번 선물할 것인가?\n(마왕을 처형)",
        "하지만 현재의 나는 인류에게 배신을 당하여 죽음의 고통을 맛보았다.","과연 이 끝없이 영원한 전쟁을 마무리하여도 또 같은 상황이 생길 것이다.",
        "그렇다면 이 끝없이 영원한 전쟁을 진행 시켜도 나에게 영향은 없을 것이다.\n(마왕을 해방)",
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
            if (index > 14)
            {
                StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 3));
            }
            TalkText.text = "";
            StartCoroutine(Wait());
        }
    }
    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");
        //대기
        yield return new WaitForSeconds(transitionTime);
        //오프닝 씬
        SceneManager.LoadScene(levelIndex);
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

    private void BackgroundIndex()
    {
        switch (index)
        {
            case 0:
                background.sprite = bgSprite1;
                break;
            case 1:
                break;
            case 2:
                background.sprite = bgSprite2;
                break;
            case 8:
                killbossBtn.SetActive(true);
                savebossBtn.SetActive(true);
                textPannel.SetActive(false);
                break;
            default:
                break;
        }
    }
}
