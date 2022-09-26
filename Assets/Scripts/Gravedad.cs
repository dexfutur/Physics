using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using System.Security.Cryptography.X509Certificates;
using Random = System.Random;

public class Gravedad : MonoBehaviour
{
    private MyVector position;
    [SerializeField] private MyVector acceleration;
    [SerializeField] private MyVector velocity;
    [SerializeField] private Camera camera;
    [SerializeField] private Transform planet;

    void Start()
    {
        position = new MyVector(transform.position.x, transform.position.y);
    }
    private void FixedUpdate()
    {
        Move();
    }

    private void Update()
    {
        position.Draw(Color.blue);
        velocity.Draw(position, Color.red);
        acceleration.Draw(position, Color.green);
        MyVector position1 = new MyVector(transform.position.x, transform.position.y);
        MyVector planetPosition = new MyVector(planet.position.x, planet.position.y);
        acceleration = planetPosition - position1;

    }

    public void Move()
    {
        velocity = velocity + acceleration * Time.fixedDeltaTime;
        position = position + velocity * Time.fixedDeltaTime;

        transform.position = new Vector3(position.x, position.y);

    }
}