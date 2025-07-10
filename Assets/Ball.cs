using UnityEngine;

public class Ball : MonoBehaviour
{
    public GameObject[] Pet;
    public PetController[] petControllers;
    public GameObject mouseBall;
    public bool canBite = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        canBite = false;
        Pet = GameObject.FindGameObjectsWithTag("Pet");
        for (int i = 0; i < Pet.Length-1; i++)
        {
            petControllers[i] = Pet[i].GetComponent<PetController>();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.TryGetComponent<PetController>(out var pet) && canBite)
        {         
            pet.mouseBall.SetActive(true);
            foreach (var pc in petControllers)
            {
                pc.state = PetState.wait;
            }
            canBite = false;
            gameObject.SetActive(false);

        }
    }

    [ContextMenu("CatchBall")]
    public void CatchBall()
    {
        canBite = true;
        foreach (var pc in petControllers)
        {
            pc.state = PetState.play ;
        }
    }
}
