using System;
using System.Text.RegularExpressions;

namespace DesafioFundamentos.Models
{
    public class Veiculo
    {
        public string Placa { get; }
        public int NumeroVaga { get; }

        public Veiculo(string placa, int numeroVaga)
        {
            placa = placa.ToUpper();

            if (!IsValidPlaca(placa))
            {
                throw new ArgumentException("Placa inválida! Tente novamente.");
            }

            Placa = placa; 
            NumeroVaga = numeroVaga;
        }


        public static bool IsValidPlaca(string placa)
        {
            placa = placa.ToUpper();
            string pattern = @"^[A-Z]{3}[0-9][A-Z0-9][0-9]{2}$";
            return System.Text.RegularExpressions.Regex.IsMatch(placa, pattern);
        }
    }
}
