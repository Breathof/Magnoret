using System.Collections;
using UnityEngine;

public class SwitchController : MonoBehaviour
{

    public GameObject Switch;
    public float Speed;
    public GameManager GM;

    private bool Sleep;
    private bool FirstSleep;

    void Start()
    {
        FirstSleep = true;
    }

    void Update()
    {
        if (GM.Playing)
        {
            if (FirstSleep)
            {
                FirstSleep = false;
                StartCoroutine(RandSleep());
            }
            MoveUp();
        }
    }

    private void MoveUp()
    {
        if (!Sleep)
        {
            Switch.transform.Translate(Vector3.up * Time.deltaTime * Speed);
        }
        if (Switch.transform.position.y > 5.5f)
        {
            StartCoroutine(RandSleep());
            Vector3 bottom = new Vector3(Random.Range(-1.2f, 1.2f), -6.5f, Switch.transform.position.z);
            Switch.transform.position = bottom;
        }
    }

    private IEnumerator RandSleep()
    {
        Sleep = true;
        var rand = Random.Range(5f, 15f);
        yield return new WaitForSeconds(rand);
        Sleep = false;
    }
}
