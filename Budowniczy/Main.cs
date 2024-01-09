using System;

public class Computer
{
  private string _type;
  public string MotherBoard { get; set; }  
  public double Price { get; set; }
  public string Screen { get; set;}
  public string Disc { get; set;}
  public string Processor { get; set;}

  public Computer(string computerType)
  {
      _type = computerType;
      Price = 0;
  }

  public void DisplayConfiguration()
  {
    Console.WriteLine("Typ: " + _type);
    Console.WriteLine("Płyta główna: " + MotherBoard);
    Console.WriteLine("Procesor: " + Processor);
    Console.WriteLine("Dysk: " + Disc);
    Console.WriteLine("Monitor: " + Screen);
    Console.WriteLine("Cena: " + String.Format("{0:F2}",Price) + " zł");
    Console.WriteLine();
  }
}


public class ComputerShop
{  
  public void ConstructComputer(ComputerBuilder computerBuilder)
  {
    computerBuilder.BuildScreen();  
    computerBuilder.BuildMotherboard();
    computerBuilder.BuildProcessor();
    computerBuilder.BuildDisc();
  }  
}


public abstract class ComputerBuilder
{
  
  public Computer Computer { get; set; }
  public abstract void BuildScreen();  
  public abstract void BuildMotherboard();  
  public abstract void BuildProcessor();  
  public abstract void BuildDisc();  
}


public class OfficeComputerBuilder : ComputerBuilder
{

  public OfficeComputerBuilder()
  {
    Computer = new Computer("biurowy");
  }
    
  public override void BuildScreen()
  {
    Computer.Screen = "BenQ GW2270H (1920x1080)"; 
    Computer.Price += 369.00;
  }
  public override void BuildMotherboard()
  {
    Computer.MotherBoard = "Asus Prime A320M-E"; 
    Computer.Price += 259.90;
  }
  public override void BuildProcessor()
  {
    Computer.Processor = "AMD Ryzen 5 2600"; 
    Computer.Price += 589.00;
  }
  public override void BuildDisc()
  {
    Computer.Disc = "Goodram CX400 250 GB SATA3"; 
    Computer.Price += 149.99;
  }
}


public class GamingComputerBuilder : ComputerBuilder
{

  public GamingComputerBuilder()
  {
    Computer = new Computer("gamingowy");
  }
    
  public override void BuildScreen()
  {
    Computer.Screen = "HP Z4W65A4 (3840x1600)"; 
    Computer.Price += 4927.75;
  }
  public override void BuildMotherboard()
  {
    Computer.MotherBoard = "Gigabyte X570 Aorus Elite"; 
    Computer.Price += 895.60;
  }
  public override void BuildProcessor()
  {
    Computer.Processor = "Intel i7 9700K"; 
    Computer.Price += 1829.00;
  }
  public override void BuildDisc()
  {
    Computer.Disc = "Samsung 970 EVO Plus 2TB M.2"; 
    Computer.Price += 2028.45;
  }
}


public class ProfessionalComputerBuilder : ComputerBuilder
{

  public ProfessionalComputerBuilder()
  {
    Computer = new Computer("dla profesjonalistów");
  }
    
  public override void BuildScreen()
  {
    Computer.Screen = "Eizo CG319X"; 
    Computer.Price += 20749.00;
  }
  public override void BuildMotherboard()
  {
    Computer.MotherBoard = "Supermicro MBD X11DPH"; 
    Computer.Price += 2755.30;
  }
  public override void BuildProcessor()
  {
    Computer.Processor = "Intel Xeon Gold 5120"; 
    Computer.Price += 7999.00;
  }
  public override void BuildDisc()
  {
    Computer.Disc = "Seagate Skyhawk 14TB 3.5\""; 
    Computer.Price += 2101.75;
  }
}

public class Program
{
  static void Main(string[] args)
  {
    
    ComputerShop computerShop = new ComputerShop();
    
    ComputerBuilder computerOfficeBuilder = new OfficeComputerBuilder();
    computerShop.ConstructComputer(computerOfficeBuilder);
  
    ComputerBuilder computerGamingBuilder = new GamingComputerBuilder();
    computerShop.ConstructComputer(computerGamingBuilder);

    ComputerBuilder computerProfessionalBuilder = new ProfessionalComputerBuilder();
    computerShop.ConstructComputer(computerProfessionalBuilder);

    computerOfficeBuilder.Computer.DisplayConfiguration();
    computerGamingBuilder.Computer.DisplayConfiguration();
    computerProfessionalBuilder.Computer.DisplayConfiguration();

  }
} 