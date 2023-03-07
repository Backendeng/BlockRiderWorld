using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WalletLogin: MonoBehaviour
{
    public Toggle rememberMe;
    public Text walletAddress;
    public GameObject connectWallet;
    public GameObject disConnectWallet;

    void Start() {
        GetNFTs.GetAllNFTs();
        // if remember me is checked, set the account to the saved account
        if(PlayerPrefs.HasKey("RememberMe") && PlayerPrefs.HasKey("Account"))
        {
            if (PlayerPrefs.GetInt("RememberMe") == 1 && PlayerPrefs.GetString("Account") != "")
            {
                // move to next scene
                // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                walletAddress.text = PlayerPrefs.GetString("Account");
                connectWallet.SetActive(false);
                disConnectWallet.SetActive(true);
                GetNFTs.GetAllNFTs();
            } else {
                connectWallet.SetActive(true);
                disConnectWallet.SetActive(false);
            }
        } else {
                connectWallet.SetActive(true);
                disConnectWallet.SetActive(false);
        }
    }

    async public void OnLogin()
    {
        // get current timestamp
        int timestamp = (int)(System.DateTime.UtcNow.Subtract(new System.DateTime(1970, 1, 1))).TotalSeconds;
        // set expiration time
        int expirationTime = timestamp + 60;
        // set message
        string message = expirationTime.ToString();
        // sign message
        string signature = await Web3Wallet.Sign(message);
        // verify account
        string account = await EVM.Verify(message, signature);
        int now = (int)(System.DateTime.UtcNow.Subtract(new System.DateTime(1970, 1, 1))).TotalSeconds;
        // validate
        if (account.Length == 42 && expirationTime >= now) {
            // save account
            PlayerPrefs.SetString("Account", account);
            if (rememberMe.isOn)
                PlayerPrefs.SetInt("RememberMe", 1);
            else
                PlayerPrefs.SetInt("RememberMe", 0);
            print("Account: " + account);

            walletAddress.text = account;
            connectWallet.SetActive(false);
            disConnectWallet.SetActive(true);
            GetNFTs.GetAllNFTs();
            // load next scene
            // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    public void OnLogOut()
    {
        PlayerPrefs.SetString("Account", "");
        connectWallet.SetActive(true);
        disConnectWallet.SetActive(false);
        walletAddress.text = "wallet Address";
    }
}
