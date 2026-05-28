namespace ATMSimulator {
  public class User {
    public string Name { get; set; }
    public string PinCode { get; set; }
    public decimal Balance { get; set; }

    public User(string name, string pinCode, decimal balance) {
      Name = name;
      PinCode = pinCode;
      Balance = balance;
    }
  }
}