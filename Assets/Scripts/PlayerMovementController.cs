using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Audio;
using UnityEngine.InputSystem;

public class PlayerMovementController : MonoBehaviour
{
    private CharacterController characterController;
    private Vector3 moveDirection = Vector3.zero;
    private Vector3 inputDir;
    private Vector3 velocity;
    private float magnitude;
    public float runSpeed = 6f;
    public float walkSpeed = 2f;
    public float turnSmoothTime = 0.2f;
    float turnSmoothVelocity;
    public float speedSmoothTime = 0.1f;
    float speedSmoothVelocity;
    float currentSpeed;
    public float rotationSpeed;
    private float ySpeed;
    public float jumpHeight;
    [SerializeField] private float gravityMultiplyer;
    [SerializeField] private Game game;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask thisIsMyGround;
    [SerializeField] private float radius;
    [SerializeField] private VelocityOfThePlayer playerVelocityOnY;
    [SerializeField] private TrailRenderer trailRenderer;
    private float originalStepOffset;
    public float jumpButtonGracePeriod;
    private float? lastGroundTime;
    private float? jumpButtonPressedTime;
    public float rewindAvailable;
    //private Transform transform;
    private float iAmDashingTimeStart;

    private int superJump;

    float speed;

    private float? lastGroundTouch;

    private bool isJumping;
    private bool isGrounded;

    float jumpPower = 1f;

    Transform CameraT;

    Animator animator;

    public bool isRewinding = false;

    List<PointInTime> pointsInTime;
    List<float> isRunning;

    private bool canDash = true;
    private bool isDashing;
    private float dashingTime = 0.3f;
    private bool suddenYSpeedChangeFix;

    [SerializeField] private RayCast rayScript;

    List<float> shiftPressed;
    private bool checkRunning;

    List<bool> dashing;

    [SerializeField] GameObject spike;

    public bool respawnRewind = false;

    private float rPressed;

    private Respawn respawn;

    private Game gameManager;

    private float ctrlPressed;


    private float walkSoundStart = 0f;
    private float runSoundStart = 0f;
    private bool rewindSoundCheck = false;
    [SerializeField] private AudioSource walkingSound;
    [SerializeField] private AudioSource runSound;
    [SerializeField] private AudioSource dash;
    [SerializeField] private AudioSource collection;
    [SerializeField] private AudioSource rewind;

    private InputAction move;
    private GameManagerInputSystem playerInputSystem;
    private bool jumpButtonDown;
    private bool shiftPressedByNew;
    private bool jumpFirstPress = false;
    public bool rPressedByNew = false;
    public bool timeIsStopped = false;

    private void Awake()
    {
        respawn = FindObjectOfType<Respawn>();
        gameManager = FindObjectOfType<Game>();
        playerInputSystem = new GameManagerInputSystem();
    }

    private void OnEnable()
    {
        move = playerInputSystem.PlayerControls.Move;
        move.Enable();

        playerInputSystem.PlayerControls.Jump.started += JumpStart;
        playerInputSystem.PlayerControls.Jump.performed += JumpStart;
        playerInputSystem.PlayerControls.Jump.canceled += JumpEnd;
        playerInputSystem.PlayerControls.Jump.Enable();

        playerInputSystem.PlayerControls.Dash.performed += Dash;
        playerInputSystem.PlayerControls.Dash.Enable();

        playerInputSystem.PlayerControls.Shift.performed += ShiftPressed;
        playerInputSystem.PlayerControls.Shift.canceled += ShiftNotPressed;
        playerInputSystem.PlayerControls.Shift.Enable();

        playerInputSystem.PlayerControls.Rewind.performed += RPressed;
        playerInputSystem.PlayerControls.Rewind.canceled += RPressed;
        playerInputSystem.PlayerControls.Rewind.Enable();

        playerInputSystem.PlayerControls.Ctrl.started += TimeManipulation;
        playerInputSystem.PlayerControls.Ctrl.performed += TimeManipulation;
        playerInputSystem.PlayerControls.Ctrl.canceled += TimeManipulation;
        playerInputSystem.PlayerControls.Ctrl.Enable();
    }

