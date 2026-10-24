using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tp_lab2
{
    //Класс Лучник. Наследуется от Unit и использует интерфейсы "атакует" и "передвигается"
    //Класс не абстрактный, потому что лучник - это уже конкретная боевая единица
    public class Archer : Unit, IAttacker, IMoveable
    {
        //Наносимый урон и дальность передвижения задаются 1 раз и не изменяются снаружи
        public float Damage { get; }
        public int MoveRange { get; }

        //Конструктор с значениями по умолчанию (передадутся если в main не указать значения для них)
        public Archer(string name, float hp = 100, float damage = 15, int moveRange = 6,
                      int x = 0, int y = 0) : base(name, hp, x, y)
        {
            Damage = damage;
            MoveRange = moveRange;
        }

        //Реализация метода атаки
        public void Attack(Unit unit)
        {
            if (!IsAlive())
            {
                Console.WriteLine($"{Name} мертв и не может атаковать");
                return;
            }
            unit.ReceiveDamage(Damage); //Наносим урон цели
        }

        //Реализация метода передвтжения
        public void MoveTo(int x, int y)
        {
            if (!IsAlive())
            {
                Console.WriteLine($"{Name} мертв и не может двигаться");
                return;
            }
                
            int distance = Math.Abs(x - X) + Math.Abs(y - Y); //Считаем сколько клеток дистанция

            if (distance <= MoveRange)
            {
                X = x;
                Y = y; 
                //Успешно переместились
            }
            else
            {
                Console.WriteLine($"Заданная точка находится слишком далеко от лучника!");
            }
        }
    }
}
