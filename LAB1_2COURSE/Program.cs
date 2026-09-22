using BusinessLogical;
internal class Program
{
    static void Main(string[] args)
    {
        Logic logic = new Logic();
        Console.WriteLine("Введите ID работника: ");
        int id = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Введите имя работника: ");
        string name = Console.ReadLine();
        Console.WriteLine("Введите отдел работника: ");
        string departament = Console.ReadLine();
        Console.WriteLine("Введите зарплату работника: ");
        decimal salary = Convert.ToDecimal(Console.ReadLine());
        logic.AddWorker(id, name, departament, salary);
    }
}