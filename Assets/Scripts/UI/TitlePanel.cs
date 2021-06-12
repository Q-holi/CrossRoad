using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitlePanel : MonoBehaviour
{
    [SerializeField] GameObject infoBossBtn;
    public void StartGameClick()
    {
        infoBossBtn.SetActive(false);
        GameManager.Inst.StartGame();
        Active(false);
    }

    public void Active(bool isActive)
    {
        gameObject.SetActive(isActive);
    }
}
