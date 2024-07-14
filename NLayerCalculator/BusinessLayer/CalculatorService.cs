using Calculator.CommonLayer.Dto;

namespace Calculator.Business
{
    public class CalculatorService
    {
        public Result Add(AddDto addDto)
        {
            return new Result
            {
                Value = addDto.Number1 + addDto.Number2   //Toplama İşlemi fonksiyonu
            };
        }

        public Result Subtract(AddDto addDto)
        {
            return new Result
            {
                Value = addDto.Number1 - addDto.Number2  //Çıkartma
            };
        }

        public Result Multiply(AddDto addDto)
        {
            return new Result
            {
                Value = addDto.Number1 * addDto.Number2  //Çarpma
            };
        }

        public Result Divide(AddDto addDto)
        {
            if (addDto.Number2 == 0)
            {
                throw new DivideByZeroException("Number2 cannot be zero.");  //Bölme işlemi ayrıca 2.sayı 0 olamaz
            }

            return new Result
            {
                Value = addDto.Number1 / addDto.Number2
            };
        }
    }
}
