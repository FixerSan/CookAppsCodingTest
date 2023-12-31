using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static Define;

public class GameManager : Singleton<GameManager>
{
    //현재 게임 데이터 및 상태
    public GameState state;
    public BattleInfo battleInfo;

    public void Awake()
    {
        Managers.Resource.LoadAllAsync<UnityEngine.Object>("Preload", _completeCallback: () => 
        {
            StartGame(() => 
            {
                state = GameState.BattleBefore;
                battleInfo = new BattleInfo();
            });
        });
    }

    //게임 시작
    public void StartGame(Action _callback)
    {
        Managers.Data.LoadPreData(() => { Managers.Scene.LoadScene(Define.Scene.Main); _callback?.Invoke(); });
    }

    //게임 저장
    public void SaveGame()
    {
        Managers.Data.SavePlayerData(Managers.Data.playerData);
    }

    private void Update()
    {
        if(battleInfo != null)
            battleInfo.Update();
    }

    public void OnApplicationPause(bool pause)
    {
        if (pause)
            SaveGame();
    }
}

[System.Serializable]
public class BattleInfo
{
    //현재 진행중인 스테이지 데이터
    public StageData currentStage;
    public int clearStar;

    //현재 진행중인 스테이지의 배치된 플레이어 entity  및 위치
    public BattleEntityData[] armyFront;
    public BattleEntityData[] armyCenter;
    public BattleEntityData[] armyRear;

    //현재 진행중인 스테이지중 플레이어의 상태
    public int isCanUseBattleEntityCount;
    public int nowUseBattleEntityCount;
    public int armyCurrentHP;
    public int armyMaxHP;
    public int armyAttackForce;
    public int armybattleForce;
    public int allBattlePoint;

    //데미지의 맞춰 정렬된 플레이어 엔티티들
    public List<BattleEntityController> battleMVPPoints;

    //현재 진행중인 스테이지의 배치된 적 entity  및 위치
    public BattleEntityData[] enemyFront;
    public BattleEntityData[] enemyCenter;
    public BattleEntityData[] enemyRear;

    //현재 진행중인 스테이지중 적의 상태
    public int nowEnemyCount;
    public int enemyCurrentHP;
    public int enemyMaxHP;
    public int enemyAttackForce;
    public int enemybattleForce;

    //게임 진행 설정 및 정보
    public bool isAutoSkill;
    public bool isFastSpeed;
    public float time;


    //플레이어 엔티티 배치
    public bool UseBattleEntity(BattleEntityData _data)
    {
        //더 배치할 수 있는지 체크
        if (nowUseBattleEntityCount == isCanUseBattleEntityCount) return false;
        nowUseBattleEntityCount++;

        //비어있는 배열 체크 및 적용
        int nullIndex = armyFront.FindEmptyArrayIndex();
        if (nullIndex != -1)
        {
            armyFront[nullIndex] = _data;
            SetArmyBattleForceValue();
            return true;
        }

        nullIndex = armyCenter.FindEmptyArrayIndex();
        if(nullIndex != -1)
        {
            armyCenter[nullIndex] = _data;
            SetArmyBattleForceValue();
            return true;
        }

        nullIndex = armyRear.FindEmptyArrayIndex();
        if (nullIndex != -1)
        {
            armyRear[nullIndex] = _data;
            SetArmyBattleForceValue();
            return true;
        }

        //비어있는 배열이 없을 시 False 반환
        return false;
    }

