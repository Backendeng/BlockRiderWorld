using System.Collections;
using System.Numerics;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class GetNFTs : MonoBehaviour
{


    public static int[] allNFTTokenIDs;

    public static async void GetAllNFTs() {
        string chain = "ethereum";
        string network = "mainnet";
        string account = PlayerPrefs.GetString("Account");
        string contract = "0x296749978AEa0134F712BFE3d83c3F65D1e85AeA";
        
        int lastokenID = 23;
        
        string[] accounts = new string[lastokenID];
        string[] tokenIds = new string[lastokenID];
        allNFTTokenIDs = new int[lastokenID];
        for (int i = 0; i < lastokenID; i++ ) {
            accounts[i] = account;
            tokenIds[i] = i.ToString();
        }

        List<BigInteger> batchBalances = await ERC1155.BalanceOfBatch(chain, network, contract, accounts, tokenIds);
        
        int index = 0;

        for (int i = 0; i < lastokenID; i++) {
            if ((int) batchBalances[i] > 0 ){
                allNFTTokenIDs[index] = i;
                index++;
            }

            print ("BalanceOfBatch: " + allNFTTokenIDs[i]);
        }

        print(index);
    }

    public static async void GetAllNFTsERC721() {

        string chain = "ethereum";
        string network = "mainnet";
        string contract = "0x36973e350f00f5094c8551b814e76b59c7e828e2";
        string account = PlayerPrefs.GetString("Account");
        int tokenIDStart  = 1;
        int amountOfTokenIdsToSearch  = 3333;
        int balanceSearched = 0;

        int balance = await ERC721.BalanceOf(chain, network, contract, account);
        print(balance);

        allNFTTokenIDs = new int[balance];

        for (int i = 1; i < amountOfTokenIdsToSearch; i++) {

            if (balanceSearched < balance)
            {
                string ownerOf = await ERC721.OwnerOf(chain, network, contract, (tokenIDStart + i).ToString());
                Debug.Log(ownerOf);
                if (ownerOf == account)
                {
                    Debug.Log("TokenID: " + (tokenIDStart + i));
                    allNFTTokenIDs[balanceSearched] = tokenIDStart + i;
                    balanceSearched++;
                }
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
