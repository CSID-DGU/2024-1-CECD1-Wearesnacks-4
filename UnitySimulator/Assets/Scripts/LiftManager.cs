using UnityEngine;

public class LiftManager : MonoBehaviour
{
    public GameObject[] prefabList; // ������ ����Ʈ
    public Transform spawnPoint; // ��ȯ ����
    private float spawnInterval = 30f; // �ٽ� ��ȯ�Ǵ� �ð� ����

    private float timer = 0f; // ��� �ð��� ��Ÿ���� ����
    public float weight;

    // ������ �����տ� ���� ������ �����ϱ� ���� ����
    public GameObject spawnedObject { get; private set; }

    private void Start()
    {
        // ���� ���� �� ������ ��ȯ
        SpawnRandomPrefab();
    }

    private void Update()
    {
        // ���� �ð� ���ݸ��� ���ο� ��ü ����
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            timer = 0f;
            if (spawnedObject != null)
            {
                Destroy(spawnedObject);
            }
            SpawnRandomPrefab();
        }
    }


    private void SpawnRandomPrefab()
    {
        // ������ �������� �����Ͽ� ��ȯ
        int randomIndex = Random.Range(0, prefabList.Length);
        GameObject prefabToSpawn = prefabList[randomIndex];

        // �������� ũ�⸦ �����ϰ� �����մϴ�. �� �ະ�� 0.5���� 0.8 ������ ���� �����ϰ� �����մϴ�.
        Vector3 scale = new Vector3(
            Random.Range(0.7f, 0.8f),
            Random.Range(0.7f, 0.8f),
            Random.Range(0.7f, 0.8f)
        );
        float combined = (scale.x + scale.y + scale.z) / 3;

        // �������� �����ϰ� ũ�⸦ �����մϴ�.
        spawnedObject = Instantiate(prefabToSpawn, spawnPoint.position, Quaternion.Euler(0f, 90f, 0f));
        spawnedObject.transform.localScale = scale;

        // ������ �����տ� Rigidbody ������Ʈ�� �߰��ϰ� �߷� �ɼ��� Ȱ��ȭ
        Rigidbody rigidbody = spawnedObject.GetComponent<Rigidbody>();
        if (rigidbody == null)
        {
            rigidbody = spawnedObject.AddComponent<Rigidbody>();
        }
        rigidbody.freezeRotation = true;
        rigidbody.useGravity = true;
        spawnedObject.tag = "Lift";

        if (randomIndex == 0)
        {
            spawnedObject.transform.position += new Vector3(-0.3f, 0f, -0.45f);
            weight = 70 * combined;

            BoxCollider collider = spawnedObject.GetComponent<BoxCollider>();
            if (collider != null)
            {
                Vector3 newSize = collider.size;
                newSize.y *= 1.5f;
                collider.size = newSize;
                Vector3 newCenter = collider.center;
                newCenter.y = 1f;
                collider.center = newCenter;
            }
        }
        else if(randomIndex == 1)
        {
            spawnedObject.transform.position += new Vector3(0f, 0f, 0.25f);
            weight = 50 * combined;
            
            CapsuleCollider collider = spawnedObject.GetComponent<CapsuleCollider>();
            if (collider != null)
            {
                collider.height *= 1.5f;
                Vector3 newCenter = collider.center;
                newCenter.y = 1f;
                collider.center = newCenter;
            }
        }
        else if (randomIndex == 2)
        {
            spawnedObject.transform.position += new Vector3(0.1f, 0f, -0.25f);
            weight = 30 * combined;

            BoxCollider collider = spawnedObject.GetComponent<BoxCollider>();
            if (collider != null)
            {
                Vector3 newSize = collider.size;
                newSize.y *= 2f;
                collider.size = newSize;
                Vector3 newCenter = collider.center;
                newCenter.y = 1f;
                collider.center = newCenter;
            }
        }
    }
}
