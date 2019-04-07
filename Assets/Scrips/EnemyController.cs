using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public GameObject Enemy;
    public GameManager GM;

    public float Speed;

    private bool Sleep;

    void Start()
    {
        Speed = GM.Slider.value; 
    }

    void Update()
    {
        if (GM.Playing)
        {
            MoveUp();
        }
    }

    #region Move
    public void MoveUp()
    {
        if (!Sleep)
        {
            Enemy.transform.Translate(Vector3.up * Time.deltaTime * Speed);
            if (Enemy.transform.position.y > 5.5f)
            {
                Vector3 randomBottomPosition = new Vector3(Random.Range(-2.2f, 2.2f), -6.5f, Enemy.transform.position.z);
                Enemy.transform.position = randomBottomPosition;
                GM.UpdateScore(false);
                Sleep = false;
            }
        }
    }
    #endregion

    public void OnTriggerEnter(Collider other)
    {
        string tag = other.tag;
        if (tag == "Player")
        {
            Animation objectAnimation = (Animation) this.transform.GetComponent("Animation");
            objectAnimation.Play();
        }
    }
}
