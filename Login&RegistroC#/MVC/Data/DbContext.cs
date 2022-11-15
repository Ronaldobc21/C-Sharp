namespace MVC.Data
{
    public class DbContext //Esto es un metodo 
    {
        public DbContext(string valor) => Valor = valor;//tendra un inicializador publico 
        //el string valor es el parametro
        //Hace referencia a otro parametro 

        public string Valor { get; }

    }
}