namespace tp_lab2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Игровые объекты

            //Создаем лучника. Указываем имя, здоровье, урон, дальность хода и координаты
            var archer = new Archer("Герой", hp: 80, damage: 20, moveRange: 3, x: 1, y: 1);

            //Создаем второго лучника (врага). Он будет получать урон
            var enemy = new Archer("Враг", hp: 50, damage: 10, moveRange: 2, x: 5, y: 5);

            //Создаем крепость. Указываем имя, урон и координаты
            var fort = new Fort("Главная крепость", damage: 30, x: 0, y: 0);

            //Создаем дом на колесах. Указываем имя, дальность хода и координаты
            var house = new MobileHouse("Дом на колесах", moveRange: 40, x: 10, y: 10);

            Console.WriteLine("Начальное состояние объектов:");
            Console.WriteLine(archer);
            Console.WriteLine(enemy);
            Console.WriteLine(fort);
            Console.WriteLine(house);



            //Недостроенная крепость не может стрелять
            Console.WriteLine("\nПробуем выстрелить из недостроенной крепости:");
            fort.Attack(enemy); // Урон не пройдет, так как IsBuilt = false
            Console.WriteLine("Здоровье врага после выстрела недостроенной крепости: " + enemy.GetHp());

            //Теперь строим крепость
            Console.WriteLine("\nСтроим крепость");
            fort.Build();
            Console.WriteLine(fort); //Выведет статус built

            //Стреляем из построенной крепости
            Console.WriteLine("\nКрепость стреляет по врагу:");
            fort.Attack(enemy);
            Console.WriteLine("Здоровье врага после выстрела крепости: " + enemy.GetHp());

            //Лучник стреляет по врагу
            Console.WriteLine("\nЛучник атакует врага:");
            archer.Attack(enemy);
            Console.WriteLine("Здоровье врага после атаки лучника: " + enemy.GetHp());

            //Проверяем, что мертвый лучник не может атаковать
            Console.WriteLine(enemy); //Покажет status=dead

            Console.WriteLine("\nМертвый враг пытается атаковать лучника:");
            enemy.Attack(archer); //Ничего не произойдет, так как сработает проверка if (!IsAlive())
            Console.WriteLine("Здоровье лучника после атаки мертвеца: " + archer.GetHp()); //Не поменялось

            //Проверяем движение
            Console.WriteLine("\nПроверка движения с учетом дальности хода:");
            Console.WriteLine($"Текущая позиция Героя: ({archer.GetX()}, {archer.GetY()})");

            archer.MoveTo(10, 10); //Выдаст сообщение, что слишком далеко

            archer.MoveTo(2, 3);
            Console.WriteLine($"Новая позиция лучника: ({archer.GetX()}, {archer.GetY()})");

            //Ограничения движения
            Console.WriteLine("Пытаемся переместить мертвого юнита:");
            enemy.MoveTo(1, 1);

            Console.WriteLine("\nПытаемся переместить недостроенный дом на колесах:");
            house.MoveTo(5, 5);
        }
    }
}
