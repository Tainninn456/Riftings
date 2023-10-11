using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private int kickCount;
    private int coinCount;

    private State state;
    private ModeName gameMode;
    public enum State
    {
        menu,
        game,
        result
    }
    public enum ModeName
    {
        soccer,
        tennis,
        baseball,
        boring,
        panchi,
        tabletennis,
        ragby,
        biriyard,
        volley
    }
    public enum Information
    {
        kick,
        coin,
        mode,
        state
    }

    public enum Instruction
    {
        add,
        use,
        insert
    }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //GameManager内の情報に外部からアクセスする
    public int InformationAccess(Information info, Instruction inst)
    {
        if(info == Information.kick) { return KickCountOperation(inst); }
        else if(info == Information.coin) { return CoinCountOperation(inst); }
        return 0;
    }

    public int InformationAccess(Information info, Instruction inst, ModeName insertName, State insertState)
    {
        if (info == Information.mode) { return GameModeOperation(inst, insertName); }
        else if(info == Information.state) { return StateOperation(inst, insertState); }
        return 0;
    }

    private int StateOperation(Instruction inst, State instate)
    {
        if (inst == Instruction.insert) { state = instate; return 0; }
        else if (inst == Instruction.use) { return (int)state; }
        return 0;
    }

    private int GameModeOperation(Instruction inst, ModeName insertName)
    {
        if (inst == Instruction.insert) { gameMode = insertName; return 0; }
        else if (inst == Instruction.use) { return (int)gameMode; }
        return 0;
    }
    private int KickCountOperation(Instruction inst)
    {
        if(inst == Instruction.add) { kickCount++; return 0; }
        else if(inst == Instruction.use) { return kickCount; }
        return 0;
    }

    private int CoinCountOperation(Instruction inst)
    {
        if (inst == Instruction.add) { coinCount++; return 0; }
        else if (inst == Instruction.use) { return coinCount; }
        return 0;
    }
}
