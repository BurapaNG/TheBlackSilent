using UnityEngine;

public class MapManager: MonoBehaviour
{
    public static MapManager instance;

    [SerializeField] private GameObject _miniMap;
    [SerializeField] private GameObject _LargeMap;

    public bool IsLargeMapOpen { get; private set; }


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        CloseLargeMap();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (IsLargeMapOpen)
            {
                CloseLargeMap();
            }
            else
            {
                OpenLargeMap();
            }
        }
    }

    public void OpenLargeMap()
    {
        _miniMap.SetActive(false);
        _LargeMap.SetActive(true);
        IsLargeMapOpen = true;
    }

    public void CloseLargeMap()
    {
        _miniMap.SetActive(true);
        _LargeMap.SetActive(false);
        IsLargeMapOpen = false;
    }


}
