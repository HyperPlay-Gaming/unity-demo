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

        // call publicBurn function https://docs.hyperplaygaming.com/api/rpc-endpoint/rpc-call-examples/send-transaction
        string jsonString = "{ \"request\": { \"method\": \"eth_sendTransaction\", \"params\": [{ \"from\": \"" + account + "\", \"to\": \"0xDD6ff2bA7fD02D19e8e4e1d99b65802eD9705437\", \"value\": \"0\", \"data\": \"0x14f97b63\" }] }, \"chain\": { \"chainId\": \"5\" } }";
        byte[] jsonBytes = System.Text.Encoding.UTF8.GetBytes(jsonString);

        UnityWebRequest request = new UnityWebRequest("localhost:9680/rpc", "POST");
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
