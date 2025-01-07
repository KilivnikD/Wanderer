using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Wanderer.Utils;
public class EnemyAI : MonoBehaviour
{
    [SerializeField] private State startingState;
    [SerializeField] private float roamingDistanceMax = 7f; //макс расстояние 
    [SerializeField] private float roamingDistanceMin = 3f; //минимальное расстояние на которое будет отходить наш враг 

    [SerializeField] private float roamingTimeMax = 2f;//время в течение которого враг будет двигаться
    private float roamingTime;//текущее время брождения 

    private NavMeshAgent navMeshAgent;
    private State state;//локальная переменная чтобы хранить состояние текущего обьекта

    private Vector3 roamPosirion;//новая точка в направлении которой мы будем двигаться
    private Vector3 startingPosition;//стартовая позиция 

    private enum State
    {
        
        Roaming,
        Death

    }
   // private void Start()
    //{
    //    startingPosition = transform.position;
    //}

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.updateRotation = false; // в игре не нужно чтобы враг вращался
        navMeshAgent.updateUpAxis = false;
        state = startingState;
    }
    private void Update()
    {
        switch (state)
        {
            default:
            case State.Roaming:
                roamingTime -= Time.deltaTime;
                if (roamingTime < 0)
                {
                    Roaming();
                    roamingTime = roamingTimeMax;
                }

                break;
            case State.Death:
                break;
        }
    }

    public void SetDeathState()
    {
        if (state != State.Death)
        {
            state = State.Death;
            navMeshAgent.isStopped = true; // Останавливаем передвижение
        }
    }

    private void Roaming()
    {
        startingPosition = transform.position;
        roamPosirion = GetRoamingPosition();
        ChangeFacingDirection(startingPosition, roamPosirion);
        navMeshAgent.SetDestination (roamPosirion);
    }

    private Vector3 GetRoamingPosition()
    {
        return startingPosition + Utils.GetRandomDir() * UnityEngine.Random.Range(roamingDistanceMin, roamingDistanceMax);
    }


    //поворот вправо влево обьекта 
    private void ChangeFacingDirection(Vector3 sourcePosition, Vector3 targetPosition)//Vector3 sourcePosition - позиция где мы сейчас , Vector3 targerPosition - куда двигаемся 
    {
        if(sourcePosition.x > targetPosition.x)
        {
            transform.rotation = Quaternion.Euler(0, -180, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
       
    }
}
