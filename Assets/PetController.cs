using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;


public enum PetState
{
    wait, play, chase
}
public class PetController : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public GameObject player;

    public GameObject[] ball;
    public PetState state = PetState.wait;
    //public Animator petAnimator;

    //public float timer;
    // public Collider triggerObject;

    public Transform basePosition;
    public float searchRadius, playerDistance;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        ball = GameObject.FindGameObjectsWithTag("Ball");
    }


    void DistanceCheck()
    {
        if (state == PetState.play) return; //�÷����߿� ����. �÷��̸� ������ �ٽ� WAIT���� �ٲٴ� ��ưui����� �ʿ�?
        playerDistance = Vector3.Distance(transform.position, player.transform.position);
        if (playerDistance < searchRadius) //�÷��̾ �����Ÿ� �̻� ��������� ��������
        {
            state = PetState.chase;
        }
        else //�־����� �����
        {
            state = PetState.wait;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        DistanceCheck();
        if (state == PetState.play) //�÷��� ����϶� ���� �i�°ǰ�
        {
            navMeshAgent.SetDestination(ball[0].transform.position);
        }

        else if (state == PetState.chase) //�����϶� �÷��̾� ����
        {
            navMeshAgent.SetDestination(player.transform.position);
        }

        else //����忡�� ���� ��ġ�� ����
        {
            navMeshAgent.SetDestination(basePosition.position);
        }


        if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
        {
            //petAnimator.SetBool("Run", false);
            navMeshAgent.isStopped = true;
        }

        else if (navMeshAgent.remainingDistance > navMeshAgent.stoppingDistance)
        {
            //petAnimator.SetBool("Run", true);
            navMeshAgent.isStopped = false;
        }

    }

    /*
    void Delay()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            triggerObject.enabled = false;
        }

        else if (timer <= 0)
        {
            triggerObject.enabled = true;
        }
    }*/
}
