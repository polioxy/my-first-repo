using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tp_lab2
{
    //Интерфейс — это механизм в ООП, который описывает контракт поведения объекта.
    //Он отвечает на вопрос «Что объект умеет делать?» 

    //Атакует
    public interface IAttacker
    {
        void Attack(Unit unit);
        float Damage { get; }
    }

    //Передвигается
    public interface IMoveable
    {
        void MoveTo(int x, int y);
        int MoveRange { get; }
    }

    //Интерфейсы тут нужны, чтобы объединить классы с разной природой (Unit и Building) по их способностям
}
