using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tp_lab2
{
    //Класс Unit наследуется от GameObject и получает его поля и методы
    public abstract class Unit : GameObject
    {
        //Protected защищает от внешних изменений, но классы-наследники могут менятб хп (при атаке) 
        protected float Hp;
        protected float MaxHp;


        //Конструктор класса
        //Через : base мы передаем имя и координаты в базовый класс GameObject, 
        //чтобы он сохранил их в свои свойства и выдал уникальный ID
        protected Unit(string name, float maxHp, int x=0, int y=0): base (name, x, y)
        {
            Hp = maxHp; //При появлении на карте юнит полностью здоров
            MaxHp = maxHp;
        }

        //Методы
        public bool IsAlive() => Hp>0;
        public float GetHp() => Hp;
        public string LifeStatus()
        {
            if (IsAlive()) return "alive";
            else return "dead";
        }

        //Виртуальный метод - наследники класса смогут его переопределить. Полиморфизм
        //(например для ситуации когда юнит в броне и получает меньше урона)
        public virtual void ReceiveDamage(float damage)
        {
            if (!IsAlive()) return;

            Hp -= damage;
            if (Hp < 0) Hp = 0; //Не дает здоровью принимать отриц. значения
        }

        //Переопределение вывода
        public override string ToString()
        {
            //Сначала обращаемся к род. методу через base и далее дописываем к нему необходимую инфу
            return $"{base.ToString()} HP={Hp}/{MaxHp} status={LifeStatus()}";
        }
    }
}
