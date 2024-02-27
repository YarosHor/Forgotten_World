using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    private PlayerInput playerInput;
    public GameObject cam;
    private CinemachineVirtualCamera virtualCamera;

    public Vector2 MovementInput { get; private set; }
    public Vector2 CameraInput { get; private set; }
    public int MovementInputX { get; private set; }
    public int MovementInputY { get; private set; }
    public int CameraInputX { get; private set; }
    public int CameraInputY { get; private set; }
    public bool PauseInput { get; private set; }
    public bool MapInput { get; private set; }
    public bool Light1Input { get; private set; }
    public bool Light2Input { get; private set; }
    public bool Heavy1Input { get; private set; }
    public bool UseItemInput { get; private set; }
    public bool HealInput { get; private set; }
    public bool RollInput { get; private set; }
    public bool JumpInput { get; private set; }
    public bool JumpInputStop { get; private set; }
    public bool InteractInput { get; private set; }

    [SerializeField]
    private float inputHoldTime = 0.2f;
    private float jumpInputStartTime;

    public GameObject pauseMenu;
    public GameObject optionMenu;
    public static bool GameIsPaused = false;
    public GameObject PauseMenuUi;
    private void Start()
    {
        //cam = GetComponent<Camera>();
        //virtualCamera = cam.GetComponent<CinemachineVirtualCamera>();
        playerInput = GetComponent<PlayerInput>();


        //cam = Camera.main;
    }

    private void Update()
    {
        CheckJumpInputHoldTime();
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        if (!GameIsPaused)
        {
            Debug.Log("heyyou");
            MovementInput = context.ReadValue<Vector2>();
            MovementInputX = Mathf.RoundToInt(MovementInput.x);
            MovementInputY = Mathf.RoundToInt(MovementInput.y);
        }
    }

    public void OnCameraInput(InputAction.CallbackContext context)
    {
        if (!GameIsPaused)
        {
            Debug.Log("Camera");
            CameraInput = context.ReadValue<Vector2>();
            CameraInputX = Mathf.RoundToInt(CameraInput.x);
            CameraInputY = Mathf.RoundToInt(CameraInput.y);
            virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>().m_ScreenX = (float)(-(CameraInputX * 0.3) + 0.5);
            virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>().m_ScreenY = (float)((CameraInputY * 0.3) + 0.5);
        }
    }

    public void OnPauseInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            PauseInput = true;
            pauseMenu.SetActive(!pauseMenu.activeSelf);
            
            if (pauseMenu.activeSelf)
            {
                Debug.Log("treg1");
                optionMenu.SetActive(false);
                //optionMenu.SetActive(false);
                GameIsPaused = true;
                Time.timeScale = 0f;
            }
            else if (optionMenu.activeSelf)
            {
                Debug.Log("treg2");
                pauseMenu.SetActive(false);
                //optionMenu.SetActive(false);
                GameIsPaused = true;
                Time.timeScale = 0f;
            }
            else
            {
                Debug.Log("treg3");
                optionMenu.SetActive(false);
                pauseMenu.SetActive(false);
                GameIsPaused = false;
                Time.timeScale = 1f;
            }
        }

        if (context.canceled)
        {
            PauseInput = false;
        }
    }

    public void OnMapInput(InputAction.CallbackContext context)
    {
        if (!GameIsPaused)
        {
            if (context.started)
            {
                MapInput = true;
            }

            if (context.canceled)
            {
                MapInput = false;
            }
        }
        
    }

    public void OnLight1Input(InputAction.CallbackContext context)
    {
        if (!GameIsPaused)
        {
            if (context.started)
            {
                Light1Input = true;
            }

            if (context.canceled)
            {
                Light1Input = false;
            }
        }
    }

    public void OnLight2Input(InputAction.CallbackContext context)
    {
        if (!GameIsPaused)
        {
            if (context.started)
            {
                Light2Input = true;
            }

            if (context.canceled)
            {
                Light2Input = false;
            }
        }
    }

    public void OnHeavy1Input(InputAction.CallbackContext context)
    {
        if (!GameIsPaused)
        {
            if (context.started)
            {
                Heavy1Input = true;
            }

            if (context.canceled)
            {
                Heavy1Input = false;
            }
        }
    }

    public void OnUseItemInput(InputAction.CallbackContext context)
    {
        if (!GameIsPaused)
        {
            if (context.started)
            {
                UseItemInput = true;
            }

            if (context.canceled)
            {
                Light2Input = false;
            }
        }
    }

    public void OnHealInput(InputAction.CallbackContext context)
    {
        if (!GameIsPaused)
        {
            if (context.started)
            {
                HealInput = true;
            }

            if (context.canceled)
            {
                HealInput = false;
            }
        }
    }
    public void OnRollInput(InputAction.CallbackContext context)
    {
        if (!GameIsPaused)
        {
            Debug.Log("RollInput");
            if (context.started)
            {
                RollInput = true;
            }

            if (context.canceled)
            {
                RollInput = false;
            }
        }
    }


    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (!GameIsPaused)
        {
            Debug.Log("Jump");
            if (context.started)
            {
                JumpInput = true;
                JumpInputStop = false;
                jumpInputStartTime = Time.time;
            }

            if (context.canceled)
            {
                JumpInputStop = true;
            }
        }
    }

    public void OnInteractInput(InputAction.CallbackContext context)
    {
        if (!GameIsPaused)
        {
            if (context.started)
            {
                InteractInput = true;
            }

            if (context.canceled)
            {
                InteractInput = false;
            }
        }
    }

    public void UseJumpInput() {
        if (!GameIsPaused)
        {
            JumpInput = false;
        }
    }

    private void CheckJumpInputHoldTime()
    {
        if (!GameIsPaused)
        {
            if (Time.time >= jumpInputStartTime + inputHoldTime)
            {
                JumpInput = false;
            }
        }
    }
}
