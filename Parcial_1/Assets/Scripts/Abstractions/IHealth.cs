using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Abstractions
{
    public interface IHealth
    {
        void Heal(int amount);
        void Damage(int amount);
    }
}
