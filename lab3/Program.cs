namespace tp_lab3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //CheckSingleton();
            //CheckFactory();
            //CheckAbstractFactory();
            //CheckBuilder();

        }
        //Методы проверки
        static void CheckSingleton()
        {
            Console.WriteLine("1. Паттерн Singleton");
            //Первый администратор продал обычный абонемент (через один терминал)
            Cashier cashier1 = Cashier.GetInstance(); //Вызываем метод у самого класса (экземпляр касса создается, тк обращаемся впервые)
            cashier1.AddRevenue(2000);

            //Второй администратор продал вип-абонемент (через другой терминал)
            Cashier cashier2 = Cashier.GetInstance(); //Пытаемся сделать это второй раз,
                                                      //но переменная уже не пуста -> новый объект не создастся
            cashier2.AddRevenue(5000);

            //Проверяем общую выручку через первую переменную
            Console.WriteLine($"Общая выручка: {cashier1.TotalRevenue} руб.");
        }
        static void CheckFactory()
        {
            Console.WriteLine("2. Паттерн Factory");

            //Клиент выбирает йогу 
            WorkoutFactory yogaFactory = new YogaFactory();
            yogaFactory.StartWorkout(); //Фабрика сама создаст тренировку Хатха-Йога и запустит ее

            //Клиент выбирает бокс
            WorkoutFactory boxingFactory = new BoxingFactory();
            boxingFactory.StartWorkout(); //Фабрика сама создаст тренировку Тайский бокс и запустит ее
        }

        static void CheckAbstractFactory()
        {
            Console.WriteLine("3. Паттерн Abstract Factory");

            //Клиент создает фабрику и просит у нее продукты
            IGymFactory sFactory = new StandardFactory();
            Console.WriteLine($"Тип абонемента: {sFactory.CreateMembership().Type}  Доступ: {sFactory.CreateKey().AccessLevel}");

            IGymFactory pFactory = new PremiumFactory();
            Console.WriteLine($"Тип абонемента: {pFactory.CreateMembership().Type}  Доступ: {pFactory.CreateKey().AccessLevel}");
        }

        static void CheckBuilder()
        {
            Console.WriteLine("4. Паттерн Builder");

            //Создаем строителя для похудения
            ITrainingBuilder weightLossBuilder = new WeightLossBuilder();
            ProgramDirector director1 = new ProgramDirector(weightLossBuilder);

            //Программа строится
            director1.ConstructProgram();

            //Результат
            TrainingProgram weightLossProgram = weightLossBuilder.GetResult();
            Console.WriteLine(weightLossProgram);

            //Теперь то же самое, но для набора массы
            ITrainingBuilder muscleGainBuilder = new MuscleGainBuilder();
            ProgramDirector director2 = new ProgramDirector(muscleGainBuilder);
            director2.ConstructProgram();

            //Получаем вторую программу
            TrainingProgram muscleGainProgram = muscleGainBuilder.GetResult();
            Console.WriteLine(muscleGainProgram);
        }
    }
    //ПАТТЕРН SINGLETONE

    //Класс Касса
    public class Cashier
    {
        // Приватное статическое поле для хранения единственного экземпляра
        private static Cashier _instance;

        //Приватный конструктор, чтобы предотвратить создание второй кассы извне 
        private Cashier()
        {
            TotalRevenue = 0; //В начале выручка равна 0
        }

        public double TotalRevenue { get; private set; }

        //Статический метод для получения единственного объекта Касса
        public static Cashier GetInstance()
        {
            if (_instance == null) //Если переменная еще не хранит объект, создаем его
            {
                _instance = new Cashier();
            }
            return _instance;
        }

        //Метод для добавления денег в кассу
        public void AddRevenue(double amount)
        {
            TotalRevenue += amount;
        }
    }



    //ПАТТЕРН FACTORY

    //Интерфейс тренировки
    public interface IWorkout
    {
        string Name { get; } //Должно быть название
        void Conduct(); //Должен быть реализован метод проведения
    }

    //Конкретный продукт: йога
    public class YogaWorkout : IWorkout //реализует интерфейс
    {
        public string Name => "Хатха-йога";
        public void Conduct() => Console.WriteLine($"Проводим тренировку {Name}. Упражнения: Тадасана, Врикшасана, Уткатасана, Марджариасана");
    }

    //Конкретный продукт: бокс
    public class BoxingWorkout : IWorkout
    {
        public string Name => "Тайский бокс";
        public void Conduct() => Console.WriteLine($"Проводим тренировку {Name}. Упражнения: Джеб, Кросс, Хук, Апперкот, Лоу-кик");
    }

    //Абстрактный класс Создатель (фабрика)
    public abstract class WorkoutFactory
    {
        //Фабричный метод
        //Реализация ложится на конкретные фабрики (наследников).
        public abstract IWorkout CreateWorkout();

        //Метод, использующий фабричный метод
        public void StartWorkout()
        {
            IWorkout workout = CreateWorkout(); //Вызываем фабричный метод (создается конкретный объект)
            workout.Conduct(); //Вызываем метод у созданного объекта
        }
    }

    //Конкретный создатель: фабрика для йоги
    public class YogaFactory : WorkoutFactory
    {
        public override IWorkout CreateWorkout() => new YogaWorkout(); //Переопределяем метод, чтобы создавал объект йога
    }

    //Конкретный создатель: фабрика для бокса
    public class BoxingFactory : WorkoutFactory
    {
        public override IWorkout CreateWorkout() => new BoxingWorkout(); //Переопределяем метод, чтобы создавал объект бокс
    }



    //ПАТТЕРН ABSTRACT FACTORY

    //Вид абонемента
    public interface IMembership
    {
        string Type { get; }
        double Price { get; }
    }

    public class StandardMembership : IMembership
    {
        public string Type => "Стандартный";
        public double Price => 3000;
    }

    public class PremiumMembership : IMembership
    {
        public string Type => "Премиум";
        public double Price => 6000;
    }

    //Ключ-карта
    public interface IAccessKey
    {
        string AccessLevel { get; }
    }

    public class StandardKey : IAccessKey
    {
        public string AccessLevel => "Тренажерный зал, групповые тренировки";
    }

    public class PremiumKey : IAccessKey
    {
        public string AccessLevel => "Тренажерный зал, бассейн, спа-зона, групповые тренировки";
    }

    //Интерфейс абстрактной фабрики
    public interface IGymFactory
    {
        IMembership CreateMembership();
        IAccessKey CreateKey();
    }

    //Конкретная фабрика: тариф Стандартный 
    public class StandardFactory : IGymFactory
    {
        public IMembership CreateMembership() => new StandardMembership();
        public IAccessKey CreateKey() => new StandardKey();
    }

    //Конкретная фабрика: тариф Премиум 
    public class PremiumFactory : IGymFactory
    {
        public IMembership CreateMembership() => new PremiumMembership();
        public IAccessKey CreateKey() => new PremiumKey();
    }



    //ПАТТЕРН BUILDER

    //Программа тренировок
    public class TrainingProgram
    {
        //свойства
        public string WarmUp { get; set; }
        public string MainPart { get; set; }
        public string CoolDown { get; set; }

        public override string ToString()
        {
            return $"Программа тренировок:\n - Разминка: {WarmUp}\n - Основа: {MainPart}\n - Заминка: {CoolDown}";
        }
    }

    //Интерфейс бмлдера
    public interface ITrainingBuilder
    {
        void BuildWarmUp();
        void BuildMainPart();
        void BuildCoolDown();
        TrainingProgram GetResult();
    }

    //Конкретные строители

    //Строитель 1: Программа для похудения
    public class WeightLossBuilder : ITrainingBuilder
    {
        private TrainingProgram _program = new TrainingProgram();

        public void BuildWarmUp() => _program.WarmUp = "Ходьба по дорожке с наклоном 5 (15 мин)";
        public void BuildMainPart() => _program.MainPart = "Интервальное кардио + круговая тренировка";
        public void BuildCoolDown() => _program.CoolDown = "Легкий пилатес + растяжка всего тела";

        public TrainingProgram GetResult() => _program;
    }

    //Строитель 2: Программа для массонабора и рельефа
    public class MuscleGainBuilder : ITrainingBuilder
    {
        private TrainingProgram _program = new TrainingProgram();

        public void BuildWarmUp() => _program.WarmUp = "Суставная гимнастика (10 мин)";
        public void BuildMainPart() => _program.MainPart = "Базовые силовые упражнения";
        public void BuildCoolDown() => _program.CoolDown = "Упрежнения на кор + качественная растяжка";

        public TrainingProgram GetResult() => _program;
    }

    //Определяет порядок этапов тренировки
    public class ProgramDirector
    {
        private ITrainingBuilder _builder;

        public ProgramDirector(ITrainingBuilder builder)
        {
            _builder = builder; //выбор нужного строителя
        }
        //Порядок
        public void ConstructProgram()
        {
            _builder.BuildWarmUp();
            _builder.BuildMainPart();
            _builder.BuildCoolDown();
        }
    }
}


 
