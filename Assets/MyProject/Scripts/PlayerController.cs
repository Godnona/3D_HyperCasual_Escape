using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public Joystick joystick;
    public Vector3 m_input;

    [SerializeField] private float speed = 5f;
    [SerializeField] private ParticleSystem effect;
    
    private Animator animator;
    private Rigidbody m_rb;

    private bool m_isOutShop = true;
    private bool isInvincible = false;

    void Start()
    {
        m_rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        StopEffect();
    }

    void Update()
    {
        HandleInput();
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle") && !isInvincible)
        {
            TakeDamage();
        }

        if (collision.gameObject.CompareTag("NextLevel"))
        {
            GameManager.Instance.LoadNextLevel();
        }

        if(collision.gameObject.CompareTag("ShopInGame") && m_isOutShop)
        {
            m_isOutShop = false;
            MenuManager.Instance.shopInGamePanel.gameObject.SetActive(true);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("ShopInGame"))
            m_isOutShop=true;
    }

    private void HandleInput()
    {
        if (joystick == null)
            return;

        float _horizontal = joystick.Horizontal;
        float _vertical = joystick.Vertical;

        m_input = new Vector3(_horizontal, 0, _vertical).normalized;
    }

    private void HandleMovement()
    {
        if (m_input.sqrMagnitude > 0.01f)
        {
            m_rb.MovePosition(m_rb.position + m_input * speed * Time.deltaTime);
            m_rb.MoveRotation(Quaternion.LookRotation(m_input));
        }
    }


    private void TakeDamage()
    {
        if (SaveManager.Instance.life <= 0)
        {
            Die();
            return;
        }

        bool stillAlive = SaveManager.Instance.LoseLife();
        if (!stillAlive)
        {
            Die();
        }
        else
        {
            Debug.Log("Player lost 1 life, still alive");

            // ⭐ hiệu ứng bất tử ngắn (optional)
            StartCoroutine(InvincibleCoroutine());

            // ⭐ đẩy player lùi nhẹ
            m_rb.linearVelocity = Vector3.zero;
        }

        
    }

    private IEnumerator InvincibleCoroutine()
    {
        isInvincible = true;
        PlayerEffect();

        yield return new WaitForSeconds(2.5f);
        isInvincible = false;
        StopEffect();
    }

    private void Die()
    {
        Debug.Log("Player Die!");

        SaveManager.Instance.ResetLife(1);
        GameManager.Instance.ResetGame();  // ⭐ Respawn + reset joystick
        Destroy(gameObject);
    }

    private void PlayerEffect()
    {
        effect.Play();
        animator.SetBool("IsCollided", true);
    }

    private void StopEffect()
    {
        effect.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        animator.SetBool("IsCollided", false);
    }
}