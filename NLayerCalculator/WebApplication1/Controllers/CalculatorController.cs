using Microsoft.AspNetCore.Mvc;
using Calculator.Business;
using Calculator.Common.Dto;
using System.Globalization;
using Calculator.CommonLayer.Dto;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace Calculator.Web.Controllers
{
    public class CalculatorController : Controller
    {
        private readonly CalculatorService _calculatorService;

        public CalculatorController()
        {
            _calculatorService = new CalculatorService();
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(new AddDto());  // AddDto'yu view'a gönderir
        }

        [HttpPost]
        public IActionResult Index(AddDto model, string operation)
        {
            if (operation == "clear")
            {
                ModelState.Clear();  // Clear butonunu çalıştıracak olan fonksiyon
                return View(new AddDto());
            }

            try
            {
                model.Number1 = double.Parse(model.Number1.ToString().Replace(",", "."), CultureInfo.InvariantCulture);
                model.Number2 = double.Parse(model.Number2.ToString().Replace(",", "."), CultureInfo.InvariantCulture);
            }
            catch (FormatException)
            {
                ModelState.AddModelError("", "Geçersiz sayı formatı.");
                return View(model);
            }
          

            Result result = null;

            try
            {
                switch (operation.ToLower())
                {
                    case "add":
                        result = _calculatorService.Add(model);
                        break;
                    case "sub":
                        result = _calculatorService.Subtract(model);  // Seçeceğimiz operatör durumları
                        break;
                    case "mul":
                        result = _calculatorService.Multiply(model);
                        break;
                    case "div":
                        result = _calculatorService.Divide(model);
                        break;
                    default:
                        ModelState.AddModelError("", "Geçersiz işlem.");
                        return View(model);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }

            if (result != null)
            {
                model.Result = result.Value;
                ViewBag.Result = result.Value;

                // İşlem geçmişine ekleme
                var history = HttpContext.Session.GetObjectFromJson<List<CalculationHistory>>("CalculationHistory") ?? new List<CalculationHistory>();
                history.Add(new CalculationHistory
                {
                    Number1 = model.Number1,
                    Number2 = model.Number2,  // History için bir Liste yapısı içerisinde yaptığımız işlemleri tutacak
                    Operation = operation,
                    Result = model.Result,
                    Timestamp = DateTime.Now
                });
                HttpContext.Session.SetObjectAsJson("CalculationHistory", history);  // Geçmiş verileri JSON formatında kaydeder
            }

            return View(model);
        }
    }
}
