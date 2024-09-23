using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

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

    public AudioSource source;
    public AudioClip clip;

    public float interval = 0.0f; // Интервал между выстрелами

    private float timer = 0f; // Таймер для отслеживания времени
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
            CameraShaker.Instance.ShakeOnce(.1f, 2f, .1f, 1f);

            // Увеличиваем таймер
            timer += Time.deltaTime;

            // Если прошло достаточно времени для следующего выстрела
            if (timer >= interval)
            {
                interval = 6f;
                // Воспроизводим звук выстрела
                source.PlayOneShot(clip);

                // Сбрасываем таймер
                timer = 0f;
            }
        }
        else
        {
            interval = 0.0f;
            // Если кнопка не удерживается, сбрасываем таймер
            timer = 0f;
            source.Stop(); 

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

        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, distance))
        {
            GameObject muzzleInstance = Instantiate(muzzle, spawnPoint.position, spawnPoint.localRotation);
            muzzleInstance.transform.parent = spawnPoint;

            Vector3 impactPoint;

            // Проверяем, находится ли точка попадания достаточно далеко от камеры, чтобы не быть перед ней
            if ((hit.point - cam.transform.position).magnitude > 1.5f)
            {
                impactPoint = hit.point;
            }
            else
            {
                // Если точка слишком близко к камере, то создаем эффект попадания на расстоянии перед камерой
                impactPoint = cam.transform.position + cam.transform.forward * 1.5f;
            }

            GameObject impactGO = Instantiate(impact, impactPoint, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO, 2f);

            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                target.TakeDamage(damage);
                
            }
        }
    }
}