    private void OnDisable()
    {
        move = playerInputSystem.PlayerControls.Move;
        move.Disable();

        playerInputSystem.PlayerControls.Jump.performed -= JumpStart;
        playerInputSystem.PlayerControls.Jump.started -= JumpStart;
        playerInputSystem.PlayerControls.Jump.canceled -= JumpEnd;
        playerInputSystem.PlayerControls.Jump.Disable();

        playerInputSystem.PlayerControls.Dash.performed -= Dash;
        playerInputSystem.PlayerControls.Dash.Disable();

        playerInputSystem.PlayerControls.Shift.performed -= ShiftPressed;
        playerInputSystem.PlayerControls.Shift.canceled -= ShiftNotPressed;
        playerInputSystem.PlayerControls.Shift.Disable();

        playerInputSystem.PlayerControls.Rewind.performed -= RPressed;
        playerInputSystem.PlayerControls.Rewind.canceled -= RPressed;
        playerInputSystem.PlayerControls.Rewind.Disable();

        playerInputSystem.PlayerControls.Ctrl.started -= TimeManipulation;
        playerInputSystem.PlayerControls.Ctrl.performed -= TimeManipulation;
        playerInputSystem.PlayerControls.Ctrl.canceled -= TimeManipulation;
        playerInputSystem.PlayerControls.Ctrl.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        originalStepOffset = characterController.stepOffset;
        CameraT = Camera.main.transform;
        pointsInTime = new List<PointInTime>();
        isRunning = new List<float>();
        //transform = GetComponent<Transform>();
        shiftPressed = new List<float>();
        dashing = new List<bool>();

        rPressed = 1f;
        ctrlPressed = 1f;

        for (int i = 0; i < 100; i++)
        {
            shiftPressed.Add(0);
        }

        transform.position = respawn.posi;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.gameIsPause && !IsGrounded())
        {
            ySpeed = 0f * Time.fixedDeltaTime;
        }

        if (timeIsStopped)
        {
            ctrlPressed = Time.time;
        }

        if (IsGrounded() == false && (Time.time - lastGroundTouch) > 4f && (Time.time - rPressed) > 4f && (Time.time - ctrlPressed) > 4f && isRewinding == false && gameManager.finish == false && gameManager.gameIsPause == false)
        {
            FindObjectOfType<Game>().Restart();
            lastGroundTouch = null;
        }

        if (Time.timeScale < 0.1f)
        {
            return;
        }

        Vector3 input = Quaternion.Euler(0, CameraT.eulerAngles.y, 0) * new Vector3(move.ReadValue<Vector2>().x, 0, move.ReadValue<Vector2>().y);
        inputDir = input.normalized;

        bool running = shiftPressedByNew;

        checkRunning = shiftPressedByNew;

        if (checkRunning && IsGrounded() && inputDir != Vector3.zero)
        {
            shiftPressed.Add(1);
        }
        else if (!checkRunning && IsGrounded() && inputDir != Vector3.zero)
        {
            shiftPressed.Add(0);
        }
        else if (IsGrounded() && inputDir == Vector3.zero)
        {
            shiftPressed.Add(0.5f);
        }

        if (shiftPressed.Count > 100000f)
        {
            isRunning.RemoveAt(0);
        }

        if (!IsGrounded() && shiftPressed[shiftPressed.Count - 1] == 1 && jumpButtonDown)
        {
            speed = runSpeed * inputDir.magnitude;
        }
        else if (!IsGrounded() && shiftPressed[shiftPressed.Count - 1] == 0 && jumpButtonDown)
        {
            speed = walkSpeed * inputDir.magnitude;
        }
        else if (running & IsGrounded())
        {
            speed = runSpeed * inputDir.magnitude;
        }
        else if (!running & IsGrounded())
        {
            speed = walkSpeed * inputDir.magnitude;
        }
        else if (!IsGrounded() && shiftPressed[shiftPressed.Count - 1] == 0.5f)
        {
            speed = walkSpeed * inputDir.magnitude;
        }