    //지정된 위치에 플레이어 엔티티 배치
    public bool UseBattleEntity(BattleEntityData _data, PlaceType _type)
    {
        if (nowUseBattleEntityCount == isCanUseBattleEntityCount) return false;
        nowUseBattleEntityCount++;

        int nullIndex = 0;
        switch (_type)
        {
            case PlaceType.Front:
                nullIndex = armyFront.FindEmptyArrayIndex();
                if (nullIndex != -1) 
                {
                    armyFront[nullIndex] = _data;
                    SetArmyBattleForceValue();                
                }
                return true;

            case PlaceType.Center:
                nullIndex = armyCenter.FindEmptyArrayIndex();
                if (nullIndex != -1)
                {
                    armyCenter[nullIndex] = _data;
                    SetArmyBattleForceValue();
                }
                return true;

            case PlaceType.Rear:
                nullIndex = armyRear.FindEmptyArrayIndex();
                if (nullIndex != -1)
                {
                    armyRear[nullIndex] = _data;
                    SetArmyBattleForceValue();
                }
                return true;
        }

        return false;
    }

    //플레이어 배치 취소 
    public void UnUseBattleEntity(BattleEntityData _data)
    {
        //배치 카운트 감소
        nowUseBattleEntityCount--;

        //각 배치 검사 후 그 배치에 있으면 배치 취소
        for (int i = 0; i < armyFront.Length; i++)
        {
            if (armyFront[i] == _data)
            {
                armyFront[i] = null;
                SetArmyBattleForceValue();
                return;
            }
        }

        for (int i = 0; i < armyCenter.Length; i++)
        {
            if (armyCenter[i] == _data)
            {
                armyCenter[i] = null;
                SetArmyBattleForceValue();
                return;
            }
        }

        for (int i = 0; i < armyRear.Length; i++)
        {
            if (armyRear[i] == _data)
            {
                armyRear[i] = null;
                SetArmyBattleForceValue();
                return;
            }
        }
    }

    //플레이어 배치 초기화
    public void UnUseAllBattleEntity()
    {
        armyFront = new BattleEntityData[3];
        armyCenter = new BattleEntityData[3];
        armyRear = new BattleEntityData[3];

        nowUseBattleEntityCount = 0;
        armybattleForce = 0;

        UpdateUI();
    }

    //배틀 포인트 정리
    private void SetArmyBattleForceValue()
    {
        //토탈 변수 초기화
        armyAttackForce = 0;
        armyMaxHP = 0;
        armybattleForce = 0;

        //각 배치에 오브젝트가 있는지 체크 후 있다면 각 토탈변수에 적립
        for (int i = 0; i < armyFront.Length; i++)
        {
            if (armyFront[i] != null)
            {
                armyAttackForce += armyFront[i].attackForce;
                armyMaxHP += armyFront[i].maxHP;
            }
        }

        for (int i = 0; i < armyCenter.Length; i++)
        {
            if (armyCenter[i] != null)
            {
                armyAttackForce += armyCenter[i].attackForce;
                armyMaxHP += armyCenter[i].maxHP;
            }
        }

        for (int i = 0; i < armyRear.Length; i++)
        {
            if (armyRear[i] != null)
            {
                armyAttackForce += armyRear[i].attackForce;
                armyMaxHP += armyRear[i].maxHP;
            }
        }

        //배틀 포인트 계산
        armybattleForce = armyAttackForce + armyMaxHP;
        UpdateUI();
    }

    /// <summary>
    /// World맵에서 스테이지 선택시 호출됨
    /// </summary>
    /// <param name="_UID">스테이지 데이터 인덱스</param>
    public void SetStageData(int _UID)
    {
        currentStage = Managers.Data.GetStageData(_UID);

        nowEnemyCount = 0;
        enemyAttackForce = 0;
        enemyMaxHP = 0;
        enemybattleForce = 0;
        time = 60;

        enemyFront = new BattleEntityData[3];

        for (int i = 0; i < currentStage.frontEnemyUIDs.Length; i++)
        {
            BattleEntityData data = Managers.Data.GetBattleEntityData(currentStage.frontEnemyUIDs[i], currentStage.frontEnemyLevels[i]);
            enemyFront[i] = data;
            nowEnemyCount++;
            enemyAttackForce += data.attackForce;
            enemyMaxHP += data.maxHP;
        }

        enemyCenter = new BattleEntityData[3];
        for (int i = 0; i < currentStage.centerEnemyUIDs.Length; i++)
        {
            BattleEntityData data = Managers.Data.GetBattleEntityData(currentStage.centerEnemyUIDs[i], currentStage.centerEnemyLevels[i]);
            enemyCenter[i] = data;
            nowEnemyCount++;
            enemyAttackForce += data.attackForce;
            enemyMaxHP += data.maxHP;
        }

        enemyRear = new BattleEntityData[3];
        for (int i = 0; i < currentStage.rearEnemyUIDs.Length; i++)
        {
            BattleEntityData data = Managers.Data.GetBattleEntityData(currentStage.rearEnemyUIDs[i], currentStage.rearEnemyLevels[i]);
            enemyRear[i] = data;
            nowEnemyCount++;
            enemyAttackForce += data.attackForce;
            enemyMaxHP += data.maxHP;
        }

        enemybattleForce = enemyMaxHP + enemyAttackForce;
    }

