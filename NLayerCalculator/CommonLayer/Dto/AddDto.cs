namespace Calculator.CommonLayer.Dto
{
    public class AddDto
    {
        public double Number1 { get; set; }
        public double Number2 { get; set; }
        public  string Operation { get; set; }    //Hesap makinesinde kullanacağımız değişkenler

        public double Result { get; set; }
    }
}
