using RP.DAL.Repository;
using Ineval.DAL;
using System;

namespace Ineval.BO
{
    public class NumberingService : EntityService<Numbering>
    {
        public NumberingService()
        {

        }
        public NumberingService(IUnitOfWork unitOfWork)
                : base(unitOfWork)
        {
        }

        public static string GetCodigoSecuencial(string TipoDoc, char caracter, int numeroDigitos)
        {
            var secuencial = GetSecuencial(TipoDoc);
            return secuencial.ToString().PadLeft(numeroDigitos, caracter);
        }
        public static int GetSecuencial(string TipoDoc)
        {
            try
            {
                var numero = 1;

                using (var service = new NumberingService())
                {
                    var result = service.FirstOrDefault(n => n.DocumentType == TipoDoc);
                    if (result == null)
                    {
                        result = new Numbering()
                        {
                            DocumentType = TipoDoc,
                            Sequential = numero
                        };
                        service.Create(result);
                    }
                    else
                    {
                        numero = result.Sequential + 1;
                        result.Sequential = numero;
                        service.Update(result);
                    }
                }

                return numero;
            }
            catch
            {
                return 0;
            }
        }
    }
}