using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[System.Serializable]
public class Sound
{
    public string name; //곡 이름
    public AudioClip clip; // 곡
}
// !!!! 주석처리 되어있는 코드는 이제 안쓰는 코드! 실제로 쓰이는 코드만 살펴보기!!!!
public class SoundManager : MonoBehaviour
{ // 15 ~ 29는 싱글톤 기법 사용한 것 - 인터넷 보고 꼭 공부해보기! 간단히 말하면 한씬에 하나의 객체만 존재하도록 하는것! 씬 이동할때마다 얘가 같이 따라감!
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

    public Sound[] effectSounds; //효과음 오디오 클립들
    public AudioSource[] AudioSourceEfects;// 효과음들을 동시에 여러개 재생 될 수 있음
    public string[] playSoundName; //재생 중인 효과음 사운드 이름 배열

    public Sound[] attackEffectSounds; //공격 효과음 용 오디오 클립들
    public AudioSource[] attackAudioSourceEfects;//공격 효과음들을 동시에 여러개 재생 될 수 있음
    public string[] attackPlaySoundName; //재생 중인 공격 효과음 사운드 이름 배열

    float masterValue; //마스터 볼륨 값
    float bgmValue; //브금 볼륨 값
    float sfxValue; //효과음 볼륨 값

    public AudioSource bgmPlayer; //브금 플레이 오디오 소스

    [SerializeField]
    private AudioClip mainBgmAudioClip; //메인게임화면에서 사용할 BGM
    [SerializeField]
    private AudioClip TitleBgmAudioClip; //게임 시작 화면 BGM

    // AudioSource는 음악을 재생시킬 수 있는 재생기, AudioClip은 노래나 효과음 하나하나를 의미한다고 생각하기!

    private void Awake() // 씬이 처음 시작할때! 다른 씬으로 이동하고 처음 순간!
    {
        if (Instance != this) // 만들어둔 객체가 이것이 아닐때!
        {
            Destroy(this.gameObject); // 새로 생긴 객체는 삭제 한다!
        }
        DontDestroyOnLoad(this.gameObject); //여러 씬으로 이동할 때 이 객체가 삭제되지 않도록 하는 코드

        bgmPlayer = GameObject.Find("BGMSoundPlayer").GetComponent<AudioSource>(); //BGM 플레이할 오디오 소스
        // 60번줄 추가 설명 : 하이어아키 창에 있는 Sound Manager 객체 안에 이 스크립트가 들어있기 때문에 Sound Manager 객체가 GameObject이다. 
        //                    또한 이 객체에 속해있는 객체 중 BGMSoundPlayer라는 이름의 객체가 가지고 있는 AudioSource 컴포넌트를 찾아서 bgmPlayer라는 변수에 넣고, 사용할 것이란 얘기


        //sfxPlayer = GameObject.Find("SFXSoundPlayer").GetComponent<AudioSource>();

        PlayBGMSound(); // 씬이 시작할 때 브금을 플레이 한다.
    }

    private void Start() // 시작할 때 딱 처음 실행되는 메소드 아마도 awake와 비슷하지만 awake가 조오ㅗ오오오ㅗㅇ오금더 빠르게 실행되는것으로 알고있다...
    {
        playSoundName = new string[AudioSourceEfects.Length]; // 가지고 있는 AudioSource의 크기에 맞춰 새 변수 생성한다.
        attackPlaySoundName = new string[attackAudioSourceEfects.Length]; // 가지고 있는 AudioSource의 크기에 맞춰 새 변수 생성한다.
    }

