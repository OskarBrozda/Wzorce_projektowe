using System;
using System.Collections.Generic;

namespace Pamiatka
{

    public interface IMemento
    {
      public int GetYear();
    }

   class Memento : IMemento
   {
       private int year;

       public Memento(int year)
       {
         this.year = year;
       }

       public int GetYear() => year;
    }

    public interface IMovie 
    {
      public void SetYear(int year);
      public IMemento Save();
      public void Restore(IMemento memento);
    }

    class BackToTheFuture : IMovie 
    {
        private int year;

        public BackToTheFuture(int year)
        {
            this.year = year;
            Console.WriteLine("Początkowy rok: " + this.year);
        }

        public void SetYear(int year)
        {
          this.year = year;
          Console.WriteLine("Rok zmieniony na: " + this.year);
        }

        public IMemento Save()
        {
            return new Memento(this.year);
        }

        public void Restore(IMemento memento)
        {
            this.year = memento.GetYear();
        }
    }

    class Caretaker
    {
        private List<IMemento> mementos = new List<IMemento>();

        private IMovie movie;

        public Caretaker(IMovie movie)
        {
            this.movie = movie;
        }

        public void Save()
        {
            IMemento memento = movie.Save();
            mementos.Add(memento);
            Console.WriteLine("Zapisano pamiątkę z roku: " + memento.GetYear());
        }

        public void Undo()
        {
            if(mementos.Count == 0)
          {
            Console.WriteLine("Nie można cofnąć - brak zapisanych danych");
            return;
          }

            var memento = this.mementos[this.mementos.Count - 1];
            mementos.Remove(memento);
            movie.Restore(memento);

            Console.WriteLine("Przywrócony rok: " + memento.GetYear());
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            BackToTheFuture favoriteMovie = new BackToTheFuture(1985);
            Caretaker caretaker = new Caretaker(favoriteMovie);

            caretaker.Undo(); // test ;)

            Console.WriteLine();

            Console.WriteLine("Część I:");
            favoriteMovie.SetYear(1955); 
            caretaker.Save();
            favoriteMovie.SetYear(1985);

            Console.WriteLine();

            Console.WriteLine("Część II:");
            favoriteMovie.SetYear(2015); 
            favoriteMovie.SetYear(1985);
            caretaker.Undo();
            favoriteMovie.SetYear(1985);
            caretaker.Save();

            Console.WriteLine();

            Console.WriteLine("Część III:");      
            favoriteMovie.SetYear(1885);
            caretaker.Undo();
        }
    }
}