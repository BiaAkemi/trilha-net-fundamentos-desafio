using DesafioFundamentos.Models;

class Program
{
    static void Main()
    {
        // Coloca o encoding para UTF8 para exibir acentuação
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        
        Console.WriteLine("Seja bem vindo ao sistema de estacionamento!");

        decimal precoInicial = ObterDecimalInput("Digite o preço inicial:");
        decimal precoPorHora = ObterDecimalInput("Agora digite o preço por hora:");

        // Instancia a classe Estacionamento, já com os valores obtidos anteriormente
        Estacionamento estacionamento = new Estacionamento(precoInicial, precoPorHora);

        bool exibirMenu = true;

        // Realiza o loop do menu
        while (exibirMenu)
        {
            Console.Clear();
            Console.WriteLine("Digite a sua opção:");
            Console.WriteLine("1 - Cadastrar veículo");
            Console.WriteLine("2 - Remover veículo");
            Console.WriteLine("3 - Listar veículos");
            Console.WriteLine("4 - Encerrar");

            switch (Console.ReadLine())
            {
                case "1":
                    estacionamento.AdicionarVeiculo();
                    break;

                case "2":
                    estacionamento.RemoverVeiculo();
                    break;

                case "3":
                    estacionamento.ListarVeiculos();
                    break;

                case "4":
                    exibirMenu = false;
                    break;

                default:
                    Console.WriteLine("Opção inválida");
                    break;
            }

            Console.WriteLine("Pressione uma tecla para continuar");
            Console.ReadLine();
        }

        Console.WriteLine("O programa se encerrou");
    }

    private static decimal ObterDecimalInput(string mensagem)
    {
        decimal valor;
        while (true)
        {
            Console.WriteLine(mensagem);
            if (decimal.TryParse(Console.ReadLine(), out valor))
            {
                return valor;
            }
            Console.WriteLine("Entrada inválida. Por favor, insira um valor decimal válido.");
        }
    }
}
