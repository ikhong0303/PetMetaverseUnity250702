using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.XR.Interaction.Toolkit.AffordanceSystem.Receiver;


public enum PetState
{
    wait, play, chase
}
public class PetController : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public GameObject player;

    public GameObject ball;
    public PetState state = PetState.wait;

    public GameObject mouseBall;
    //public Animator petAnimator;

    //public float timer;
    // public Collider triggerObject;

    public Vector3 basePosition;
    public float searchRadius, playerDistance;

    Animator anim;


    private void Awake()
    {
        mouseBall = transform.GetComponentInChildren<BallFinder>().gameObject;
        

    }
    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        ball = GameObject.FindGameObjectWithTag("Ball");
        basePosition = this.transform.position;
        mouseBall.SetActive(false);
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

        if(playerDistance < 2 && mouseBall.activeInHierarchy && !ball.activeInHierarchy)
        {
            mouseBall.SetActive(false);

            ball.SetActive(true);
            ball.transform.position = Camera.main.transform.position + new Vector3(Random.Range(0.5f,2),1, Random.Range(0.5f,2));
            Rigidbody rb = ball.GetComponent<Rigidbody>();
      
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            print("ball"); 
        }
        
        
    }

    // Update is called once per frame
    void Update()
    {
        DistanceCheck();
        anim.SetBool("isWalk", navMeshAgent.velocity.magnitude > 0.5f);
        if (state == PetState.play) //�÷��� ����϶� ���� �i�°ǰ�
        {
            navMeshAgent.SetDestination(ball.transform.position);
        }

        else if (state == PetState.chase || mouseBall.activeInHierarchy) //�����϶� �÷��̾� ����
        {
            navMeshAgent.SetDestination(player.transform.position);
            if(playerDistance < 2)
            {
                navMeshAgent.ResetPath();
            }
        }

        else //����忡�� ���� ��ġ�� ����
        {
            navMeshAgent.SetDestination(basePosition);
         
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
