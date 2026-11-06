namespace tp_lab4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //CheckStrategy();
            //CheckChain();
            //CheckIterator();
        }
        static void CheckStrategy()
        {
            Console.WriteLine("1. Паттерн Strategy");

            //Создаем контекст (кассу)
            PaymentContext context = new PaymentContext();

            //Клиент оплачивает картой
            context.SetStrategy(new CardPayment());
            context.ProcessPayment(5000);

            //Клиент оплачивает наличными
            context.SetStrategy(new CashPayment());
            context.ProcessPayment(3000);
        }

        static void CheckChain()
        {
            Console.WriteLine("2. Паттерн Chain of Responsibility");

            //Собираем цепочку: Бот - Менеджер - Директор
            SupportHandler bot = new BotHandler();
            SupportHandler manager = new ManagerSupportHandler();
            SupportHandler director = new DirectorSupportHandler();

            bot.SetNextHandler(manager);
            manager.SetNextHandler(director);

            //Простой вопрос для бота
            Console.WriteLine("\nЗапрос 1: 'Сколько стоит абонемент?'");
            bot.HandleRequest(new SupportRequest(SupportRequestType.Simple));

            //Вопрос по оплате ддля менеджера
            Console.WriteLine("\nЗапрос 2: 'Я оплатил абонемент, но он не добавился в личный кабинет. Оплата прошла?'");
            bot.HandleRequest(new SupportRequest(SupportRequestType.Billing));

            //Жалоба директору
            Console.WriteLine("\nЗапрос 3: 'У меня своровали кроссовки из раздевалки! Усильте качество охраны!!!!'");
            bot.HandleRequest(new SupportRequest(SupportRequestType.Complaint));
        }

        static void CheckIterator()
        {
            Console.WriteLine("3. Паттерн Iterator");

            //Создаем расписание
            TrainingSchedule schedule = new TrainingSchedule();

            //Получаем итератор для нашего расписания
            IIterator<string> iterator = schedule.CreateIterator();

            //Проходимся по коллекции, пока есть элементы
            Console.WriteLine("Расписание групповых тренировок на день:");
            while (iterator.HasNext())
            {
                string workout = iterator.Next();
                Console.WriteLine($" - {workout}");
            }
        }
    }

    //ПАТТЕРН STRATEGY
    //Интерфейс стратегия. Любой способ оплаты обязан иметь метод Pay
    public interface IPaymentStrategy
    {
        void Pay(double amount);
    }

    //Стратегия оплаты картой
    public class CardPayment : IPaymentStrategy
    {
        public void Pay(double amount) => Console.WriteLine($"Оплата картой на сумму {amount} руб. прошла успешно");
    }

    //Стратегия оплаты наличными
    public class CashPayment : IPaymentStrategy
    {
        public void Pay(double amount) => Console.WriteLine($"Оплата наличными на сумму {amount} руб. принята в кассу");
    }

    //Контекст использует стратегию, не зная её конкретной реализации
    public class PaymentContext
    {
        private IPaymentStrategy _strategy; //Переменная будет хранить ссылку на текущий переданный способ оплаты

        public void SetStrategy(IPaymentStrategy strategy)
        {
            _strategy = strategy;
        }

        //Выполняем оплату, используя сохраненный способ
        public void ProcessPayment(double amount)
        {
            _strategy.Pay(amount);
        }
    }



    //ПАТТЕРН CHAIN OF RESPONSIBILITY

    //Перечисление enum типов вопросов
    public enum SupportRequestType
    {
        Simple,   //Простые вопросы
        Billing,  //Вопросы по деньгам и абонементам
        Complaint //Жалобы
    }

    //Класс запроса. Хранит в себе тип запроса
    public class SupportRequest
    {
        public SupportRequestType Type { get; set; }

        public SupportRequest(SupportRequestType type)
        {
            Type = type;
        }
    }

    //Базовый шаблон-обработчик
    public abstract class SupportHandler
    {
        //Ссылка на следующего сотрудника в цепочке
        protected SupportHandler _nextHandler;

        //Метод передачи задачи следующему обработчику
        public void SetNextHandler(SupportHandler nextHandler)
        {
            _nextHandler = nextHandler;
        }

        public abstract void HandleRequest(SupportRequest request); //Абстрактный метод, которй должен реализоваться в наследниках
    }


    //Конкретный обработчик 1: Бот для обработки простых вопросов
    public class BotHandler : SupportHandler
    {
        public override void HandleRequest(SupportRequest request)
        {
            //Проверка типа вопроса
            if (request.Type == SupportRequestType.Simple)
            {
                Console.WriteLine("Бот: Здравствуйте! Вот ссылка на наше расписание и цены.");
            }
            //Передача запроса
            else if (_nextHandler != null)
            {
                Console.WriteLine("Бот: Это сложный вопрос, соединяю с менеджером...");
                _nextHandler.HandleRequest(request);
            }
        }
    }

    //Конкретный обработчик 2: Менеджер для обработки вопросов о деньгах и абонементах
    public class ManagerSupportHandler : SupportHandler
    {
        public override void HandleRequest(SupportRequest request)
        {
            if (request.Type == SupportRequestType.Billing)
            {
                Console.WriteLine("Менеджер: Да, ваш абонемент активен. Оплата прошла успешно.");
            }
            else if (_nextHandler != null)
            {
                Console.WriteLine("Менеджер: Это вне моей компетенции, перевожу на директора...");
                _nextHandler.HandleRequest(request);
            }
        }
    }

    //Конкретный обработчик 3: Директор для обработки жалоб
    public class DirectorSupportHandler : SupportHandler
    {
        public override void HandleRequest(SupportRequest request)
        {
            if (request.Type == SupportRequestType.Complaint)
            {
                Console.WriteLine("Директор: Приношу извинения за неудобства. Мы обязательно разберемся и накажем виновных.");
            }
            else if (_nextHandler != null)
            {
                _nextHandler.HandleRequest(request);
            }
        }
    }


    //ПАТТЕРН ITERATOR

    //Интерфейс итератора 
    public interface IIterator<T> //<T> означает, что итератор может работать с любым типом данных (у нас строки)
    {
        bool HasNext();
        //Возвращает текущий элемент типа T и сдвигает указатель на следующий
        T Next();
    }
    //Интерфейс расписания
    public interface ITrainingCollection
    {
        IIterator<string> CreateIterator(); //Должна уметь создавать итератор через метод
    }

    //Коллекция: расписание тренировок
    public class TrainingSchedule : ITrainingCollection
    {
        private string[] _workouts;

        public TrainingSchedule()
        {
            _workouts = new string[] { "Йога (10:00)", "Бокс (12:00)", "Кроссфит (18:00)", "Пилатес (20:00)" };
        }

        //Создает и возвращает итератор, через который клиент будет перебирать элементы расписания
        public IIterator<string> CreateIterator()
        {
            return new ScheduleIterator(_workouts);
        }
        //Внутренний класс, который реализует логику перебора элементов. Скрыт внутри расписания
        private class ScheduleIterator : IIterator<string>
        {
            private string[] _items;
            private int _position; //Курсор, который хранит номер текущего элемента в массиве

            public ScheduleIterator(string[] items)
            {
                _items = items;
                _position = 0;
            }

            //Проверяет, остались ли в массиве неперебранные элементы
            public bool HasNext()
            {
                return _position < _items.Length;
            }
            //Возвращает текущий элемент и переключает курсор на следующий 
            public string Next()
            {
                if (this.HasNext())
                {
                    return _items[_position++];
                }
                throw new IndexOutOfRangeException("Элементов больше нет"); //Если элементов больше нет - выдает ошибку
            }
        }
    }
}
