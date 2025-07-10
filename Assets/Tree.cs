using Unity.XR.CoreUtils;
using UnityEngine;

public class Tree : MonoBehaviour
{
    public GameObject target;

    private void Awake()
    {
        target = Camera.main.gameObject;
    }
    void Update()
    {
        Vector3 lookPos = target.transform.position;
        lookPos.y = transform.position.y; // y�� ����(����)
        transform.LookAt(lookPos);

    }
}
