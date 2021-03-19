/**
 * Ana Rita Rodrigues - 2018284515
 * Bruno Faria - 2018295474
 * Dylan Perdigão - 2018233092	
 */
using UnityEngine;

public class SensorController : MonoBehaviour
{
    private Vector3 offset;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = player.transform.position + offset;
    }
}
