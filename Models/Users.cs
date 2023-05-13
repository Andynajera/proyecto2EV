namespace Classes;

//Clase
 public class User{
    
//Propiedades

   public int id {get;set;}
    public string? name {get;set;}
    public string? password {get;set;}
    public string? email {get;set;}
    //public string? password {get;set;}
    public bool gender {get;set;}
    public DateTime  dateInscription {get;set;}
    public decimal puntosAcumulados {get;set;}
    public virtual ICollection<Promocion>? Promociones{get;set;}


 /*   
//Contructor
    //si no incicializas en el contructor las propiedades no aparecera por consola
    public User(string name,string email,string nameDegree, bool gender,DateTime birth, decimal height,int brothers){
        this.name=name;
        this.email=email;
        this.nameDegree=nameDegree;
        this.gender=gender;
        this.birth=birth;
        this.height=height;
        this.brothers=brothers;
    }
    */
//Cambia de genero 


//Resumen de todos los parametros
//Cambia las propiedades a string con el toString
    public string Summary(){
 return  dateInscription.ToString("MM/dd/yyyy")+" ";    }
 }


 