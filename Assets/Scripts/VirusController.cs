using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusController : MonoBehaviour
{
    public List<Color> colorList;
    public LayerMask layerToAffect;
    public int infectionRadius = 2;
    public float lifetime = 2;

    float frequency = 1f;
    float amplitude = 0.5f;

    float yPos;
    bool isInitialised = false;
    float randomness;

    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        randomness = Random.Range(0f, 1f);
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = colorList[Random.Range(0, colorList.Count)];
    }

    // Update is called once per frame
    void Update()
    {
        if (isInitialised && spriteRenderer.isVisible)
        {
            transform.position = new Vector3(transform.position.x, Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude + yPos + randomness, transform.position.z);
        }

    }
    public void Initialize(float y)
    {
        yPos = y;
        isInitialised = true;
    }
    //private void CheckInfectRadius()
    //{
    //    Collider[] colliders = Physics.OverlapSphere(transform.position, transform.localScale.x * infectionRadius, layerToAffect);
    //    foreach (Collider collider in colliders)
    //    {
    //        if (collider.TryGetComponent<PlayerHealthManager>(out PlayerHealthManager playerHealthManager))
    //        {
    //            playerHealthManager.Affected();
    //        }
    //        else if (collider.TryGetComponent<InfectedAi>(out InfectedAi infectedAi))
    //        {
    //            infectedAi.Affected();
    //        }    
    //    }
    //}
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlayerHealthManager>(out PlayerHealthManager playerHealthManager))
        {
            playerHealthManager.Affected();
        }
        else if (other.TryGetComponent<InfectedAi>(out InfectedAi infectedAi))
        {
            infectedAi.Affected();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position,transform.localScale.x*infectionRadius);
    }
}
