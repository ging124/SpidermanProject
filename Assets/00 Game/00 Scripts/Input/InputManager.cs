using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{
    public InputSO inputSO;

    public float camsensitivity = 1f;

    [Header("Input modules")]
    public FloatingJoystick joystickMove;
    public GameObject lookPanel;

    [Header("Input button")]
    [SerializeField] private KeyCode _jumpKey;
    [SerializeField] private KeyCode _zipKey;
    [SerializeField] private KeyCode _attackKey;
    [SerializeField] private KeyCode _dodgeKey;
    [SerializeField] private KeyCode _gadgetKey;
    [SerializeField] private KeyCode _ultimateKey;
    [SerializeField] private KeyCode _scanKey;


    [Header("Input value(Readonly)")]
    public float timeScale;

    private Vector2 cachedTouchPos;
    private int lookFingerID;
    private Vector2 targetLook;

    private float pcCamSenmultiplier = 1;

    public GameEventListener playerDead;

    private void Awake()
    {
        playerDead.Register();
    }

    private void OnDestroy()
    {
        playerDead.Unregister();
    }

    private void Start()
    {
        #if UNITY_EDITOR || UNITY_STANDALONE_WIN
            Cursor.lockState = CursorLockMode.Locked;
        #endif
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.H))
            Debug.Break();

#if UNITY_EDITOR || UNITY_STANDALONE_WIN
        inputSO.move = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        inputSO.look = new Vector2(Input.GetAxisRaw("Mouse Y"), Input.GetAxisRaw("Mouse X"));
        inputSO.look *= (camsensitivity * pcCamSenmultiplier);

        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            Cursor.lockState = CursorLockMode.None;
        }
        if (Input.GetKeyUp(KeyCode.LeftAlt))
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        if (Cursor.lockState == CursorLockMode.None)
        {
            inputSO.move = Vector2.zero;
            inputSO.look = Vector2.zero;
        }

        if (Input.GetKeyDown(KeyCode.PageUp))
            pcCamSenmultiplier += 0.2f;
        if (Input.GetKeyUp(KeyCode.PageDown))
        {
            pcCamSenmultiplier -= 0.2f;
            if (pcCamSenmultiplier <= 0)
                pcCamSenmultiplier = 0.2f;
        }

        if(!inputSO.disableInput)
        {
            if (Input.GetKey(_jumpKey))
            {
                ButtonJump();
            }

            if (Input.GetKeyDown(_jumpKey))
            {
                ButtonJumpDown();
            }

            if (Input.GetKeyUp(_jumpKey))
            {
                ButtonJumpUp();
            }

            if (Input.GetKeyDown(_dodgeKey))
            {
                ButtonDodgeDown();
            }

            if (Input.GetKeyUp(_dodgeKey))
            {
                ButtonDodgeUp();
            }

            if (Input.GetKeyUp(_zipKey))
            {
                ButtonZip();
            }

            if(Input.GetKey(_attackKey))
            {
                ButtonAttack();
            }

            if (Input.GetKey(_gadgetKey))
            {
                ButtonGadget();
            }

            if (Input.GetKey(_ultimateKey))
            {
                ButtonUltimate();
            }

            if (Input.GetKey(_scanKey))
            {
                ButtonScan();
            }
        }
#endif

        if (inputSO.disableInput)
        {
            if (joystickMove.gameObject.activeInHierarchy)
            {
                joystickMove.gameObject.SetActive(false);
                joystickMove.OnPointerUp(null);
                lookPanel.SetActive(false);
            }
            inputSO.move = Vector2.zero;
            inputSO.look = Vector2.zero;
            return;
        }

        if (!joystickMove.gameObject.activeInHierarchy)
        {
            joystickMove.gameObject.SetActive(true);
            lookPanel.SetActive(true);
        }

