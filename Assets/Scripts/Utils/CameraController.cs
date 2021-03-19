/**
 * Ana Rita Rodrigues - 2018284515
 * Bruno Faria - 2018295474
 * Dylan Perdigão - 2018233092	
 */
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Vector3 offset;
    public GameObject player;

    /**
     * Limita o frame rate para obter resultados homogenios nos nossos computadores
     */
    void Start()
    {
        Application.targetFrameRate = 60;
        offset = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = player.transform.position + offset;
        
        Debug.Log(1.0f / Time.deltaTime);
    }
}
