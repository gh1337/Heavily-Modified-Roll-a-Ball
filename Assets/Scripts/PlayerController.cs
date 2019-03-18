using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;


public class PlayerController : MonoBehaviour
{

    public float speed;
    public Text countText;
    public Text winText;

    private Rigidbody rb;
    private int count;

    public Text Timetext;

    public Button quitButton;



    public int jumppadheight;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        winText.text = "";
        
    }

    private void Update()
    {
        Timetext.text = Time.time.ToString();
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rb.AddForce(movement * speed);
    }

    public void quitgame()
    {
        Application.Quit();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
        }

        if (other.gameObject.CompareTag("Deathzone"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (other.gameObject.CompareTag("Shrink"))
        {
            transform.localScale += new Vector3(-0.3f, -0.3f, -0.3f);
            other.gameObject.SetActive(false);
        }

        if (other.gameObject.CompareTag("Grow"))
        {
            transform.localScale += new Vector3(1, 1, 1);
            other.gameObject.SetActive(false);
        }

        if (other.gameObject.CompareTag("Jump Pad"))
        {
            rb.AddForce(Vector3.up * jumppadheight, ForceMode.Impulse);
        }

        if (other.gameObject.CompareTag("Teleport"))
        {
            transform.position = new Vector3(1, 1, 1);
            SceneManager.LoadScene("Win");
        }

        if (other.gameObject.CompareTag("Cake"))
        {
            SceneManager.LoadScene("Win-2");
            
        }
    }

    

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= 6)
        {
            winText.text = "You Win!";
            SceneManager.LoadScene("Level2");
        }
    }
}