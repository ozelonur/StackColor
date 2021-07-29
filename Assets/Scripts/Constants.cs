using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Constants
{
    // Collider Tags (If we use tags, but must use Interface not tags!)
    public static string FINIS_LINE_END = "FinishLineEnd";
    public static string CUBE = "Cube";
    public static string WALL = "Wall";
    public static string CASE = "Case";
    public static string COLOR = "_Color";

    // Method Names for Invoke
    public static string KICK_INTERACTION = "KickInteraction";
    public static string END_GAME_INTERACTS = "EndGameInteractions";
    public static string THROW_CUBES = "ThrowCubes";
    public static string GAME_COMPLETE_INTERACTS = "GameComplete";
    public static string LAUNCH_CONFETTI = "LaunchConfetti";
    public static string DESTROY_COIN = "DestroyCoin";


    // Animations
    public static string ANIM_RUN = "Run";
    public static string ANIM_DIE = "Die";
    public static string ANIM_DANCE = "Dance";
    public static string ANIM_KICK = "Kick";
    public static string ANIM_IDLE = "Idle";

    // Texts
    public static string TRY_AGAIN = "Try Again\n";
    public static string NEXT_LEVEL = "Next Level\n";
    public static string TAP_TO_START = "Tap to Start\n";
    public static string BEST_SCORE = "Best Score\n";

    // PlayerPrefs
    public static string COIN_COUNT = "CoinCount";
    public static string HIGH_SCORE = "HighScore";

}
