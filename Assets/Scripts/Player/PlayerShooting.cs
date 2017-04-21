using UnityEngine;
using UnityEngine.UI;

public class PlayerShooting : MonoBehaviour
{
    public int damagePerShot = 20;
    public float timeBetweenBullets = 0.15f;
    public float range = 100f;
	public Text pickButtonText;
	public Button fireButton;
    public Button pickButton;
    private Color pickButtonColor1;
    private Color pickButtonColor2;

    private Color bulletButtonColor1;
    private Color bulletButtonColor2;

    public Text bulletText;
    public Button bulletButton;
	public int bullet;

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


    void Awake ()
    {
		shootableMask = 1 << LayerMask.NameToLayer ("Shootable");
        gunParticles = GetComponent<ParticleSystem> ();
        gunLine = GetComponent <LineRenderer> ();
        gunAudio = GetComponent<AudioSource> ();
        gunLight = GetComponent<Light> ();

        pickButtonColor1 = pickButton.colors.normalColor;
        pickButtonColor2 = pickButton.colors.disabledColor;

        bulletButtonColor1 = bulletButton.colors.normalColor;
        bulletButtonColor2 = bulletButton.colors.disabledColor;


        this.weaponArea = false;
		this.clickShoot = false;
		setMyweapon ();
    }


    void Update ()
    {
        timer += Time.deltaTime;
	

		if(Input.GetButton ("Fire1") && timer >= timeBetweenBullets && Time.timeScale != 0)
		{
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

		if(this.clickShoot && timer >= timeBetweenBullets && Time.timeScale != 0)
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
			this.bullet = 10;
			this.bulletText.text = this.bullet + " / 10";
            CancelInvoke("blinkBulletButton");
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
	}

	public void reBullet(){
		this.bullet = 10;
		this.bulletText.text = this.bullet + " / 10";
	}

	public void weaponShoot(){
		print ("fire button click!!");
		this.clickShoot = true;
	}

	public void pickWeapon(){
		if (this.weaponArea) {
			//print ("Good");
			this.activeWeapon.SetActive (false);
			this.weapon.transform.position = this.activeWeapon.transform.position;
			this.weapon.transform.rotation = this.activeWeapon.transform.rotation;
			this.weapon.transform.localScale = this.activeWeapon.transform.localScale;
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
            gunLine.SetPosition (1, shootHit.point);
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
}