    //스테이지가 시작됄 시 초기화
    public void StartStage()
    {
        clearStar = 0;
        allBattlePoint = 0;

        armyCurrentHP = armyMaxHP;
        enemyCurrentHP = enemyMaxHP;

        battleMVPPoints.Clear();
        battleMVPPoints = Managers.Object.Armys;
        UpdateUI();
    }   

    //MVP포인트가 변경될 시 초기화 및 높은 포인트 순으로 배열 정렬
    public void UpdateMVPPoints()
    {
        List<BattleEntityController> tempList = new List<BattleEntityController>();
        bool[] isChecked = new bool[Managers.Object.Armys.Count];
        for (int i = 0; i < isChecked.Length; i++)
            isChecked[i] = false;
        int bestPoint = 0;
        int bestIndex = -1;

        for (int i = 0; i < Managers.Object.Armys.Count; i++)
        {
            bestPoint = 0;
            for (int j = 0; j < Managers.Object.Armys.Count; j++)
            {
                if (isChecked[j]) continue;
                if(bestPoint < Managers.Object.Armys[j].mvpPoint)
                {
                    bestPoint = Managers.Object.Armys[j].mvpPoint;
                    bestIndex = j;
                }
            }
            isChecked[bestIndex] = true;
            tempList.Add(Managers.Object.Armys[bestIndex]);
        }

        battleMVPPoints = tempList;
        allBattlePoint = 0;

        for (int i = 0; i < battleMVPPoints.Count; i++)
            allBattlePoint += battleMVPPoints[i].mvpPoint;

        UpdateUI();
    }

    //팀 체력이 달았을 시 초기화
    public void UpdateTeamHP(VoidEventType _type)
    {
        if (_type != VoidEventType.OnChangeControllerStatus) return;
        if (Managers.Game.state != GameState.BattleProgress) return;
        armyCurrentHP = 0;
        enemyCurrentHP = 0;

        
        foreach (var item in Managers.Object.Armys)
            armyCurrentHP += item.status.CurrentHP;
        foreach (var item in Managers.Object.Enemys)
            enemyCurrentHP += item.status.CurrentHP;
        UpdateUI();
    }

    //게임 스피드 상태 변경 및 처리
    public void ChangeFastSpeed()
    {
        isFastSpeed = !isFastSpeed;
        if (isFastSpeed) Time.timeScale = 1.5f;
        else Time.timeScale = 1.0f;
    }

    //오토 스킬 상태 변경
    public void ChangeAutoSkill()
    {
        isAutoSkill = !isAutoSkill;
    }

    //UI 업데이트 이벤트 호출
    public void UpdateUI()
    {
        Managers.Event.OnVoidEvent?.Invoke(VoidEventType.OnChangeBattleInfo);
    }

    //스테이지가 진행중일 때 시간 감소 처리 및 상태 변경
    public void CheckTime()
    {
        if (Managers.Game.state == GameState.BattleProgress)
        {
            time -= Time.deltaTime;
            UpdateUI();
            if (time <= 0)
            {
                time = 0;
                TimeOver();
            }
        }
    }

