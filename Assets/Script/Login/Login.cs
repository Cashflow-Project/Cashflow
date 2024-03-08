using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using UnityEngine.SceneManagement;

public class Login : MonoBehaviour
{
    private const string PASSWORD_REGEX = "((?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9]).{6,30})";
    [SerializeField] private string LoginEndpoint = "http://127.0.0.1:13756/account/login";
    [SerializeField] private string createEndpoint = "http://127.0.0.1:13756/account/create";

    [SerializeField] private TextMeshProUGUI alertText;
    [SerializeField] private Button loginButton;
    [SerializeField] private Button createButton;
    [SerializeField] private TMP_InputField usernameInputField;
    [SerializeField] private TMP_InputField passwordInputField;

    public void OnLoginClick()
    {
        alertText.text = "Signing in...";
        ActivateButtons(false);
        StartCoroutine(TryLogin());
    }

    public void OnCreateClick()
    {
        alertText.text = "Creating account...";
        ActivateButtons(false);
        StartCoroutine(TryCreate());
    }
    private IEnumerator TryLogin()
    {

        string username = usernameInputField.text;
        string password = passwordInputField.text;

        if(username.Length < 3 || username.Length > 30)
        {
            alertText.text = "Invalid username";
            ActivateButtons(true);
            yield break;
        }

        if (!Regex.IsMatch(password,PASSWORD_REGEX))
        {
            alertText.text = "Invalid credentials";
            ActivateButtons(true);
            yield break;
        }

        WWWForm form = new WWWForm();
        form.AddField("rUsername", username);
        form.AddField("rPassword", password);

        UnityWebRequest request = UnityWebRequest.Post(LoginEndpoint,form);
        var handler = request.SendWebRequest();

        float startTime = 0.0f;
        while (!handler.isDone)
        {
            startTime += Time.deltaTime;

            if(startTime > 10.0f)
            {
                break;
            }

            yield return null;
        }

        if(request.result == UnityWebRequest.Result.Success)
        {
            //Debug.Log(request.downloadHandler.text);
            LoginResponse response = JsonUtility.FromJson<LoginResponse>(request.downloadHandler.text);


            if (response.code == 0)//login success
            {

                ActivateButtons(false);
                GameAccount returnedAccount = JsonUtility.FromJson<GameAccount>(request.downloadHandler.text);
                alertText.text = $"{returnedAccount._id} Welcome "+((response.data.adminFlag == 1) ? " Admin" : "");
                //Debug.Log($"{username}:{password}");
                SceneManager.LoadScene("Main Menu");
            }
            else
            {
                switch (response.code)
                {
                    case 1:
                        alertText.text = "Invalid credentials";
                        //Debug.Log("case 1");
                        ActivateButtons(true);
                        break;
                    default :
                        alertText.text = "Corruption detected";
                        ActivateButtons(false);
                        break;
                }
                

            }


        }
        else
        {
            alertText.text = "Error connecting to the server...";
            //Debug.Log("Unable to connect to the server...");
            ActivateButtons(true);
        }
        

        yield return null;
    }

    private void ActivateButtons(bool on)
    {
        loginButton.interactable = on;
        createButton.interactable = on;
    }

    private IEnumerator TryCreate()
    {
        string username = usernameInputField.text;
        string password = passwordInputField.text;

        if (username.Length < 3 || username.Length > 30)
        {
            alertText.text = "Invalid username";
            ActivateButtons(true);
            yield break;
        }

        if (!Regex.IsMatch(password, PASSWORD_REGEX))
        {
            alertText.text = "Invalid credentials";
            ActivateButtons(true);
            yield break;
        }

        WWWForm form = new WWWForm();
        form.AddField("rUsername", username);
        form.AddField("rPassword", password);

        UnityWebRequest request = UnityWebRequest.Post(createEndpoint, form);
        var handler = request.SendWebRequest();

        float startTime = 0.0f;
        while (!handler.isDone)
        {
            startTime += Time.deltaTime;

            if (startTime > 10.0f)
            {
                break;
            }

            yield return null;
        }

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log(request.downloadHandler.text);
            CreateResponse response = JsonUtility.FromJson<CreateResponse>(request.downloadHandler.text);

            if (response.code == 0)
            {
                alertText.text = "Account has been created";
            }
            else
            {
                switch (response.code)
                {
                    case 1:
                        alertText.text = "Invalid credentials";
                        break;
                    case 2:
                        alertText.text = "Username already in use";
                        break;
                    case 3:
                        alertText.text = "Unsafe password";
                        break;
                    default:
                        alertText.text = "Corruption detected";
                        break;
                }
            }

        }
        else
        {
            alertText.text = "Error connecting to the server...";
            
        }
        ActivateButtons(true);

        yield return null;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
