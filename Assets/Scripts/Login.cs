using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;
using System;
using System.Text.RegularExpressions;

public class Login : MonoBehaviour {
    // LOCAL : 'http://localhost:3000/api/v1'
    // CLOUD : 'https://sheltered-castle-75234.herokuapp.com/api/v1'
    private string url = "http://localhost:3000/api/v1";
	public GameObject username;
	public GameObject password;
	private string Username;
	private string Password;
	private String[] Lines;
	private string DecryptedPass;

    public void LoginButton()
    {
        StartCoroutine(AuthUser());
    }

    IEnumerator AuthUser()
    {
        WWWForm form = new WWWForm();
        form.AddField("username", Username);
        form.AddField("password", Password);
        using (UnityWebRequest www = UnityWebRequest.Post(url + "/auth/authUser", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                // Show results as text
                Debug.Log(www.downloadHandler.text);

                // Or retrieve results as binary data
                byte[] results = www.downloadHandler.data;
                Application.LoadLevel("Start Menu");
            }
        }
    }

    // Update is called once per frame
    void Update () {
		if (Input.GetKeyDown(KeyCode.Tab)){
			if (username.GetComponent<InputField>().isFocused){
				password.GetComponent<InputField>().Select();
			}
		}
		if (Input.GetKeyDown(KeyCode.Return)){
			if (Password != ""&&Password != ""){
				LoginButton();
			}
		}
		Username = username.GetComponent<InputField>().text;
		Password = password.GetComponent<InputField>().text;	
	}
}
