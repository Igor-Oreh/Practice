// dotnet new console -n BankConsoleApp - создаем приложение
// dotnet build --configuration Release - сборка приложения(Предварительно cd BankConsoleApp)
// dotnet run --project ./BankConsoleApp - запуск приложения
using System;
class BankAccount
{
    public int AccountNumber { get; }
    public int Balance { get; private set; }
    private static int _nextAccountNumber = 1;
    public BankAccount()
    {
        AccountNumber = _nextAccountNumber++;
        Balance = 0;
    }


    public void Deposit(int cents)
    {
        if (cents < 0)
            throw new ArgumentOutOfRangeException(nameof(cents), "На счет нельзя положить отрицательную сумму");
        Balance += cents;
    }

    public void Withdraw(int cents)
    {
        if (cents < 0)
            throw new ArgumentOutOfRangeException(nameof(cents), "Нельзя снять отрицательную сумму");
        if (cents > Balance)
            throw new ArgumentOutOfRangeException(nameof(cents), "Вы не можете снять больше, чем имеете на счете");
        Balance -= cents;
    }

}

class Program
{
    static void Main(string[] args)
    {
        bool run = true;
        var bank = new BankAccount();
        while (run)
        {
            Console.WriteLine("Введите желаемую операцию:\n1)add сумма - положить деньги на счет;\n2)get сумма - снять деньги со счета;\n3)cash - проверить баланс\n4)accnum - узнать номер счета\n5)quit - выйти");
            string choice = Console.ReadLine();
            string[] words = choice.Split(" ");
            if (words.Length == 2)
            {
                if (words[0] == "add")
                {
                    try
                    {
                        float cash = float.Parse(words[1]);
                        bank.Deposit((int)(cash * 100));
                        Console.WriteLine($"Была добавлена на счет сумма: {cash}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Не удалось положить сумму на счет по причине:");
                        Console.WriteLine(ex.Message);
                    }
                }
                else if (words[0] == "get")
                {
                    try
                    {
                        float cash = float.Parse(words[1]);
                        bank.Withdraw((int)(cash * 100));
                        Console.WriteLine($"Была снята со счета сумма: {cash}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Не удалось снять сумму со счета по причине:");
                        Console.WriteLine(ex.Message);
                    }
                }
            }
            else if (words[0] == "cash")
            {
                Console.WriteLine($"Сумма на счете: {(float)bank.Balance / 100}");
            }
            else if (words[0] == "accnum")
            {
                Console.WriteLine($"Номер счета: {bank.AccountNumber}");
            }
            else if (words[0] == "quit")
            {
                run = false;
                Console.WriteLine("Goodbye world!");
            }
            else
            {
                Console.WriteLine("Ошибка в написании команды");
            }
        }
    }
}