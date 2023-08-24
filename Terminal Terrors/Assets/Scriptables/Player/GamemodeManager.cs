using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GAMEMODE
{
    fps,
    tufftop
}
public class GamemodeManager : MonoBehaviour
{
    //any way for this to be defined here?
    public GameEvent onChangeGameMode;
    public static bool hasTufftop;
    public static GAMEMODE currentGameMode = GAMEMODE.fps;

    public void changeGameMode(GAMEMODE mode)
    {
        currentGameMode = mode;
        onChangeGameMode.TriggerEvent();
        Debug.Log(mode.ToString());
    }
    private void Awake()
    {
        changeGameMode(GAMEMODE.fps);
    }
    private void Update()
    {
        //DEBUG ONLY
        if(Input.GetKeyDown(KeyCode.I)) 
        {
            changeGameMode(GAMEMODE.tufftop);
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            changeGameMode(GAMEMODE.fps);
        }
    }
}
//THERE CAN ONLY BE ONE OF THESE PER SCENE
/*
_______________7777777777777777777777___________________
__________77777_____________________77777_______________
_______7777_____________________________777_____________
_____7u___________________________________777___________
___717_______________________________________777________
___5___________________________________________71_______
__7n_____________________________________________17_____
_71_______________________________7_______________77____
_u________________________________7________________u____
_u________________________________7______7_________7u___
u__________________________71_____7_____7___________5___
u__________________________o867___7_____7____73o7___7u__
5__________________________71d03________7__768d5_____71_
n__________________n5777774n__u005_77_____508u________u_
1__________________6u7u34u1nnnn4d86777771u8046u777711147
u___7777777________4174u7566u77_7d807__70806qd64447__7b5
n7__7777777771nnnnub47447nu3445477d81__48073n146457___du
_5_________________16777______7u7__b4uq8q7717777117__707
_71_________________ou36964664447__67_7n3u51777_7777_435
__n7_________7_____7____7_777777nnnu_____37771n4443u467n
___7n_______7n54_77_____777____77_________1____7__7__77n
_____u______74dd7bn_____77____n7___________71___7_7____3
_____7u______ud9u51______7__77_777__________5___777___nq
______u______74dd7_77____7_77__71____7_____unn__77n7_1n_
______u7______1d5_ub7___7_7u31__7u571577_717_44_15n43q1_
______u7_______1o1q37___5u47454d088888880d3u774_1d3uu77_
______u7______7__73u4u43n076888888888888888884776q647___
______5_______7___77u0u77775888888888888888801_77db7____
_____17_______7_____u8454u4n88888888888888807_771577____
_____5______________1u_7ub67ud8888888888d6137740d7______
____17____________________ndu774d444q57773u3dd375_______
___n7______________________50b76b75uu97757q9udb7________
 */
