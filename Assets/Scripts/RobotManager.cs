using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 하드웨어 로봇의 step 정보들을 저장하고, PLC -> 로봇으로 신호를 줄 때, 로봇의 시퀀스를 수행
/// 속성: 로봇의 IK-toolkit, step 컨테이너 리스트
/// 기능: single cycle, cycle, stop, setOrigin
/// </summary>
public class RobotManager : MonoBehaviour
{
    [Serializable]
    public struct Step
    {
        public int stepNum;
        public Vector3 position;
        public Quaternion rotation;
        public bool isSuctionOn;
        public float speed;
        public float interval;
    }

    [SerializeField] IK_toolkit robot1;
    [SerializeField] List<Step> steps;
    private bool isYPlusOn;
    private int xPos;
    [SerializeField] float multiplier = 0.01f;
    [SerializeField] bool isMoving = false;

    [SerializeField] TMP_InputField xPosInput;
    [SerializeField] TMP_InputField yPosInput;
    [SerializeField] TMP_InputField zPosInput;
    [SerializeField] TMP_InputField xRotInput;
    [SerializeField] TMP_InputField yRotInput;
    [SerializeField] TMP_InputField zRotInput;
    [SerializeField] Toggle suctionToggle;
    [SerializeField] TMP_InputField speedInput;
    [SerializeField] TMP_InputField intervalInput;
    private float yPos;
    private float zPos;
    private bool isZPlusOn;
    private bool isXPlusOn;
    private bool isXMinusOn;
    private bool isYMinusOn;
    private bool isZMinusOn;
    private bool isXPlusRotOn;
    private bool isXMinusRotOn;
    private bool isYPlusRotOn;
    private bool isYMinusRotOn;
    private bool isZPlusRotOn;
    private bool isZMinusRotOn;
    private int xRot;
    private int yRot;
    private int zRot;
    private int stepCnt;

    private void Start()
    {
        speedInput.text = "1";
        intervalInput.text = "1";
    }

    /// <summary>
    /// 현재 로봇상태를 steps 리스트에 추가
    /// </summary>
    public void OnTeachBtnClkEvent()
    {
        Step newStep = new Step();
        newStep.position = robot1.ik.localPosition;
        newStep.rotation = robot1.ik.localRotation;
        newStep.isSuctionOn = suctionToggle.isOn;

        bool isParsed = float.TryParse(speedInput.text, out newStep.speed);
        if (!isParsed)
        {
            Debug.LogWarning("속도를 숫자로 입력해 주세요.");
            return;
        }

        isParsed = float.TryParse(intervalInput.text, out newStep.interval);
        if (!isParsed)
        {
            Debug.LogWarning("인터벌을 숫자로 입력해 주세요.");
            return;
        }

        newStep.stepNum = stepCnt;

        Debug.Log($"{stepCnt}번째 Step이 저장되었습니다.");

        steps.Add(newStep);

        stepCnt++;
    }

    public void OnDeleteBtnClkEvent()
    {
        steps.Clear();

        Debug.Log("스탭이 초기화 되었습니다.");
    }

    public void OnStartBtnClkEvent()
    {

    }

    /// <summary>
    /// SetOrigin 버튼 클릭시 원점으로 복귀
    /// </summary>
    public void SetOrigin()
    {

    }

    /// <summary>
    /// 로봇의 Cycle 실행
    /// </summary>
    public void Cycle()
    {

    }

    /// <summary>
    /// 로봇의 Sigle Cycle을 실행
    /// </summary>
    public void SigleCycle()
    {

    }

    /// <summary>
    /// 로봇을 현재 위치에서 작동 정지
    /// </summary>
    public void Stop()
    {
        
    }

    private void Update()
    {
        if (isMoving) return;

        UpdatePosition();

        UpdateRotation();
    }

    private void UpdatePosition()
    {
        if (isXPlusOn) xPos++;
        else if (isXMinusOn) xPos--;
        else xPos = 0;

        if (isYPlusOn) yPos++;
        else if (isYMinusOn) yPos--;
        else yPos = 0;

        if (isZPlusOn) zPos++;
        else if (isZMinusOn) zPos--;
        else zPos = 0;

        xPosInput.text = robot1.ik.localPosition.x.ToString("0.00");
        yPosInput.text = robot1.ik.localPosition.y.ToString("0.00");
        zPosInput.text = robot1.ik.localPosition.z.ToString("0.00");

        robot1.ik.localPosition += new Vector3(xPos, yPos, zPos) * multiplier;
    }

    private void UpdateRotation()
    {
        if (isXPlusRotOn) xRot++;
        else if (isXMinusRotOn) xRot--;
        else xRot = 0;

        if (isYPlusRotOn) yRot++;
        else if (isYMinusRotOn) yRot--;
        else yRot = 0;

        if (isZPlusRotOn) zRot++;
        else if (isZMinusRotOn) zRot--;
        else zRot = 0;

        xRotInput.text = robot1.ik.localRotation.x.ToString("0.00");
        yRotInput.text = robot1.ik.localRotation.y.ToString("0.00");
        zRotInput.text = robot1.ik.localRotation.z.ToString("0.00");

        robot1.ik.localRotation *= Quaternion.Euler(xRot * multiplier, yRot * multiplier, zRot * multiplier);
    }

    public void OnXPlusBtnDownEvent()
    {
        isXPlusOn = true;
    }

    public void OnXPlusBtnUpEvent()
    {
        isXPlusOn = false;
    }

    public void OnXMinusBtnDownEvent()
    {
        isXMinusOn = true;
    }

    public void OnXMinusBtnUpEvent()
    {
        isXMinusOn = false;
    }

    public void OnYPlusBtnDownEvent()
    {
        isYPlusOn = true;
    }

    public void OnYPlusBtnUpEvent()
    {
        isYPlusOn = false;
    }

    public void OnYMinusBtnDownEvent()
    {
        isYMinusOn = true;
    }

    public void OnYMinusBtnUpEvent()
    {
        isYMinusOn = false;
    }

    public void OnZPlusBtnDownEvent()
    {
        isZPlusOn = true;
    }

    public void OnZPlusBtnUpEvent()
    {
        isZPlusOn = false;
    }

    public void OnZMinusBtnDownEvent()
    {
        isZMinusOn = true;
    }

    public void OnZMinusBtnUpEvent()
    {
        isZMinusOn = false;
    }

    public void OnXPlusRotBtnDownEvent()
    {
        isXPlusRotOn = true;
    }

    public void OnXPlusRotBtnUpEvent()
    {
        isXPlusRotOn = false;
    }

    public void OnXMinusRotBtnDownEvent()
    {
        isXMinusRotOn = true;
    }

    public void OnXMinusRotBtnUpEvent()
    {
        isXMinusRotOn = false;
    }

    public void OnYPlusRotBtnDownEvent()
    {
        isYPlusRotOn = true;
    }

    public void OnYPlusRotBtnUpEvent()
    {
        isYPlusRotOn = false;
    }

    public void OnYMinusRotBtnDownEvent()
    {
        isYMinusRotOn = true;
    }

    public void OnYMinusRotBtnUpEvent()
    {
        isYMinusRotOn = false;
    }

    public void OnZPlusRotBtnDownEvent()
    {
        isZPlusRotOn = true;
    }

    public void OnZPlusRotBtnUpEvent()
    {
        isZPlusRotOn = false;
    }

    public void OnZMinusRotBtnDownEvent()
    {
        isZMinusRotOn = true;
    }

    public void OnZMinusRotBtnUpEvent()
    {
        isZMinusRotOn = false;
    }
}
