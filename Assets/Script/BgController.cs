using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgController : MonoBehaviour, IObserver
{

    public GameObject basket;
    public GameObject background;
    public Vector3 lastSpawn;
    private int betweenBasket = 4;
    private int needToSpawnBg = 0;

    public Subjects subjects;

    // Start is called before the first frame update
    

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnBasket()
    {
        lastSpawn += new Vector3(0, betweenBasket, 0);
        GameObject basketSpawn = Instantiate(basket, lastSpawn, Quaternion.identity);
        basketSpawn.transform.parent = transform;
    }

    void SpawnBackground()
    {
        GameObject bgSpawn = Instantiate(background, lastSpawn, Quaternion.identity);
        bgSpawn.transform.parent = transform;
    }

    public void OnNotify(GameAction action)
    {
        if (action == GameAction.catched)
        {
            needToSpawnBg++;
            SpawnBasket();
            
            SpawnBackground();
            
           
        }
    }

    private void OnEnable()
    {
        subjects.AddObserver(this);        
    }

    private void OnDisable()
    {
        subjects.RemoveObserver(this);
    }
}
