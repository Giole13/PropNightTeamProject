using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDoorPortal : MonoBehaviour
{
    public static ExitDoorPortal _instance;
    public static ExitDoorPortal s__instance
    {
        get
        {
            return _instance;
        }
    }

    [SerializeField] private GameObject Barn2_Door_Left;
    [SerializeField] private GameObject Barn2_Door_Right;
    [SerializeField] private ParticleSystem ExitPortal;

    private GameStatusManager _gsm = default;

    private void Awake()
    {
        _instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        // OpenPortal();
        _gsm = GameObject.Find("GameStatusManager").GetComponent<GameStatusManager>();
        ExitPortal.Stop();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // 플레이어와 부딫히면 탈출 카운트를 1 올린다.
    // 결과창을 보여준다.
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.parent.tag == "Player") { _gsm.SurvivorExit(); }
    }


    // 포탈의 문을 여는 함수
    public void OpenPortal()
    {
        Barn2_Door_Left.transform.localRotation = Quaternion.Euler(0f, 150f, 0f);
        Barn2_Door_Right.transform.localRotation = Quaternion.Euler(0f, 12f, 0f);

        // 파티클 시스템 실행
        ExitPortal.Play();
    }
}