#if UNITY_ANDROID && !UNITY_EDITOR
        inputSO.move = new Vector2(joystickMove.Horizontal, joystickMove.Vertical);

        if(Input.touchCount == 0)
        {
            inputSO.isLooking = false;
            lookFingerID = -1;
            targetLook = Vector2.zero;
        }
        else
        {
            if (inputSO.isLooking && lookFingerID != -1)
            {
                int index = -1;

                for (int i = 0; i < Input.touchCount; i++)
                {
                    if (Input.GetTouch(i).fingerId == lookFingerID)
                    {
                        index = i;
                        break;
                    }
                }

                if(index >= 0)
                {
                    Vector2 touchDelta = Input.GetTouch(index).position - cachedTouchPos;
                    cachedTouchPos = Input.GetTouch(index).position;
                    touchDelta = touchDelta / Time.deltaTime;
                    touchDelta /= (140f / camsensitivity);
                    touchDelta = Vector3.ClampMagnitude(touchDelta, 7f);
                    targetLook.x = Mathf.Abs(touchDelta.y) > 0.3f ? touchDelta.y : 0f;
                    targetLook.y = Mathf.Abs(touchDelta.x) > 0.3f ? touchDelta.x : 0f;
                }
                else
                {
                    targetLook = Vector2.zero;
                }
            }
            else
            {
                targetLook = Vector2.zero;
            }
        }

        inputSO.look = Vector2.Lerp(inputSO.look, targetLook, 15 * Time.deltaTime);

        if (Mathf.Abs(inputSO.look.x) < 0.01f)
        { 
            inputSO.look.x = 0; 
        }
        if (Mathf.Abs(inputSO.look.y) < 0.01f)
        {
            inputSO.look.y = 0;
        }
        
#endif
    }

    public void LookPressed(BaseEventData eventData)
    {
        if (inputSO.disableInput) return;
        if (inputSO.isLooking) return;
        inputSO.isLooking = true;
        PointerEventData pointerEventData = (PointerEventData)eventData;
        cachedTouchPos = pointerEventData.position;
        lookFingerID = -1;
        foreach (var touch in Input.touches)
        {
            if (touch.position == cachedTouchPos)
                lookFingerID = touch.fingerId;
        }
    }

    public void LookReleased()
    {
        if (inputSO.disableInput) return;
        foreach (var touch in Input.touches)
        {
            if (touch.fingerId == lookFingerID)
            {
                if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
                {
                    inputSO.isLooking = false;
                    lookFingerID = -1;
                    targetLook = Vector2.zero;
                }
                break;
            }
        }
    }

    public void ButtonJump()
    {
        inputSO.buttonJump = true;
        StartCoroutine(ButtonJumpFalse());
    }

    IEnumerator ButtonJumpFalse()
    {
        yield return 0;
        inputSO.buttonJump = false;
    }

    public void ButtonAttack()
    {
        inputSO.buttonAttack = true;
        StartCoroutine(ButtonAttackFalse());
    }

    IEnumerator ButtonAttackFalse()
    {
        yield return 0;
        inputSO.buttonAttack = false;
    }

    public void ButtonJumpDown()
    {
        inputSO.buttonJump = true;
    }

    public void ButtonJumpUp()
    {
        inputSO.buttonJump = false;
    }

    public void ButtonZip()
    {
        inputSO.buttonZip = true;
        StartCoroutine(ButtonZipFalse());
    }

    IEnumerator ButtonZipFalse()
    {
        yield return 0;
        inputSO.buttonZip = false;
    }

    public void ButtonGadget()
    {
        inputSO.buttonGadget = true;
        StartCoroutine(ButtonGadgetFalse());
    }

    IEnumerator ButtonGadgetFalse()
    {
        yield return 0;
        inputSO.buttonGadget = false;
    }

    public void ButtonDodgeDown()
    {
        inputSO.buttonDodge = true;
    }

    public void ButtonDodgeUp()
    {
        inputSO.buttonDodge = false;
    }

    public void ButtonUltimate()
    {
        inputSO.buttonUltimate = true;
        StartCoroutine(ButtonUltimateFalse());
    }

    IEnumerator ButtonUltimateFalse()
    {
        yield return 0;
        inputSO.buttonUltimate = false;
    }

    public void ButtonScan()
    {
        inputSO.buttonScan = true;
        StartCoroutine(ButtonScanFalse());
    }

    IEnumerator ButtonScanFalse()
    {
        yield return 0;
        inputSO.buttonScan = false;
    }

    public void DisableInput()
    {
        inputSO.disableInput = true;
    }

}

