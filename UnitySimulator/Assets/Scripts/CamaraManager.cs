using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CameraManager : MonoBehaviour
{
    public RawImage[] rawImages; // 4���� RawImage �迭
    public GameObject[] uiElements; // UI ��� �迭

    private RectTransform[] originalRects; // �� RawImage�� ���� RectTransform
    private Vector2[] originalAnchorMin;
    private Vector2[] originalAnchorMax;
    private Vector2[] originalOffsetMin;
    private Vector2[] originalOffsetMax;
    private bool isZoomed = false; // Ȯ�� ����
    private int zoomedIndex = -1; // Ȯ��� �̹��� �ε���

    void Start()
    {
        // �� RawImage�� ���� RectTransform ���� �����մϴ�.
        originalRects = new RectTransform[rawImages.Length];
        originalAnchorMin = new Vector2[rawImages.Length];
        originalAnchorMax = new Vector2[rawImages.Length];
        originalOffsetMin = new Vector2[rawImages.Length];
        originalOffsetMax = new Vector2[rawImages.Length];
        for (int i = 0; i < rawImages.Length; i++)
        {
            originalRects[i] = rawImages[i].GetComponent<RectTransform>();
            originalAnchorMin[i] = originalRects[i].anchorMin;
            originalAnchorMax[i] = originalRects[i].anchorMax;
            originalOffsetMin[i] = originalRects[i].offsetMin;
            originalOffsetMax[i] = originalRects[i].offsetMax;
        }

        // �� RawImage�� Ŭ�� �̺�Ʈ�� �߰��մϴ�.
        foreach (RawImage rawImage in rawImages)
        {
            AddClickEvent(rawImage);
        }
    }

    void AddClickEvent(RawImage rawImage)
    {
        EventTrigger trigger = rawImage.gameObject.AddComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry
        {
            eventID = EventTriggerType.PointerClick
        };
        entry.callback.AddListener((eventData) => { OnImageClick(rawImage); });
        trigger.triggers.Add(entry);
    }

    void OnImageClick(RawImage clickedImage)
    {
        if (isZoomed)
        {
            // ���� RectTransform���� ���ư��ϴ�.
            for (int i = 0; i < rawImages.Length; i++)
            {
                rawImages[i].GetComponent<RectTransform>().anchorMin = originalAnchorMin[i];
                rawImages[i].GetComponent<RectTransform>().anchorMax = originalAnchorMax[i];
                rawImages[i].GetComponent<RectTransform>().offsetMin = originalOffsetMin[i];
                rawImages[i].GetComponent<RectTransform>().offsetMax = originalOffsetMax[i];
            }

            // UI ��ҵ��� �ٽ� Ȱ��ȭ�մϴ�.
            foreach (GameObject uiElement in uiElements)
            {
                uiElement.SetActive(true);
            }

            isZoomed = false;
            zoomedIndex = -1;
        }
        else
        {
            // ��� RawImage�� ����� Ŭ���� RawImage�� Ȯ���մϴ�.
            for (int i = 0; i < rawImages.Length; i++)
            {
                rawImages[i].GetComponent<RectTransform>().anchorMin = new Vector2(0, 0);
                rawImages[i].GetComponent<RectTransform>().anchorMax = new Vector2(0, 0);
                rawImages[i].GetComponent<RectTransform>().offsetMin = Vector2.zero;
                rawImages[i].GetComponent<RectTransform>().offsetMax = Vector2.zero;
            }

            clickedImage.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0);
            clickedImage.GetComponent<RectTransform>().anchorMax = new Vector2(1, 1);
            clickedImage.GetComponent<RectTransform>().offsetMin = Vector2.zero;
            clickedImage.GetComponent<RectTransform>().offsetMax = Vector2.zero;

            // UI ��ҵ��� ��Ȱ��ȭ�մϴ�.
            foreach (GameObject uiElement in uiElements)
            {
                uiElement.SetActive(false);
            }

            isZoomed = true;
            zoomedIndex = System.Array.IndexOf(rawImages, clickedImage);
        }
    }
}
