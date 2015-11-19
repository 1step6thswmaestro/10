using UnityEngine;
using System.Collections;

public class TankEnding : MonoBehaviour {

    public bool startEnding;

    // Shooting 관련
    public Transform gunPoint;
    public GameObject tankTower;
    public GameObject bulletPrefab;

    private AudioSource shootingSound;
    private AudioSource marchSound;

    private float reloadTime = 1f;
	private float bulletSpeed = 1000f;
	private int changeDirection = 1;

    public GameObject wall;
	public GameObject mainBGM;
	public GameObject monitor;

    private float timeElapsed = 30.0f;
    

	// Use this for initialization
	void Start () {
        InitSound();
        wall.SetActive(false); 
		mainBGM.SetActive (false);
		monitor.SetActive (false);

        marchSound.Play();
	}
    // 일정 시간이 지나면 첫 화면으로
    void FixedUpdate()
    {
        timeElapsed -= Time.fixedDeltaTime;
        reloadTime -= Time.fixedDeltaTime;

        if( timeElapsed < 0)
        {
            EventBus.Instance.Clear();
            Application.LoadLevel(Application.loadedLevel);
        }

        if (reloadTime < 0)
        {
            reloadTime = 1f;
            Shoot();
        }

        rotateGun();
    }
    

    private void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, gunPoint.position, gunPoint.rotation) as GameObject;               
        bullet.transform.Rotate(gunPoint.forward);        

        bullet.GetComponent<Rigidbody>().AddForce( gunPoint.forward * bulletSpeed);
        shootingSound.Play();                       
    }    
    
    
    // 각도 240 ~ 310 사이에서만 총이 회전하도록 함
    private void rotateGun()
    {
        if (tankTower.transform.localEulerAngles.y > 310)
        {
            changeDirection = -1;
        }
        else if (tankTower.transform.localEulerAngles.y < 240)
        {
            changeDirection = 1;
        }

        Quaternion rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, 20.0f));
        tankTower.transform.Rotate(changeDirection * Quaternion.ToEulerAngles(rotation));
    }

    private void InitSound()
    {
        AudioSource[] audios = this.GetComponents<AudioSource>();
        foreach (AudioSource audio in audios)
        {
            if( audio.priority == 128)
            {
                shootingSound = audio;
            }
            else if( audio.priority == 127)
            {
                marchSound = audio;
            }
        }
    }
}
