namespace Calculator.Common.Dto
{
    public class CalculationHistory
    {
        public double Number1 { get; set; }
        public double Number2 { get; set; }      //History için kullanacağımız değişkenler
        public string Operation { get; set; }
        public double Result { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