        magnitude = Mathf.Clamp01(inputDir.magnitude) * speed;

        currentSpeed = Mathf.SmoothDamp(currentSpeed, speed, ref speedSmoothVelocity, speedSmoothTime);

        float gravity;

        gravity = Physics.gravity.y * gravityMultiplyer;

        if (isJumping && ySpeed > 0 && jumpButtonDown == false && !isRewinding && gameManager.gameIsPause == false)
        {
            gravity *= 2;
        }

        if (isDashing == false && !isRewinding && gameManager.gameIsPause == false)
        {
            ySpeed += gravity * Time.deltaTime;
        }

        if (isDashing == true && !IsGrounded())
        {
            ySpeed = 0f;
        }

        if (isRewinding && !IsGrounded())
        {
            ySpeed = 0f;
        }

        if (gameManager.gameIsPause && !IsGrounded())
        {
            ySpeed = 0f;
        }

        /*        if(suddenYSpeedChangeFix == true && isDashing == false)
                {
                    ySpeed += gravity * Time.deltaTime;
                }*/

        if (superJump > 0 && jumpButtonDown)
        {
            jumpPower = 1.6f;
            superJump--;
        }

        if (characterController.isGrounded)
        {
            lastGroundTime = Time.time;
        }

        if (jumpFirstPress)
        {
            jumpButtonPressedTime = Time.time;
            jumpFirstPress = false;
        }

        if (Time.time - lastGroundTime <= jumpButtonGracePeriod && isRewinding == false)
        {
            characterController.stepOffset = originalStepOffset;
            ySpeed = -0.5f;
            animator.SetBool("IsGrounded", true);
            isGrounded = true;
            animator.SetBool("IsJumping", false);
            isJumping = false;
            animator.SetBool("IsFalling", false);



            if (Time.time - jumpButtonPressedTime <= jumpButtonGracePeriod)
            {
                ySpeed = Mathf.Sqrt(jumpHeight * -3 * gravity) * jumpPower;
                jumpPower = 1f;
                animator.SetBool("IsJumping", true);
                isJumping = true;
                jumpButtonPressedTime = null;
                lastGroundTime = null;
            }

        }
        else
        {
            characterController.stepOffset = 0;
            animator.SetBool("IsGrounded", false);
            isGrounded = false;

            if ((isJumping && ySpeed < 0) || ySpeed < -2)
            {
                animator.SetBool("IsFalling", true);
            }
        }

        if ((characterController.collisionFlags & CollisionFlags.Above) != 0 && isDashing == false)
        {
            if (velocity.y > 0)
            {
                ySpeed += 15f * gravity * Time.deltaTime;
            }
        }

        if (isRewinding == false)
        {
            velocity = inputDir * magnitude;
            velocity.y = ySpeed;
            characterController.Move(velocity * Time.deltaTime);
            //characterController.Move(inputDir * dashingPower * Time.deltaTime);
        }

        currentSpeed = new Vector2(characterController.velocity.x, characterController.velocity.z).magnitude;

