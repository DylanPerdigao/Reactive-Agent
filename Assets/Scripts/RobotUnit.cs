using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RobotUnit : MonoBehaviour
{
   

    public int resourcesGathered;
    protected Rigidbody rb;
    public float speed = 1.0f;
    public Text countText;
    public float startTime;
    public float timeElapsed = 0.0f;
    public ResourceDetectorScript resourcesDetector;
    public BlockDetectorScript blockDetector;
    private List<Tuple<float, float>> listAngleStr;
    public bool debugMode = true;
    protected int maxObjects = 0;

    private Vector3 initialPosition;
    //number of logs printed
    private int nLogs=0;

    // Start is called before the first frame update
    void Start()
    {


        //strength = 0.0f;
        maxObjects = GameObject.FindGameObjectsWithTag("Pickup").Length;
        resourcesGathered = 0;
        rb = GetComponent<Rigidbody>();
        listAngleStr = new List<Tuple<float, float>>();
        this.startTime = Time.time;
        timeElapsed = Time.time - startTime;
        SetCountText();

        //set dos limites
        blockDetector.SetLimits(0.1f, 1f, 0.0f, 1f);
        resourcesDetector.SetLimits(0.1f, 1f, 0.0f, 1f);
        //guardar posicao initial
        this.SetInitialPosition();



    }

    void FixedUpdate()
    {
        int i = 0;
        foreach(Tuple<float,float> tmp in listAngleStr){
            
            float angle = tmp.Item1;
            float strength = tmp.Item2;
            angle *= Mathf.Deg2Rad;
            float xComponent = Mathf.Cos(angle);
            float zComponent = Mathf.Sin(angle);
            Vector3 forceDirection = new Vector3(xComponent, 0, zComponent);
            if (debugMode)
            {
                Debug.DrawRay(this.transform.position, (forceDirection * strength * speed) , i == 0 ? Color.black : Color.magenta );
            }
            rb.AddForce(forceDirection * strength * speed);

            i++;
        }

        
        listAngleStr.Clear(); // cleanup
        this.TestValues();

    }

    private void LateUpdate()
    {
        SetCountText();

    }

    void SetCountText()
    {
        if(resourcesGathered < maxObjects)
        {
            this.timeElapsed = Time.time - this.startTime;
        }
        
        string minutes = ((int)(timeElapsed / 60)).ToString();
        string seconds = (timeElapsed % 60).ToString("f0");
        countText.text = "Resources Gathered: " + resourcesGathered.ToString() + "/" + maxObjects + "\nTime Elapsed: " + minutes + ":" + seconds; //start
    }

    public void applyForce(float angle, float strength)
    {
        //Debug.Log("A: " + angle + "\tS: " + strength);
        listAngleStr.Add(new Tuple<float, float>(angle, strength));
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Pickup"))
        {
            other.gameObject.SetActive(false);
            resourcesGathered++;

        }
        else
        {
            if (other.gameObject.CompareTag("Deadly"))
            {
                Debug.Log("Destroyed!");
                this.gameObject.transform.parent.gameObject.SetActive(false);
            }
        }

    }
    
    private void TestValues() {
        if (maxObjects==resourcesGathered)
        {
            this.IncrementValues();
            this.ResetMap();
            this.ShowValues();
        }
    }
    private void IncrementValues()
    {
        float infX = blockDetector.infLimitX;
        float supX = blockDetector.supLimitX;
        float infY = blockDetector.infLimitY;
        float supY = blockDetector.infLimitY;
        if (infX < supX)
        {
            infX += 0.1f;
        }
        else
        {
            infX = 0f;
            infY += 0.1f;
        }
        blockDetector.SetLimits(infX, supX, infY, supY);
        resourcesDetector.SetLimits(infX, supX, infY, supY);
    }
    private void ResetMap()
    {
        rb.transform.position = initialPosition;
        GameObject[] pickups = GameObject.FindGameObjectsWithTag("Pickup");
        foreach(GameObject p in pickups)
        {
            p.SetActive(true);

        }
       
    }
    /*
     * print the values of tested limits
     */
    private void ShowValues() {
        this.nLogs += 1;
        Debug.Log(
            "LOG #" + this.nLogs + "\n" +
            "name\t\t\tinfX\tsupX\tinfY\tsupY\n" +
            "========================================\n" +
            "BlockDetector:\t\t" + blockDetector.infLimitX + "\t" + blockDetector.supLimitX + "\t" + blockDetector.infLimitY + "\t" + blockDetector.supLimitY+"\n"+
            "ResourceDetector:\t" + resourcesDetector.infLimitX + "\t" + resourcesDetector.supLimitX + "\t" + resourcesDetector.infLimitY + "\t" + resourcesDetector.supLimitY+"\n"+
            "========================================\n" +
            "time elapsed:\t\t" + timeElapsed + "\n" +
            "ressources gathered:\t" + resourcesGathered + "\n"
        );
    }
    /*
     * sets the initial position
     */
    private void SetInitialPosition(){
        this.initialPosition = rb.transform.position;
    }
}