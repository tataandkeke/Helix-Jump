using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public bool ignoreNextCollision; // to make sre ball doesnt hit 2 things at once
    public float impulseForce = 5f;
    public Rigidbody rb;
    private Vector3 startPos;
    public int perfectPass;
    public bool isSuperSpeedActive;

    public AudioClip ballBounceSound;
    public AudioClip passSound;
    public AudioSource ballAudio;
    public ParticleSystem bounceParticle;

    // Start is called before the first frame update
    void Awake()
    {
        //sets the starting position of the ball when it called on the scene
        startPos = transform.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (ignoreNextCollision)
            return;

        //Checks for superspeed conditions
        if(isSuperSpeedActive)
        {
            if(!collision.transform.GetComponent<Goal>())
            {
                Destroy(collision.transform.parent.gameObject, 0.3f);
                Debug.Log("Destroying Platform");
            }
            
        }
        else
        {
            // This makes the ball check for a Death part
            // Then stores a refrence of the script to acces and call the Method
            DeathPart deathPart = collision.transform.GetComponent<DeathPart>();
            if (deathPart)
                deathPart.HitDeathPart();
        }

        
        
        rb.velocity = Vector3.zero; //sets the speed of the ball to zero
        rb.AddForce(Vector3.up * impulseForce, ForceMode.Impulse); // adds an instant force to the ball

        ignoreNextCollision = true;
        Invoke("AllowCollision", 0.2f);

        perfectPass = 0;
        isSuperSpeedActive = false;

        bounceParticle.transform.position = transform.position + new Vector3(0, -0.1f, 0);
        bounceParticle.Play();
        //play Audio
        ballAudio.PlayOneShot(ballBounceSound, 1.0f);

    }

    private void Update()
    {
        // Checks if the condition for superspeed is met
        // Then adds an increase downward force to the ball
        if(perfectPass >= 3 && !isSuperSpeedActive)
        {
            isSuperSpeedActive = true;
            rb.AddForce(Vector3.down * 10, ForceMode.Impulse);
        }
    }

    private void AllowCollision()
    {
        ignoreNextCollision = false;
    }

    // Update is called once per frame
    public void ResetBall()
    {
        //resets the ball to the default position when the game is over
        transform.position = startPos;
    }
}
