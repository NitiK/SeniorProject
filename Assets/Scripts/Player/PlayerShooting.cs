using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public int damagePerShot = 20;
    public float timeBetweenBullets = 0.15f;
    public float range = 100f;


    float timer;
    Ray shootRay = new Ray();
    RaycastHit shootHit;
    int shootableMask;
    ParticleSystem gunParticles;
    LineRenderer gunLine;
    AudioSource gunAudio;
    Light gunLight;
    float effectsDisplayTime = 0.2f;


    void Awake ()
    {
		shootableMask = 1 << LayerMask.NameToLayer ("Shootable");
        gunParticles = GetComponent<ParticleSystem> ();
        gunLine = GetComponent <LineRenderer> ();
        gunAudio = GetComponent<AudioSource> ();
        gunLight = GetComponent<Light> ();
    }


    void Update ()
    {
        timer += Time.deltaTime;

		if(Input.GetButton ("Fire1") && timer >= timeBetweenBullets && Time.timeScale != 0)
        {
            Shoot ();
        }

		/*if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved && timer >= timeBetweenBullets && Time.timeScale != 0)
		{
			Shoot ();
		}*/

        if(timer >= timeBetweenBullets * effectsDisplayTime)
        {
            DisableEffects ();
        }
    }


    public void DisableEffects ()
    {
        gunLine.enabled = false;
        gunLight.enabled = false;
    }


    void Shoot ()
    {
//		print ("shoot");
        timer = 0f;

        gunAudio.Play ();

        gunLight.enabled = true;

        /*gunParticles.Stop ();
        gunParticles.Play ();*/

        gunLine.enabled = true;
		gunLine.SetPosition (0, new Vector3(transform.position.x,transform.position.y-2f,transform.position.z));

        shootRay.origin = transform.position;
		shootRay.direction = transform.forward;

        if(Physics.Raycast (shootRay, out shootHit, range, shootableMask))
        {
			Debug.Log (shootHit.collider.name + " : "+shootHit.distance);
			shootHit.collider.transform.localScale = shootHit.collider.transform.localScale * 0.9f;
            /*EnemyHealth enemyHealth = shootHit.collider.GetComponent <EnemyHealth> ();
            if(enemyHealth != null)
            {
                enemyHealth.TakeDamage (damagePerShot, shootHit.point);
				Debug.Log ("Done");
            }*/
            gunLine.SetPosition (1, shootHit.point);
        }
        else
        {
            gunLine.SetPosition (1, shootRay.origin + shootRay.direction * range);
        }
    }
}
