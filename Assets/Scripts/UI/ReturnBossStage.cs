using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnBossStage : MonoBehaviour
{
    public void BackBossStage()
    {
        SceneManager.LoadScene(11);
    }
}
