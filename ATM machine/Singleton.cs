using System;
using System.Collections.Generic;
using System.Linq;

namespace ATMSimulator {
  public class Singleton {
    private static Singleton _instance;
    private readonly List<User> _users;
    private User _currentUser;
    private readonly decimal _minimumValidAmount = 0;

    private Singleton() {
      _users = new List<User> {
        new User("Павел", "6767", 67000),
        new User("Кирилл", "5252", 5200),
        new User("Никита", "4242", 4200)
      };
    }

    public static Singleton Instance {
      get {
        if (_instance == null) {
          _instance = new Singleton();
        }

        return _instance;
      }
    }

    public bool Login(string userName, string pinCode) {
      _currentUser = _users.FirstOrDefault(u => string.Equals(u.Name, userName, StringComparison.OrdinalIgnoreCase) && u.PinCode == pinCode);
      
      if (_currentUser != null) {
        Console.WriteLine($"Добро пожаловать, {_currentUser.Name}!");
        Console.WriteLine($"Ваш текущий баланс: {_currentUser.Balance} руб.\n");
        return true;
      }
      
      Console.WriteLine("Неверный PIN-код!");
      return false;
    }

    private bool IsUserLoggedIn() {
      if (_currentUser == null) {
        Console.WriteLine("Вы не вошли в систему");
        return false;
      }
      return true;
    }

    private bool IsAmountValid(decimal amount) {
      if (amount <= _minimumValidAmount) {
        Console.WriteLine("Сумма должна быть больше 0!");
        return false;
      }
      return true;
    }

    public void ShowBalance() {
      if (!IsUserLoggedIn()) return;
      Console.WriteLine($"\nВаш баланс: {_currentUser.Balance} руб.");
    }

    public void Deposit() {
      if (!IsUserLoggedIn()) return;

      Console.WriteLine($"\nТекущий баланс: {_currentUser.Balance} руб.");
      Console.Write("Введите сумму для внесения: ");
      
      bool isAmountValid = decimal.TryParse(Console.ReadLine(), out decimal depositAmount);
      
      if (!isAmountValid) {
        Console.WriteLine("Неверный формат суммы!");
        return;
      }

      if (!IsAmountValid(depositAmount)) return;

      _currentUser.Balance += depositAmount;
      Console.WriteLine($"Внесено {depositAmount} руб. Новый баланс: {_currentUser.Balance} руб.");
    }

    public void Withdraw() {
      if (!IsUserLoggedIn()) return;

      Console.WriteLine($"\nТекущий баланс: {_currentUser.Balance} руб.");
      Console.Write("Введите сумму для снятия: ");
      
      bool isAmountValid = decimal.TryParse(Console.ReadLine(), out decimal withdrawAmount);
      
      if (!isAmountValid) {
        Console.WriteLine("Неверный формат суммы!");
        return;
      }

      if (!IsAmountValid(withdrawAmount)) return;

      if (withdrawAmount > _currentUser.Balance) {
        Console.WriteLine($"Недостаточно средств! Доступно: {_currentUser.Balance} руб.");
        return;
      }

      _currentUser.Balance -= withdrawAmount;
      Console.WriteLine($"Снято {withdrawAmount} руб. Остаток: {_currentUser.Balance} руб.");
    }

    public void Transfer() {
      if (!IsUserLoggedIn()) return;

      Console.WriteLine($"\nТекущий баланс: {_currentUser.Balance} руб.");
      Console.WriteLine("\n--- Список пользователей для перевода ---");
      
      var allUsers = GetAllUsers();
      int userNumber = 1;
      foreach (string userName in allUsers) {
        if (!string.Equals(userName, _currentUser.Name, StringComparison.OrdinalIgnoreCase)) {
          Console.WriteLine($"{userNumber}. {userName}");
        }
        userNumber++;
      }

      Console.Write("Введите имя получателя: ");
      string targetUserName = Console.ReadLine();

      User targetUser = _users.FirstOrDefault(u => string.Equals(u.Name, targetUserName, StringComparison.OrdinalIgnoreCase));

      if (targetUser == null) {
        Console.WriteLine("Пользователь не найден!");
        return;
      }

      if (string.Equals(targetUser.Name, _currentUser.Name, StringComparison.OrdinalIgnoreCase)) {
        Console.WriteLine("Нельзя перевести деньги самому себе!");
        return;
      }

      Console.Write("Введите сумму перевода: ");
      bool isAmountValid = decimal.TryParse(Console.ReadLine(), out decimal transferAmount);
      
      if (!isAmountValid) {
        Console.WriteLine("Неверный формат суммы!");
        return;
      }

      if (!IsAmountValid(transferAmount)) return;

      if (transferAmount > _currentUser.Balance) {
        Console.WriteLine($"Недостаточно средств! Доступно: {_currentUser.Balance} руб.");
        return;
      }

      _currentUser.Balance -= transferAmount;
      targetUser.Balance += transferAmount;
      Console.WriteLine($"\nПереведено {transferAmount} руб. пользователю {targetUser.Name}");
      Console.WriteLine($"Ваш новый баланс: {_currentUser.Balance} руб.");
    }

    public void Logout() {
      _currentUser = null;
      Console.WriteLine("Вы вышли из системы.");
    }

    public List<string> GetAllUsers() {
      return _users.Select(u => u.Name).ToList();
    }
  }
}