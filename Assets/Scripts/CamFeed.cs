using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using System;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;


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
	float Xmultiplier,Ymultiplier,Zmultiplier;
	GameObject Map;
	Vector3 dir,last;
	Thread readThread;



	void Awake () {
        Debug.Log ("Start Braa!!!");
//		readThread = new Thread(new ThreadStart(ReceiveData));
//		readThread.IsBackground = true;
//		readThread.Start();
		oldPosition = Vector3.zero ;
		newPosition = Vector3.zero ;
		Map = GameObject.FindGameObjectWithTag ("Map");
		runner = 1f;


//      var deviceName = WebCamTexture.devices[0].name;

//      webCamTexture = new WebCamTexture();
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
		
//		dir = Input.acceleration-(Input.gyro.gravity);
////		dir = Input.acceleration-last;
//		last =  Input.acceleration;
////
//
//
//
////        output.GetComponent<RawImage>().texture = webCamTexture;
////        tx2d.SetPixels(webCamTexture.GetPixels());
////        tx2d.Apply();
////        Byte[] sendBytes = tx2d.EncodeToJPG(40);
////        udpClient.Send(sendBytes, sendBytes.Length);
//		//asyncResult = udpClient.BeginReceive( null, null );
//		//asyncResult.AsyncWaitHandle.WaitOne( TimeSpan.FromSeconds(0.033));
//		float x = (float)Math.Floor(dir.x*100f)/100f;
//		float y = (float)Math.Floor(dir.y*100f)/100f;
//		float z = (float)Math.Floor(dir.z*100f)/100f;
////
//		Vector3 round = new Vector3(AtoS(x),0f,AtoS(z));
//		Debug.Log (x+" , "+y+" ,"+z);
//		if (Math.Abs (x) > 0.2 || Math.Abs (y) > 0.2 || Math.Abs (z) > 0.2) {
//			
//			transform.parent.position += round*2000f;
//		}
//		Seq.text = round.x+ ","+round.y+ ","+round.z + "";
////	
//		Debug.Log(asyncResult.IsCompleted);
		if (asyncResult.IsCompleted && runner>= 0.8f) {
			Debug.Log ("Received!!");
			try {	
				
				IPEndPoint remoteEP = null;
				byte[] receivedData = udpClient.EndReceive(asyncResult, ref remoteEP);
				while(udpClient.Available!=0){
					receivedData = udpClient.Receive(ref remoteEP);
				}
				string returnData = Encoding.ASCII.GetString (receivedData);
				UDPJson temp = JsonUtility.FromJson<UDPJson> (returnData);
//				Debug.Log("Aval : "+udpClient.Available);
				Seq.text = temp.header.seq + "";
//				Debug.Log (temp.header.seq);
				oldPosition = newPosition;
				newPosition = new Vector3 (temp.pose.position.x * 20f, temp.pose.position.y * 20f, temp.pose.position.z * 20f);
				runner = 0f;
//				if(oldPosition==newPosition){
//					if(dir.sqrMagnitude>0.1f){
//						transform.parent.position +=new Vector3(dir.x*Xmultiplier,dir.y*Ymultiplier,dir.z*Zmultiplier);	
//					}
//				}else{
//					TranslatePosition(Vector3.Lerp(oldPosition, newPosition, runner));
//					if(Xmultiplier==0){
//						Xmultiplier = (newPosition.x-oldPosition.x)/dir.x;
//					}
//					if(Ymultiplier==0){
//						Ymultiplier = (newPosition.y-oldPosition.y)/dir.y;
//					}
//					if(Zmultiplier==0){
//						Zmultiplier = (newPosition.z-oldPosition.z)/dir.z;
//					}
//				}
				TranslatePosition(Vector3.Lerp(oldPosition, newPosition, runner));
//				transform.parent.transform.position = newPosition;
				runner += 0.33f;
				//TranslatePosition (new Vector3 (temp.pose.position.x * 100f, temp.pose.position.y * 100f, temp.pose.position.z * 100f));
//				transform.parent.transform.position = new Vector3(temp.pose.position.x*100f,temp.pose.position.y*100f,temp.pose.position.z*100f);
//				transform.rotation = new Quaternion(temp.pose.orientation.x,temp.pose.orientation.y,temp.pose.orientation.z,temp.pose.orientation.w);
//				Debug.Log("End set 0 Naja");
				// EndReceive worked and we have received data and remote endpoint
			} catch (Exception e) {
//				Debug.LogError ("Error naja!!");
				// EndReceive failed and we ended up here
			}
			asyncResult = udpClient.BeginReceive (null, null);
		} else if (runner <= 0.8F){
//			Debug.Log ("---- " + runner);
			TranslatePosition(Vector3.Lerp(oldPosition, newPosition, runner));
//			transform.parent.transform.position = newPosition;
			runner += 0.33f;
		
		}
//		Debug.Log ("Runner : "+runner);
	}

	void TranslatePosition(Vector3 input){

		Vector3 pos = input;		
		Vector3 center = Vector3.zero; 

		Quaternion rotY = Quaternion.AngleAxis (Map.transform.eulerAngles.y, Vector3.up); // get the desired rotation
		Quaternion rotZ = Quaternion.AngleAxis(Map.transform.eulerAngles.z,Vector3.forward);
		Quaternion rotX = Quaternion.AngleAxis (Map.transform.eulerAngles.x, Vector3.right);
		Vector3 dir = pos - center; // find current direction relative to center
		dir = rotX*rotY*rotZ* dir; // rotate the direction
		transform.parent.position = new Vector3((center + dir).x,-(center + dir).y/3f,(center + dir).z); // define new position


	}

	public void closeUDP(){
		udpClient.Close ();
	}
	public float AtoS(float a){
		float S =(float)( 0.5 * a * Time.deltaTime * Time.deltaTime);
		return S;
	}
//	Vector3 linear_interpolate(float y1,float y2, int mu){
//		return (y1 * (1 - mu) + y2 * mu);
//	}

}
//posisiton  y *(-1)

