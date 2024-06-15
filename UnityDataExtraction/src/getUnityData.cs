using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class localData : MonoBehaviour
{
    public Transform spine01;
    private Quaternion previousRotation;
    private Vector3 previousPosition;
    private Vector3 previousVelocity;
    private float deltaTime = 0.02f;
    private float elapsedTime = 0f;
    private int fileIndex = 1;
    private string dataDirectory;

    void Start()
    {
        dataDirectory = Application.dataPath + "/DataLogs/";
        if (!Directory.Exists(dataDirectory))
        {
            Directory.CreateDirectory(dataDirectory);
        }

        previousRotation = spine01.localRotation;
        previousPosition = spine01.localPosition;
        previousVelocity = Vector3.zero;

        InvokeRepeating("LogData", 0f, deltaTime);
    }

    void LogData()
    {
        elapsedTime += deltaTime;

        // ������ǥ��� �߷°��ӵ� ��ȯ
        Vector3 gravityAcceleration = spine01.InverseTransformDirection(Physics.gravity);

        // ���� ���� ȸ��
        Quaternion currentRotation = spine01.localRotation;
        Quaternion deltaRotation = currentRotation * Quaternion.Inverse(previousRotation);

        // ���ӵ� ��� (���� ��ǥ��)
        deltaRotation.ToAngleAxis(out float angle, out Vector3 axis);
        if (angle > 180f) angle -= 360f;
        Vector3 angularVelocity = axis * angle * Mathf.Deg2Rad / deltaTime;

        previousRotation = currentRotation;

        // ���� ���� ��ġ
        Vector3 currentPosition = spine01.localPosition;
        Vector3 currentVelocity = (currentPosition - previousPosition) / deltaTime;
        Vector3 linearAcceleration = (currentVelocity - previousVelocity) / deltaTime;

        previousPosition = currentPosition;
        previousVelocity = currentVelocity;

        // ������ ����
        string data = string.Format("Time: {0:F2}\nGravity Acceleration: ({1:F2}, {2:F2}, {3:F2})\nAngular Velocity: ({4:F2}, {5:F2}, {6:F2})\nLinear Acceleration: ({7:F2}, {8:F2}, {9:F2})\n\n",
                                    elapsedTime,
                                    gravityAcceleration.x, gravityAcceleration.y, gravityAcceleration.z,
                                    angularVelocity.x, angularVelocity.y, angularVelocity.z,
                                    linearAcceleration.x, linearAcceleration.y, linearAcceleration.z);

        string filePath = dataDirectory + "ldata_" + fileIndex + ".txt";
        File.AppendAllText(filePath, data);

        // 1.28�ʸ��� ���ο� ���� ����
        if (elapsedTime >= 1.28f * fileIndex)
        {
            fileIndex++;
        }
    }
}