using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public float velocity;
    public float accel;

    Vector3 startDir;


    void Start()
    {
        startDir = transform.forward;
    }


    void Update()
    {
        //����
        transform.position += transform.forward * Time.deltaTime * velocity;
       
        velocity += accel * Time.deltaTime;
        if (velocity < 0) velocity = 0;
    }

    public void MoveRnd(float f)
    {
        transform.Rotate(transform.up, Random.Range(-f, f)); //���� Y�� ȸ��
        transform.Rotate(transform.right, Random.Range(-f, f)); //���� Y�� ȸ��
    }
}
