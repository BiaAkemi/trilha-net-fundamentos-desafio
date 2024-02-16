namespace DesafioFundamentos.Models
{
    public class Vaga
    {
        public int Numero { get; }
        public bool Ocupada { get; private set; }
        public Veiculo VeiculoEstacionado { get; private set; }

        public Vaga(int numero)
        {
            Numero = numero;
            Ocupada = false;
            VeiculoEstacionado = null;
        }

        public void OcuparVaga(Veiculo veiculo)
        {
            Ocupada = true;
            VeiculoEstacionado = veiculo;
        }

        public void LiberarVaga()
        {
            Ocupada = false;
            VeiculoEstacionado = null;
        }
    }
}
