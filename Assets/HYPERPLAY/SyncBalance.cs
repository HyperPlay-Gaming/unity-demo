using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using System.Numerics;
using UnityEngine.Networking;
using UnityEngine.UI;

public class SyncBalance : MonoBehaviour
{
    public GameObject Player;
    public Text PlayerBalance;

    async public void OnSyncBalance()
    {
        // https://docs.gaming.chainsafe.io/erc20#balance-of_3
        string chain = "ethereum";
        string network = "goerli";
        string contract = "0xDD6ff2bA7fD02D19e8e4e1d99b65802eD9705437";
        string account = await GetAccount();
        BigInteger balanceOf = await ERC20.BalanceOf(chain, network, contract, account);

        // display on button
        PlayerBalance.text = "Sync Balance: " + balanceOf.ToString();

        if (balanceOf > 0)
        {
            // change player color to red
            Player.GetComponent<Renderer>().material.color = Color.red;
        }
        else
        {
            Player.GetComponent<Renderer>().material.color = Color.white;
        }
    }

    async private static Task<string> GetAccount() {
        string jsonString = "{\"request\":{\"method\":\"eth_accounts\"},\"chain\":{\"chainId\":\"5\"}}";
        byte[] jsonBytes = System.Text.Encoding.UTF8.GetBytes(jsonString);

        UnityWebRequest request = new UnityWebRequest("localhost:9680/rpc", "POST");
        request.uploadHandler = new UploadHandlerRaw(jsonBytes);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        await request.SendWebRequest();
        string response = request.downloadHandler.text; // ["0x638105AA1B69406560f6428aEFACe3DB9da83c64"]
        print(response);
        string account = response.Substring(2, response.Length - 4); // 0x638105AA1B69406560f6428aEFACe3DB9da83c64
        return account;
    }
}
