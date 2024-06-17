using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public class WeatherManager : MonoBehaviour
{
    public enum WeatherState { None, Rainy, Windy }
    public WeatherState currentWeather;
    public WeatherState previousWeather;
    public bool isSlipped;
    public float fallProbability;
    public List<Player> players = new List<Player>();
    public List<GameObject> workers = new List<GameObject>();

    // ��� �ٶ� ������
    public GameObject rainPrefab;
    public GameObject windPrefab;
    public GameObject nonePrefab;
    public GameObject worker;

    private Dictionary<WeatherState, float> fallProbabilities = new Dictionary<WeatherState, float>();

    private void Start()
    {
        // Player ��ũ��Ʈ ���� ��������
        Player[] foundPlayers = GameObject.FindObjectsOfType<Player>();
        foreach (Player p in foundPlayers)
        {
            players.Add(p);
        }

        // �ʱ⿡�� ���� ȿ�� ����
        SetWeather(WeatherState.None);

        InitializeFallProbabilities();
    }

    public void ChangeWeatherToNone()
    {
        SetWeather(WeatherState.None);
    }

    public void ChangeWeatherToRainy()
    {
        SetWeather(WeatherState.Rainy);
    }

    public void ChangeWeatherToWindy()
    {
        SetWeather(WeatherState.Windy);
    }

    private void SetWeather(WeatherState weatherState)
    {
        // ��� �ٶ� ȿ�� ��Ȱ��ȭ
        rainPrefab.SetActive(false);
        windPrefab.SetActive(false);
        nonePrefab.SetActive(false);

        previousWeather = currentWeather;
        currentWeather = weatherState;

        // ���� ���¿� ���� ������ ȿ�� Ȱ��ȭ
        switch (currentWeather)
        {
            case WeatherState.None:
                foreach (var worker in workers)
                {
                    worker.SetActive(true);
                }
                nonePrefab.SetActive(true);
                foreach (Player player in players)
                {
                    player.agent.speed = 1.5f;
                    player.agent.acceleration = 5;
                    player.agent.stoppingDistance = 2;
                    player.agent.autoBraking = true;
                }
                break;
            case WeatherState.Rainy:
                foreach (var worker in workers)
                {
                    worker.SetActive(false);
                }
                rainPrefab.SetActive(true);
                break;
            case WeatherState.Windy:
                foreach (var worker in workers)
                {
                    worker.SetActive(true);
                }
                windPrefab.SetActive(true);
                foreach (Player player in players)
                {
                    player.agent.speed = 1f;
                    player.agent.acceleration = 8;
                    player.agent.stoppingDistance = 1;
                    player.agent.autoBraking = false;
                }
                break;
            default:
                break;
        }

        Debug.Log("���� ����: " + currentWeather); // ������ �α� �߰�

        // ���� ������ Rainy�̾��� ��, �� ��ģ �Ŀ� ��� ����� 1.5��� ����
        if (previousWeather == WeatherState.Rainy && currentWeather != WeatherState.Rainy)
        {
            IncreaseFunctionality();
            GameManager.Instance.Restart();
        }
    }

    private void IncreaseFunctionality()
    {
        // ���� ������ Rainy���� �� �� ��ģ �� ��� ����� 1.5��� ������Ŵ
        foreach (Player player in players)
        {
            player.agent.acceleration *= 1.5f;
            player.agent.stoppingDistance /= 2;
            player.agent.autoBraking = false;
        }
        fallProbabilities[WeatherState.None] *= 1.5f;
        fallProbabilities[WeatherState.Windy] *= 1.5f;
    }

    private void InitializeFallProbabilities()
    {
        // �� ������ Ȯ���� ��ųʸ��� ����
        fallProbabilities.Add(WeatherState.None, Random.Range(10f, 60f));
        fallProbabilities.Add(WeatherState.Windy, Random.Range(30f, 80f));
    }

    public void CheckPlayerFall()
    {
        fallProbability = fallProbabilities[currentWeather];

        foreach (Player player in players)
        {
            if (player.startAnimation == "RunFwdLoop")
            {
                fallProbability *= 1.2f;
                break; 
            }
        }

        isSlipped = fallProbability > 50;
    }
}
