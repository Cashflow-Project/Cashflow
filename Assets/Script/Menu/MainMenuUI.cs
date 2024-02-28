using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using System;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    public GameObject howToPlayPage;
    public GameObject noTouchBg;
    public Canvas page1;
    public Canvas page2;
    public Canvas page3;
    public Canvas page4;
    public Canvas page5;
    public Canvas page6;

    public Button Btn1;
    public Button Btn2;
    public Button Btn3;
    public Button Btn4;
    public Button Btn5;
    public Button Btn6;

    public Button closeHTPBtn;

    public void OnStartGamePressed()
    {
        SceneManager.LoadScene("Lobby");
    }

    public void OnQuitPressed()
    {
        SceneManager.LoadScene("Login");
        //Application.Quit();
    }
    void Update()
    { 
        if(page1.enabled == true)
        {
            Btn1.enabled = false;

            Btn2.enabled = true;
            Btn3.enabled = true;
            Btn4.enabled = true;
            Btn5.enabled = true;
            Btn6.enabled = true;
        }
        if (page2.enabled == true)
        {
            Btn2.enabled = false;

            Btn1.enabled = true;
            Btn3.enabled = true;
            Btn4.enabled = true;
            Btn5.enabled = true;
            Btn6.enabled = true;
        }
        if (page3.enabled == true)
        {
            Btn3.enabled = false;

            Btn1.enabled = true;
            Btn2.enabled = true;
            Btn4.enabled = true;
            Btn5.enabled = true;
            Btn6.enabled = true;
        }
        if (page4.enabled == true)
        {
            Btn4.enabled = false;

            Btn1.enabled = true;
            Btn2.enabled = true;
            Btn3.enabled = true;
            Btn5.enabled = true;
            Btn6.enabled = true;
        }
        if (page5.enabled == true)
        {
            Btn5.enabled = false;

            Btn1.enabled = true;
            Btn2.enabled = true;
            Btn3.enabled = true;
            Btn4.enabled = true;
            Btn6.enabled = true;
        }
        if (page6.enabled == true)
        {
            Btn6.enabled = false;

            Btn1.enabled = true;
            Btn2.enabled = true;
            Btn3.enabled = true;
            Btn4.enabled = true;
            Btn5.enabled = true;
        }
    }

    private void Start()
    {
        page1.enabled = true;
        page2.enabled = false;
        page3.enabled = false;
        page4.enabled = false;
        page5.enabled = false;
        page6.enabled = false;

    }

    public void HowToPlay()
    {
        noTouchBg.SetActive(true);
        howToPlayPage.SetActive(true);
    }

    public void CloseHowToPlay()
    {
        noTouchBg.SetActive(false);
        howToPlayPage.SetActive(false);
    }

    public void OpenPage1()
    {
        page1.enabled = true;
        page2.enabled = false;
        page3.enabled = false;
        page4.enabled = false;
        page5.enabled = false;
        page6.enabled = false;
    }

    public void OpenPage2()
    {
        page2.enabled = true;
        page1.enabled = false;
        page3.enabled = false;
        page4.enabled = false;
        page5.enabled = false;
        page6.enabled = false;
    }

    public void OpenPage3()
    {
        page3.enabled = true;
        page1.enabled = false;
        page2.enabled = false;
        page4.enabled = false;
        page5.enabled = false;
        page6.enabled = false;
    }

    public void OpenPage4()
    {
        page4.enabled = true;
        page1.enabled = false;
        page2.enabled = false;
        page3.enabled = false;
        page5.enabled = false;
        page6.enabled = false;
    }

    public void OpenPage5()
    {
        page5.enabled = true;
        page1.enabled = false;
        page2.enabled = false;
        page3.enabled = false;
        page4.enabled = false;
        page6.enabled = false;
    }

    public void OpenPage6()
    {
        page6.enabled = true;
        page1.enabled = false;
        page2.enabled = false;
        page3.enabled = false;
        page4.enabled = false;
        page5.enabled = false;
    }
}
