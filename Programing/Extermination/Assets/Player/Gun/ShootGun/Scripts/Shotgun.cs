using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class Shotgun : MonoBehaviour
{
    public float healAmount = 10;

    public Transform spawnPoint;
    public float distance = 15f;
    public PlayerMovement player;
    public float damage = 10f;

    public GameObject muzzle;
    public GameObject impact;

    private PlayerHealth _playerHealth;

    private Camera cam;
    public AudioSource source;
    public AudioClip clip;

    public AudioSource source2;
    public AudioClip clip2;

    // Start is called before the first frame update
    void Start()
    {
        InitComponentLinks();
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
            CameraShaker.Instance.ShakeOnce(5f, 2f, .1f, 1f);
            source.PlayOneShot(clip);
        }
        
    }

    private void InitComponentLinks()
    {
        _playerHealth = player.GetComponent<PlayerHealth>();
    }

    private void Shoot()
    {
        RaycastHit hit;
        RaycastHit hit_1;
        RaycastHit hit_2;
        RaycastHit hit_3;

        GameObject muzzleInstance = Instantiate(muzzle, spawnPoint.position, spawnPoint.localRotation);
        muzzleInstance.transform.parent = spawnPoint;

        if (Physics.Raycast(cam.transform.position, cam.transform.forward + new Vector3(-0.2f, 0f, 0f), out hit, distance))
        {
            GameObject impactGO = Instantiate(impact, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO, 2f);
            

            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                target.TakeDamage(damage);
                AudioPopal();
                Healht();
            }
        }

        if (Physics.Raycast(cam.transform.position, cam.transform.forward + new Vector3(0.2f, 0f, 0f), out hit_1, distance))
        {
            GameObject impactGO = Instantiate(impact, hit_1.point, Quaternion.LookRotation(hit_1.normal));
            Destroy(impactGO, 2f);
            

            Target target = hit_1.transform.GetComponent<Target>();
            if (target != null)
            {
                target.TakeDamage(damage);
                AudioPopal();
                Healht();
            }
        }

        if (Physics.Raycast(cam.transform.position, cam.transform.forward + new Vector3(0f, .1f, 0f), out hit_2, distance))
        {
            GameObject impactGO = Instantiate(impact, hit_2.point, Quaternion.LookRotation(hit_2.normal));
            Destroy(impactGO, 2f);
            

            Target target = hit_2.transform.GetComponent<Target>();
            if (target != null)
            {
                target.TakeDamage(damage);
                AudioPopal();
                Healht();
            }

        }

        if (Physics.Raycast(cam.transform.position, cam.transform.forward + new Vector3(0f, -.1f, 0f), out hit_3, distance))
        {
            GameObject impactGO = Instantiate(impact, hit_3.point, Quaternion.LookRotation(hit_3.normal));
            Destroy(impactGO, 2f);
            

            Target target = hit_3.transform.GetComponent<Target>();
            if (target != null)
            {
                target.TakeDamage(damage);
                AudioPopal();
                Healht();
            }
        }


    }

    private void Healht()
    {
        _playerHealth.AddHealth(healAmount);

    }

    private void AudioPopal()
    {
        source2.PlayOneShot(clip2);
    }

}
