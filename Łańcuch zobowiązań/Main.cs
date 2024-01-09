using System;
using System.Collections.Generic;

namespace LancuchZobowiazan
{
    public interface IHandler
    {
      IHandler SetNext(IHandler handler);
      void Handle(string request);
    }

    public abstract class AbstractHandler : IHandler
    {
        private IHandler _nextHandler;

        public IHandler SetNext(IHandler handler)
        {
            _nextHandler = handler;
            return handler;
        }

        public virtual void Handle(string request)
        {
          if (request == null || _nextHandler == null)
          {
              Console.WriteLine($"Nikt nie chce tego zjeść: {request}");
              return;
          }
          _nextHandler.Handle(request);            
        }
    }

    public class MonkeyHandler : AbstractHandler
    {
        public override void Handle(string request)
        {
            if (request == "banan")
            {
              Console.WriteLine($"Małpa zjada {request}.");
            }
            else
            {
                base.Handle(request);
            }
        }
    }

  public class DogHandler : AbstractHandler
  {
      public override void Handle(string request)
      {
          if (request == "plasterek szynki" || request == "mięso")
          {
            Console.WriteLine($"Pies zjada {request}.");
          }
          else
          {
              base.Handle(request);
          }
      }
  }

  public class CatHandler : AbstractHandler
  {
      public override void Handle(string request)
      {
          if (request == "mięso")
          {
            Console.WriteLine($"Kot zjada {request}.");
          }
          else
          {
              base.Handle(request);
          }
      }
  }

    public class SquirrelHandler : AbstractHandler
    {
        public override void Handle(string request)
        {
            if (request == "orzech")
            {
                Console.WriteLine($"Wiewiórka zjada {request}.");
            }
            else
            {
               base.Handle(request);
            }
        }
    }



    public class Client
    {
        public static void ClientCode(AbstractHandler handler)
        {
          List<string> products = new List<string>{
              "orzech",
              "banan",
              "mięso",
              "plasterek szynki",
              "lody"            
          };
          
          foreach (var food in products)
          {
              Console.WriteLine($"Kto chce {food}?");
              handler.Handle(food);
          }          
        }
    }

    public class Program
    {
        static void Main(string[] args)
        {
            AbstractHandler monkey = new MonkeyHandler();
            AbstractHandler squirrel = new SquirrelHandler();
            AbstractHandler cat = new CatHandler();
            AbstractHandler dog = new DogHandler();

            monkey.SetNext(dog); 
            dog.SetNext(squirrel);
            squirrel.SetNext(cat);

          Console.WriteLine("Łańcuch: Małpa > Pies > Wiewiórka > Kot");
          Client.ClientCode(monkey);

          Console.WriteLine();

          Console.WriteLine("Podzbiór łańcucha: Wiewiórka > Kot");
          Client.ClientCode(squirrel);

        }
    }
}