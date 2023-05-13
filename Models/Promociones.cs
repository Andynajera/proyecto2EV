namespace Classes;

    //Clase
 public class Promocion{
    //Propiedades
     public int id {get;set;}
    public string name { get;set; }
    public string description { get;set; }
    public bool vigor { get;set; }
    public DateTime  date { get;set; }
    public decimal price { get;set; }

    public decimal descuento { get;set; }
    public int cantidadPersonas { get;set; }
    public User? user { get;set; }
    
/*
//Contructor
    //si no incicializas en el contructor las propiedades no aparecera por consola
    public Degree(string name,string nameDegree, bool quedanPlazas,DateTime dataExpediente, decimal price,int cantidadPlazas){
        this.name=name;
        this.nameDegree=nameDegree;
        this.quedanPlazas=quedanPlazas;
        this.dataExpediente=dataExpediente;
        this.price=price;
        this.cantidadPlazas=cantidadPlazas;
    }*/

    //Cambia si hay plazas 
    public void ChangeGender(){
        vigor=!vigor;
    }
//Resumen de todos los parametros
//Cambia las propiedades a string con el toString
    public string Summary(){
        return name+" "+description+" "+vigor.ToString()+" "+date.ToString()+" "+price.ToString()+" "+ vigor.ToString()+" "+ descuento.ToString()+" "+cantidadPersonas.ToString() ;
    }
 }


 