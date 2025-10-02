using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class AudioManager : MonoBehaviour
{
    //오브젝트 중복체크를 위한 인스턴스 생성
    public static AudioManager Instance;

    //AudioSource 풀링
    [SerializeField] private int poolSize = 20;
    List<AudioSource> audioPool = new List<AudioSource>();

    //BGM 재생할 AudioSource
    public AudioSource bgmAudioSource;

    //현재 맵 이름 저장 변수
    string currentSceneName;


    //초기화용 BGMSO들 모으기
    [SerializeField] private List<MapBGMSO> mapBGMSOList;
    [SerializeField] private List<TrialAreaBGMSO> trialAreaBGMSOList;
    [SerializeField] private List<BossBGMSO> bossBGMSOList;

    //BGM 타입별 딕셔너리 구축
    Dictionary<string, AudioClip> mapBGMDict = new();
    Dictionary<string, AudioClip> trialAreaBGMDict = new();
    Dictionary<string, AudioClip> bossBGMDict = new();


    //초기화용 SFXSO들 모으기
    [SerializeField] private List<SFXSO> sfxSOList;

    //SFX 딕셔너리 구축
    Dictionary<SFXType, AudioClip> sfxDict = new();



    private void Awake()
    {
        // 기존 인스턴스가 존재할 때 && 지금 새로운 인스턴스가 생성되려고 할 때
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);    //중복되지 않도록 지금 새롭게 생성되는 놈은 파괴시킨다
            return;
        }
        // 인스턴스 처음 할당
        Instance = this;
        DontDestroyOnLoad(gameObject);

        //audioSource 풀링 진행
        CreatePool();

        //BGMType별 딕셔너리 초기화
        foreach(MapBGMSO bgm in mapBGMSOList)
        {
            mapBGMDict[bgm.SceneName] = bgm.audioClip;
        }

        foreach (TrialAreaBGMSO bgm in trialAreaBGMSOList)
        {
            trialAreaBGMDict[bgm.gameEventDataSO.gameEventID] = bgm.audioClip;
        }

        foreach (BossBGMSO bgm in bossBGMSOList)
        {
            bossBGMDict[bgm.gameEventDataSO.gameEventID] = bgm.audioClip;
        }

        //SFX 딕셔너리 초기화
        foreach (SFXSO sfx in sfxSOList)
        {
            sfxDict[sfx.sfxType] = sfx.audioClip;
        }
    }
    void CreatePool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = new GameObject("SFX_AudioSource_" + i);
            obj.transform.SetParent(transform);
            AudioSource source = obj.AddComponent<AudioSource>();
            source.spatialBlend = 1f; // 3D 사운드
            source.playOnAwake = false;
            audioPool.Add(source);
        }
    }

    //이벤트 구독
    private void OnEnable()
    {
        //SFX 요청할 때
        AudioEvents.OnSFXRequest += PlaySFX;
        //BGM 요청할 때
        AudioEvents.OnBGMRequest += PlayBGM;
        //현재 BGM 종료 요청할 때
        AudioEvents.OnBGMEnd += ResetBGM;

        //씬 로드 시 이벤트
        SceneManager.sceneLoaded += OnSceneLoaded;


    }
    private void OnDisable()
    {
        //SFX 요청할 때
        AudioEvents.OnSFXRequest -= PlaySFX;
        //BGM 요청할 때
        AudioEvents.OnBGMRequest -= PlayBGM;
        //현재 BGM 종료 요청할 때
        AudioEvents.OnBGMEnd -= ResetBGM;

        //씬 로드 시 이벤트
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void PlaySFX(SFXType key, Transform requestTransform)
    {
        if (sfxDict.TryGetValue(key, out AudioClip clip))
        {
            //요청한 오브젝트의 거리가 너무 멀면 무시하도록
            //if (!IsWithinHearingRange(requestTransform)) return;

            AudioSource source = GetAudioSource();
            if(source != null)
            {
                source.transform.position = requestTransform.position;
                source.clip = clip;
                source.Play();
            }
        }
    }

    void PlayBGM(BGMType type, string key)
    {
        switch (type)
        {
            case BGMType.Map:
                PlayMapBGM(key);

                break;

            case BGMType.TrialArea:
                PlayTrialAreaBGM(key);

                break;

            case BGMType.Boss:
                PlayBossBGM(key);

                break;
        }
    }
    void PlayMapBGM(string sceneName)
    {
        if (mapBGMDict.TryGetValue(sceneName, out AudioClip clip))
        {
            if (bgmAudioSource.clip == clip) return;

            bgmAudioSource.clip = clip;
            bgmAudioSource.loop = true;
            bgmAudioSource.volume = 0.1f;
            bgmAudioSource.Play();
        }
    }
    void PlayTrialAreaBGM(string gameEventID)
    {
        if (trialAreaBGMDict.TryGetValue(gameEventID, out AudioClip clip))
        {
            bgmAudioSource.clip = clip;
            bgmAudioSource.loop = true;
            bgmAudioSource.volume = 0.1f;
            bgmAudioSource.Play();
        }
    }
    void PlayBossBGM(string gameEventID)
    {
        if (bossBGMDict.TryGetValue(gameEventID, out AudioClip clip))
        {
            bgmAudioSource.clip = clip;
            bgmAudioSource.loop = true;
            bgmAudioSource.volume = 0.1f;
            bgmAudioSource.Play();
        }
    }
    void ResetBGM()
    {
        if(currentSceneName != null)
        {
            AudioEvents.InvokeBGMRequest(BGMType.Map, currentSceneName);
        }
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        currentSceneName = scene.name;
        AudioEvents.InvokeBGMRequest(BGMType.Map, currentSceneName);
    }


    AudioSource GetAudioSource()
    {
        foreach (var source in audioPool)
        {
            if (!source.isPlaying)
                return source;
        }
        return null; // 풀링 오브젝트들이 다 차면 null 반환
    }
    bool IsWithinHearingRange(Transform requestTransform)
    {
        // 플레이어와의 거리 제한
        float distance = Vector3.Distance(PlayerController.Instance.transform.position, requestTransform.position);
        return distance < 20f; // 예: 20미터 이내만 재생
    }


}
