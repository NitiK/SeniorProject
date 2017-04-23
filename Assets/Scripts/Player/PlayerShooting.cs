using UnityEngine;
using UnityEngine.UI;

public class PlayerShooting : MonoBehaviour
{
    public int damagePerShot = 20;
    public float range = 100f;
	private bool canShoot;
	public Text pickButtonText;
	public Button fireButton;
    public Button pickButton;
    private Color pickButtonColor1;
    private Color pickButtonColor2;

    private Color bulletButtonColor1;
    private Color bulletButtonColor2;

    public Text bulletText;
	public Text magazineText;
    public Button bulletButton;
	private int bullet;
	private int magazine;
	private int maxBullet;
	private int maxMagazine;

	float timeBetweenBullets;
	GameObject myweapon;
	bool weaponArea;
	GameObject weapon;
	GameObject activeWeapon;

    float timer;
    Ray shootRay = new Ray();
    RaycastHit shootHit;
    int shootableMask;
    ParticleSystem gunParticles;
    LineRenderer gunLine;
    AudioSource gunAudio;
    Light gunLight;
    float effectsDisplayTime = 0.2f;
	private bool clickShoot;
	Animator anime;

    void Awake ()
    {
		shootableMask = 1 << LayerMask.NameToLayer ("Shootable");
        gunParticles = GetComponent<ParticleSystem> ();
        gunLine = GetComponent <LineRenderer> ();
        gunAudio = GetComponent<AudioSource> ();
        gunLight = GetComponent<Light> ();
		anime = GetComponentInChildren<Animator> ();

        pickButtonColor1 = pickButton.colors.normalColor;
        pickButtonColor2 = pickButton.colors.disabledColor;

        bulletButtonColor1 = bulletButton.colors.normalColor;
        bulletButtonColor2 = bulletButton.colors.disabledColor;

		this.canShoot = true;
        this.weaponArea = false;
		this.clickShoot = false;
		setMyweapon ();
    }


