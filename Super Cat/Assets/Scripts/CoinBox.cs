using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CoinBox : MonoBehaviour {

    public Text myText;
    public Rigidbody2D NoCoinBoxPrefab;
    public Transform coinBox;

    void Update()
    {
        GameObject myText = GameObject.Find("CoinText");
        Text textObject = myText.GetComponent<Text>();

        textObject.text = "Coins: " + GameMaster.gm.coin.ToString();
    }

    public void GetCoin(CoinBox box)
    {
        GameMaster.GetCoin(box);
        Destroy(box.gameObject, 0.2f);
        Instantiate(NoCoinBoxPrefab, coinBox.position, coinBox.rotation);
    }
}
