using System;
using System.Collections.Generic;

namespace ATMSimulator {
  public class Singleton {
    private static Singleton _instance;
    private Dictionary<string, decimal> _userBalances;

    private Singleton() {
      _userBalances = new Dictionary<string, decimal> {
        { "user1", 1000 },
        { "user2", 500 },
        { "user3", 2500 }
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

    public decimal GetBalance(string userName) {
      if (_userBalances.ContainsKey(userName)) {
        return _userBalances[userName];
      }

      Console.WriteLine("Пользователь не найден!");
      return 0;
    }

    public bool Deposit(string userName, decimal depositAmount) {
      decimal minimumValidAmount = 0;

      if (!_userBalances.ContainsKey(userName)) {
        Console.WriteLine("Пользователь не найден!");
        return false;
      }

      if (depositAmount <= minimumValidAmount) {
        Console.WriteLine("Сумма должна быть больше 0!");
        return false;
      }

      _userBalances[userName] += depositAmount;
      Console.WriteLine($"Внесено {depositAmount} руб. Баланс: {_userBalances[userName]} руб.");
      return true;
    }

    public bool Withdraw(string userName, decimal withdrawAmount) {
      decimal minimumValidAmount = 0;
      decimal currentBalance = _userBalances[userName];

      if (!_userBalances.ContainsKey(userName)) {
        Console.WriteLine("Пользователь не найден!");
        return false;
      }

      if (withdrawAmount <= minimumValidAmount) {
        Console.WriteLine("Сумма должна быть больше 0!");
        return false;
      }

      if (currentBalance < withdrawAmount) {
        Console.WriteLine("Недостаточно средств!");
        return false;
      }

      _userBalances[userName] -= withdrawAmount;
      Console.WriteLine($"Снято {withdrawAmount} руб. Баланс: {_userBalances[userName]} руб.");
      return true;
    }

    public bool Transfer(string fromUserName, string toUserName, decimal transferAmount) {
      decimal minimumValidAmount = 0;

      if (!_userBalances.ContainsKey(fromUserName)) {
        Console.WriteLine("Отправитель не найден!");
        return false;
      }

      if (!_userBalances.ContainsKey(toUserName)) {
        Console.WriteLine("Получатель не найден!");
        return false;
      }

      if (transferAmount <= minimumValidAmount) {
        Console.WriteLine("Сумма должна быть больше 0!");
        return false;
      }

      if (_userBalances[fromUserName] < transferAmount) {
        Console.WriteLine("Недостаточно средств для перевода!");
        return false;
      }

      _userBalances[fromUserName] -= transferAmount;
      _userBalances[toUserName] += transferAmount;
      Console.WriteLine($"Переведено {transferAmount} руб. от {fromUserName} к {toUserName}");
      return true;
    }

    public List<string> GetAllUsers() {
      return new List<string>(_userBalances.Keys);
    }
  }
}