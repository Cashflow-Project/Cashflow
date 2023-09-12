using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public Text first;
    public Text second;
    public Text third;
    // Start is called before the first frame update
    void Start()
    {
        first.text = "1st: " + SaveSettings.winners[0];
        second.text = "2nd: " + SaveSettings.winners[1];
        third.text = "3rd" + SaveSettings.winners[2];
    }

    public void BackButton(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
