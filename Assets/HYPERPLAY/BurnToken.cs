using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using System.Numerics;
using UnityEngine.Networking;

public class BurnToken : MonoBehaviour
{
    async public void OnBurnToken()
    {
        string account = await GetAccount();

        // call publicMint function https://docs.hyperplaygaming.com/api/send-contract-endpoint
        string jsonString = "{ \"contractAddress\": \"0xDD6ff2bA7fD02D19e8e4e1d99b65802eD9705437\", \"functionName\": \"publicBurn\", \"params\": [], \"abi\": [{ \"inputs\": [], \"name\": \"publicBurn\", \"outputs\": [], \"stateMutability\": \"nonpayable\", \"type\": \"function\" }, { \"inputs\": [], \"name\": \"publicMint\", \"outputs\": [], \"stateMutability\": \"nonpayable\", \"type\": \"function\" }], \"valueInWei\": \"0\", \"chain\": { \"chainId\": \"5\" } }";
        byte[] jsonBytes = System.Text.Encoding.UTF8.GetBytes(jsonString);

        UnityWebRequest request = new UnityWebRequest("localhost:9680/sendContract", "POST");
        request.uploadHandler = new UploadHandlerRaw(jsonBytes);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        await request.SendWebRequest();
        print("Error: " + request.error);
        print("Transaction Hash: " + request.downloadHandler.text);
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
        string account = response.Substring(2, response.Length - 4); // 0x638105AA1B69406560f6428aEFACe3DB9da83c64
        return account;
    }
}
