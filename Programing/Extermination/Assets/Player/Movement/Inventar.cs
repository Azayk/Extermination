using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventar : MonoBehaviour
{
    public GameObject gun1;
    public GameObject gun2;

    public GameObject aimGun1;
    public GameObject aimGun2;

    private bool isGun1Active = false;

    // Start is called before the first frame update
    void Start()
    {
        // Initially activate gun2
        ActivateGun1();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            // Toggle between guns
            if (isGun1Active)
                ActivateGun2();
            else
                ActivateGun1();
        }
    }

    void ActivateGun1()
    {
        gun1.SetActive(true);
        aimGun1.SetActive(true);

        gun2.SetActive(false);
        aimGun2.SetActive(false);

        isGun1Active = true;
    }

    void ActivateGun2()
    {
        gun2.SetActive(true);
        aimGun2.SetActive(true);

        gun1.SetActive(false);
        aimGun1.SetActive(false);

        isGun1Active = false;
    }
}