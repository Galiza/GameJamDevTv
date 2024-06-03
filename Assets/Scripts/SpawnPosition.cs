using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPosition : MonoBehaviour
{

    [SerializeField] List<GameObject> m_EnemiesList;

    [Header("Enemy Spawn Time")]
    [SerializeField] int lowerTime = 5;
    [SerializeField] int higherTime = 10;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DoSpawn());
    }

    IEnumerator DoSpawn()
    {
        while(true)
        {
            int randomTime = Random.Range(lowerTime, higherTime);
            int enemyIndex = Random.Range(0, m_EnemiesList.Count);
            Instantiate(m_EnemiesList[enemyIndex], transform.position, transform.rotation);
            yield return new WaitForSeconds(randomTime);

        }
    }
}
