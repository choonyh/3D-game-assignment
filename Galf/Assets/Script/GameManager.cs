using UnityEngine;
using UnityEngine.Audio;

public class GameManager : MonoBehaviour
{

    private void Start()
    {

        SettingManager.instance.ApplySettingOnStart();
    }


}
