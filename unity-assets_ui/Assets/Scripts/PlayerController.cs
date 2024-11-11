using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public Rigidbody rb;
    private int score = 0;
    public int health;
    public Text scoreText;
    public Text healthText;
    public Text goalText;
    // Start is called before the first frame update
    void Start()
    {
       
    }
    private void Update()
    {
        if (health == 0)
        {
            //Debug.Log("Game over!");
            goalText.transform.parent.gameObject.SetActive(true);
            goalText.text = "Game Over!";
            goalText.color = Color.white;
            goalText.GetComponentInParent<Image>().color = Color.red;
            StartCoroutine(LoadScene(3));
        }
        if (Input.GetKeyDown("escape"))
        {
            SceneManager.LoadScene("menu");
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey("w") || Input.GetKey("up"))
        {
            rb.AddForce(0,0, speed * Time.deltaTime);
        }
        if (Input.GetKey("a") || Input.GetKey("left"))
        {
            rb.AddForce(-speed * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey("s") || Input.GetKey("down"))
        {
            rb.AddForce(0, 0, -speed * Time.deltaTime);
        }
        if (Input.GetKey("d") || Input.GetKey("right"))
        {
            rb.AddForce(speed * Time.deltaTime, 0, 0);
        }
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Pickup")
        {
            score += 1;
            //Debug.Log("Score: " + score);
            SetScoreText();
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "Trap")
        {
            health -= 1;
            SetHealthText();
            //Debug.Log("Health: " + health);
        }
        if (other.gameObject.tag == "Goal")
        {
            //Debug.Log("You win!");
            goalText.transform.parent.gameObject.SetActive(true);
            goalText.text = "You Win!";
            goalText.GetComponentInParent<Image>().color = Color.green;
            goalText.color = Color.black;

        }
    }
    void SetScoreText()
    {
        scoreText.text = "Score: " + score;
    }
    void SetHealthText()
    {
        healthText.text = "Health: " + health;
    }
    IEnumerator LoadScene(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
