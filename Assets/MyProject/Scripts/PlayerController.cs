using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Joystick joystick;

    [SerializeField] private float speed = 5f;
    private Rigidbody m_rb;
    public Vector3 m_input;

    void Start()
    {
        m_rb = GetComponent<Rigidbody>();
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
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Die();
        }

        if (collision.gameObject.CompareTag("NextLevel"))
        {
            GameManager.Instance.LoadNextLevel();
        }
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

    private void Die()
    {
        Debug.Log("Player Die!");

        GameManager.Instance.ResetGame();  // ⭐ Respawn + reset joystick
        Destroy(gameObject);
    }
}