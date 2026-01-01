using TMPro;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    public Animator animator;
    public GameObject spawnObject;

    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private float remainingTime;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Invoke("Open", remainingTime);
    }

    // Update is called once per frame
    void Update()
    {
        CountDown();
    }

    void Open()
    {
        animator.SetBool("IsOpen", true);
    }    

    void CountDown()
    {
        if(remainingTime > 0)
            remainingTime -= Time.deltaTime;
        else if(remainingTime < 0)
        {
            remainingTime = 0;

            // Pass
            timerText.color = Color.green;
            Destroy(spawnObject);
        }

        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds =  Mathf.FloorToInt(remainingTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }    
}
