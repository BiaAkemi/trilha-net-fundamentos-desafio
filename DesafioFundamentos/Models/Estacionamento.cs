using System;
using System.Collections.Generic;
using System.Linq;

namespace DesafioFundamentos.Models
{
    public class Estacionamento
    {
        private decimal precoInicial = 0;
        private decimal precoPorHora = 0;
        private List<Veiculo> veiculos = new List<Veiculo>();
        private List<Vaga> vagas = new List<Vaga>();
        private string placaValida; // Armazena placa válida para reutilização

        public Estacionamento(decimal precoInicial, decimal precoPorHora, int quantidadeVagas = 10)
        {
            this.precoInicial = precoInicial;
            this.precoPorHora = precoPorHora;

            for (int i = 1; i <= quantidadeVagas; i++)
            {
                vagas.Add(new Vaga(i));
            }
        }

        public void AdicionarVeiculo()
        {
            bool sair = false;

            while (!sair)
            {
                Console.WriteLine("Digite a placa do veículo para estacionar (ou digite '0' para sair):");
                string placa = Console.ReadLine();

                if (placa == "0")
                {
                    sair = true; 
                    break;
                }

                if (Veiculo.IsValidPlaca(placa))
                {
                    if (VeiculoJaAdicionado(placa))
                    {
                        Console.WriteLine($"O veículo com a placa {placa} já está estacionado. Escolha outra placa.");
                    }
                    else
                    {
                        placaValida = placa; 
                        break; 
                    }
                }
                else
                {
                    Console.WriteLine("Placa inválida. Certifique-se de inserir uma placa válida.");
                }
            }

            if (!sair)
            {
                // Loop para solicitar a vaga até que o usuário forneça uma vaga válida ou digite '0' para sair
                while (true)
                {
                    Console.WriteLine("Digite o número da vaga do veículo (ou digite '0' para sair):");
                    string input = Console.ReadLine();

                    if (input == "0")
                    {
                        break; // Sai do loop após obter '0'
                    }

                    if (int.TryParse(input, out int numeroVaga))
                    {
                        if (ValidarNumeroVaga(numeroVaga))
                        {
                            Vaga vaga = ObterVaga(numeroVaga);

                            if (VagaOcupada(numeroVaga))
                            {
                                Console.WriteLine($"A vaga {numeroVaga} está ocupada. Escolha outra vaga.");
                            }
                            else
                            {
                                veiculos.Add(new Veiculo(placaValida, numeroVaga));
                                Console.WriteLine($"Veículo adicionado na vaga {numeroVaga}!");
                                break; // Sai do loop após adicionar o veículo
                            }
                        }
                        else
                        {
                            Console.WriteLine("Número de vaga inválido. Certifique-se de inserir um número válido.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Número de vaga inválido. Certifique-se de inserir um número válido.");
                    }
                }
            }
        }

        private bool VeiculoJaAdicionado(string placa)
        {
            string placaMaiuscula = placa.ToUpper();
        return veiculos.Any(v => v.Placa == placaMaiuscula);
        }

        private bool VagaOcupada(int numeroVaga)
        {
            return veiculos.Any(v => v.NumeroVaga == numeroVaga);
        }

                public void RemoverVeiculo()
        {
            Console.WriteLine("Digite a placa do veículo para remover:");
            string placa = Console.ReadLine();
            
            if (Veiculo.IsValidPlaca(placa))
            {
                string placaMaiuscula = placa.ToUpper();
                Veiculo veiculo = veiculos.FirstOrDefault(v => v.Placa == placaMaiuscula);

                if (veiculo != null)
                {
                    RemoverVeiculoEExibirPreco(veiculo);
                }
                else
                {
                    Console.WriteLine("Desculpe, esse veículo não está estacionado aqui. Confira se digitou a placa corretamente");
                }
            }
            else
            {
                Console.WriteLine("Placa inválida. Certifique-se de inserir uma placa válida.");
            }
        }

        private void RemoverVeiculoEExibirPreco(Veiculo veiculo)
        {
            Console.WriteLine("Digite a quantidade de horas que o veículo permaneceu estacionado:");

            if (int.TryParse(Console.ReadLine(), out int horas) && horas >= 0)
            {
                decimal valorTotal = precoInicial + precoPorHora * horas;

                LiberarVaga(veiculo.NumeroVaga);
                veiculos.Remove(veiculo);

                Console.WriteLine($"O veículo {veiculo.Placa} foi removido e o preço total foi de: R$ {valorTotal}");
            }
            else
            {
                Console.WriteLine("Quantidade de horas inválida. Certifique-se de inserir um número válido.");
            }
        }

        public void ListarVeiculos()
        {
            if (veiculos.Any())
            {
                Console.WriteLine("Os veículos estacionados são:");
                foreach (Veiculo veiculo in veiculos)
                {
                    Console.WriteLine($"Placa: {veiculo.Placa}, Vaga: {veiculo.NumeroVaga}");
                }
            }
            else
            {
                Console.WriteLine("Não há veículos estacionados.");
            }
        }

        private bool ValidarNumeroVaga(int numeroVaga)
        {
            return numeroVaga > 0 && numeroVaga <= vagas.Count;
        }

        private Vaga ObterVaga(int numeroVaga)
        {
            Vaga vaga = vagas.FirstOrDefault(v => v.Numero == numeroVaga);

            if (vaga == null)
            {
                throw new ArgumentException($"A vaga {numeroVaga} não existe. Escolha uma vaga válida.");
            }

            return vaga;
        }

        private void LiberarVaga(int numeroVaga)
        {
            Vaga vaga = ObterVaga(numeroVaga);

            if (vaga.Ocupada)
            {
                vaga.LiberarVaga();
            }
        }
    }
}
