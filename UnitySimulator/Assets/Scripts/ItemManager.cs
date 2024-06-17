using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public GameObject item1; // ù ��° ������
    public GameObject item2; // �� ��° ������
    public GameObject item3; // �� ��° ������
    public GameObject item4;

    private float rotationSpeed1 = -1f; // ù ��° �������� ȸ�� �ӵ�
    private float rotationSpeed2 = 2f;  // �� ��° �������� ȸ�� �ӵ�
    private float rotationSpeed3 = 1f;  // �� ��° �������� ȸ�� �ӵ�

    void Update()
    {
        // �������� X ������ ȸ����Ű��
        RotateItem(item1, rotationSpeed1);
        RotateItem(item2, rotationSpeed2);
        RotateItem(item3, rotationSpeed3);
    }

    // �������� �־��� �ӵ��� ȸ����Ű�� �Լ�
    void RotateItem(GameObject item, float rotationSpeed)
    {
        if (item != null)
        {
            item.transform.Rotate(Vector3.right, rotationSpeed * Time.deltaTime);
        }
    }
}
