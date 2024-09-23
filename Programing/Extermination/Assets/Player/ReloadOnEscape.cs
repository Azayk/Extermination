using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadOnEscape : MonoBehaviour
{
    /*public PlayerMovement player;
    private PlayerHealth _playerHealth;
    
    void Start()
    {
        InitComponentLinks();
    }
    */
    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            //_playerHealth.ResumeGame(); 
            //_playerHealth.pause = false;
        }
    }

    /*private void InitComponentLinks()
    {
        _playerHealth = player.GetComponent<PlayerHealth>();
    }*/
}
