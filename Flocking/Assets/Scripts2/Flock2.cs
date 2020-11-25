using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock2 : MonoBehaviour
{
    [Header("Spawn Setup")]
    [SerializeField] private FlockUnit flockUnityPrefab;
    [SerializeField] private int flockSize;
    [SerializeField] private Vector3 spawnBounds;

    [Header("Speed Setup")]
    [Range(0,10)]
    [SerializeField] private float _minSpeed;
    public float minSpeed { get { return _minSpeed;}}
    [Range(0,10)]
    [SerializeField] private float _maxSpeed;
    public float maxSpeed { get { return _maxSpeed;}}

    [Header("Detection Distances")]
    [Range(0,10)]
    [SerializeField] public float _cohesionDistance;
    public float cohesionDistance {get {return _cohesionDistance;}}

    [Range(0,10)]
    [SerializeField] public float _avoidanceDistance;
    public float avoidanceDistance {get {return _avoidanceDistance;}}

    [Range(0,10)]
    [SerializeField] public float _alignmentDistance;
    public float alignmentDistance {get {return _alignmentDistance;}}

    [Range(0,30)]
    [SerializeField] public float _boundsDistance;
    public float boundsDistance {get {return _boundsDistance;}}

     [Header("Behavior Weight")]
    [Range(0,10)]
    [SerializeField] public float _cohesionWeight;
    public float cohesionWeight {get {return _cohesionWeight;}}

    [Range(0,10)]
    [SerializeField] public float _avoidanceWeight;
    public float avoidanceWeight {get {return _avoidanceWeight;}}

    [Range(0,10)]
    [SerializeField] public float _alignmentWeight;
    public float alignmentWeight {get {return _alignmentWeight;}}

    [Range(0,10)]
    [SerializeField] public float _boundsWeight;
    public float boundsWeight {get {return _boundsWeight;}}

    public FlockUnit[] allUnits { get; set;}

    private float time = 0;
    private float poissonTime = 0;
    private float lambda = 0.1f;

    private void Start() {
        {
            GenerateUnits();
        }
        poissonTime = -(1/lambda)*Mathf.Log(Random.Range(0.1f,1.0f));
        poissonTime += time;
    }

    private void Update() 
    {
        
        for(int i = 0; i < allUnits.Length; i++)  
        {
            allUnits[i].MoveUnit();
        }

        time += Time.deltaTime;
        if (time > poissonTime)
        {
            poissonTime += -(1/lambda)*Mathf.Log(Random.Range(0.1f,1.0f));
            Debug.Log(poissonTime);
            _cohesionWeight = Random.Range(0.1f, 3.0f);
            _avoidanceWeight = Random.Range(0.1f, 3.0f);
            _alignmentWeight = Random.Range(0.1f, 3.0f);
        }
        


    }

    private void GenerateUnits()
    {
        allUnits = new FlockUnit[flockSize];
        for (int i = 0; i < flockSize; i++)
        {
            var randomVector = UnityEngine.Random.insideUnitSphere;
            randomVector = new Vector3(randomVector.x * spawnBounds.x, randomVector.y * spawnBounds.y, randomVector.z * spawnBounds.z);
            var spawnPosition= transform.position + randomVector;
            var rotation = Quaternion.Euler(0, UnityEngine.Random.Range(0, 360), 0);
            allUnits[i] = Instantiate(flockUnityPrefab, spawnPosition, rotation);
            allUnits[i].AssignFlock(this);
            allUnits[i].InitializeSpeed(UnityEngine.Random.Range(minSpeed,maxSpeed));
        }
    }
    
}
//8:11