using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Weapon {
	int GetBullet ();
	int GetMagazine();
	void Hit (GameObject target, Vector3 point);
}
