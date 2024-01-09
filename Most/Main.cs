using System;


public interface ITelewizor {
  
  int Kanal { get; set; }
  void Wlacz();
  void Wylacz();
	void ZmienKanal(int kanal);
	
}



public class TvLg : ITelewizor {
  
  public TvLg(){
	  this.Kanal = 1;
  }
  
  public int Kanal { get; set; }
  
	public void Wlacz() {
		Console.WriteLine("Telewizor LG - włączam się.");
	}
 
	public void Wylacz() {
    Console.WriteLine("Telewizor LG - wyłączam się.");
	}
 
	public void ZmienKanal(int kanal) {
    Console.WriteLine($"Telewizor LG - zmieniam kanał: {kanal}");
    this.Kanal = kanal;
	}
	
}




public abstract class PilotAbstrakcyjny {
  
	private ITelewizor tv;
 
	public PilotAbstrakcyjny(ITelewizor tv){
    this.tv = tv;
	}
 
	public void ZmienKanal(int kanal){
    tv.ZmienKanal(kanal);
	}
  
  public void Wlacz() => tv.Wlacz();
  public void Wylacz() => tv.Wylacz();
}



public class PilotHarmony : PilotAbstrakcyjny {
 
	public PilotHarmony(ITelewizor tv) : base(tv) {	}
 
	public void DoWlacz(){
		Console.WriteLine("Pilot Harmony - włącz telewizor...");
		Wlacz();
	}
	
  public void DoWylacz(){
    Console.WriteLine("Pilot Harmony - wyłącz telewizor...");
    Wylacz();
  }

  public void DoZmienKanal(int kanal){
    Console.WriteLine("Pilot Harmony - zmienia kanał...");
    base.ZmienKanal(kanal);
  }
	
}

public class PilotLG : PilotAbstrakcyjny {

  public PilotLG(ITelewizor tv) : base(tv) { }
  
  public void DoWlacz(){
    Console.WriteLine("Pilot LG - włącz telewizor...");
    Wlacz();
  }
	
	public void DoWylacz(){
		Console.WriteLine("Pilot LG - wyłącz telewizor...");
		Wylacz();
	}

  public void DoZmienKanal(int kanal){
    Console.WriteLine("Pilot LG - zmienia kanał...");
    base.ZmienKanal(kanal);
  }
}



class MainClass {
  public static void Main (string[] args) {
    
		ITelewizor tv = new TvLg();
    PilotLG pilotLG = new PilotLG(tv);
    PilotHarmony pilotHarmony = new PilotHarmony(tv);

		
		pilotHarmony.DoWlacz();
    Console.WriteLine();
    
		Console.WriteLine("Sprawdź kanał - bieżący kanał: " + tv.Kanal);
    Console.WriteLine();
    
    pilotLG.DoZmienKanal(100);
    Console.WriteLine();
    
		Console.WriteLine("Sprawdź kanał - bieżący kanał: " + tv.Kanal);
    Console.WriteLine();
    
		pilotHarmony.DoWylacz();
		
  }
}