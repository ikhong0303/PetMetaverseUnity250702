using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PetHeadLook : MonoBehaviour
{
    //�����ϸ� �÷��̾ �ٶ󺸱⸸ �ϴ� �༮
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public float searchRadius, playerDistance;
    public Transform head;
    public float speed = 10f;

    GameObject player;
    bool cached = false;
    private Quaternion targetRotation;
    private Quaternion startRotation;
    private Quaternion originRotation;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        originRotation = head.transform.rotation; 
    }



    void HeadRotate()
    {
        playerDistance = Vector3.Distance(transform.position, player.transform.position);
        if (playerDistance < searchRadius) 
        {
            transform.rotation = Quaternion.Slerp(startRotation, targetRotation, speed * Time.fixedTime);
            //�Ӹ��� ���ư��°Ŵϱ� �������� �ʿ�
        }
        else 
        {
            transform.rotation = Quaternion.Slerp(startRotation, originRotation, speed * Time.fixedTime);
        }
    }

    private void CacheStartOnceBefore()
    {
        if (cached) return;

        targetRotation = Quaternion.LookRotation(player.transform.position - head.transform.position);
        startRotation = head.transform.rotation;
        cached = true;
    }

    private void FixedUpdate()
    {
        CacheStartOnceBefore();
        HeadRotate();
        
    }
}
