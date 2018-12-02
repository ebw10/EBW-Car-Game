using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EBWPLayercontroller : MonoBehaviour {

    public float speed = 1500f;
    public float rotationSpeed = 15f;

    private int count;

    private float timer;
    private int wholetime;

    public Text endText;
    public Text winText;

    private float movement = 0f;
    private float rotation = 0f;

    public Rigidbody2D rb;

    public WheelJoint2D backWheel;
    public WheelJoint2D frontWheel;

    private AudioSource source;
    public AudioClip coin;
    public AudioClip car;
    private float volLowRange = .5f;
    private float volHighRange = 1.0f;

    void Start()
    {
        endText.text = "";
        winText.text = "";
    }

    void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    void Update()
    {
       movement = -Input.GetAxisRaw("Vertical") * speed;
        rotation = Input.GetAxisRaw("Horizontal");
    }

    void FixedUpdate()
    {
        if (movement == 0f)
        {
            backWheel.useMotor = false;
            frontWheel.useMotor = false;
        }
        else
        {
            backWheel.useMotor = true;
            frontWheel.useMotor = true;
            JointMotor2D motor = new JointMotor2D { motorSpeed = movement, maxMotorTorque = 10000 };
            backWheel.motor = motor;
            frontWheel.motor = motor;
        }

        rb.AddTorque(-rotation * rotationSpeed * Time.fixedDeltaTime);

        timer = timer + Time.deltaTime;
        if (timer >= 10 && count != 10)
        {
            endText.text = "You Lose! :(";
            //StartCoroutine(ByeAfterDelay(2));

        }

    }

    private void OnTriggerEnter2D(Collider2D colinfo)
    {
        if (colinfo.CompareTag("Ant"))
        {
            float vol = Random.Range(volLowRange, volHighRange);
            source.PlayOneShot(car);
            endText.text = "You Lose! :(";
            StartCoroutine(ByeAfterDelay(2));
       }

        if (colinfo.gameObject.CompareTag("PickUp"))
        {
            colinfo.gameObject.SetActive(false);

            //Add one to the current value of our count variable.
            count = count + 1;

            float vol = Random.Range(volLowRange, volHighRange);
            source.PlayOneShot(coin);
          
            //GameLoader.AddScore(1);

            //Update the currently displayed count by calling the SetCountText function.
            SetCountText();
        }

    }

    void SetCountText()
    {
        //Set the text property of our our countText object to "Count: " followed by the number stored in our count variable.
        //  countText.text = "Count: " + count.ToString();

        //Check if we've collected all 12 pickups. If we have...
        if (count == 10 && timer < 10)
        {
            //... then set the text property of our winText object to "You win!"
            //  winText.text = "You win!";
            winText.text = "You win!";
            StartCoroutine(ByeAfterDelay(2));



        }
    }

    IEnumerator ByeAfterDelay(float time)
    {
        yield return new WaitForSeconds(time);

        // Code to execute after the delay
        //GameLoader.gameOn = false;
    }
}



   




