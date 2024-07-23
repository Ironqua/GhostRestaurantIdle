using UnityEngine;
using UnityEngine.Events;

public class CoreUISignals : MonoBehaviour
{
    public UnityAction<int> onOpenPanels = delegate { };
    public UnityAction<int> onClosePanels = delegate { };
    public UnityAction<int> onRoomUIIndex = delegate { };
    public static CoreUISignals Instance { get; set; }
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

            if (transform.parent != null)
            {
                transform.SetParent(null);
            }
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
