using System;

namespace ATMSimulator {
  class Program {
    static void Main() {
      string menuOptionExit = "5";
      int maxLoginAttempts = 3;

      Console.WriteLine("=== БАНКОМАТ ===\n");

      Singleton atm = Singleton.Instance;

      bool isLoggedIn = false;
      int loginAttempts = 0;

      while (!isLoggedIn && loginAttempts < maxLoginAttempts) {
        Console.WriteLine("--- Доступные пользователи ---");
        var users = atm.GetAllUsers();
        int userIndex = 1;
        foreach (string userName in users) {
          Console.WriteLine($"{userIndex}. {userName}");
          userIndex++;
        }

        Console.Write("\nВыберите пользователя (введите имя): ");
        string selectedUserName = Console.ReadLine();

        Console.Write("Введите PIN-код: ");
        string enteredPin = Console.ReadLine();

        if (atm.Login(selectedUserName, enteredPin)) {
          isLoggedIn = true;
        } else {
          loginAttempts++;
          int remainingAttempts = maxLoginAttempts - loginAttempts;
          if (remainingAttempts > 0) {
            Console.WriteLine($"\nОсталось попыток: {remainingAttempts}\n");
          }
        }
      }

      if (!isLoggedIn) {
        Console.WriteLine("\nПревышено количество попыток входа!");
        Console.WriteLine("Нажмите Enter для выхода...");
        Console.ReadKey();
        return;
      }

      while (true) {
        Console.WriteLine("\n1-Баланс \n2-Внести \n3-Снять \n4-Перевести \n5-Выйти");
        Console.Write("Выбор: ");
        string userChoice = Console.ReadLine();

        if (userChoice == menuOptionExit) {
          break;
        }

        switch (userChoice) {
          case "1":
            atm.ShowBalance();
            break;
          case "2":
            atm.Deposit();
            break;
          case "3":
            atm.Withdraw();
            break;
          case "4":
            atm.Transfer();
            break;
          default:
            Console.WriteLine("Неверный выбор! Выберите 1-5");
            break;
        }
      }

      atm.Logout();
      Console.WriteLine("Досвидания!");
      Console.ReadKey();
    }
  }
}