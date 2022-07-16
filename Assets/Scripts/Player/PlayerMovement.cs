using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed;
    public float turnSpeed = 0.1f;
    private float smoothVel;

    [SerializeField] Joystick joystick;

    float x, z;
    //CharacterController controller;
    Vector3 moveDirection;
    Transform camTransfrom;

    Rigidbody rb;
    Animator anim;

    public static PlayerMovement instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(instance);

        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        camTransfrom = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");

        x = joystick.Horizontal;
        z = joystick.Vertical;

        moveDirection = new Vector3(x, 0, z).normalized;
        if (moveDirection.magnitude >= 0.1f && !CureMechanincs.instance.isVaccinating)
        {
            //transform.rotation = Quaternion.LookRotation(moveDirection * camTransfrom.eulerAngles.y);

            //controller.Move(moveDirection.normalized * movementSpeed * Time.deltaTime);
            //float targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg + camTransfrom.eulerAngles.y;
            //float angle = Mathf.SmoothDamp(transform.eulerAngles.y, targetAngle, ref smoothVel, turnSpeed);

            //transform.rotation = Quaternion.Euler(0f, angle, 0f);

            //moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            transform.Translate(moveDirection * movementSpeed * Time.deltaTime, Space.World);
            transform.rotation = Quaternion.LookRotation(moveDirection);

            //controller.Move(moveDirection * movementSpeed * Time.deltaTime);
        }
        else
            moveDirection = Vector3.zero;
        anim.SetFloat("magnitude", moveDirection.magnitude);
    }

    private void FixedUpdate()
    {
        rb.angularVelocity = Vector3.zero;
        rb.velocity = Vector3.zero;
    }

    private void OnDisable()
    {
        moveDirection = Vector3.zero;
    }


}
