using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TestSMS : MonoBehaviour
{

	public Button Add;
	public Button GetCoder;
	public Button VerifyCoder;
	public Button btnClear;
	public Button btnQuery;

	public InputField inputCoder;
	public InputField inputSmsid;
	public Text message;

	public AndroidJavaObject jo;
	// Use this for initialization
	void Start()
	{
		Add.onClick.AddListener(AddTest);
		GetCoder.onClick.AddListener(getCoder);
		VerifyCoder.onClick.AddListener(verifyCoder);
		btnClear.onClick.AddListener(clearMsg);
		btnQuery.onClick.AddListener(queryState);
	}

	void AddTest()
	{
		//防在Start初始化失败
		AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		jo = jc.GetStatic<AndroidJavaObject>("currentActivity");

		int res = jo.Call<int>("test1",12,13);
		message.text += "12+13="+res.ToString();
	}
	void getCoder()
	{
		jo.Call("requestSendMsg");
	}
	void verifyCoder()
	{
		jo.Call("verifiyCoder", inputCoder.text);
	}

	void clearMsg()
	{
		message.text = "";
		inputSmsid.text="";
	}

	void test2(string str)
	{
		message.text += str;
	}

	public void OnCoderReturn(string str)
	{ 
		message.text+=str;
	}

	public void queryState()
	{
		jo.Call("queryState", inputSmsid.text);
	}

	void Update()
	{
		if (Input.GetKey(KeyCode.Escape))
		{
			Application.Quit();
		}
	}
}
