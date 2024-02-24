using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Photon.Pun;

public class Timer : MonoBehaviourPunCallbacks
{

    public static Timer instace;

    [SerializeField]
    public bool Countdown = true;

    private float timerDuration = 1f * 30f;
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
    private bool isSynchronizing;
    public float flashTimer;
    public float flashDuration = 1f;
    void Start()
    {
        ResetTimer();
    }

    void Update()
    {
        if (photonView.IsMine)
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
                //timer <= 0
                Flash();
                photonView.RPC("TimeLessThanZero", RpcTarget.All);
            }
            /*
            if (GameManager.instace.state == GameManager.States.START_TURN)
            {
                ResetTimer();
            }*/
        }
        else
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
                //timer <= 0
                Flash();
                photonView.RPC("TimeLessThanZero", RpcTarget.All);
                
            }
            /*
            if (GameManager.instace.state == GameManager.States.START_TURN)
            {
                ResetTimer();
            }*/
        }

        if (GameManager.instace.state == GameManager.States.START_TURN)
        {
            ResetTimer();
        }

    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // Send the timer value to other players
            stream.SendNext(timer);
        }
        else
        {
            // Receive the timer value from the network
            timer = (float)stream.ReceiveNext();
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

    public void UpdateTimerDisplay(float time)
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
    void TimeLessThanZero()
    {
            if ((GameManager.instace.state == GameManager.States.START_TURN || GameManager.instace.state == GameManager.States.ROLL_DICE)
                    && GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].isInRedRoute == false)
            {
            UIController.instance.cardShow.enabled = false;
                GameManager.instace.ActivateButton(false);
                UIController.instance.InvestCanvas.SetActive(false);
                UIController.instance.passButton.SetActive(false);
                UIController.instance.SellListFromMarketCanvas.SetActive(false);
                UIController.instance.MarketPayButton.SetActive(false); ;
                UIController.instance.MarketSellButton.SetActive(false);
                ResetTimer();
                GameManager.instace.state = GameManager.States.SWITCH_PLAYER;
            }
            else if (GameManager.instace.state == GameManager.States.WAITING && GameManager.instace.playerList[PhotonNetwork.LocalPlayer.ActorNumber - 1].isInRedRoute == false)
            {
            UIController.instance.cardShow.enabled = false;
            UIController.instance.SetAllFalse(false);
                GameManager.instace.ActivateButton(false);
                UIController.instance.InvestCanvas.SetActive(false);
                UIController.instance.passButton.SetActive(false);
                UIController.instance.SellListFromMarketCanvas.SetActive(false);
                UIController.instance.MarketPayButton.SetActive(false); ;
                UIController.instance.MarketSellButton.SetActive(false);
            ResetTimer();
                GameManager.instace.state = GameManager.States.SWITCH_PLAYER;
            }
            else if (GameManager.instace.state == GameManager.States.WAITING && GameManager.instace.playerList[GameManager.instace.activePlayer].isInRedRoute == true
                && GameManager.instace.playerList[GameManager.instace.activePlayer].isDrawButtonOn == true
                && GameManager.instace.playerList[GameManager.instace.activePlayer].isSpendAlready == false)
            {
            UIController.instance.cardShow.enabled = false;
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
            UIController.instance.cardShow.enabled = false;
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
            UIController.instance.cardShow.enabled = false;
            UIController.instance.SetAllFalse(false);
                GameManager.instace.ActivateButton(false);
                UIController.instance.passButton.SetActive(false);

                GameManager.instace.playerList[GameManager.instace.activePlayer].isInRedRoute = false;
                ResetTimer();
                GameManager.instace.state = GameManager.States.SWITCH_PLAYER;
            }
        
        
    }
}
