using InputManager;
using UnityEngine;

public class PlayerController: MonoBehaviour
{
    #region Props
    private const float MAX_X_AXIS = 2.3f;

    public GameManager GM;
    public Magnet Magnet;

    private float SpeedLeft;
    private float SpeedRight;

    public GameObject Player;
    public float Speed = 6;
    public bool Swap = false;

    #endregion

    #region Player
    public int Lives = 3;
    #endregion

    Factory Factory = new Factory();

    IInput InputTarget;

    private void Start()
    {

        if(SystemInfo.deviceType == DeviceType.Desktop)
        {
            InputTarget = Factory.GetKeyboard();
        }
        else
        {
            InputTarget = Factory.GetTouchScreen();
        }
    }

    void Update()
    {
        if (GM.Playing)
        {
            MoveCenter();
            MoveLeft();
            MoveRight();
        }
    }

    #region Movement

    private void MoveLeft()
    {
        if (InputTarget.Left())
        {
            if (Swap)
            {
                //SetRightMagnetColor();
                UpdateSpeedRight();
                Player.transform.Translate(Vector3.down * Time.deltaTime * SpeedRight);
            } else
            {
                //SetLeftMagnetColor();
                UpdateSpeedLeft();
                Player.transform.Translate(Vector3.up * Time.deltaTime * SpeedLeft);
            }

        }
    }

    private void MoveRight()
    {
        if (InputTarget.Right())
        {
            if (Swap)
            {
                //SetLeftMagnetColor();
                UpdateSpeedLeft();
                Player.transform.Translate(Vector3.up * Time.deltaTime * SpeedLeft);
            } else
            {
                //SetRightMagnetColor();
                UpdateSpeedRight();
                Player.transform.Translate(Vector3.down * Time.deltaTime * SpeedRight);
            }
        }
    }

    private void MoveCenter()
    {
        if (InputTarget.Idle())
        {
            if (Player.transform.position.x > 0.2f)
            {
                //SpeedLeft = Mathf.SmoothStep(Player.transform.position.x + MAX_X_AXIS, 5f, 0.5f);
                UpdateSpeedLeft();
                Player.transform.Translate(Vector3.up * Time.deltaTime * SpeedLeft);
            } else if (Player.transform.position.x < -0.2f)
            {
                //SpeedRight = Mathf.SmoothStep(Player.transform.position.x + MAX_X_AXIS, 5f, 0.5f);
                UpdateSpeedRight();
                Player.transform.Translate(Vector3.down * Time.deltaTime * SpeedRight);
            }
        }

    }

    private void UpdateSpeedLeft()
    {
        SpeedLeft = Mathf.Abs(Player.transform.position.x + MAX_X_AXIS);
        SpeedLeft = SpeedLeft * Speed;
    }

    private void UpdateSpeedRight()
    {
        SpeedRight = Mathf.Abs(Player.transform.position.x - MAX_X_AXIS);
        SpeedRight = SpeedRight * Speed;
    }

    private void SetLeftMagnetColor()
    {
        var RightMagnetShader = Magnet.R_Magnet.gameObject.GetComponent<MeshRenderer>().material;
        var LeftMagnetShader = Magnet.L_Magnet.gameObject.GetComponent<MeshRenderer>().material;
        LeftMagnetShader.color = new Color(0.4214578f, 0.4892261f, 0.7264151f);
        RightMagnetShader.color = new Color(0.8490566f, 0.252314f, 0.252314f);
    }

    private void SetRightMagnetColor()
    {
        var RightMagnetShader = Magnet.R_Magnet.gameObject.GetComponent<MeshRenderer>().material;
        var LeftMagnetShader = Magnet.L_Magnet.gameObject.GetComponent<MeshRenderer>().material;
        LeftMagnetShader.color = new Color(0.2144446f, 0.3330334f, 0.745283f);
        RightMagnetShader.color = new Color(0.8773585f, 0.4510947f, 0.4510947f);
    }

    #endregion

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Enemy":
                Lives -= 1;
                GM.UpdateLiveCount();
                GM.CameraShake();
                break;
            case "Live":
                Lives += 1;
                break;
            case "Switch":
                GM.SwitchMagnets();
                Swap = !Swap;
                break;
        }

    }
}
