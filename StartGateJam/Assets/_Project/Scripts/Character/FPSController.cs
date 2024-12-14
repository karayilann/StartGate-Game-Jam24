using System.Collections;
using _Project.Scripts.Core;
using _Project.Scripts.UI;
using UnityEngine;

namespace _Project.Scripts.Character
{
    public class FPSController : MonoSingleton<FPSController>
    {
        [Header("Movement Settings")]
        [SerializeField] private float moveSpeed = 100f;
        [SerializeField] private float sprintMultiplier = 1.5f;
        [SerializeField] private float jumpForce = 5f;
        public bool canMove = true;
        public bool canPlayable = true;

        [Header("Camera Settings")]
        [SerializeField] private Camera playerCamera;
        public float mouseSensitivity = 100f;
        [SerializeField] private float cameraVerticalLimit;
        public bool canLook = true;

        [Header("Ground Check")]
        [SerializeField] private Transform groundCheck;
        [SerializeField] private float groundCheckRadius = 0.2f;
        [SerializeField] private LayerMask groundLayer;

        [Header("Audio")]
        public AudioSource audioSource;
        public AudioClip footstepClip;
        public AudioClip jumpClip;
        
        [Space(10f)]
        public Rigidbody rb;
        public GameObject pausePanel;
        private float _verticalRotation = 0f;
        private bool _isGrounded;
        private bool _isMoving;

        private Vector3 _movementInput;

        void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        void Update()
        {
            HandleMouseLook();
            HandleMovementInput();
            HandleJump();
            // if (Input.GetKeyDown(KeyCode.Escape) && canPlayable)
            // {
            //     pausePanel.SetActive(true);
            //     canMove = false;
            //     canLook = false;
            //     Cursor.lockState = CursorLockMode.None;
            //     Cursor.visible = true;
            //     canPlayable = false;
            // }
            audioSource.volume = Mathf.Clamp(MenuManager.MasterVolumeValue, 0, 1);
            mouseSensitivity = MenuManager.MouseSensitivityValue;
        }

        void FixedUpdate()
        {
            ApplyMovement();
        }

        private void HandleMouseLook()
        {
            if(!canLook) return;
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime * 10;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime * 10;

            _verticalRotation -= mouseY;
            _verticalRotation = Mathf.Clamp(_verticalRotation, -cameraVerticalLimit, cameraVerticalLimit);

            rb.MoveRotation(rb.rotation * Quaternion.Euler(Vector3.up * mouseX));
            playerCamera.transform.localRotation = Quaternion.Euler(_verticalRotation, 0f, 0f);
        }


        private void HandleMovementInput()
        {
            float horizontalInput = Input.GetAxisRaw("Horizontal");
            float verticalInput = Input.GetAxisRaw("Vertical");

            _movementInput = (transform.right * horizontalInput + transform.forward * verticalInput).normalized;
        }

        private void ApplyMovement()
        {
            if (!canMove) return;
            _isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);

            float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? moveSpeed * sprintMultiplier : moveSpeed;
            Vector3 targetVelocity = _movementInput * currentSpeed;

            rb.velocity = new Vector3(targetVelocity.x, rb.velocity.y, targetVelocity.z);
            _isMoving = _movementInput.magnitude > 0;

            // if (_isGrounded && _isMoving)
            // {
            //     if (!audioSource.isPlaying)
            //     {
            //         audioSource.clip = footstepClip;
            //         audioSource.loop = true;
            //         audioSource.Play();
            //         Debug.Log("Footstep sound started");
            //     }
            // }
        }

        private void HandleJump()
        {
            if (Input.GetButtonDown("Jump") && _isGrounded)
            {
                rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);

                //audioSource.Stop();
                //audioSource.clip = jumpClip;
                audioSource.PlayOneShot(jumpClip);


                //StartCoroutine(TemporaryGroundedDelay());
            }
        }

        private IEnumerator TemporaryGroundedDelay()
        {
            yield return new WaitForSeconds(0.1f);
            _isGrounded = false;
        }
        
        void OnDrawGizmosSelected()
        {
            if (groundCheck != null)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
            }
        }
    }
}
