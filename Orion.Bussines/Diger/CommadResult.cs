using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orion.Bussines.Diger
{
    public class CommadResult
    {
        private const string DefaultSuccessMessage = "İşlem Tamamlanadı";
        private const string DefaultFailureMessage = "İşlem Tamamlanmadı";

        public bool IsSuccess { get; private set; }
        public string Message { get; private set; }
        //Factory Pattery
        public static CommadResult Success()
        {
            return Success(DefaultSuccessMessage);
        }
        public static CommadResult Success(string messaj)
        {
            return new CommadResult()
            {
                IsSuccess = true,
                Message = messaj
            };
        }
        public static CommadResult Failure()
        {
            return Failure(DefaultFailureMessage);
        }
        public static CommadResult Failure(string messaj)
        {
            return new CommadResult()
            {
                IsSuccess = false,
                Message = messaj
            };
        }
    }
}
