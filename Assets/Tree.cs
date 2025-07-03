using UnityEngine;

public class Tree : MonoBehaviour
{
    public GameObject target;

    void Update()
    {
        Vector3 lookPos = target.transform.position;
        lookPos.y = transform.position.y; // y�� ����(����)
        transform.LookAt(lookPos);

    }
}
