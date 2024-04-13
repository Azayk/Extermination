using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : MonoBehaviour
{
    public Transform spawnPoint;
    public float distance = 15f;

    public float damage = 10f;

    public Transform stvol;
    public Transform stvol2;
    public float rotationSpeed = 200;

    public float rotationSpeed2 = 50;
    public Transform patron;

    public GameObject muzzle;
    public GameObject impact;

    private Camera cam;


    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {

        stvol.Rotate(Vector3.forward, Time.deltaTime * rotationSpeed);
        stvol2.Rotate(Vector3.forward, Time.deltaTime * (-rotationSpeed));


        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();

        }
    }
    
    private void Shoot()
    {
        RaycastHit hit;
        RaycastHit hit_1;
        RaycastHit hit_2;
        RaycastHit hit_3;

        GameObject muzzleInstance = Instantiate(muzzle, spawnPoint.position, spawnPoint.localRotation);
        muzzleInstance.transform.parent = spawnPoint;

        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, distance))
        {
            GameObject impactGO = Instantiate(impact, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO, 2f);

            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }
        }

        if (Physics.Raycast(cam.transform.position, cam.transform.forward + new Vector3(-2f, 0f, 0f), out hit_1, distance))
        {
            GameObject impactGO = Instantiate(impact, hit_1.point, Quaternion.LookRotation(hit_1.normal));
            Destroy(impactGO, 2f);

            Target target = hit_1.transform.GetComponent<Target>();
            if (target != null)
            {
                target.TakeDamage(damage);
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
            }
        }


    }
}
