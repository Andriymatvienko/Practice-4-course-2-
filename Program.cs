class Child
{
    public int Number { get; }
    public Child ( int number)
    {
        Number = number;
    }

}

class PlaySchool
{
    public delegate void NotPlacesHandler();
    public event NotPlacesHandler NotPlaces;

    private int _places;

    public PlaySchool(int places)
    {
        _places = places;
    }

    public void PushChild (Child child)
    {
        if (_places > 0)
        {
            _places--;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Ребенок {child.Number} зачислен");
            Console.ResetColor();
        }
        else
        {
            NotPlaces?.Invoke();
        }
    }

    public void ShowPlaces()
    {
        Console.WriteLine($"Количество мест в детском саду:{_places}");
    }
}

class Managerees {
    public delegate void ZapysHandler();
    public event ZapysHandler Zapys;

    public void Queue()
    {
        Console.ForegroundColor= ConsoleColor.Red;
        Console.WriteLine("Заведующая:Мест нет! Предлагаю встать в очередь");
        Console.ResetColor();
        Zapys?.Invoke();
    }
}

class Department
{
    public void Place()
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("Районо :Остальные дети записываются в очередь");
        Console.ResetColor ();  
    }
}

class program
{
    static void Main(string[] args)
    {
        var playSchool = new PlaySchool(6);
        var manageress = new Managerees();
        var department = new Department();

        playSchool.NotPlaces += manageress.Queue;
        manageress.Zapys += department.Place;
        playSchool.ShowPlaces();
        for (int i = 1; i<=7; i++)
        {
            var child = new Child(i);
            playSchool.PushChild(child);
        }

        Console.ReadKey();
    }
}