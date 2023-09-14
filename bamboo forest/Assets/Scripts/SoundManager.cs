using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[System.Serializable]
public class Sound
{
    public string name; //�� �̸�
    public AudioClip clip; // ��
}
// !!!! �ּ�ó�� �Ǿ��ִ� �ڵ�� ���� �Ⱦ��� �ڵ�! ������ ���̴� �ڵ常 ���캸��!!!!
public class SoundManager : MonoBehaviour
{ // 15 ~ 29�� �̱��� ��� ����� �� - ���ͳ� ���� �� �����غ���! ������ ���ϸ� �Ѿ��� �ϳ��� ��ü�� �����ϵ��� �ϴ°�! �� �̵��Ҷ����� �갡 ���� ����!
    private static SoundManager instance;

    public static SoundManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<SoundManager>();
            }

            return instance;
        }
    }

    public Sound[] effectSounds; //ȿ���� ����� Ŭ����
    public AudioSource[] AudioSourceEfects;// ȿ�������� ���ÿ� ������ ��� �� �� ����
    public string[] playSoundName; //��� ���� ȿ���� ���� �̸� �迭

    public Sound[] attackEffectSounds; //���� ȿ���� �� ����� Ŭ����
    public AudioSource[] attackAudioSourceEfects;//���� ȿ�������� ���ÿ� ������ ��� �� �� ����
    public string[] attackPlaySoundName; //��� ���� ���� ȿ���� ���� �̸� �迭

    float masterValue; //������ ���� ��
    float bgmValue; //��� ���� ��
    float sfxValue; //ȿ���� ���� ��

    public AudioSource bgmPlayer; //��� �÷��� ����� �ҽ�

    [SerializeField]
    private AudioClip mainBgmAudioClip; //���ΰ���ȭ�鿡�� ����� BGM
    [SerializeField]
    private AudioClip TitleBgmAudioClip; //���� ���� ȭ�� BGM

    // AudioSource�� ������ �����ų �� �ִ� �����, AudioClip�� �뷡�� ȿ���� �ϳ��ϳ��� �ǹ��Ѵٰ� �����ϱ�!

    private void Awake() // ���� ó�� �����Ҷ�! �ٸ� ������ �̵��ϰ� ó�� ����!
    {
        if (Instance != this) // ������ ��ü�� �̰��� �ƴҶ�!
        {
            Destroy(this.gameObject); // ���� ���� ��ü�� ���� �Ѵ�!
        }
        DontDestroyOnLoad(this.gameObject); //���� ������ �̵��� �� �� ��ü�� �������� �ʵ��� �ϴ� �ڵ�

        bgmPlayer = GameObject.Find("BGMSoundPlayer").GetComponent<AudioSource>(); //BGM �÷����� ����� �ҽ�
        // 60���� �߰� ���� : ���̾��Ű â�� �ִ� Sound Manager ��ü �ȿ� �� ��ũ��Ʈ�� ����ֱ� ������ Sound Manager ��ü�� GameObject�̴�. 
        //                    ���� �� ��ü�� �����ִ� ��ü �� BGMSoundPlayer��� �̸��� ��ü�� ������ �ִ� AudioSource ������Ʈ�� ã�Ƽ� bgmPlayer��� ������ �ְ�, ����� ���̶� ���


        //sfxPlayer = GameObject.Find("SFXSoundPlayer").GetComponent<AudioSource>();

        PlayBGMSound(); // ���� ������ �� ����� �÷��� �Ѵ�.
    }

    private void Start() // ������ �� �� ó�� ����Ǵ� �޼ҵ� �Ƹ��� awake�� ��������� awake�� �����ǿ������Ǥ����ݴ� ������ ����Ǵ°����� �˰��ִ�...
    {
        playSoundName = new string[AudioSourceEfects.Length]; // ������ �ִ� AudioSource�� ũ�⿡ ���� �� ���� �����Ѵ�.
        attackPlaySoundName = new string[attackAudioSourceEfects.Length]; // ������ �ִ� AudioSource�� ũ�⿡ ���� �� ���� �����Ѵ�.
    }

    void OnEnable() //76 ~ 92 ���� ���� ����� �� ���带 �ٲ� �� �ֵ��� �ϱ� ���� ���Ǵ� �ڵ��̴�. ��״� �״�� ������ ���� ChangeBGM�� �����ؼ� �����ؼ� ���� �ɵ�!
        // �ñ��ϸ� ������~~!
    {
        // �� �Ŵ����� sceneLoaded�� ü���� �Ǵ�.
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // ü���� �ɾ �� �Լ��� �� ������ ȣ��ȴ�.
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ChangeBGM(); // ��� �����ϴ� �Լ�

    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void PlaySE(string _name) // ���������� ����� ���� ����Ʈ ���ϴ� �޼ҵ��̴�
    {
        for (int i = 0; i < effectSounds.Length; i++) // ������ �ִ� ����Ʈ�� ������ŭ for �� ������.
        {
            if (_name == effectSounds[i].name) // �޼ҵ忡�� �޾ƿ� �̸��� i��° ����Ʈ�� �̸��� ��ġ�Ѵٸ�
            {
                for (int j = 0; j < AudioSourceEfects.Length; j++) // ������ �ִ� ������� ���ڸ�ŭ for �� ������. 
                {
                    if (!AudioSourceEfects[j].isPlaying) // j��° ����Ⱑ ������� �ƴ϶��~~
                    {
                        AudioSourceEfects[j].clip = effectSounds[i].clip; //j��° ����� �ҽ� ����Ʈ�� i��° ����Ʈ Ŭ���� �־��ش�., Ŭ���� ���ڵ���, ����� �ҽ��� ���ڵ� ���.. ����
                        AudioSourceEfects[j].Play(); // ���ϴ� ����Ʈ�� �־�����, �÷��� �ϱ�!
                        playSoundName[j] = effectSounds[i].name; // ������� ȿ���� ���� �̸� �־��ش�.
                        return;
                    }
                }
                Debug.Log("��� ���� AudioSource�� ��� ���Դϴ�."); // ���� ��� ����� �ҽ��� ������̶� ����Ʈ ȿ������ ������� ���� ��� ���
                return;
            }
        }
        Debug.Log(_name + "���尡 SoundManager�� ��ϵ��� �ʾҽ��ϴ�."); // ���� ��ϵ��� ���� ���� �̸��� �־��� ���
    }

    public void PlayattackSE(string _name) // ���� ȿ���� ���� , PlaySE �޼ҵ� �ڵ�� �����ϴ�
    {
        for (int i = 0; i < attackEffectSounds.Length; i++)
        {
            if (_name == attackEffectSounds[i].name)
            {
                for (int j = 0; j < attackAudioSourceEfects.Length; j++)
                {
                    if (!attackAudioSourceEfects[j].isPlaying)
                    {
                        attackAudioSourceEfects[j].clip = attackEffectSounds[i].clip;
                        attackAudioSourceEfects[j].Play();
                        attackPlaySoundName[j] = attackEffectSounds[i].name;
                        return;
                    }
                }
                Debug.Log("��� ���� AudioSource�� ��� ���Դϴ�.");
                return;
            }
        }
        Debug.Log(_name + "���尡 SoundManager�� ��ϵ��� �ʾҽ��ϴ�.");
    }

    public void ChangeBGM() //bgm �ٲܶ� ����ϴ� �ڵ�!! �� ����ÿ� ��� ����ϰ� �Ѵ�
    { 
        if (SceneManager.GetActiveScene().name == "GameScene") // ���� ���� �̸��� GameScene�̶��
        { //���� �Ѿ����� �ϳ��� ��ݸ� ���� ������ ����� �ҽ��� bgmPlayer �ϳ��̴�!
            bgmPlayer = GameObject.Find("BGMSoundPlayer").GetComponent<AudioSource>(); // bgmPlayer ����� �ҽ���
            bgmPlayer.clip = mainBgmAudioClip; // mainBgmAudioClip �� Ŭ���� �־��ְ�
            bgmPlayer.Play(); // �÷����Ѵ�.
        }// ������ �ٸ� ����� �ְ� �ʹٸ� �� ���� �̸��� �̿��� 143~148 �ڵ带 ������ �ʿ��� �κи� �ٲ㼭 �������ָ� �ǰ���~~?
    }

    // ȿ�� ���� ��� : �̸��� �ʼ� �Ű�����, ������ ������ �Ű������� ����
    //public void PlaySFXSound(string sfxname, AudioClip clip)
    //{
    //    switch (sfxname)
    //    {
    //        case "getcoin":
    //            sfxPlayer.PlayOneShot(clip);
    //            break;
    //        case "click":
    //            sfxPlayer.PlayOneShot(click);
    //            break;
    //        default:
    //            Debug.Log("ȿ������ ã�� �� �����ϴ�.");
    //            break;
    //    }

    //}

    //BGM ���� ��� : ������ ������ �Ű������� ����
    public void PlayBGMSound() // ó�� ���� ���� �� ���� �Ǵ� �޼ҵ��̴�! 
    {
        bgmPlayer.loop = true; //BGM �����̹Ƿ� ��������(��� ����ǰ� �Ұ���~~?)
        bgmPlayer.volume = PlayerPrefs.GetFloat("bgmvolume", 0.5f); // ��� �÷��̾��� ������ �����ϴ� ��! PlayerPrefs�� �� �����ϱ�!! �̰� ���� ����!
        bgmPlayer.clip = TitleBgmAudioClip; // bgmPlayer�� Ŭ�� �ֱ�
        bgmPlayer.Play(); // ����� �����ϱ�~~!
        //if (scenemanager.getactivescene().buildindex == 1)
        //{
        //    bgmplayer.clip = mainbgmaudioclip;
        //    bgmplayer.play();
        //}
        //else if (scenemanager.getactivescene().buildindex == 2)
        //{
        //    bgmplayer.clip = adventurebgmaudioclip;
        //    bgmplayer.play();
        //}
    }

    public void ChangeMasterVolume(Slider slider) // ������ ���� ������ ���� �����̴� �����ϴ� �޼ҵ�
        // �� �����̴��� �����̸� ��ݰ� ����Ʈ ������ ��� �����ϰų� �����ϴ� �� ����������.
    {
        float ExtraVolume; // �ִ� ����
        ExtraVolume = slider.value - masterValue; // ������ ���� ������ �߰��� ���� ���� �� �� ex) 70 ���� 90���� �ٲ���ٸ� 20�� ����!

        bgmPlayer.volume += ExtraVolume; // ���� ������ ������ �������� �߰����ش�.
        bgmValue += ExtraVolume; // �߰��� ���� ����ŭ �Ȱ��� �־��ְ�,
        PlayerPrefs.SetFloat("bgmvolume", bgmValue); // �޶��� ������ ���� �������ش�.

        for (int i = 0; i < AudioSourceEfects.Length; i++) // ������ҽ��� ����ŭ �ݺ��� ����
        {
            AudioSourceEfects[i].volume += ExtraVolume; // ��� ����Ʈ�� ExtraVolume�� ����ŭ �߰��� �־��ش�.
        }
        sfxValue += ExtraVolume;
        PlayerPrefs.SetFloat("sfxvolume", sfxValue);

        AudioListener.volume = slider.value;
        masterValue = slider.value; // �߰��� ���� ����ŭ �Ȱ��� �־��ְ�,.
        PlayerPrefs.SetFloat("mastervolume", masterValue);// �޶��� ������ ���� �������ش�.
    }

    public void ChangeBgmVolume(Slider slider) // ��� �����ϴ� �޼ҵ�
    {
        bgmPlayer.volume = slider.value; // ������ �����̴��� ���� �������� �־��ش�.
        bgmValue = slider.value; // bgmValue ������ ���� �������� �������ش�.
        PlayerPrefs.SetFloat("bgmvolume", bgmValue); // ���� ��� ������ �������ش�.
    }

    public void ChangeSfxVolume(Slider slider)// ���� ����Ʈ �����ϴ� �޼ҵ�
    {
        //sfxPlayer.volume = slider.value;

        for (int i = 0; i < AudioSourceEfects.Length; i++) // ������ҽ��� ����ŭ �ݺ��� ���
        {
            AudioSourceEfects[i].volume = slider.value; // ��� ����� �ҽ��� ���� ���� ���� �����̴��� ������ ���� �־��ش�.
        }

        for (int i = 0; i < attackAudioSourceEfects.Length; i++)// ���� ������ҽ��� ����ŭ �ݺ��� ���
        {
            attackAudioSourceEfects[i].volume = slider.value;// ��� ���� ����� �ҽ��� ���� ���� ���� �����̴��� ������ ���� �־��ش�.
        }
        sfxValue = slider.value; // sfxValue ������ �����̴� ���� �־��ش�.
        PlayerPrefs.SetFloat("sfxvolume", sfxValue); // ���� �������� ���� �����صд�.
    }
}