    void Update ()
    {
        timer += Time.deltaTime;
		this.bulletText.text = this.bullet + " / " + this.maxBullet;
		this.magazineText.text = this.magazine + " / " + this.maxMagazine;
		

		/*if(Input.GetButton ("Fire1") && timer >= timeBetweenBullets && Time.timeScale != 0 && this.canShoot)
		{	
			
			if (this.bullet > 0) {
				this.bullet -= 1;
				Shoot ();
			}
            if (this.bullet == 3)
            {
                InvokeRepeating("blinkBulletButton", 0f, 0.1f);
            }
		}*/

		if(this.clickShoot && timer >= timeBetweenBullets && Time.timeScale != 0 && this.canShoot)
		{
			this.clickShoot = false;
			if (this.bullet > 0) {
				this.bullet -= 1;
				this.bulletText.text = this.bullet + " / 10"; 
				Shoot ();
			}
			if (this.bullet == 3)
			{
				InvokeRepeating("blinkBulletButton", 0f, 0.1f);
			}
		}

		if (Input.GetKeyDown ("e")) {
			//print (this.weaponArea);
			pickWeapon();
		}
		if (Input.GetKeyDown ("r")) {
			//print (this.weaponArea);
			reBullet();
//			this.bullet = 10;
//			this.bulletText.text = this.bullet + " / 10";
//            CancelInvoke("blinkBulletButton");
        }
		//if(Input.touchCount > 0 && (Input.GetTouch(0).phase == TouchPhase.Moved || Input.GetTouch(0).phase == TouchPhase.Began || Input.GetTouch(0).phase == TouchPhase.Stationary) && timer >= timeBetweenBullets && Time.timeScale != 0)
		//{
		//	if (this.bullet > 0) {
		//		this.bullet -= 1;
		//		this.bulletText.text = this.bullet + " / 10";
		//		Shoot ();
		//	}
		//}

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

	void setMyweapon(){
		foreach (Transform t in transform)
		{
			if(t.tag == "Weapon")// Do something to child one
			{
				this.activeWeapon = t.gameObject;

				Debug.Log (t.name);
				this.bullet = this.activeWeapon.GetComponent<Weapon> ().GetBullet ();
				this.maxBullet = this.bullet;
				this.magazine = this.activeWeapon.GetComponent<Weapon> ().GetMagazine ();
				this.timeBetweenBullets = this.activeWeapon.GetComponent<Weapon> ().GetTimeBetweenBullet();
				this.maxMagazine = this.magazine;
				foreach (Transform tchild in t)
				{
					if(tchild.tag == "Weapon_Top")// Do something to child one
					{
						this.myweapon = tchild.gameObject;
						//print (this.myweapon.name);
					}

				}
			}

		}
		anime = GetComponentInChildren<Animator> ();
		anime.enabled = true;
	}

	public void reBullet(){
		if (this.magazine > 0) {
			this.magazine -= 1;
			anime.Play ("Reload");
			Debug.Log ("Reload");
			this.canShoot = false;
			Invoke ("reLoad", 1.8f);
			Invoke ("delayReload", 1.8f);
		}
	}

	void reLoad(){
			this.bullet = this.maxBullet;
			//this.bulletText.text = this.bullet + " / "+this.maxBullet;
			CancelInvoke("blinkBulletButton");

			ColorBlock colorBlock = bulletButton.colors;
			colorBlock.normalColor = bulletButtonColor1;
			bulletButton.colors = colorBlock;
	}

	void delayReload(){
		this.canShoot = true;
	}

	public void weaponShoot(){
		print ("fire button click!!");
		this.clickShoot = true;
	}

	public void pickWeapon(){
		if (this.weaponArea) {
			this.canShoot = false;
			Invoke ("delayReload", 1f);
			//print ("Good");
			this.activeWeapon.SetActive (false);
			this.weapon.transform.position = this.activeWeapon.transform.position;
			//print (this.weapon.transform.eulerAngles + " : " + this.activeWeapon.transform.eulerAngles);
			this.weapon.transform.eulerAngles = this.activeWeapon.transform.eulerAngles;
			//print (this.weapon.transform.eulerAngles);
			this.weapon.transform.localScale = this.weapon.transform.localScale/2;
			//print (this.weapon.transform.rotation);
			Destroy (this.weapon.transform.GetComponent<Rigidbody>());
			Destroy (this.weapon.transform.GetComponent<BoxCollider>());
			Destroy (this.weapon.transform.GetComponent<SphereCollider>());
			this.weapon.transform.parent = this.transform;
			Destroy (this.activeWeapon.transform.gameObject);
			this.weaponArea = false;
			setMyweapon ();
		}
	}		

    void Shoot ()
    {
//		print ("shoot");
        timer = 0f;

        gunAudio.Play ();

        gunLight.enabled = true;

		anime.Play ("Gun");
		if (this.bullet == 0) {
			Invoke ("OutOfBullet",0.333f);
		}
        /*gunParticles.Stop ();
        gunParticles.Play ();*/

        gunLine.enabled = true;
		gunLine.SetPosition (0, new Vector3(myweapon.transform.position.x,myweapon.transform.position.y,myweapon.transform.position.z));

        shootRay.origin = transform.position;
		shootRay.direction = transform.forward;

        if(Physics.Raycast (shootRay, out shootHit, range, shootableMask))
        {
			Debug.Log (shootHit.collider.name + " : "+shootHit.distance);

            this.activeWeapon.GetComponent<Weapon>().Hit(shootHit.collider.gameObject, shootHit.point);
            /*EnemyHealth enemyHealth = shootHit.collider.GetComponent <EnemyHealth> ();
            if(enemyHealth != null)
            {
                enemyHealth.TakeDamage (damagePerShot, shootHit.point);
				Debug.Log ("Done");
            }*/
			gunLine.SetPosition (1, shootRay.origin + shootRay.direction * range);
        }
        else
        {
            gunLine.SetPosition (1, shootRay.origin + shootRay.direction * range);
        }
    }

	void OnTriggerEnter(Collider other) {
		//Destroy(other.gameObject);
		print(other.gameObject.tag);
		if (other.gameObject.tag == "Weapon") {
			this.weaponArea = true;
			this.weapon = other.gameObject;
            this.pickButton.interactable = true;
            InvokeRepeating("blinkPickButton", 0f, 0.1f);
            //this.pickButtonText.text = "Weapon";
        } 
	}

	void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == "Weapon")
		{
			this.weaponArea = false;
            this.pickButton.interactable = false;
            //this.pickButtonText.text = "Plane";
            CancelInvoke("blinkPickButton");

			ColorBlock colorBlock = pickButton.colors;
			colorBlock.normalColor = pickButtonColor1;
			pickButton.colors = colorBlock;
        }
	}

    void blinkPickButton()
    {
        ColorBlock colorBlock = pickButton.colors;
        colorBlock.normalColor = Color.Lerp(pickButtonColor1, pickButtonColor2, Mathf.Abs(Mathf.Cos(Time.fixedTime % 1 / 1 * Mathf.PI)));
        pickButton.colors = colorBlock;
    }

    void blinkBulletButton()
    {
        ColorBlock colorBlock = bulletButton.colors;
        colorBlock.normalColor = Color.Lerp(pickButtonColor1, pickButtonColor2, Mathf.Abs(Mathf.Cos(Time.fixedTime % 1 / 1 * Mathf.PI)));
        bulletButton.colors = colorBlock;
    }
	void OutOfBullet(){
		anime.Play ("OutOfBullet");
	}
}
