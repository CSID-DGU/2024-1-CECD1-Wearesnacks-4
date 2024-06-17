using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallObject : MonoBehaviour
{
    // �ʱ� transform ������ ������ �迭
    public Transform[] objectsToReset;
    public Transform[] objectsToReset1;

    // �ʱ� transform ������ ������ ����
    private Vector3[] initialPositions;
    private Vector3[] initialPositions1;
    private Quaternion[] initialRotations;
    private Quaternion[] initialRotations1;

    private Vector3 initialPrefabPosition;
    private Vector3 initialPrefabPosition1;
    private Quaternion initialPrefabRotation;
    private Quaternion initialPrefabRotation1;

    void Start()
    {
        // �ʱ� transform ���� ����
        SaveInitialTransforms();
        SaveInitialTransforms1();
    }

    void SaveInitialTransforms()
    {
        // �迭 ũ�⸸ŭ �ʱ� transform ���� ����
        int objectCount = objectsToReset.Length;
        initialPositions = new Vector3[objectCount];
        initialRotations = new Quaternion[objectCount];

        for (int i = 0; i < objectCount; i++)
        {
            initialPositions[i] = objectsToReset[i].position;
            initialRotations[i] = objectsToReset[i].rotation;
        }
    }
    void SaveInitialTransforms1()
    {
        // �迭 ũ�⸸ŭ �ʱ� transform ���� ����
        int objectCount1 = objectsToReset1.Length;
        initialPositions1 = new Vector3[objectCount1];
        initialRotations1 = new Quaternion[objectCount1];

        for (int i = 0; i < objectCount1; i++)
        {
            initialPositions1[i] = objectsToReset1[i].position;
            initialRotations1[i] = objectsToReset1[i].rotation;
        }
    }

    public void ResetObjectsToInitialTransforms()
    {
        // ����� �ʱ� transform ������ ����Ͽ� ��� ������Ʈ�� transform�� �缳��
        int objectCount = objectsToReset.Length;
        for (int i = 0; i < objectCount; i++)
        {
            objectsToReset[i].position = initialPositions[i];
            objectsToReset[i].rotation = initialRotations[i];
        }
    }
    public void ResetObjectsToInitialTransforms1()
    {
        // ����� �ʱ� transform ������ ����Ͽ� ��� ������Ʈ�� transform�� �缳��
        int objectCount1 = objectsToReset1.Length;
        for (int i = 0; i < objectCount1; i++)
        {
            objectsToReset1[i].position = initialPositions1[i];
            objectsToReset1[i].rotation = initialRotations1[i];
        }
    }
}
