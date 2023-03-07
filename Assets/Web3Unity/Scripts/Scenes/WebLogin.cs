using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

#if UNITY_WEBGL
public class WebLogin : MonoBehaviour
{

    public Text walletAddress;
    public GameObject connectWallet;
    public GameObject disConnectWallet;


    [DllImport("__Internal")]
    private static extern void Web3Connect();

    [DllImport("__Internal")]
    private static extern string ConnectAccount();

    [DllImport("__Internal")]
    private static extern void SetConnectAccount(string value);

    private int expirationTime;
    private string account; 

    public void OnLogin()
    {
        Web3Connect();
        OnConnected();
    }

    async private void OnConnected()
    {
        account = ConnectAccount();
        while (account == "") {
            await new WaitForSeconds(1f);
            account = ConnectAccount();
        };
        // save account for next scene
        PlayerPrefs.SetString("Account", account);
        walletAddress.text = account;
        connectWallet.SetActive(false);
        disConnectWallet.SetActive(true);
        GetNFTs.GetAllNFTs();
        // reset login message
        SetConnectAccount("");
        // load next scene
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void OnSkip()
    {
        // burner account for skipped sign in screen
        PlayerPrefs.SetString("Account", "");
        // move to next scene
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void OnSignOut()
    {
        // Clear Account
        PlayerPrefs.SetString("Account", "0x0000000000000000000000000000000000000001");
        // go to login scene
        // SceneManager.LoadScene(0);
        connectWallet.SetActive(true);
        disConnectWallet.SetActive(false);
        walletAddress.text = "wallet Address";
    }
}
#endif