        if (inputDir != Vector3.zero)
        {
            animator.SetBool("IsMoving", true);
            // transform.forward = inputDir; 
            Quaternion toRotation = Quaternion.LookRotation(inputDir, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
            //  float targetRotation = Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg + CameraT.eulerAngles.y;
            // transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, turnSmoothTime);

        }
        else
        {
            animator.SetBool("IsMoving", false);
        }


        if (IsGrounded())
        {
            animator.SetBool("IsGrounded", true);
        }
        else
        {
            animator.SetBool("IsGrounded", false);
        }

        if (Time.time - iAmDashingTimeStart >= dashingTime)
        {
            animator.SetBool("Dash", false);
            isDashing = false;
            trailRenderer.emitting = false;
        }

        if (Time.time - iAmDashingTimeStart >= dashingTime + 0.5f)
        {
            suddenYSpeedChangeFix = false;
        }

        if (Time.time - iAmDashingTimeStart >= .5f && IsGrounded())
        {
            canDash = true;
        }


        if (rPressedByNew == false || isRewinding == false)
        {
            float animationSpeedPercent = ((running) ? currentSpeed / runSpeed : currentSpeed / walkSpeed * .5f);
            animator.SetFloat("speedPercent", animationSpeedPercent, speedSmoothTime, Time.deltaTime);
        }


        if (IsGroundedBigSphere() && isRewinding)
        {
            animator.SetBool("GroundThud", true);
        }
        else if (IsGroundedBigSphere() != false && isRewinding != false)
        {
            animator.SetBool("GroundThud", false);
        }


        if (rPressedByNew && isRewinding == true && IsGrounded() && dashing[dashing.Count - 1] == false)
        {
            animator.SetBool("GroundThud", false);
            animator.SetBool("IsGroundedInReverse", true);
            animator.SetBool("IsJumpingInReverse", false);
            animator.SetBool("ReverseDash", false);
            animator.SetFloat("reversePercent", isRunning[isRunning.Count - 1]);
            isRunning.RemoveAt(isRunning.Count - 1);
            dashing.RemoveAt(dashing.Count - 1);
        }

        if (rPressedByNew && isRewinding == true && IsGrounded() == false && playerVelocityOnY.valueNeeded >= 0)
        {
            animator.SetBool("IsJumpingInReverse", true);
            animator.SetBool("IsGroundedInReverse", false);
            animator.SetBool("GroundThud", false);
        }
        else if (rPressedByNew && isRewinding == true && IsGrounded() == false && playerVelocityOnY.valueNeeded < 0)
        {
            animator.SetBool("VelocityChange", true);
        }
        else if (rPressedByNew && isRewinding == true && dashing[dashing.Count - 1] == true)
        {
            animator.SetBool("ReverseDash", true);
            animator.SetBool("IsGroundedInReverse", false);
            dashing.RemoveAt(dashing.Count - 1);
        }


        if (isRewinding == false && IsGrounded() && isDashing == false)
        {
            if (currentSpeed > 3.1)
            {
                isRunning.Add(1);
            }
            else if (currentSpeed <= 3 && currentSpeed > 0)
            {
                isRunning.Add(0.5f);
            }
            else if (currentSpeed == 0)
            {
                isRunning.Add(0);
            }

            if (isRunning.Count > Mathf.Round(5f / Time.fixedDeltaTime))
            {
                isRunning.RemoveAt(0);
            }
            // Debug.Log(isRunning[isRunning.Count - 1]);
        }

        if (isRewinding == false && isDashing == true)
        {
            dashing.Add(true);
        }
        else if (isRewinding == false && isDashing == false)
        {
            dashing.Add(false);
        }
        if (dashing.Count > Mathf.Round(5f / Time.fixedDeltaTime))
        {
            dashing.RemoveAt(0);
        }

        if (IsGrounded())
        {
            lastGroundTouch = Time.time;
        }

        if (rPressedByNew)
        {
            rPressed = Time.time;
        }

        if (speed > 0 && checkRunning == false && IsGrounded() && isRewinding == false && isDashing == false && (Time.time - walkSoundStart) > 0.6f)
        {
            walkingSound.Play();
            walkSoundStart = Time.time;
        }
        if (speed > 0 && ShifPressedCheck() && IsGrounded() && isRewinding == false && isDashing == false && (Time.time - runSoundStart) > 0.3f)
        {
            runSound.Play();
            runSoundStart = Time.time;
        }

        if (isRewinding && rewindSoundCheck)
        {
            rewind.Play();
            rewindSoundCheck = false;
        }
        else if (isRewinding == false && rewindSoundCheck == false)
        {
            rewind.Stop();
            rewindSoundCheck = true;
        }

    }

