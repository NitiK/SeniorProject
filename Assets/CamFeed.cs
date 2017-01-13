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
    private Texture2D tx2d;
	private WebCamTexture webCamTexture;
    private UdpClient udpClient;
	private IPEndPoint RemoteIpEndPoint;
//	private TcpClient tcpClient;


	void Start () {
        //Debug.Log ("Start Braa!!!");
        var deviceName = WebCamTexture.devices[0].name;

        webCamTexture = new WebCamTexture(deviceName,480,360,60);
		tx2d = new Texture2D(webCamTexture.width, webCamTexture.height);
		webCamTexture.Play();
        udpClient = new UdpClient(8080);
		RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);
        //tcpClient = new TcpClient(11000);
        try
        {
            udpClient.Connect("192.168.0.100", 8080);

            //udpClient.Connect("192.168.1.118", 8080);
            //tcpClient.Connect("192.168.1.116", 11000);
        }
        catch (Exception e)
        {
            Console.Write(e.ToString());
        }

	}
	
	// Update is called once per frame
	void Update () {
//        output.GetComponent<RawImage>().texture = webCamTexture;
        tx2d.SetPixels(webCamTexture.GetPixels());
        tx2d.Apply();
        Byte[] sendBytes = tx2d.EncodeToJPG(40);
        udpClient.Send(sendBytes, sendBytes.Length);
		IAsyncResult asyncResult = udpClient.BeginReceive( null, null );
		asyncResult.AsyncWaitHandle.WaitOne( TimeSpan.FromSeconds(0.3));
		if (asyncResult.IsCompleted)
		{
			Debug.Log("Received!!");
			try
			{
				IPEndPoint remoteEP = null;
				byte[] receivedData = udpClient.EndReceive( asyncResult, ref remoteEP );
				string returnData = Encoding.ASCII.GetString(receivedData);
				UDPJson temp = JsonUtility.FromJson<UDPJson>(returnData);
				transform.position = new Vector3(temp.pose.position.x,temp.pose.position.y,temp.pose.position.z);
				transform.rotation = new Quaternion(temp.pose.orientation.x,temp.pose.orientation.y,temp.pose.orientation.z,transform.rotation.w);

				// EndReceive worked and we have received data and remote endpoint
			}
			catch (Exception ex)
			{
				// EndReceive failed and we ended up here
			}

		} 
	}
}


