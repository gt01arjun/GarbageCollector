using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Vehicle : MonoBehaviour
{
    private PlayerControls _playerControls;
    private Vector2 _inputAxis;

    public bool controllable = true;

    [Header("Components")]
    public Transform vehicleModel;
    public Rigidbody sphere;

    [Header("Parameters")]
    [Range(5.0f, 50.0f)] public float acceleration = 30f;
    [Range(0, 160.0f)] public float steering = 80f;
    [Range(0.0f, 20.0f)] public float gravity = 10f;
    [Range(0.0f, 1.0f)] public float drift = 1f;

    public bool touching;
    public bool touched;
    public Transform lookAt;
    public Transform desiredRot;

    private Transform body;
    private Transform wheelFrontLeft, wheelFrontRight;
    private ParticleSystem smokeLeft, smokeRight;

    private GameManager gameManager;
    private Vector3 lastTouchPos;
    private float speed, speedTarget;

    private void Awake()
    {
        _playerControls = new PlayerControls();

        foreach (Transform t in GetComponentsInChildren<Transform>())
        {
            switch (t.name)
            {
                // Vehicle components
                case "wheelFrontLeft": wheelFrontLeft = t; break;
                case "wheelFrontRight": wheelFrontRight = t; break;
                case "GarbageTruckBody": body = t; break;

                // Vehicle effects
                case "SmokeLeft": smokeLeft = t.GetComponent<ParticleSystem>(); break;
                case "SmokeRight": smokeRight = t.GetComponent<ParticleSystem>(); break;
            }
        }
        gameManager = FindObjectOfType<GameManager>();
    }

    private void OnEnable()
    {
        _playerControls.Controls.Accelerate.performed += OnAccelerate;
        _playerControls.Controls.Accelerate.canceled += OnAccelerate;
        _playerControls.Controls.Rotate.performed += OnRotate;
        _playerControls.Controls.Rotate.canceled += OnRotate;
        _playerControls.Enable();
    }
    private void OnDisable()
    {
        _playerControls.Controls.Accelerate.performed -= OnAccelerate;
        _playerControls.Controls.Accelerate.canceled -= OnAccelerate;
        _playerControls.Controls.Rotate.performed -= OnRotate;
        _playerControls.Controls.Rotate.canceled -= OnRotate;
        _playerControls.Disable();
    }

    public void OnAccelerate(InputAction.CallbackContext context)
    {
        if (context.ReadValueAsButton())
        {
            touching = true;
        }
        else
        {
            touching = false;
            touched = false;
        }
    }

    public void OnRotate(InputAction.CallbackContext context)
    {
        _inputAxis = context.ReadValue<Vector2>();
    }


    private void Update()
    {
        if (gameManager.gameOver) return;

        // Steering
        transform.rotation = Quaternion.Lerp(transform.rotation, desiredRot.rotation, Time.deltaTime * steering);

        // Acceleration
        speedTarget = Mathf.SmoothStep(speedTarget, speed * 0.25f, Time.deltaTime * 12f);
        speed = 0f;

        //Wheel and body tilt
        if (wheelFrontLeft != null) { wheelFrontLeft.localRotation = Quaternion.Euler(0, Mathf.Clamp(Mathf.LerpAngle(wheelFrontLeft.localRotation.y, lookAt.eulerAngles.y - transform.eulerAngles.y, 1), -45, 45), 0); }
        if (wheelFrontRight != null) { wheelFrontRight.localRotation = wheelFrontLeft.localRotation; }
        body.localRotation = Quaternion.Slerp(body.localRotation, Quaternion.Euler(new Vector3(-speedTarget * 5, 0, Mathf.Clamp(Mathf.LerpAngle(body.localRotation.y, lookAt.eulerAngles.y - transform.eulerAngles.y, 1), -10, 10))), Time.deltaTime * 4);

        // Particles
        ParticleSystem.EmissionModule smokeEmission = smokeLeft.emission;
        smokeEmission.enabled = sphere.velocity.magnitude > (acceleration / 5) && (Vector3.Angle(sphere.velocity, vehicleModel.forward) > 30.0f);
        smokeEmission = smokeRight.emission;
        smokeEmission.enabled = sphere.velocity.magnitude > (acceleration / 5) && (Vector3.Angle(sphere.velocity, vehicleModel.forward) > 30.0f);

        if (speed == 0 && sphere.velocity.magnitude < 4f)
        {
            sphere.velocity = Vector3.Lerp(sphere.velocity, Vector3.zero, Time.deltaTime * 2.0f);
        }
    }


    private void FixedUpdate()
    {
        if (gameManager.gameOver)
            return;

        ControlAccelerate();

        //if (touching)
        //{
            ControlSteer();
        //}

        RaycastHit hitNear;
        Physics.Raycast(transform.position, Vector3.down, out hitNear, 1.3f);
        vehicleModel.up = Vector3.Lerp(vehicleModel.up, hitNear.normal, Time.deltaTime * 8.0f);
        vehicleModel.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
        transform.position = sphere.transform.position + new Vector3(0, 0.5f, 0);
        Vector3 localVelocity = transform.InverseTransformVector(sphere.velocity);
        localVelocity.x *= 0.9f + (drift / 10);

        sphere.AddForce(vehicleModel.forward * speedTarget, ForceMode.VelocityChange);
        sphere.velocity = transform.TransformVector(localVelocity);
    }

    public void ControlAccelerate()
    {
        if (controllable == false)
            return;

        if (touching)
        {
            speed = acceleration;
        }
        else
        {
            speed = 0;
        }
    }

    public void ControlSteer()
    {
        if (controllable == false)
            return;

        Vector3 v1 = Vector3.zero;
        Vector3 v2 = new Vector3(_inputAxis.x, _inputAxis.y, 0);
        v1 = v2.normalized;
        if (v1.sqrMagnitude < 1E-05f)
        {
            v1 = lastTouchPos;
        }
        else
        {
            Vector3 curTouchPos = transform.position + v1;
            Vector3 v3 = curTouchPos - transform.position;
            float angle = Mathf.Atan2(v3.y, v3.x) * 57.29578f;

            if (touched)
                angle -= 90;

            lookAt.rotation = Quaternion.AngleAxis(angle, Vector3.down);
            desiredRot.rotation = Quaternion.Lerp(desiredRot.rotation, Quaternion.Euler(0, lookAt.eulerAngles.y, 0), steering * Time.deltaTime);
            touched = true;
        }
    }
}