using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigunShoot : MonoBehaviour
{
    public List<Transform> stvol;
    public float rotationSpeed = 200;
    public Transform patron;

    public Transform spawnPoint;
    public Transform spawnPoint2;
    public float distance = 15f;

    public float damage = 10f;

    public GameObject muzzle;
    public GameObject impact;

    Camera cam;
    // Start is called before the first frame update
    void Start()
    {

        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {

        foreach (Transform obj in stvol)
        {
            obj.Rotate(Vector3.forward, Time.deltaTime * rotationSpeed);
        }

        if (Input.GetButton("Fire1"))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        patron.Rotate(Vector3.up, Time.deltaTime * rotationSpeed);
        foreach (Transform obj in stvol)
        {
            obj.Rotate(Vector3.forward, Time.deltaTime * (rotationSpeed * 5));
        }

        RaycastHit hit;
        RaycastHit hit_1;

        GameObject muzzleInstance = Instantiate(muzzle, spawnPoint.position, spawnPoint.localRotation);
        muzzleInstance.transform.parent = spawnPoint;

        GameObject muzzleInstance2 = Instantiate(muzzle, spawnPoint2.position, spawnPoint2.localRotation);
        muzzleInstance2.transform.parent = spawnPoint2;

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

        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit_1, distance))
        {
            GameObject impactGO = Instantiate(impact, hit_1.point, Quaternion.LookRotation(hit_1.normal));
            Destroy(impactGO, 2f);

            Target target = hit_1.transform.GetComponent<Target>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }
        }
    }
}
