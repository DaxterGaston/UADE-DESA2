using Assets.Scripts.Abstractions;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.DP.Commands
{
    public class HealCommand : ICommand
    {
        private HealthController _hc;
        private int _amount;
        public HealCommand(HealthController hc, int amount)
        {
            _hc = hc;
            _amount = amount;
        }

        public void Execute()
        {
            _hc.Heal(_amount);
        }
    }
}