using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BasketController : MonoBehaviour
{
    public float speed = 10f;
    //public Rigidbody2D _rigidbody;
    private Vector3 direction;
    private List<Vector3> dirList;
    // Start is called before the first frame update
    void Start()
    {
        //_rigidbody = GetComponent<Rigidbody2D>();
        dirList = new List<Vector3>()
        {
            new Vector3(0,0,0)
            , new Vector3(1,0,0), new Vector3(-1,0,0)
        };
        direction = dirList[Random.Range(0, dirList.Count)];

    }

    // Update is called once per frame
    void Update()
    {
        //_rigidbody.MovePosition(_rigidbody.position + direction * speed * Time.deltaTime);
        this.transform.Translate(direction*speed*Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == null) return;
        if (collision.CompareTag("border"))
        {
            direction *= -1;
        }
    }

}
