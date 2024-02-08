using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtonController : MonoBehaviour
{
    [SerializeField]
    private SceneInformation sceneInfo;

    public void playGame()
    {
        sceneInfo.isFirstLoad = true;
        SceneManager.LoadScene("Character Select");
    }

    public void showInstruction()
    {
        SceneManager.LoadScene("Instruction");
    }

    public void goToGithub()
    {
        Application.OpenURL("");
    }
    
    public void quitGame()
    {
        Application.Quit();
    }
}
