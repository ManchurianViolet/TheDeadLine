using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class Bazier : MonoBehaviour
{
    public float endPosRnd = 3;
    public Vector2 time;
    public float m_distanceFromStart = 6.0f; // ���� ������ �������� �󸶳� ������.
    public float m_distanceFromEnd = 3.0f; // ���� ������ �������� �󸶳� ������.
    public LayerMask LayerMask;


    Vector3[] points = new Vector3[4];
    float timerMax = 0;
    float timerCurrent = 0;
    Vector3 before;
    float beforeVelocity;

    void Start()
    {
        Physics.Raycast(transform.position, transform.forward, out RaycastHit hit , 99, LayerMask);
        Debug.Log(hit.point);
        Transform start = transform;
        Vector3  end = hit.point;


        // ���� ������ �ð��� �������� ��.
        timerMax = Random.Range(time.x, time.y);


        Vector3 o = end;
        o += Random.Range(-endPosRnd, endPosRnd) * Vector3.one;
        o += Random.Range(-endPosRnd, endPosRnd) * Vector3.one;
        o.y = 0;




        // ���� ����.
        points[0] = start.position;

        // ���� ������ �������� ���� ����Ʈ ����.
        points[1] = start.position +
            (m_distanceFromStart * Random.Range(-1.0f, 1.0f) * start.right) + // X (��, �� ��ü)
            (m_distanceFromStart * Random.Range(0.1f, 1.0f) * start.up) + // Y (�Ʒ��� ����, ���� ��ü)
            (m_distanceFromStart * Random.Range(-1.0f, -0.8f) * start.forward); // Z (�� �ʸ�)

        // ���� ������ �������� ���� ����Ʈ ����.
        points[2] = o +
            (m_distanceFromEnd * Random.Range(-1.0f, 1.0f) * Vector3.right) + // X (��, �� ��ü)
            (m_distanceFromEnd * Random.Range(0.1f, 1.0f) * Vector3.up) + // Y (��, �Ʒ� ��ü)
            (m_distanceFromEnd * Random.Range(0.8f, 1.0f) * Vector3.forward); // Z (�� �ʸ�)

        // ���� ����.
        points[3] = o;

        transform.position = start.position;
        before = transform.position;
    }

    void Update()
    { 
        if (timerCurrent < timerMax)
        {
            // ��� �ð� ���.
            timerCurrent += Time.deltaTime;

            // ������ ����� X,Y,Z ��ǥ ���.
            transform.position = new Vector3(
                CubicBezierCurve(points[0].x, points[1].x, points[2].x, points[3].x),
                CubicBezierCurve(points[0].y, points[1].y, points[2].y, points[3].y),
                CubicBezierCurve(points[0].z, points[1].z, points[2].z, points[3].z));

            beforeVelocity = 5 +  Vector3.Distance(transform.position, before) / Time.deltaTime;
        }
        else
        {
            //����
            transform.position += transform.forward * beforeVelocity* Time.deltaTime;
        }

        transform.forward = transform.position - before;



        before = transform.position;
    }

    private float CubicBezierCurve(float a, float b, float c, float d)
    {
        float t = timerCurrent / timerMax; // (���� ��� �ð� / �ִ� �ð�)

        float ab = Mathf.Lerp(a, b, t);
        float bc = Mathf.Lerp(b, c, t);
        float cd = Mathf.Lerp(c, d, t);

        float abbc = Mathf.Lerp(ab, bc, t);
        float bccd = Mathf.Lerp(bc, cd, t);

        return Mathf.Lerp(abbc, bccd, t);
    }
}