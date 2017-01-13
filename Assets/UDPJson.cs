using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Header
{
	public int stamp;
	public string frame_id;
	public int seq;
}

[System.Serializable]
public class Position
{
	public float y;
	public float x;
	public float z;

}

[System.Serializable]
public class Orientation
{
	public float y;
	public float x;
	public float z;
}

[System.Serializable]
public class Pose
{
//	public Pose(){
//		position = new Position ();
//		orientation = new Orientation ();
//	}
	public Position position;
	public Orientation orientation;
}

[System.Serializable]
public class UDPJson
{
//	public UDPJson(){
//		header = new Header ();
//		pose = new Pose ();
//	}
	public Header header;
	public Pose pose;


	public override string ToString(){
		return "Undone EiEi!!";
	}
}
