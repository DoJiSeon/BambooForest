using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BgmSound : MonoBehaviour
{
    public Slider masterVolume; //마스터 볼륨 슬라이더
    public Slider bgmVolume; //브금 볼륨 슬라이더
    public Slider sfxVolume; //효과음 볼륨 슬라이더

    private void FixedUpdate()
    {
        masterVolume.value = PlayerPrefs.GetFloat("mastervolume", 0.5f); // 이전에 게임을 플레이했다면 mastervolume에 저장되어있는 플로트 값이 들어가고, 값이 없다면 0.5f를 넣어준다.
        bgmVolume.value = PlayerPrefs.GetFloat("bgmvolume", 0.5f);// 이전에 게임을 플레이했다면 bgmvolume에 저장되어있는 플로트 값이 들어가고, 값이 없다면 0.5f를 넣어준다.
        sfxVolume.value = PlayerPrefs.GetFloat("bgmvolume", 0.5f);// 이전에 게임을 플레이했다면 bgmvolume에 저장되어있는 플로트 값이 들어가고, 값이 없다면 0.5f를 넣어준다.
    }

    public void ChangeMasterVolume() // 마스터 볼륨의 값을 변경한다.
    {// 아래와 같은 방법을 사용하면 다른 스크립트에 있는 메소드를 사용할 수 있다~~! 싱글톤 기법과 함께 열심히 공부해보기~~!
        SoundManager.Instance.ChangeMasterVolume(masterVolume); // 마스터 볼륨 조정하는 슬라이더 값을 넘겨준다.
    }

    public void ChangeBgmVolume()
    {
        SoundManager.Instance.ChangeBgmVolume(bgmVolume); // 마스터 볼륨 조정하는 슬라이더 값을 넘겨준다.
    }

    public void ChangeSfxVolume()
    {
        SoundManager.Instance.ChangeSfxVolume(sfxVolume); // 마스터 볼륨 조정하는 슬라이더 값을 넘겨준다.
    }
}