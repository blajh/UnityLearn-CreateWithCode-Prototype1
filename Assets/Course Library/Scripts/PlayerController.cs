using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public GameObject obstacleHolder;
    private BoxCounter boxCounter;

    // Private variables

    [SerializeField] TextMeshProUGUI gameWonText;
    [SerializeField] float speed;
    [SerializeField] TextMeshProUGUI speedometerText;
    [SerializeField] float rpm;
    [SerializeField] TextMeshProUGUI rpmText;
    [SerializeField] float horsePower = 20.0f;
    [SerializeField] float turnSpeed = 45.0f;
    [SerializeField] List<WheelCollider> allWheels;
    private int wheelsOnGround;

    private float horizontalInput;
    private float verticalInput;
    private float brakeInput;

    private Rigidbody rb;


    [SerializeField] GameObject centerOfMass;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = centerOfMass.transform.localPosition;

        boxCounter = obstacleHolder.GetComponent<BoxCounter>();
    }

    private void Update()
    {

        if (IsOnGround())
        {
            speed = rb.velocity.z;
            speed = Mathf.Round(speed * 2.237f * 100f) / 100f; // this magic number converts the Vector to kph
            speedometerText.text = "Speed: " + speed + " km/h";

            rpm = Mathf.Round((speed % 30) * 40);
            rpm = Mathf.Round(rpm * 100f) / 100f; // let's get it down to two decimal spaces, just to keep all numbers nicely formatted
            rpm = Mathf.Abs(rpm);
            rpmText.text = "RPM: " + rpm;
        }

        // Reset on drop from road
        if (transform.position.y < -10)
        {
            SceneManager.LoadScene(SceneManager.GetSceneAt(0).name);
        }

        // Check if won
        if (boxCounter.ObstaclesAreGone())
        {
            rb.AddForce(Vector3.up * 50000);
            gameWonText.gameObject.SetActive(true);

        }
    }

    void FixedUpdate()
    {
        // Get Player Input
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        brakeInput = Input.GetAxis("Brake");

        if (IsOnGround())
        {

            // Add motor torque and brake torque to the wheels
            foreach (var wheel in allWheels)
            {
                wheel.motorTorque = horsePower * verticalInput;
                wheel.brakeTorque = horsePower * 1000 * brakeInput;
            }

            // Adds forward force to the vehicle based on vertical input
            // rb.AddForce(Vector3.forward * horsePower * verticalInput);
            // Removed since the motorTorque on wheel is the right way to go when doing wheel colliders

            // Rotates the vehicle based on horizontal input
            transform.Rotate(Vector3.up * Time.deltaTime * turnSpeed * horizontalInput);

            /*
            // Moves the vehicle forward based on vertical input
            transform.Translate(Vector3.forward * Time.deltaTime * speed * verticalInput);
            */
        }


    }

    private bool IsOnGround()
    {
        wheelsOnGround = 0;

        foreach (WheelCollider wheel in allWheels)
        {
            if (wheel.isGrounded)
            {
                wheelsOnGround++;
            }
        }

        if (wheelsOnGround >= 2)
        {
            return true;
        }

        else
        {
            return false;
        }
    }
}
