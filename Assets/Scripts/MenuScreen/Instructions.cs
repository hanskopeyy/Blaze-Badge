using UnityEngine;
using UnityEngine.SceneManagement;

public class Instructions : MonoBehaviour
{
    public void goBacktoMenu()
    {
        SceneManager.LoadScene("MenuScreen");
    }
}
