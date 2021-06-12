using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OpenningText : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 2f;
    public Text TalkText;
    private int index = 0;
    private int i = 0;
    public float speed;
    public Image background;
    public Sprite bgSprite1;
    public Sprite bgSprite2;
    public Sprite bgSprite3;
    public Sprite bgSprite4;
    public Sprite bgSprite5;
    public Sprite bgSprite6;
    public Sprite bgSprite7;
    public GameObject EndCursor;
    string[] text = {
        "나는 인류의 방패로 칭송받아온 대 마법사다.\n마법의 극의에 달해 영생에 가까운 삶을 살 수 있는 나는 오랫동안 그들을 수호해왔다.",
        "마족이라는 인류 공공의 적이 있던 인류는 그들에 맞서기 위해 하나로 뭉칠 수 있었지만, 오랜 세월 내 품안에 숨어 번영해온 인류는 그 내면에 잠재된 탐욕을 잠재우지 못하고 서로 분열하기 시작했다.",

        "결국 그들은 서로 전쟁을 하기 시작했고, 나는 인류의 수호자라는 내 칭호에 걸맞게 그 전쟁으로 인해 고통받고있는 백성들을 보호 했다.\n물론 전쟁중인 인간들은 수호 대상에서 제외시켰다.",
        "그리하면 전쟁을 시작한 그들은 마족의 공격도 막아내야 하니 전쟁을 오래 지속하지 못하겠지, 나는 그렇게 생각했다.",
        "그러나 그건 내 착오였다.",

        "인간들은 내가 없어도 자기 자신을 지킬 수 있을 정도의 힘을 나도 모르는새 길러 왔던 것이다.\n그리고는 마족과 손을 잡고 나를 공격해오기 시작했다.",
        "그들의 사소한 탐욕을 중재하지 못한 내 탓일까?\n내면에서 무언가 치밀어 올라왔지만 나를 믿고 따라주는 이들이 있으니 그들 만큼은 지켜내야겠지 그렇게 다짐했다.",

        "그러나 그 다짐마저도 얼마 가지 않아 내부의 배신자에게 당해 무너지고 말았다.\n마을을 지켜주는 답례라며 보내온 선물들 사이에 함정이 있었다.",
        "나는 배신자들에게 잡혀 적국으로 넘겨졌고, 그들은 나를 마족들에게 팔아넘겼다.",
        "순수한 선의로써, 동족으로써 믿고 지켜온 인간들에 대한 신뢰가 무너져 내렸다.",

        "오랜기간 침략을 방해해온 나를 마족들은 며칠후 한 장소에 매달아두고 공개처형을 시킬 셈인듯 했다.",
        "그러나 선의를 배신으로 보답받은 나는 복수심에 불타 절대 이런곳에서 죽을 수 없었다.",
        "나는 감옥에서 남은 마력을 끌어 모아 내게 부활의 마법을 새겼다.\n크나큰 금기를 저지르는 짓인것은 알고 있지만, 내게 그런걸 생각할 여유는 없었다.",

        "며칠뒤 나는 마족들의 영토 한가운데 광장 같은 곳에서 공개적으로 화형을 당했다.\n반드시 복수하겠다며 울부짖으며 나는 잿더미가 되었다.",

        "그리고 눈을 뜬 나는, 어떤 도시의 용병마법사로 전생한 모양이었다."," "
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
            if(index > 14)
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
        for (i = 0; i < text[index].Length ; i++)
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
            case 3:
                break;
            case 4:
                break;
            case 5:
                background.sprite = bgSprite3;
                break;
            case 6:
                break;
            case 7:
                background.sprite = bgSprite4;
                break;
            case 8:
                break;
            case 9:
                break;
            case 10:
                background.sprite = bgSprite5;
                break;
            case 11:
                break;
            case 12:
                break;
            case 13:
                background.sprite = bgSprite6;
                break;
            case 14:
                background.sprite = bgSprite7;
                break;
            default:
                break;
        }
    }
}
