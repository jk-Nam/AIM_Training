using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunCtrl : MonoBehaviour
{

    public Transform gunTr;
    public AudioClip fireSfx;
    public Transform raycastStartSpot;
    public GameObject go_Tartget;

    public float delayBeforeFire = 0.1f;
    public bool recoil = true;                          
    public float recoilKickBackMin = 0.1f;              
    public float recoilKickBackMax = 0.3f;              
    public float recoilRotationMin = 0.1f;              
    public float recoilRotationMax = 0.25f;             
    public float recoilRecoveryRate = 0.01f;

    private new AudioSource audio;
    private MeshRenderer muzzleFlash;
    
    private float nextFire;
    private readonly float fireRate = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        muzzleFlash = raycastStartSpot.GetComponentInChildren<MeshRenderer>();
        muzzleFlash.enabled = false;        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(raycastStartSpot.position, raycastStartSpot.forward * 20.0f, Color.red);

        if (GameManager.onStart == true && !GameManager.isPause)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (Time.time >= nextFire)
                {
                    Fire();

                    nextFire = Time.time + fireRate;
                }
            }

            if (recoil)
            {
                gunTr.transform.position = Vector3.Lerp(gunTr.transform.position, transform.position, recoilRecoveryRate * Time.deltaTime);
                gunTr.transform.rotation = Quaternion.Lerp(gunTr.transform.rotation, transform.rotation, recoilRecoveryRate * Time.deltaTime);
            }
        }
    }

    void Fire()
    {
        audio.PlayOneShot(fireSfx, 1.0f);
        StartCoroutine(ShowMuzzleFlash());
        if (recoil)
        {
            Recoil();
        }
    }

    IEnumerator ShowMuzzleFlash()
    {
        Vector2 offset = new Vector2(Random.Range(0, 2), Random.Range(0, 2)) * 0.5f;
        muzzleFlash.material.mainTextureOffset = offset;
        Vector3 scale = Vector3.one * Random.Range(0.4f, 0.8f);
        muzzleFlash.transform.localScale = scale;
        float angle = Random.Range(0, 24) * 15.0f;
        muzzleFlash.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, angle);
        muzzleFlash.enabled = true;
        yield return new WaitForSeconds(Random.Range(0.01f, 0.06f));
        muzzleFlash.enabled = false;
    }

    void Recoil()
    {
        
        float kickBack = Random.Range(recoilKickBackMin, recoilKickBackMax);
        float kickRot = Random.Range(recoilRotationMin, recoilRotationMax);

        gunTr.transform.Translate(new Vector3(0, 0, -kickBack), Space.Self);
        gunTr.transform.Rotate(new Vector3(-kickRot, 0, 0), Space.Self);
    }
}
