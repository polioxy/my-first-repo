using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tp_lab2
{
    //Класс Форт. Наследуется от Building и использует интерфейс "атакует"
    //Класс не абстрактный, потому что форт - это уже конкретная постройка
    public class Fort : Building, IAttacker
    {
        public float Damage { get; }

        public Fort(string name, float damage = 25, int x = 0, int y = 0) : base(name, x, y)
        {
            Damage = damage;
        }

        //Реализация метода атаки
        public void Attack(Unit unit)
        {
            if (!IsBuilt) //Недостроенная крепость не стреляет
            {
                Console.WriteLine($"{Name} еще не построена! Стрельба невозможна");
                return;
            } 
            unit.ReceiveDamage(Damage);
        }
    }
}
