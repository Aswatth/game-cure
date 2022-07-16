using UnityEngine;
using PathCreation;

public class Path_Follower : MonoBehaviour
{
    public PathCreator pathCreator;
    public float speed;
    public float distanceTravelled;
    public LayerMask layerToStop;


    bool isStopped = false;
    bool isBeingVaccinated = false;
    
    private void Awake()
    {
        distanceTravelled += Random.Range(1,5);
    }

    private void Update()
    {
        if (!isStopped)
        {
            distanceTravelled += speed * Time.deltaTime;

            transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled);
            transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled);
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
        }
        if(!isBeingVaccinated)
            CheckInFront();
        
    }

    public Vector3 GetNearestPoint()
    {
        distanceTravelled = pathCreator.path.GetClosestDistanceAlongPath(transform.position);
        return pathCreator.path.GetClosestPointOnPath(transform.position);
    }

    public void SetDistance(float distance)
    {
        distanceTravelled = distance;
    }

    public void StopMovement(Vector3 pos,bool isBeingVaccinated)
    {
        isStopped = true;
        transform.LookAt(pos);
        this.isBeingVaccinated = isBeingVaccinated;
    }
    public void ResumeMovement()
    {
        isStopped = false;
    }

    public void CheckInFront()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit,1f, layerToStop))
        {
            isStopped = true;
        }
        else
            isStopped = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1f,layerToStop))
            Gizmos.DrawLine(transform.position, hit.point);
    }

}
