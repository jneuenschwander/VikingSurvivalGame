using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VKG;

public class PlayerLocomotion : MonoBehaviour
{
    private Transform cameraObject;
    private InputHandler inputHandler;
    private Vector3 moveDirection;
    [HideInInspector] 
    public Transform mytransform;

    [HideInInspector] 
    public AnimatorHandler animatorHandler;
    public new Rigidbody rigidbody;
    public GameObject normalCamera;

    [Header("Stats")] 
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private float rotationsSpeed = 10f;
    

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        inputHandler = GetComponent<InputHandler>();
        animatorHandler = GetComponentInChildren<AnimatorHandler>();
        if (!(Camera.main is null)) cameraObject = Camera.main.transform;
        mytransform = transform;
        animatorHandler.Initialize();

    }

    public void Update()
    {
        float delta = Time.deltaTime;
        inputHandler.TickInput(delta);
        moveDirection = cameraObject.forward * inputHandler.vertical;
        moveDirection += cameraObject.right * inputHandler.horizontal;
        moveDirection.Normalize();
        moveDirection.y = 0;
        float speed = movementSpeed;
        moveDirection *= speed;

        Vector3 projectedVelocity = Vector3.ProjectOnPlane(moveDirection, normalVector);
        rigidbody.velocity = projectedVelocity;
        
        animatorHandler.UpdateAnimatorValues(inputHandler.moveAmount, 0);
        
        if (animatorHandler.canRotate)
        {
            HandleRotation(delta);//??
        }
    }
    
    //region Movement
    private Vector3 normalVector; 
    private Vector3 targetPosition;

    public PlayerLocomotion(Vector3 normalVector)
    {
        this.normalVector = normalVector;
    }

    private void HandleRotation(float delta)
    {
        Vector3 targetDir = Vector3.zero;
        float moveOverride = inputHandler.moveAmount;

        targetDir = cameraObject.forward * inputHandler.vertical;
        targetDir += cameraObject.right * inputHandler.horizontal;

        targetDir.Normalize();
        targetDir.y = 0;

        if (targetDir == Vector3.zero)
        {
            targetDir = mytransform.forward;

        }

        float rs = rotationsSpeed;
        Quaternion tr = Quaternion.LookRotation(targetDir);
        Quaternion targetRotation = Quaternion.Slerp(mytransform.rotation, tr, rs * delta);

        mytransform.rotation = targetRotation;

    }
}
