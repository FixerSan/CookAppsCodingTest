using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager
{
    public Dictionary<int, DialogData> dialogDatas;
    public T Get<T>(int _UID) where T : Data
    {
        if (typeof(T) == typeof(DialogData)) if (dialogDatas.TryGetValue(_UID, out DialogData _data)) return _data as T;

        Debug.LogError("데이터가 반환되지 않았습니다.");
        return null;
    }

    public DataManager()
    {
        dialogDatas = new Dictionary<int, DialogData> ();
    }
}

public class Data
{

}

[System.Serializable]
public class DialogData : Data
{
    public int dialogUID;
    public string speakerName;
    public string speakerImageKey;
    public string speakerType;
    public string sentence;
    public string buttonOneContent;
    public string buttonTwoContent;
    public string buttonThreeContent;
    public int nextDialogUID;
}
