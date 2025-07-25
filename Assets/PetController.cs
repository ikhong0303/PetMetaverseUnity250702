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
        navMeshAgent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        ball = GameObject.FindGameObjectWithTag("Ball");
        basePosition = this.transform.position;
        mouseBall.SetActive(false);

    }
    // Start is called before the first frame update
    void Start()
    {

    }


    void DistanceCheck()
    {
        if (state == PetState.play) return; //플레이중엔 무시. 플레이를 끝내면 다시 WAIT모드로 바꾸는 버튼ui라던가 필요?
        playerDistance = Vector3.Distance(transform.position, player.transform.position);
        if (playerDistance < searchRadius || mouseBall.activeInHierarchy) //플레이어가 일정거리 이상 가까워지면 추적모드로
        {
            state = PetState.chase;
        }
        else //멀어지면 대기모드
        {
            state = PetState.wait;
        }

        if(playerDistance < 2 && mouseBall.activeInHierarchy && !ball.activeInHierarchy)
        {
            ball.transform.position = mouseBall.transform.position;
            mouseBall.SetActive(false);

            ball.SetActive(true);
                            
            

        }
        
        
    }

    // Update is called once per frame
    void Update()
    {
        DistanceCheck();
        anim.SetBool("isWalk", navMeshAgent.velocity.magnitude > 0.5f);
        if (state == PetState.play) //플레이 모드일때 공을 쫒는건가
        {
            navMeshAgent.SetDestination(ball.transform.position);
        }

        else if (state == PetState.chase ) //추적일때 플레이어 추적
        {
            navMeshAgent.SetDestination(player.transform.position);
            if(playerDistance < 2)
            {
                navMeshAgent.ResetPath();
            }
        }

        else //대기모드에선 원래 위치로 복귀
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
