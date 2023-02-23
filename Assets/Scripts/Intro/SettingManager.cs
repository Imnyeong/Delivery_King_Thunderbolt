using System;
using UnityEngine;
using UnityEngine.UI;

public class SettingManager : MonoBehaviour
{
    string settingString = "setting";
    // 0 == 볼륨, 1 == 진동
    [SerializeField] Sprite[] sprites;
    [SerializeField] Image[] buttonImages;

    void Start()
    {
        for (int i = 0; i < buttonImages.Length; ++i)
        {
            buttonImages[i].sprite = sprites[PlayerPrefs.GetInt(settingString + i.ToString())];
        }
        // 단말기에 저장된 설정대로 스프라이트 설정
    }

    public void OnClickButton(int _index)
    {
        bool check = Convert.ToBoolean(PlayerPrefs.GetInt(settingString + _index.ToString()));
        PlayerPrefs.SetInt(settingString + _index.ToString(), Convert.ToInt32(!check));
        buttonImages[_index].sprite = sprites[PlayerPrefs.GetInt(settingString + _index.ToString())];
        // on, off 설정을 변경하고 그에 맞는 스프라이트로 변경
    }
}
