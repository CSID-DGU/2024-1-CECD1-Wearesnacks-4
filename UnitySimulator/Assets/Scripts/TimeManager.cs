using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public float dayLengthInSeconds = 120f; // �Ϸ��� ���� (��)
    public float elapsedTime = 0f; // ����� �ð�

    public Light light1; // ���� 1
    public Light light2; // ���� 2

    private bool light1Active = true; // ���� 1 Ȱ��ȭ ����
    private bool light2Active = true; // ���� 2 Ȱ��ȭ ����

    private void Update()
    {
        elapsedTime += Time.deltaTime; // ����� �ð��� ����

        if (elapsedTime >= dayLengthInSeconds)
        {
            elapsedTime = 0f; // �ٽ� �ʱ�ȭ
            // ���� �ʱ�ȭ
            light1.gameObject.SetActive(true);
            light2.gameObject.SetActive(true);
            light1Active = true;
            light2Active = true;
            Debug.Log("�����Դϴ�.");
        }
        else if (elapsedTime >= dayLengthInSeconds / 3f && light1Active) // 1/3 ��� ��
        {
            light1.gameObject.SetActive(false); // ���� 1 ��Ȱ��ȭ
            light1Active = false;
            Debug.Log("�����Դϴ�.");
        }
        else if (elapsedTime >= (dayLengthInSeconds / 3f) * 2f && light2Active) // 2/3 ��� ��
        {
            light2.gameObject.SetActive(false); // ���� 2 ��Ȱ��ȭ
            light2Active = false;
            Debug.Log("�����Դϴ�.");
        }
    }
}
