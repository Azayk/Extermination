using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventar : MonoBehaviour
{
    public GameObject gun1;
    public GameObject gun2;

    public GameObject aimGun1;
    public GameObject aimGun2;

    [Header("Keybinds")]
    public KeyCode gun1On;
    public KeyCode gun2On;

    // Start is called before the first frame update
    void Start()
    {
        gun2.SetActive(true);
        aimGun2.SetActive(true);

        gun1.SetActive(false);
        aimGun1.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(gun1On))
        {
            gun2.SetActive(false);
            aimGun2.SetActive(false);

            gun1.SetActive(true);
            aimGun1.SetActive(true);

        }

        if (Input.GetKey(gun2On))
        {
            gun1.SetActive(false);
            aimGun1.SetActive(false);

            gun2.SetActive(true);
            aimGun2.SetActive(true);
        }
    }
}
