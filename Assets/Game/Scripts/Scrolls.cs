using UnityEngine;

public class Scrolls : MonoBehaviour
{
    public GameObject scroll1;
    public GameObject scroll2;

    void Awake()
    {
        scroll1.SetActive(true);
        scroll2.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeybindManager.keybind.scroll1))
        {
            scroll1.SetActive(true);
            scroll2.SetActive(false);
        }
        if (Input.GetKeyDown(KeybindManager.keybind.scroll2))
        {
            scroll1.SetActive(false);
            scroll2.SetActive(true);
        }
    }
}
