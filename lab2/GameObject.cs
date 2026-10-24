using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tp_lab2
{
    //Абстрактный класс - это класс-шаблон, от которого нельзя напрямую создать объект
    //Создан для того, чтобы от него наследовались другие классы (Unit, Building)
    //и им передавались общие св-ва (ID, Name, X, Y) и методы
    public abstract class GameObject
    {
        //Инкапсуляция - private для невозможности изменения id извне
        //Static - принадлежит не конкрет. объекту, а всему классу, и счетчик не обновляется
        private static int _id = 1;

        //ID доступен только для чтения
        public int Id { get; }
        public string Name { get; set; }

        //Устанавливать и менять координаты можно только наследникам из-за модификатора protected
        public int X { get; protected set; }
        public int Y { get; protected set; }


        //Конструктор класса
        protected GameObject(string name, int x, int y)
        {
            //Значения передаются объектам класса и наследникам по умолчанию
            Id = _id++;
            Name = name;
            X = x;
            Y = y;
        }

        //Методы для получения данных
        public int GetId() => Id;
        public string GetName() => Name;
        public int GetX() => X;
        public int GetY() => Y;

        //Полиморфизм - перезаписываем метод ToString для красивого вывода
        public override string ToString()
        {
            //{GetType().Name} возвращает имя класса объекта
            return $"{GetType().Name} ID: {Id} | Name: {Name} | at ({X}, {Y})";
        }
    }
}
