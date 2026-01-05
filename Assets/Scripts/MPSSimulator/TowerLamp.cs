using UnityEngine;

/// <summary>
/// PLC로 부터 X00, X01, X02 등의 신호가 들어오면 Lamp들에 불이 켜진다.
/// 속성: Red, Yellow, Green의 MeshRenderer, PLC로 부터 들어오는 신호 3개
/// </summary>
public class TowerLamp : MonoBehaviour
{
    [Header("PLC 신호")]
    public bool redLamSignal;
    public bool yellowLamSignal;
    public bool greenLamSignal;

    [Header("타워램프 설정")]
    [SerializeField] MeshRenderer redLampMR;
    [SerializeField] MeshRenderer yellowLampMR;
    [SerializeField] MeshRenderer greenLampMR;

    // Update is called once per frame
    void Update()
    {
        if(redLamSignal)
            redLampMR.material.color = new Color(1, 0, 0, 0.7f);
        else
            redLampMR.material.color = new Color(0, 0, 0, 0.7f);

        if (yellowLamSignal)
            yellowLampMR.material.color = new Color(1, 1, 0, 0.7f);
        else
            yellowLampMR.material.color = new Color(0, 0, 0, 0.7f);

        if (greenLamSignal)
            greenLampMR.material.color = new Color(0, 1, 0, 0.7f);
        else
            greenLampMR.material.color = new Color(0, 0, 0, 0.7f);
    }
}
