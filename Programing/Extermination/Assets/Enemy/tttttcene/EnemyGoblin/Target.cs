using UnityEngine;

public class Target : MonoBehaviour
{
    private PlayerRecord _playerRecord;
    public PlayerMovement player;

    public float health = 50f;
    public Animator animator;

    void Start()
    {
        InitComponentLinks();
    }

    public void TakeDamage(float amount)
    {
        health -= amount;

        if (health <= 0f)
        {
            Die();
        }
        else
        {
            animator.SetTrigger("hit");
        }
    }

    void Die()
    {
        Destroy(gameObject);
        _playerRecord.value += 95;

    }

    private void InitComponentLinks()
    {
        _playerRecord = player.GetComponent<PlayerRecord>();
    }

}
