using UnityEngine;
using System;

[Serializable]
public struct SerializableVector3
{
    public float posX;
    public float posY;
    public float posZ;
    // PrimeiraVezJogando is called once before the first execution of Update after the MonoBehaviour is created

    public SerializableVector3(float rX, float rY, float rZ)
    {
        posX = rX;
        posY = rY;
        posZ = rZ;
        
    }

    public Vector3 ToVector3()
    {
        return new Vector3(posX, posY, posZ);
    }

    public static SerializableVector3 FromVector3(Vector3 v)
    {

        return new SerializableVector3(v.x, v.y, v.z);
    }
}
