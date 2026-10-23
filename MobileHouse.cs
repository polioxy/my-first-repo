using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tp_lab2
{
    //Класс мобильный дом. Наследуется от Building и использует интерфейс "передвигается"
    //Класс не абстрактный, потому что дом - это уже конкретная постройка
    public class MobileHouse : Building, IMoveable
    {
        public int MoveRange { get; }

        public MobileHouse(string name, int moveRange = 20, int x = 0, int y = 0) : base(name, x, y)
        {
            MoveRange = moveRange;
        }

        public void MoveTo(int x, int y)
        {
            if (!IsBuilt)
            {
                Console.WriteLine($"{Name} недостроенный и не может ехать");
                return; //Недостроенный дом не поедет
            }
            int distance = Math.Abs(x - X) + Math.Abs(y - Y);
            if (distance <= MoveRange)
            {
                X = x;
                Y = y;
            }
            else
            {
                Console.WriteLine($"{Name} слишком далеко ехать!");
            }
        }
    }
}
