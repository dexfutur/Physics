using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BolitaForces : MonoBehaviour
{
    public enum BolitaRunMode
    {
        Friction,
        FluidFriction,
        Gravity,
        Gravity2

    }
    public float Mass => mass;

    [SerializeField] private Camera cam;
    [SerializeField] private BolitaForces otherBolita;
    [SerializeField] private MyVector force;

    private MyVector position;
    private MyVector netForce;

    [SerializeField] private MyVector acceleration;
    [SerializeField] private MyVector velocity;
    [SerializeField] private float mass = 1f;
    [SerializeField] private BolitaRunMode runMode;

    [Header("Forces")]
    [SerializeField] private MyVector wind;
    [SerializeField] private MyVector gravity;


    [Header("World")]
    [Range(0f, 1f)][SerializeField] private float DampingFactor = 0.9f;
    [Range(0f, 1f)][SerializeField] private float frictionCoefficient = 0.9f;
    [SerializeField] private bool useFluidFriction = false;
    [SerializeField] private BolitaForces Bolita;


    private void Start()
    {
        position = new MyVector(transform.position.x, transform.position.y);

    }
    private void FixedUpdate()
    {
        acceleration *= 0f;
  

        if (runMode == BolitaRunMode.Gravity)
        {
            MyVector weight = gravity * mass;
            ApplyForce(weight);
        }
        if (runMode == BolitaRunMode.FluidFriction)
        {
            ApplyFluidFriction();
        }
        else if ( runMode == BolitaRunMode.Friction)
        {
            ApplyFriction();
        }
        else if (runMode == BolitaRunMode.Gravity2)
        {
            MyVector diff = otherBolita.position - position;
            float distance = diff.magnitude;
            float scalarPart = (mass * otherBolita.mass) / (distance * distance);
            MyVector gravity = scalarPart * diff.normalized;
            ApplyForce(gravity);
        }

        Move();

    }




    private void Update()
    {

        position.Draw(Color.blue);
        velocity.Draw(position, Color.green);
    }
    
    public void Move()
    {
        velocity = velocity + acceleration * Time.fixedDeltaTime;
        position = position + velocity * Time.fixedDeltaTime;

        if (runMode == BolitaRunMode.Gravity2)
        {
            CheckLimitSpeed();
        }
        else
        {
            CheckWolrdBoxBounds();
        }
    
        

        transform.position = new Vector3(position.x, position.y); 

    }

    private void ApplyForce(MyVector force)
    {
        
        acceleration += force / mass;
    }

    private void ApplyFriction()
    {
        float N = -mass * gravity.y;
        MyVector friction = -frictionCoefficient * N * velocity.normalized;
        ApplyForce(friction);
        friction.Draw(position, Color.blue);
    }

    private void ApplyFluidFriction()
    {

        if (transform.localPosition.y <= 0)
        {
            float frontalArea = transform.localScale.x;
            float rho = 1;
            float fluidDragCoefficient = 1;
            float velocityMagnitude = velocity.magnitude;


            float scalarPart = -0.5f * rho * velocityMagnitude * velocityMagnitude * frontalArea * fluidDragCoefficient;
            MyVector friction = scalarPart * velocity.normalized;
            ApplyForce(friction);
        }
    }

    private void CheckLimitSpeed(float maxSpeed = 10)
    {
        if (velocity.magnitude > maxSpeed)
        {
            velocity = maxSpeed * velocity.normalized;
        }
    }

    private void CheckWolrdBoxBounds()
    {

        if (Mathf.Abs(position.x) > cam.orthographicSize)
        {
            position.x = Mathf.Sign(position.x) * cam.orthographicSize;
            velocity *= -DampingFactor;
        }

        if (Mathf.Abs(position.y) > cam.orthographicSize)
        {
            position.y = Mathf.Sign(position.y) * cam.orthographicSize;
            velocity *= -DampingFactor;
        }
    }

    private void Gravity()
    {
        MyVector r = Bolita.position - position;
        MyVector gravity = ((mass * Bolita.mass) / Mathf.Pow(r.magnitude, 2)) * r.normalized;
        ApplyForce(gravity);
    }
}
