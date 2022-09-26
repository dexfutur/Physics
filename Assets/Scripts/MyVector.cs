using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public struct MyVector
{
    public float x;
    public float y;


    public float magnitude
    {
        get
        {
            return Mathf.Sqrt(x * x + y * y);
        }
    }

    public MyVector normalized
    {
        get
        {
            float m = magnitude;

            if (m <= 0.0001f)
            {
                return new MyVector(0f, 0f);
            }

            return new MyVector(x / m, y / m);
        }
    }

    public MyVector(float x, float y)
    {
        this.x = x;
        this.y = y;
    }

    public MyVector Scale(float a)
    {
        MyVector d = new MyVector(
            this.x * a,
            this.y * a
            );

        return d;
    }

    public void Normalize()
    {
        float tolerance = 0.0001f;
        float m = magnitude;

        if (m <= tolerance)
        {
            x = 0; y = 0;

            return;
        }

        x /= m;
        y /= m;

    }
    public MyVector div(float a)
    {
        return new MyVector(
            x / a,
            y / a);
    }

    public static MyVector operator /(float b, MyVector a)
    {
        return a.div(b);
    }

    public static MyVector operator /(MyVector a, float b)
    {
        return a.div(b);
    }
    public void Draw(Color color)
    {
        Debug.DrawLine(Vector3.zero, new Vector3(x, y), color);
    }
    public void Draw(MyVector newOrigin, Color color)
    {
        Debug.DrawLine(new Vector3(newOrigin.x, newOrigin.y), new Vector3(newOrigin.x + x, newOrigin.y + y), color);
    }
    public static MyVector operator +(MyVector a, MyVector b)
    {  //Suma
        return new MyVector(a.x + b.x, a.y + b.y);
    }
    public static MyVector operator -(MyVector a, MyVector b)
    {  //Resta
        return new MyVector(a.x - b.x, a.y - b.y);
    }
    public static MyVector operator *(MyVector a, float b)
    {     //Producto1
        return new MyVector(a.x * b, a.y * b);
    }
    public static MyVector operator *(float b, MyVector a)
    {     //Producto2
        return new MyVector(a.x * b, a.y * b);
    }
    public MyVector Mult(MyVector a)
    {                           //Producto3
        return new MyVector(x * a.x, y * a.y);
    }
}