    void OnEnable() //76 ~ 92 줄은 씬이 변경될 때 사운드를 바꿀 수 있도록 하기 위해 사용되는 코드이다. 얘네는 그대로 가져다 쓰고 ChangeBGM만 공부해서 수정해서 쓰면 될듯!
        // 궁금하면 연락해~~!
    {
        // 씬 매니저의 sceneLoaded에 체인을 건다.
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // 체인을 걸어서 이 함수는 매 씬마다 호출된다.
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ChangeBGM(); // 브금 변경하는 함수

    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void PlaySE(string _name) // 실질적으로 사용할 사운드 이펙트 정하는 메소드이다
    {
        for (int i = 0; i < effectSounds.Length; i++) // 가지고 있는 이펙트의 개수만큼 for 문 돌린다.
        {
            if (_name == effectSounds[i].name) // 메소드에서 받아온 이름이 i번째 이펙트의 이름과 일치한다면
            {
                for (int j = 0; j < AudioSourceEfects.Length; j++) // 가지고 있는 재생기의 숫자만큼 for 문 돌린다. 
                {
                    if (!AudioSourceEfects[j].isPlaying) // j번째 재생기가 사용중이 아니라면~~
                    {
                        AudioSourceEfects[j].clip = effectSounds[i].clip; //j번째 오디오 소스 이펙트에 i번째 이펙트 클립을 넣어준다., 클립은 레코드판, 오디오 소스는 레코드 기계.. 느낌
                        AudioSourceEfects[j].Play(); // 원하는 이펙트를 넣었으니, 플레이 하기!
                        playSoundName[j] = effectSounds[i].name; // 재생중인 효과음 사운드 이름 넣어준다.
                        return;
                    }
                }
                Debug.Log("모든 가용 AudioSource가 사용 중입니다."); // 만약 모든 오디오 소스가 사용중이라 이펙트 효과음을 재생하지 못할 경우 출력
                return;
            }
        }
        Debug.Log(_name + "사운드가 SoundManager에 등록되지 않았습니다."); // 만약 등록되지 않은 사운드 이름을 넣었을 경우
    }

    public void PlayattackSE(string _name) // 공격 효과음 전용 , PlaySE 메소드 코드와 동일하다
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
                Debug.Log("모든 가용 AudioSource가 사용 중입니다.");
                return;
            }
        }
        Debug.Log(_name + "사운드가 SoundManager에 등록되지 않았습니다.");
    }

    public void ChangeBGM() //bgm 바꿀때 사용하는 코드!! 씬 변경시에 대게 사용하곤 한다
    { 
        if (SceneManager.GetActiveScene().name == "GameScene") // 현재 씬의 이름이 GameScene이라면
        { //보통 한씬에는 하나의 브금만 들어가기 때문에 오디오 소스는 bgmPlayer 하나이다!
            bgmPlayer = GameObject.Find("BGMSoundPlayer").GetComponent<AudioSource>(); // bgmPlayer 오디오 소스에
            bgmPlayer.clip = mainBgmAudioClip; // mainBgmAudioClip 이 클립을 넣어주고
            bgmPlayer.Play(); // 플레이한다.
        }// 씬마다 다른 브금을 넣고 싶다면 각 씬의 이름을 이용해 143~148 코드를 복사해 필요한 부분만 바꿔서 수정해주면 되겠죠~~?
    }

    // 효과 사운드 재생 : 이름을 필수 매개변수, 볼륨을 선택적 매개변수로 지정
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
    //            Debug.Log("효과음을 찾을 수 없습니다.");
    //            break;
    //    }

    //}

    //BGM 사운드 재생 : 볼륨을 선택적 매개변수로 지정
    public void PlayBGMSound() // 처음 게임 켰을 때 실행 되는 메소드이다! 
    {
        bgmPlayer.loop = true; //BGM 사운드이므로 루프설정(계속 실행되게 할건지~~?)
        bgmPlayer.volume = PlayerPrefs.GetFloat("bgmvolume", 0.5f); // 브금 플레이어의 볼륨을 설정하는 것! PlayerPrefs도 꼭 공부하기!! 이거 많이 쓰여!
        bgmPlayer.clip = TitleBgmAudioClip; // bgmPlayer에 클립 넣기
        bgmPlayer.Play(); // 재생기 실행하기~~!
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

    public void ChangeMasterVolume(Slider slider) // 설정에 들어가는 마스터 볼륨 슬라이더 조절하는 메소드
        // 이 슬라이더를 움직이면 브금과 이펙트 볼륨이 모두 증가하거나 감소하는 등 움직여진다.
    {
        float ExtraVolume; // 최대 볼륨
        ExtraVolume = slider.value - masterValue; // 이전의 볼륨 값에서 추가된 볼륨 값을 뺀 수 ex) 70 에서 90으로 바뀌었다면 20을 저장!

        bgmPlayer.volume += ExtraVolume; // 원래 볼륨에 증가한 볼륨값을 추가해준다.
        bgmValue += ExtraVolume; // 추가한 볼륨 값만큼 똑같이 넣어주고,
        PlayerPrefs.SetFloat("bgmvolume", bgmValue); // 달라진 변수의 값을 저장해준다.

        for (int i = 0; i < AudioSourceEfects.Length; i++) // 오디오소스의 수만큼 반복문 실행
        {
            AudioSourceEfects[i].volume += ExtraVolume; // 모든 이펙트에 ExtraVolume의 값만큼 추가로 넣어준다.
        }
        sfxValue += ExtraVolume;
        PlayerPrefs.SetFloat("sfxvolume", sfxValue);

        AudioListener.volume = slider.value;
        masterValue = slider.value; // 추가한 볼륨 값만큼 똑같이 넣어주고,.
        PlayerPrefs.SetFloat("mastervolume", masterValue);// 달라진 변수의 값을 저장해준다.
    }

    public void ChangeBgmVolume(Slider slider) // 브금 조절하는 메소드
    {
        bgmPlayer.volume = slider.value; // 조절한 슬라이더의 값을 볼륨값에 넣어준다.
        bgmValue = slider.value; // bgmValue 변수에 현재 볼륨값을 저장해준다.
        PlayerPrefs.SetFloat("bgmvolume", bgmValue); // 현재 브금 볼륨을 저장해준다.
    }

    public void ChangeSfxVolume(Slider slider)// 사운드 이펙트 조절하는 메소드
    {
        //sfxPlayer.volume = slider.value;

        for (int i = 0; i < AudioSourceEfects.Length; i++) // 오디오소스의 수많큼 반복문 재생
        {
            AudioSourceEfects[i].volume = slider.value; // 모든 오디오 소스의 볼륨 값에 현재 슬라이더로 조정한 값을 넣어준다.
        }

        for (int i = 0; i < attackAudioSourceEfects.Length; i++)// 공격 오디오소스의 수많큼 반복문 재생
        {
            attackAudioSourceEfects[i].volume = slider.value;// 모든 공격 오디오 소스의 볼륨 값에 현재 슬라이더로 조정한 값을 넣어준다.
        }
        sfxValue = slider.value; // sfxValue 변수에 슬라이더 값을 넣어준다.
        PlayerPrefs.SetFloat("sfxvolume", sfxValue); // 현재 볼륨값을 따로 저장해둔다.
    }
}