    private void FixedUpdate()
    {
        if (isRewinding)
        {
            Rewind();
        }
        else
        {
            Record();
        }

    }

    public void StartRewind()
    {
        isRewinding = true;
        animator.SetBool("IsReverse", true);
    }

    public void StopRewind()
    {
        isRewinding = false;
        if (rewindAvailable > 0)
        {
            rewindAvailable--;
        }

        if (gameManager.gameIsPause)
        {
            Time.timeScale = 1f;
            gameManager.gameIsPause = false;
        }
        animator.SetBool("IsReverse", false);
        animator.SetBool("GroundThud", false);
        animator.SetBool("VelocityChange", false);
        // animator.SetBool("IsJumpingInReverse", false);
        animator.SetBool("IsGroundedInReverse", false);
    }

    public void Rewind()
    {
        if (pointsInTime.Count > 0 && isRunning.Count > 0)
        {
            PointInTime pointInTime = pointsInTime[0];
            transform.position = pointInTime.position;
            transform.rotation = pointInTime.rotation;
            pointsInTime.RemoveAt(0);
        }
        else
        {
            StopRewind();
        }
    }

    public void Record()
    {
        if (pointsInTime.Count > Mathf.Round(120f / Time.fixedDeltaTime))
        {
            pointsInTime.RemoveAt(pointsInTime.Count - 1);
        }
        pointsInTime.Insert(0, new PointInTime(transform.position, transform.rotation));

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8 && other.gameObject.tag == "Coin")
        {
            superJump++;
            Destroy(other.gameObject);
        }

        if (other.gameObject.layer == 12 && other.gameObject.tag == "RewindCoin")
        {
            Destroy(other.gameObject);
            collection.Play();
            respawnRewind = true;
            rewindAvailable++;
        }
    }


    /*    private void OnApplicationFocus(bool focus)
        {
            if (focus)
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
            }
        }*/

    public bool IsGrounded()
    {
        return Physics.CheckSphere(groundCheck.position, radius, thisIsMyGround);
    }

    public bool IsGroundedBigSphere()
    {
        return Physics.CheckSphere(groundCheck.position, 0.8f, thisIsMyGround);
    }

    public bool ShifPressedCheck()
    {
        return shiftPressedByNew;
    }

    public void JumpStart(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            jumpFirstPress = true;
        }
        if (context.performed)
        {
            jumpButtonDown = true;
        }
    }

    public void JumpEnd(InputAction.CallbackContext context)
    {
        if (context.canceled)
        {
            jumpButtonDown = false;
        }
    }

    public void Dash(InputAction.CallbackContext context)
    {
        if (canDash == true && isRewinding == false)
        {
            animator.SetBool("IsJumping", false);
            isDashing = true;
            iAmDashingTimeStart = Time.time;
            canDash = false;
            dash.Play();
            // transform.DOMove(transform.position + (transform.forward * rayScript.hitDistance), dashingTime).SetEase(Ease.OutSine);
            animator.SetBool("Dash", true);
            trailRenderer.emitting = true;
            suddenYSpeedChangeFix = true;


            if (rayScript.IsHit())
            {
                transform.DOMove(transform.position + (transform.forward * (rayScript.hitDistance)), 0.2f).SetEase(Ease.OutSine);
            }
            else
            {
                transform.DOMove(transform.position + (transform.forward * 5f), dashingTime).SetEase(Ease.OutSine);
            }
        }
    }

    public void ShiftPressed (InputAction.CallbackContext context)
    {
        shiftPressedByNew = true;
    }

    public void ShiftNotPressed (InputAction.CallbackContext context)
    {
        shiftPressedByNew = false;
    }

    public void RPressed (InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if(rewindAvailable > 0)
            {
                StartRewind();
            }
            rPressedByNew = true;
        }
        if (context.canceled)
        {
            StopRewind();
            rPressedByNew=false;
        }
    }

    public void TimeManipulation(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            timeIsStopped = true;
        }

        if (context.canceled)
        {
            timeIsStopped = false;
        }
    }

}
