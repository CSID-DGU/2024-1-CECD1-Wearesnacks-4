using UnityEngine;

public class WindEffect : MonoBehaviour
{
    // �ٶ� ȿ���� ��Ÿ���� ������Ʈ�� ����
    public AudioClip windSound;

    private AudioSource audioSource;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }
    private void Start()
    {
        // �ٶ� �Ҹ� ���
        if (windSound != null && audioSource != null)
        {
            audioSource.clip = windSound;
            audioSource.loop = true; // �ݺ� ��� ����
            audioSource.Play();
        }

        // ��� ��Ӱ� �����
        DarkenBackground();
    }

    private void DarkenBackground()
    {
        // ��� ��Ӱ� ����� (���÷� ȭ���� Ǫ���� ��������ϴ�)
        Camera.main.backgroundColor = Color.blue * 0.3f; // ��Ӱ� ����
    }

    private void OnDestroy()
    {
        // �Ҹ� ����
        if (audioSource != null)
        {
            audioSource.Stop();
        }

        Camera.main.backgroundColor = Color.black;
    }
}
