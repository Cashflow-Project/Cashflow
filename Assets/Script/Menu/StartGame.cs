using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < SaveSettings.players.Length; i++)
        {
            SaveSettings.players[i] = "CPU";
        }
    }

    public void StartTheGame(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
