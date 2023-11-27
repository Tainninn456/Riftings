using UnityEngine;


/// <summary>
/// シーン遷移の際にデータを受け渡しするために一時的に値を格納するクラス
/// </summary>
public class DataReciver : MonoBehaviour
{
    //スポーツの種類を識別する変数
    [HideInInspector]
    public int sportType = 0;
    //ハートの数を保持する変数
    [HideInInspector]
    public int heartAmount = 0;
    //コインのレベルを保持する変数
    [HideInInspector]
    public int coinLevel = 0;
    //対象のスポーツで使用するspriteを保持する変数
    [HideInInspector]
    public Sprite clothSprite = null;
}