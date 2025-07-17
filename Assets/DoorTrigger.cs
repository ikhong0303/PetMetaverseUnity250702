using NUnit.Framework.Internal.Filters;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{

    [SerializeField] Transform rightDoor;
    [SerializeField] Transform leftDoor;
    [SerializeField] float spd = 4;
    Vector3 rpos, lpos;
    [SerializeField]bool isOpen = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rpos = rightDoor.position;
        lpos = leftDoor.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            StopAllCoroutines();
            StartCoroutine(AutoDoor(1));
            

        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StopAllCoroutines();
            StartCoroutine(AutoDoor(-1));
            
        }

    }

    IEnumerator AutoDoor(float _dir)
    {
       while(true)
       {
            rightDoor.position += Vector3.right * _dir * Time.deltaTime * spd;
            leftDoor.position  += Vector3.left * _dir * Time.deltaTime* spd;
            if (rightDoor.position.x >= rpos.x + 0.77 && _dir == 1)       
            {
                break;
            }
            else if (rightDoor.position.x <= rpos.x && _dir == -1)
            {
                break;
            }
                yield return null;
        }
        StopAllCoroutines();
    }
}
