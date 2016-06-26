using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public enum PayType
{
	ZhiFuBao,
	WeiXin,
}

public class BmobPayManager : MonoBehaviour
{
	public static BmobPayManager Instance { get; private set; }

	public GameObject panel;
	public Text messageText;
	private PayType payType = PayType.ZhiFuBao ;
	private float price;
	private string goodsName;

	// Use this for initialization
	void Start()
	{
		if(Instance==null)
			Instance = this;
		messageText.text = "";
	}

	/// <summary>
	/// 点击购买
	/// </summary>
	/// <param name="goodsprice"></param>
	/// <param name="name"></param>
	public void OnBuyButtonClick(float goodsprice,string name)
	{
		Debug.Log(goodsprice + " " + name);
		price = goodsprice;
		goodsName = name;
		panel.SetActive(true);
	}

	/// <summary>
	/// 关闭面板
	/// </summary>
	public void OnCloseClick()
	{
		panel.SetActive(false);
		print("close");
	}

	/// <summary>
	/// 确认购买
	/// </summary>
	public void OnSureClick()
	{
		AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
		if (payType == PayType.ZhiFuBao)
		{
			jo.Call("payByZhiFuBao",price,goodsName);
		}
		else
		{
			jo.Call("payByWeiXin", price, goodsName);
		}
		jo.Dispose();
		panel.SetActive(false);
	}

	public void OnToggleZhiFuBaoClick(bool isSelect)
	{
		if (isSelect==true)
		{
			payType = PayType.ZhiFuBao;
			Debug.Log("Alipay");
		}
	}

	public void OnToggleWeiXinClick(bool isSelect)
	{
		if (isSelect==true)
		{
			payType = PayType.WeiXin;
			Debug.Log("WeChat");
		}
	}

	/// <summary>
	/// 返回的支付结果
	/// </summary>
	/// <param name="str"></param>
	public void OnPayResultReturn(string str)
	{           
		panel.SetActive(false);
		messageText.text = str;
		string[] argv = str.Split('|');
		switch (argv[0])
		{
			case "0":
				messageText.text = "支付失败:"+str[1];
				break;
			case "1":
				messageText.text = "支付成功!!!";
				break;
			case "2":
				messageText.text = "网络错误...";
				break;
			default:
				break;
		}
	}


}
