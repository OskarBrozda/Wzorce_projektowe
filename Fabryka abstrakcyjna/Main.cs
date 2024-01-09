using System;
using System.Text;


interface ILetters
{
  public string ShowAlfa();
}

interface INums
{
  public string ShowNum();
}

class AlphabetFactory
{  
  private SystemFactory systemFactory;
  public ILetters letters;
  public INums numbers;

  public AlphabetFactory(SystemFactory systemFactory)
  {
    this.systemFactory = systemFactory;
  }

  public void Generate()
  {
    numbers = systemFactory.CreateNum();
    letters = systemFactory.CreateAlfa();
  }
}


abstract class SystemFactory
{
  public abstract ILetters CreateAlfa();
  public abstract INums CreateNum();
}
    

class LacinkaFactory : SystemFactory
{
  public override ILetters CreateAlfa() => new LacinkaLetters();
  public override INums CreateNum() => new LacinkaNumbers();
}

class GrekaFactory : SystemFactory
{
    public override ILetters CreateAlfa() => new GrekaLetters();
    public override INums CreateNum() => new GrekaNumbers();  
}

class CyrylicaFactory : SystemFactory
{
    public override ILetters CreateAlfa() => new CyrylicaLetters();
    public override INums CreateNum() => new CyrylicaNumbers();  
}


class LacinkaLetters : ILetters
{
    string letters;

    public LacinkaLetters()
    {
        letters = "abcde";
    }

    public string ShowAlfa() => letters;
}

class CyrylicaLetters : ILetters
{
    string letters;

    public CyrylicaLetters()
    {
        letters = "абвгд";
    }

    public string ShowAlfa() => letters;
}

class GrekaLetters : ILetters
{
    string letters;

    public GrekaLetters()
    {
        letters = "αβγδε";
    }

    public string ShowAlfa() => letters;
}


class LacinkaNumbers : INums
{
    string numbers;

    public LacinkaNumbers()
    {
        numbers = "I II III";
    }  

    public string ShowNum() => numbers;
}

class CyrylicaNumbers : INums
{
    string numbers;

    public CyrylicaNumbers()
    {
        numbers = "1 2 3";
    } 

    public string ShowNum() => numbers;
}

class GrekaNumbers : INums
{
    string numbers;

    public GrekaNumbers()
    {
        numbers = "αʹ βʹ γʹ";
    }

    public string ShowNum() => numbers;
}


public class Application
{
    public static void Main(String[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;
      
        AlphabetFactory alphabet_lacinka = new AlphabetFactory(new LacinkaFactory());
        alphabet_lacinka.Generate();
        Console.WriteLine(alphabet_lacinka.letters.ShowAlfa() + " " + alphabet_lacinka.numbers.ShowNum());
      
        AlphabetFactory alphabet_cyrylica = new AlphabetFactory(new CyrylicaFactory());
        alphabet_cyrylica.Generate();
        Console.WriteLine(alphabet_cyrylica.letters.ShowAlfa() + " " + alphabet_cyrylica.numbers.ShowNum());
        
        AlphabetFactory alphabet_greka = new AlphabetFactory(new GrekaFactory());
        alphabet_greka.Generate();
        Console.WriteLine(alphabet_greka.letters.ShowAlfa() + " " + alphabet_greka.numbers.ShowNum());
    }
}

  