/// <summary>
/// データを定義しているクラス
/// </summary>
[System.Serializable]
public class Data
{
    //各スポーツのスコアを保持
    public int[] GameScores = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    //総コイン枚数を保持
    public int CoinAmount = 0;
    //着せ替えをどこまでアンロックしているか
    public int[] clothAchive = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    //コインのアンロックレベル
    public int coinLevel = 1;
    //ハートのアンロックレベル
    public int heartLevel = 1;
    //インデックスに対応した着せ替えの内容
    public int[] sportCloth = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    //ゲームの記録
    public int[] PlayScores = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
}