    //승리 처리
    public void Victory()
    {
        Managers.Game.state = Define.GameState.BattleAfter;
        Managers.Routine.StartCoroutine(VictoryRoutine());
    }

    //패배 처리
    public void Lose()
    {
        Managers.Game.state = Define.GameState.BattleAfter;
        Managers.Routine.StartCoroutine(LoseRoutine());
    }

    //공통된 종료 처리
    private void BaseEndStage()
    {
        //값 초기화
        armyFront = new BattleEntityData[3];
        armyCenter = new BattleEntityData[3];
        armyRear = new BattleEntityData[3];
        nowUseBattleEntityCount = 0;
        armyCurrentHP = 0;
        armyMaxHP = 0;
        armyAttackForce = 0;
        armybattleForce = 0;
        Time.timeScale = 1.0f;

        //각 오브젝트들 삭제
        for (int i = 0; i < Managers.Object.Armys.Count; i++)
        {
            Managers.Object.Armys[i].StopAllRoutine();
            Managers.Resource.Destroy(Managers.Object.Armys[i].gameObject);
        }
        Managers.Object.Armys.Clear();
        

        for (int i = 0; i < Managers.Object.Enemys.Count; i++)
        {
            Managers.Object.Enemys[i].StopAllRoutine();
            Managers.Resource.Destroy(Managers.Object.Enemys[i].gameObject);
        }
        Managers.Object.Enemys.Clear();
    }

    //승리 처리 부분
    private IEnumerator VictoryRoutine()
    {
        //남아있는 오브젝트 승리 포즈
        for (int i = 0; i < Managers.Object.Armys.Count; i++)
            Managers.Object.Armys[i].ChangeState(BattleEntityState.EndBattle);

        //승리 처리 및 UI 호출
        yield return new WaitForSeconds(2);
        Managers.Screen.FadeIn(0.5f, () =>
        {
            BaseEndStage();
            int aliveArmyCount = 0;
            for (int i = 0; i < Managers.Object.Armys.Count; i++)
                if (Managers.Object.Armys[i].state != BattleEntityState.Die)
                    aliveArmyCount++;

            if (aliveArmyCount == Managers.Object.Armys.Count)
                clearStar = 3;

            else if (aliveArmyCount >= ((float)Managers.Object.Armys.Count / 3) * 2)
                clearStar = 2;

            else clearStar = 1;

            Managers.UI.ShowPopupUI<UIPopup_Result>().Init(Define.GameResult.Victory);
            Managers.Screen.FadeOut(0.5f);
        });
    }


    //승리 처리 부분
    private IEnumerator LoseRoutine()
    {
        //적 오브젝트 승리 포즈
        for (int i = 0; i < Managers.Object.Enemys.Count; i++)
            Managers.Object.Enemys[i].ChangeState(BattleEntityState.EndBattle);

        //패배 처리 및 UI 호출
        yield return new WaitForSeconds(2);
        Managers.Screen.FadeIn(0.5f, () =>
        {
            BaseEndStage();
            Managers.UI.ShowPopupUI<UIPopup_Result>().Init(Define.GameResult.Lose);
            Managers.Screen.FadeOut(0.5f);
        });
    }

    //타임 오버시 패배 처리
    public void TimeOver()
    {
        Lose();
    }

    public void Update()
    {
        CheckTime();
    }

    public BattleInfo()
    {
        armyRear = new BattleEntityData[3];
        armyCenter = new BattleEntityData[3];
        armyFront = new BattleEntityData[3];

        enemyRear = new BattleEntityData[3];
        enemyCenter = new BattleEntityData[3];
        enemyFront = new BattleEntityData[3];

        isCanUseBattleEntityCount = 4;
        battleMVPPoints = new List<BattleEntityController>();
        isAutoSkill = false;
        isFastSpeed = false;

        Managers.Event.OnVoidEvent -= UpdateTeamHP;
        Managers.Event.OnVoidEvent += UpdateTeamHP;
    }
}