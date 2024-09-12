using UnityEngine;

namespace CTC.Game
{
    public static class Events
    {
        public static CrystalCaptureEvent CrystalCaptureEvent = new CrystalCaptureEvent();
        public static BonusRoundEvent RoundBeginEvent = new BonusRoundEvent();
        public static RoundOverEvent RoundOverEvent = new RoundOverEvent();
        public static GameOverEvent GameOverEvent = new GameOverEvent();
        public static DeathEvent DeathEvent = new DeathEvent();
        public static DamageEvent DamageEvent = new DamageEvent();
    }

    public class CrystalCaptureEvent : GameEvent
    {
        public GameObject CrystalCarrier;
    }

    public class BonusRoundEvent : GameEvent
    {
        public bool isBonusRound;
    }

    public class RoundOverEvent : GameEvent
    {
        public int PlayerTeamPoints;
        public int OpTeamPoints;
    }

    public class GameOverEvent : GameEvent
    {
        public bool Win;
    }

    public class DeathEvent : GameEvent { }

    public class DamageEvent : GameEvent
    {
        public GameObject Source;
        public float DamageAmount;
    }
}
