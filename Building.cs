using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tp_lab2
{
    //Класс Building наследуется от GameObject и получает его поля и методы
    public abstract class Building : GameObject
    {
        public bool IsBuilt { get; protected set; }

        //Конструктор класса и вызов родительского конструктора через base
        protected Building(string name, int x = 0, int y = 0) : base(name, x, y)
        {
            IsBuilt = false; //Состояние - не достроена 
        }

        //Методы
        public void Build() => IsBuilt = true; //Публичный метод для построения 
        public bool CheckIsBuilt() => IsBuilt;
        public string BuiltStatus()
        {
            if (IsBuilt) return "built";
            else return "not built";
        }

        //Переопределение вывода
        public override string ToString() 
        {
            //Сначала обращаемся к род. методу через base и далее дописываем к нему необходимую инфу
            return $"{base.ToString()} status={BuiltStatus()}";
        }
            
            
    }
}
