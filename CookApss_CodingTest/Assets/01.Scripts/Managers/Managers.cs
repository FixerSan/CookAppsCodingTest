﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : Singleton<Managers>
{
    // 각각의 매니저들을 private 변수로 선언
    private ResourceManager resource;
    private PoolManager pool;
    private ObjectManager obj;
    private SoundManager sound;
    private DialogManager dialog;
    private EventManager event_;
    private GameManager game;
    private CoroutineManager routine;
    private UIManager ui;
    private DataManager data;
    private ScreenManager screen;
    private SceneManager scene;

    // 각각의 매니저들에 대한 public 프로퍼티를 추가
    public static ResourceManager Resource { get { return Instance?.resource; } }
    public static PoolManager Pool { get { return Instance?.pool; } }
    public static ObjectManager Object { get { return Instance?.obj; } }
    public static SoundManager Sound { get { return Instance?.sound; } }
    public static DialogManager Dialog { get { return Instance?.dialog; } }
    public static EventManager Event { get { return Instance?.event_; } }
    public static UIManager UI { get { return Instance?.ui; } }
    public static DataManager Data { get { return Instance?.data; } }
    public static ScreenManager Screen { get { return Instance?.screen; } }
    public static SceneManager Scene { get { return Instance?.scene; } }
    public static GameManager Game { get { return Instance?.game; } }
    public static CoroutineManager Routine { get { return Instance.routine; } }

    [RuntimeInitializeOnLoadMethod]
    public static void CreateManager()
    {
        Instance.Init();
    }

    public void Awake()
    {
        Init();
    }

    public void Init()
    {
        if (isInit) return;
        isInit = true;
        Instance.CreateManagers();
    }

    private void CreateManagers()
    {
        resource = new ResourceManager(); 
        pool = new PoolManager(); 
        obj = new ObjectManager();
        sound = new SoundManager();
        dialog = new DialogManager();
        event_ = new EventManager();
        ui = new UIManager();
        data = new DataManager();
        screen = new ScreenManager();
        scene = new SceneManager();

        game = GameManager.Instance;
        routine = CoroutineManager.Instance;
    }
}
