using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public Camera playerCamera;
    public float walkSpeed = 6f;
    public float runSpeed = 12f;
    public float jumpForce = 4f;
    public float gravity = 9.8f;

    public float lookSpeed = 2f;
    public float lookVerticalLimit = 60f;

    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;

    public bool canMove = true;
    public bool hasKey = false;
    public bool isWon = false;

    public GameObject UI;
    public GameObject PauseMenu;
    public GameObject OptionsMenu;

    [SerializeField] private Transform _interactionPoint;
    [SerializeField] private float _interactionPointRadius = 0.5f;
    [SerializeField] private LayerMask _interactableMask;
    [SerializeField] private InteractionPromptUI _interactionPromptUI;

    private readonly Collider[] _colliders = new Collider[3];
    [SerializeField] private int _numFound;

    private IInteractable _interactable;

    CharacterController characterController;
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void setMovement(bool movement)
    {
        canMove = movement;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void setKey(bool keyStatus)
    {
        hasKey = keyStatus;
    }

    public void setWon(bool won)
    {
        isWon = won;
    }

    public bool getKey() { return hasKey; }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            _numFound = Physics.OverlapSphereNonAlloc(_interactionPoint.position, _interactionPointRadius, _colliders, _interactableMask);

            if (_numFound > 0)
            {
                var _interactable = _colliders[0].GetComponent<IInteractable>();

                if (_interactable != null)
                {
                    if (!_interactionPromptUI.isDisplayed || _interactionPromptUI.getText() != _interactable.InteractionPrompt) _interactionPromptUI.SetUp(_interactable.InteractionPrompt);
                    if (Input.GetKeyDown(KeyCode.E)) _interactable.Interact(this);
                }
            }
            else
            {
                if (_interactable != null) _interactable = null;
                if (_interactionPromptUI.isDisplayed) _interactionPromptUI.Close();
            }
        }
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpForce;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        characterController.Move(moveDirection * Time.deltaTime);

        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookVerticalLimit, lookVerticalLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }

        if (Input.GetKeyDown(KeyCode.Escape) && !isWon)
        {
            if (!UI.activeSelf)
            {
                canMove = false;
                UI.SetActive(true);
                PauseMenu.SetActive(true);
                OptionsMenu.SetActive(false);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                PauseMenu.SetActive(false);
                OptionsMenu.SetActive(false);
                UI.SetActive(false);
                canMove = true;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }
}
