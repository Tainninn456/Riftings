using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private int kickCount;
    private int coinCount;

    private State state;
    public enum State
    {
        Menu,
        Play,
        Result
    }
    public enum Information
    {
        kick,
        coin
    }

    public enum Instruction
    {
        add,
        use
    }
    private void Awake()
    {
        if(Instance == null) { Instance = this; }
    }

    //GameManager内の情報に外部からアクセスする
    public int InformationAccess(Information info, Instruction inst)
    {
        if(info == Information.kick) { return kick(inst); }
        else if(info == Information.coin) { return coin(inst); }
        return 0;
    }

    //ゲームの状態を持つ
    public void StateChanger(State stateInfo)
    {
        state = stateInfo;
    }

    private int kick(Instruction inst)
    {
        if(inst == Instruction.add) { kickCount++; return 0; }
        else if(inst == Instruction.use) { return kickCount; }
        return 0;
    }

    private int coin(Instruction inst)
    {
        if (inst == Instruction.add) { coinCount++; return 0; }
        else if (inst == Instruction.use) { return coinCount; }
        return 0;
    }
}
