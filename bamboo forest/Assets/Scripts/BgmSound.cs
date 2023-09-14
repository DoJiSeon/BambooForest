using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BgmSound : MonoBehaviour
{
    public Slider masterVolume; //������ ���� �����̴�
    public Slider bgmVolume; //��� ���� �����̴�
    public Slider sfxVolume; //ȿ���� ���� �����̴�

    private void FixedUpdate()
    {
        masterVolume.value = PlayerPrefs.GetFloat("mastervolume", 0.5f); // ������ ������ �÷����ߴٸ� mastervolume�� ����Ǿ��ִ� �÷�Ʈ ���� ����, ���� ���ٸ� 0.5f�� �־��ش�.
        bgmVolume.value = PlayerPrefs.GetFloat("bgmvolume", 0.5f);// ������ ������ �÷����ߴٸ� bgmvolume�� ����Ǿ��ִ� �÷�Ʈ ���� ����, ���� ���ٸ� 0.5f�� �־��ش�.
        sfxVolume.value = PlayerPrefs.GetFloat("bgmvolume", 0.5f);// ������ ������ �÷����ߴٸ� bgmvolume�� ����Ǿ��ִ� �÷�Ʈ ���� ����, ���� ���ٸ� 0.5f�� �־��ش�.
    }

    public void ChangeMasterVolume() // ������ ������ ���� �����Ѵ�.
    {// �Ʒ��� ���� ����� ����ϸ� �ٸ� ��ũ��Ʈ�� �ִ� �޼ҵ带 ����� �� �ִ�~~! �̱��� ����� �Բ� ������ �����غ���~~!
        SoundManager.Instance.ChangeMasterVolume(masterVolume); // ������ ���� �����ϴ� �����̴� ���� �Ѱ��ش�.
    }

    public void ChangeBgmVolume()
    {
        SoundManager.Instance.ChangeBgmVolume(bgmVolume); // ������ ���� �����ϴ� �����̴� ���� �Ѱ��ش�.
    }

    public void ChangeSfxVolume()
    {
        SoundManager.Instance.ChangeSfxVolume(sfxVolume); // ������ ���� �����ϴ� �����̴� ���� �Ѱ��ش�.
    }
}