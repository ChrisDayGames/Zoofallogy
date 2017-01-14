using UnityEngine;
using System.Collections;

[System.Serializable]
public class PlatformerControllerSpecs {

    public float speed = 10.0f;
    public float acceleration = 1.0f;
    public float friction = -1.0f;
    public float jumpImpulse = 30.0f;
    public float jumpPower = 15.0f;
    public int jumpsAllowed = 1;
    public float jumpHoldTime = 1.0f;
    public float gravity = -9.8f;
    public float terminalVelocity = -25.0f;

}

[RequireComponent(typeof(Rigidbody))]
public class PlatformerController : MonoBehaviour {

    //a class that will determine the "feel"
    //of the platformer controller with values
    public PlatformerControllerSpecs specs;

    //cached reference to the required rigidbody component
    Rigidbody rb;

    //vector3 for interpolation
    Vector3 nextPosition;

    //floats to keep track of velocity
    float horizontalMotion = 0.0f;
    float verticalMotion = 0.0f;

    //vars to keep track of jumping
    int jumpCount = 0;
    float currentHoldTime = 0.0f;

    //vars to carry input into fixed update
    float x = 0.0f;
    float y = 0.0f;
    bool jumpKeyDown = false;
    bool jumpKeyPressed = false;
    bool jumpKeyUp = false;

    //Accessors for specs
    float speed { get { return specs.speed; } }
    float acceleration { get { return specs.acceleration; } }
    float friction { get { return specs.friction; } }
    float jumpImpulse { get { return specs.jumpImpulse; } }
    float jumpPower { get { return specs.jumpPower; } }
    float jumpsAllowed { get { return specs.jumpsAllowed; } }
    float jumpHoldTime { get { return specs.jumpHoldTime; } }
    float gravity { get { return specs.gravity; } }
    float terminalVelocity { get { return specs.terminalVelocity; } }

    public string horizontal;
    public string vertical;
    public KeyCode jumpKey;

    // Use this for initialization
    void Start() {
        rb = GetComponent<Rigidbody>();
        nextPosition = transform.position;
    }

    // Update is called once per frame
    void Update() {
        ReadInput();
    }

    void FixedUpdate() {

        if (GameController.state != "playing")
            return;

        if (isOnGround() && verticalMotion <= 0.0f) {
            ApplyFriction();
            verticalMotion = 0.0f;
            jumpCount = 0;
        } else {
            ApplyFriction();
            ApplyGravity();
        }

        HandleInput();
        Vector3 nextPositionHorizontal = transform.position + new Vector3(horizontalMotion, 0.0f, 0.0f) * Time.fixedDeltaTime;
        Vector3 nextPositionVertical = transform.position + new Vector3(0.0f, verticalMotion, 0.0f) * Time.fixedDeltaTime;

        RequestMovement(nextPositionHorizontal);
        RequestMovement(nextPositionVertical);

    }

    public void ReadInput() {

        x = Input.GetAxisRaw(horizontal);
        y = Input.GetAxisRaw(vertical);

        if (Input.GetKeyDown(jumpKey)) {
            jumpKeyDown = true;
        }

        if (Input.GetKey(jumpKey)) {
            jumpKeyPressed = true;
        }

        if (Input.GetKeyUp(jumpKey)) {
            jumpKeyUp = true;
        }

    }

    void HandleInput() {

        Move(x, y);

        if (jumpsAllowed - jumpCount <= 0)
            return;

        if (jumpKeyDown) {
            verticalMotion = 0;
            Jump(jumpImpulse);
            jumpKeyDown = false;
        }

        if (jumpKeyPressed) {
            if (currentHoldTime < jumpHoldTime) {
                Jump(jumpPower);
                currentHoldTime += Time.deltaTime;
            }
        }

        if (jumpKeyUp) {
            currentHoldTime = 0.0f;
            jumpCount++;

            jumpKeyPressed = false;
            jumpKeyUp = false;
        }

    }

    void Move(float x, float y) {

        if (Mathf.Abs(horizontalMotion) < speed) {
            horizontalMotion += x * acceleration;
        } else if (x != 0.0f) {
            horizontalMotion = x * speed;
        }

    }

    public LayerMask solidMask;

    void RequestMovement(Vector3 _nextPosition) {

        if(!Physics.CheckBox(_nextPosition, Vector3.one * 0.3f, Quaternion.identity, solidMask)) {

            rb.MovePosition(transform.position + new Vector3(horizontalMotion, verticalMotion, 0.0f) * Time.fixedDeltaTime);

        }

    }

    void ApplyFriction() {

        if (Mathf.Abs(horizontalMotion) + friction > 0.0f) {
            horizontalMotion += friction * Mathf.Sign(horizontalMotion);
        } else {
            horizontalMotion = 0.0f;
        }

    }

    void Jump(float power) {
        verticalMotion += power;
    }

    void ApplyGravity() {

        if (verticalMotion > terminalVelocity) {
            verticalMotion += gravity;
        } else {
            verticalMotion = terminalVelocity;
        }

    }

    public bool isOnGround() {
        return Physics.Raycast(transform.position, -transform.up, transform.localScale.y * 0.6f);
    }

}