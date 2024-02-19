using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Photon.Pun;

public class Timer : MonoBehaviourPunCallbacks
{

    public static Timer instace;

    [SerializeField]
    public bool Countdown = true;

    private float timerDuration = 2f * 60f;
    public float timer;

    [SerializeField]
    public TMP_Text firstMinutes;
    [SerializeField]
    public TMP_Text secondMinutes;
    [SerializeField]
    public TMP_Text Separator;
    [SerializeField]
    public TMP_Text firstSeconds;
    [SerializeField]
    public TMP_Text secondSeconds;

    public float flashTimer;
    public float flashDuration = 1f;
    void Start()
    {
        ResetTimer();
    }

    void Update()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            if (Countdown && timer > 0)
            {
                timer -= Time.deltaTime;
                UpdateTimerDisplay(timer);
            }
            else if (!Countdown && timer < timerDuration)
            {
                timer += Time.deltaTime;
                UpdateTimerDisplay(timer);
            }
            else
            {
                Flash();
                if ((GameManager.instace.state == GameManager.States.START_TURN || GameManager.instace.state == GameManager.States.ROLL_DICE)
                    && GameManager.instace.playerList[GameManager.instace.activePlayer].isInRedRoute == false)
                {
                    GameManager.instace.ActivateButton(false);
                    UIController.instance.InvestCanvas.SetActive(false);
                    UIController.instance.passButton.SetActive(false);
                    ResetTimer();
                    GameManager.instace.state = GameManager.States.SWITCH_PLAYER;
                }
                else if (GameManager.instace.state == GameManager.States.WAITING && GameManager.instace.playerList[GameManager.instace.activePlayer].isInRedRoute == false)
                {
                    UIController.instance.SetAllFalse(false);
                    GameManager.instace.ActivateButton(false);
                    UIController.instance.InvestCanvas.SetActive(false);
                    UIController.instance.passButton.SetActive(false);
                    ResetTimer();
                    GameManager.instace.state = GameManager.States.SWITCH_PLAYER;
                }
                else if (GameManager.instace.state == GameManager.States.WAITING && GameManager.instace.playerList[GameManager.instace.activePlayer].isInRedRoute == true
                    && GameManager.instace.playerList[GameManager.instace.activePlayer].isDrawButtonOn == true
                    && GameManager.instace.playerList[GameManager.instace.activePlayer].isSpendAlready == false)
                {
                    UIController.instance.drawButton.SetActive(false);
                    GameManager.instace.playerList[GameManager.instace.activePlayer].isDrawButtonOn = false;
                    SpendDeckController.instance.DrawCardToHand();
                    //delay
                    SpendDeckController.instance.PayCost();
                    //GameManager.instace.playerList[GameManager.instace.activePlayer].isSpendAlready = true;
                    UIController.instance.SetAllFalse(false);
                    GameManager.instace.ActivateButton(false);
                    UIController.instance.passButton.SetActive(false);
                    GameManager.instace.playerList[GameManager.instace.activePlayer].isInRedRoute = false;
                    ResetTimer();
                    GameManager.instace.state = GameManager.States.SWITCH_PLAYER;
                }
                else if (GameManager.instace.state == GameManager.States.WAITING && GameManager.instace.playerList[GameManager.instace.activePlayer].isInRedRoute == true
                    && GameManager.instace.playerList[GameManager.instace.activePlayer].isDrawButtonOn == false
                    && GameManager.instace.playerList[GameManager.instace.activePlayer].isSpendAlready == false)
                {

                    SpendDeckController.instance.PayCost();
                    UIController.instance.SetAllFalse(false);
                    GameManager.instace.ActivateButton(false);
                    UIController.instance.passButton.SetActive(false);
                    GameManager.instace.playerList[GameManager.instace.activePlayer].isInRedRoute = false;
                    ResetTimer();
                    GameManager.instace.state = GameManager.States.SWITCH_PLAYER;
                }
                else if (GameManager.instace.state == GameManager.States.WAITING && GameManager.instace.playerList[GameManager.instace.activePlayer].isInRedRoute == true
                    && GameManager.instace.playerList[GameManager.instace.activePlayer].isDrawButtonOn == false
                    && GameManager.instace.playerList[GameManager.instace.activePlayer].isSpendAlready == true)
                {

                    UIController.instance.SetAllFalse(false);
                    GameManager.instace.ActivateButton(false);
                    UIController.instance.passButton.SetActive(false);

                    GameManager.instace.playerList[GameManager.instace.activePlayer].isInRedRoute = false;
                    ResetTimer();
                    GameManager.instace.state = GameManager.States.SWITCH_PLAYER;
                }
            }
            if (GameManager.instace.state == GameManager.States.START_TURN)
            {
                ResetTimer();
            }

            photonView.RPC("TimeUpdateDisplayPunRPC", RpcTarget.All, timer);
        }
            
    }

    public void ResetTimer()
    {
        if (Countdown)
        {
            timer = timerDuration;
        }
        else
        {
            timer = 0;
        }
        SetTextDisplay(true);
    }

    private void UpdateTimerDisplay(float time)
    {
        float minites = Mathf.FloorToInt(time / 60);
        float seconds = Mathf.FloorToInt(time % 60);

        string currentTime = string.Format("{00:00}{01:00}",minites,seconds);
        firstMinutes.text = currentTime[0].ToString();
        secondMinutes.text = currentTime[1].ToString();
        firstSeconds.text = currentTime[2].ToString();
        secondSeconds.text = currentTime[3].ToString();
    }

    private void Flash()
    {
        if(Countdown && timer != 0)
        {
            timer = 0;
            UpdateTimerDisplay(timer);
        }

        if (!Countdown && timer != timerDuration)
        {
            timer = timerDuration;
            UpdateTimerDisplay(timer);
        }


        if (flashTimer <= 0)
        {
            flashTimer = flashDuration;
        }else if(flashTimer >= flashDuration/2)
        {
            flashTimer -= Time.deltaTime;
            SetTextDisplay(false);
        }else
        {
            flashTimer -= Time.deltaTime;
            SetTextDisplay(true);
        }
    }

    private void SetTextDisplay(bool on)
    {
        firstMinutes.enabled = on;
        secondMinutes.enabled = on;
        Separator.enabled = on;
        firstSeconds.enabled = on;
        secondSeconds.enabled = on;
    }

    [PunRPC]
    void TimeUpdateDisplayPunRPC(float time)
    {
       float minites = Mathf.FloorToInt(time / 60);
        float seconds = Mathf.FloorToInt(time % 60);

        string currentTime = string.Format("{00:00}{01:00}",minites,seconds);
        firstMinutes.text = currentTime[0].ToString();
        secondMinutes.text = currentTime[1].ToString();
        firstSeconds.text = currentTime[2].ToString();
        secondSeconds.text = currentTime[3].ToString();
    }
}
