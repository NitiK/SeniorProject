using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.IO;  

public class SaveObjects : MonoBehaviour {
	public GameObject Cube;
	public string fileName = "QuickSave.xml";

	public void Save(){
		var sr = File.CreateText(Application.persistentDataPath + "/" + fileName);
		foreach(CubeInfo a in GetComponentsInChildren<CubeInfo>()){
			sr.WriteLine (a.GetInfo());
		}
		sr.Close();
		Debug.Log ("Map Saved!!");
	}
	public void Load(){
		try
		{
			string line;
			StreamReader theReader = new StreamReader(Application.persistentDataPath + "/" + fileName, Encoding.Default);
			using (theReader)
			{
				do
				{
					line = theReader.ReadLine();

					if (line != null)
					{
						initFromString(line);
					}
				}
				while (line != null);   
				theReader.Close();
			}
		}
		catch (Exception e)
		{
			Debug.LogError (e);
		}
		Debug.Log ("Map Loaded!!");
	}

	void initFromString(string line){
		string line2 = line.Replace("(","");
		line2 = line2.Replace(")","");
		line2 = line2.Replace(" ","");
		string[] arr= line2.Split(':');
		string[] point1 = arr[0].Split(',');
		string[] point2 = arr[1].Split(',');
		string[] tran = arr[2].Split(',');
		string[] ro = arr[3].Split(',');
		string[] scale = arr[4].Split(',');
		Vector3 p1 = new Vector3(System.Convert.ToSingle (point1[0]),System.Convert.ToSingle (point1[1]),System.Convert.ToSingle (point1[2]));
		Vector3 p2 = new Vector3(System.Convert.ToSingle (point2[0]),System.Convert.ToSingle (point2[1]),System.Convert.ToSingle (point2[2]));
		Vector3 trr = new Vector3(System.Convert.ToSingle (tran[0]),System.Convert.ToSingle (tran[1]),System.Convert.ToSingle (tran[2]));
		Vector3 sca = new Vector3 (System.Convert.ToSingle (scale[0]),System.Convert.ToSingle (scale[1]),System.Convert.ToSingle (scale[2]));
		Quaternion rot = new Quaternion(System.Convert.ToSingle (ro[0]),System.Convert.ToSingle (ro[1]),System.Convert.ToSingle (ro[2]),System.Convert.ToSingle (ro[3]));
		GameObject cube = GameObject.Instantiate (Cube,transform);
		cube.transform.position = trr;
		cube.transform.rotation = rot;
		cube.transform.localScale = sca;
		cube.GetComponent<CubeInfo> ().Init (p1,p2);
	}
	void LateUpdate () {
//		if (Input.GetKeyDown (KeyCode.F5)) 
//		{   
//			Save();   
//		} 
////		float num = System.Convert.ToSingle ("-3.652168E-16");
////		Debug.Log (num);
//		if (Input.GetKeyDown (KeyCode.F9)) 
//		{   
//			Load();   
//		}
	}
}