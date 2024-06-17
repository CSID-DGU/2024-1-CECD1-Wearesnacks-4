using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private WeatherManager wm;
    public GameObject warning;
    private float timer = 0f; // ��� �ð��� �����ϴ� ����
    private RawImage warningRawImage;

    public Scrollbar scrollbar; // UI Scrollbar
    public Text scaleText;
    private GameObject[] workers; // Worker ������Ʈ �迭
    public GameObject worker3;

    void Awake()
    {
        warningRawImage = warning.GetComponent<RawImage>();
        wm = GameObject.FindObjectOfType<WeatherManager>();

        if (wm == null)
        {
            Debug.LogError("WeatherManager not found in the scene!");
        }
    }

    void Start()
    {
        // Worker �±׸� ���� ��� ������Ʈ�� ã���ϴ�.
        workers = GameObject.FindGameObjectsWithTag("Worker");
        if (workers == null || workers.Length == 0)
        {
            Debug.LogWarning("No Workers found with tag 'Worker'!");
        }

        if (scrollbar != null)
        {
            // ��ũ�ѹ� �� ���� �� ȣ��� �ݹ� �Լ� ����
            scrollbar.onValueChanged.AddListener(OnScrollBarValueChanged);
            // �ʱ� �� ����
            scrollbar.value = 0.5f; // �߰� �� (0.5�� �ش��ϴ� y�� �������� 0.9)
            OnScrollBarValueChanged(scrollbar.value);
        }
        else
        {
            Debug.LogError("Scrollbar is not assigned!");
        }
    }

    void Update()
    {
        if (wm != null && wm.currentWeather == WeatherManager.WeatherState.Rainy)
        {
            Blink();
        }
        else if (warning != null)
        {
            warning.SetActive(false);
        }
    }

    void Blink()
    {
        if (warning != null)
        {
            warning.SetActive(true);

            // ��� �ð��� ����
            timer += Time.deltaTime;

            // ���� ���� ��� (0.2���� 1���� �պ�)
            float alpha = Mathf.PingPong(timer, 1.0f) * 0.8f + 0.2f;

            if (warningRawImage != null)
            {
                // warning ������Ʈ�� RawImage ������Ʈ�� ������ ����
                Color color = warningRawImage.color;
                color.a = alpha;
                warningRawImage.color = color;
            }

            // ���� ���ݸ��� ������ �����մϴ�.
            if (timer >= 1.0f)
            {
                timer = 0f; // Ÿ�̸Ӹ� �缳��
            }
        }
    }

    public void OnScrollBarValueChanged(float value)
    {
        // y�� ������ ���� 0.8���� 1.0 ���̷� ����
        float newYScale = Mathf.Lerp(0.8f, 1.0f, value);

        if (workers != null)
        {
            // ��� Worker ������Ʈ�� y�� �������� ������Ʈ
            foreach (GameObject worker in workers)
            {
                if (worker != null)
                {
                    Vector3 newScale = worker.transform.localScale;
                    newScale.y = newYScale;
                    worker.transform.localScale = newScale;
                }
            }
        }
        // ������ ũ�⿡ 180�� ���� ���� �ؽ�Ʈ�� ǥ��
        float scaleValue = (newYScale - 0.9f) * 100 + 170;

        Debug.Log("Scale Value: " + scaleValue); // Debug �α� �߰�

        if (scaleText != null)
        {
            scaleText.fontSize = 40;
            scaleText.color = Color.white;
            scaleText.text = "Height : " + scaleValue.ToString("F2") + " cm";
        }
        else
        {
            Debug.LogError("scaleText is not assigned!");
        }
    }
    public void IncreasePeople()
    {
        worker3.SetActive(true);
    }
    public void DecreasePeople()
    {
        worker3.SetActive(false);
    }
}
