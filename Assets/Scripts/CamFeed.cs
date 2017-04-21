using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using System;
using System.Text;
using UnityEngine;
using UnityEngine.UI;


public class CamFeed : MonoBehaviour {

	// Use this for initialization
//    public GameObject output;
	public Text Seq;
    private Texture2D tx2d;
//	private WebCamTexture webCamTexture;
    private UdpClient udpClient;
//	private IPEndPoint RemoteIpEndPoint;
//	private TcpClient tcpClient;
	private IAsyncResult asyncResult;
	public Vector3 oldPosition;
	public Vector3 newPosition;
	private float runner; 
	private bool udpIsclose;


	void Awake () {
        Debug.Log ("Start Braa!!!");
		oldPosition = Vector3.zero ;
		newPosition = Vector3.zero ;
		runner = 1f;

//        var deviceName = WebCamTexture.devices[0].name;

//        webCamTexture = new WebCamTexture();
//		tx2d = new Texture2D(480, 360);
//		webCamTexture.Play();
        udpClient = new UdpClient(8080);
//		RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);
        //tcpClient = new TcpClient(11000);
        try
        {
            udpClient.Connect("192.168.0.111", 8080);

            //udpClient.Connect("192.168.1.118", 8080);
            //tcpClient.Connect("192.168.1.116", 11000);
        }
        catch (Exception e)
        {
            Console.Write(e.ToString());
        }
		asyncResult = udpClient.BeginReceive( null, null );

	}
	
	// Update is called once per frame
	void FixedUpdate () {
//        output.GetComponent<RawImage>().texture = webCamTexture;
//        tx2d.SetPixels(webCamTexture.GetPixels());
//        tx2d.Apply();
//        Byte[] sendBytes = tx2d.EncodeToJPG(40);
//        udpClient.Send(sendBytes, sendBytes.Length);
		//asyncResult = udpClient.BeginReceive( null, null );
		//asyncResult.AsyncWaitHandle.WaitOne( TimeSpan.FromSeconds(0.033));

		if (asyncResult.IsCompleted && runner>= 0.8f) {
			Debug.Log ("Received!!");
			try {	
				IPEndPoint remoteEP = null;
				byte[] receivedData = udpClient.EndReceive (asyncResult, ref remoteEP);
				string returnData = Encoding.ASCII.GetString (receivedData);
				UDPJson temp = JsonUtility.FromJson<UDPJson> (returnData);
				Seq.text = temp.header.seq + "";
				Debug.Log (temp.header.seq);
				oldPosition = newPosition;
				newPosition = new Vector3 (temp.pose.position.x * 100f, temp.pose.position.y * 100f, temp.pose.position.z * 100f);
				runner = 0f;
				TranslatePosition(Vector3.Lerp(oldPosition, newPosition, runner));
				runner += 0.33f;
				//TranslatePosition (new Vector3 (temp.pose.position.x * 100f, temp.pose.position.y * 100f, temp.pose.position.z * 100f));
//				transform.parent.transform.position = new Vector3(temp.pose.position.x*100f,temp.pose.position.y*100f,temp.pose.position.z*100f);
//				transform.rotation = new Quaternion(temp.pose.orientation.x,temp.pose.orientation.y,temp.pose.orientation.z,temp.pose.orientation.w);

				// EndReceive worked and we have received data and remote endpoint
			} catch (Exception e) {
				// EndReceive failed and we ended up here
			}
			asyncResult = udpClient.BeginReceive (null, null);
		} else if (runner <= 0.8F){
			Debug.Log ("---- " + runner);
			TranslatePosition(Vector3.Lerp(oldPosition, newPosition, runner));
			runner += 0.33f;
		
		}
	}

	void TranslatePosition(Vector3 input){
		transform.parent.transform.position = input;
		Vector3 pos = transform.parent.transform.position;
		Vector3 center = new Vector3 (0f,3.8f, 2f); 
		Quaternion rot = Quaternion.AngleAxis(-1.16f,Vector3.up); // get the desired rotation
		Vector3 dir = pos - center; // find current direction relative to center
		dir = rot * dir; // rotate the direction
		transform.parent.transform.position = center + dir; // define new position
		rot = Quaternion.AngleAxis(-9.67f,Vector3.forward); // get the desired rotation
		dir = pos - center; // find current direction relative to center
		dir = rot * dir; // rotate the direction
		transform.parent.transform.position = center + dir; // define new position

	}

	public void closeUDP(){
		udpClient.Close ();
	}

//	Vector3 linear_interpolate(float y1,float y2, int mu){
//		return (y1 * (1 - mu) + y2 * mu);
//	}

}
//posisiton  y *(-1)

