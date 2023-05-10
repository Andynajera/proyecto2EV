namespace Classes;

//Clase
public class TipoGasoleo{

//propiedades


    public int id {get;set;}
    public int number {get;set;}

    public string name{set;get;}
    public decimal price { get;set; }
    public bool contamina { get;set; }
    public DateTime date { get;set; }
    public string description { get;set; }



public string Summary(){
        return  date.ToString("MM/dd/yyyy")+" ";
    }
}