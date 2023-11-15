
namespace jogoDaVelha
{
    internal class JogoDaVelha
    {
        private bool fimDeJogo;
        private char[] posicoes;
        private char vez;
        private int quantidadePreenchida;

        public JogoDaVelha()
        {
            fimDeJogo = false;
            posicoes = new[] { '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            vez = (new Random()).NextDouble() < 0.5 ? 'X' : 'O';
            quantidadePreenchida = 0;
        }

        public void Iniciar()
        {
            while (!fimDeJogo)
            {
                RenderizarTabela();
                LerEscolhaDoUsuario();
                RenderizarTabela();
                VerificarFimDoJogo();
                MudarVez();
            }
        }

        private void MudarVez()
        {
            vez = vez == 'X' ? 'O' : 'X';
        }

        private void VerificarFimDoJogo()
        {
            if (quantidadePreenchida < 5)
                return;

            if(ExisteVitoriaDiagonal() || ExisteVitoriaVertical() || ExisteVitoriaHorizontal())
            {
                fimDeJogo = true;
                Console.WriteLine($"Vítória de {vez}!");
                return;
            }

            if(quantidadePreenchida is 9)
            {
                fimDeJogo = true;
                Console.WriteLine("EMPATE ");
            }
        }

        private bool ExisteVitoriaHorizontal()
        {
            bool vitoriaLinha1 = posicoes[0] == posicoes[1] && posicoes[0] == posicoes[2];
            bool vitoriaLinha2 = posicoes[3] == posicoes[4] && posicoes[3] == posicoes[5];
            bool vitoriaLinha3 = posicoes[6] == posicoes[7] && posicoes[6] == posicoes[8];

            return vitoriaLinha1 || vitoriaLinha2 || vitoriaLinha3;

        }

        private bool ExisteVitoriaVertical()
        {
            bool vitoriaColuna1 = posicoes[0] == posicoes[3] && posicoes[0] == posicoes[6];
            bool vitoriaColuna2 = posicoes[1] == posicoes[4] && posicoes[1] == posicoes[7];
            bool vitoriaColuna3 = posicoes[2] == posicoes[5] && posicoes[2] == posicoes[8];

            return vitoriaColuna1 || vitoriaColuna2 || vitoriaColuna3;
        }

        private bool ExisteVitoriaDiagonal()
        {
            bool vitoriaDiagonal1 = posicoes[2] == posicoes[4] && posicoes[2] == posicoes[6];
            bool vitoriaDiagonal2 = posicoes[0] == posicoes[4] && posicoes[0] == posicoes[8];

            return vitoriaDiagonal1 || vitoriaDiagonal2;
        }

        private void LerEscolhaDoUsuario()
        {

            Console.WriteLine($"Jogador {vez}, é a sua vez. Escolha uma posição de 1 a 9 na tabela:");
            bool conversao = int.TryParse(Console.ReadLine(), out int posicaoEscolhida);

            while (!conversao || !ValidarEscolhaUsuario(posicaoEscolhida))
            {
                Console.WriteLine("Campo inválido. Digite um número entre 1 e 9 disponível na tabela.");
                conversao = int.TryParse(Console.ReadLine(), out posicaoEscolhida);
            }

            preencherEscolha(posicaoEscolhida);
        }

        private void preencherEscolha(int posicaoEscolhida  )
        {
            int indice = posicaoEscolhida - 1;
            posicoes[indice] = vez;
            quantidadePreenchida++;
        }

        private bool ValidarEscolhaUsuario(int posicaoEscolhida)
        {
            var indice = posicaoEscolhida - 1;

            return indice >= 0 && indice < posicoes.Length && posicoes[indice] != 'O' && posicoes[indice] != 'X';
        }

        private void RenderizarTabela()
        {
            Console.Clear();
            Console.WriteLine(ObterTabela());
        }

        private string ObterTabela()
        {
            Console.ForegroundColor = ConsoleColor.White;
            int larguraDaJanela = Console.WindowWidth;

            int posicaoInicial = (larguraDaJanela - 37) / 2; 

            string tabelaCentralizada =
                $"Seja bem vindo ao Jogo da Velha! \n" +
                $"\n" +
                $"{new string(' ', posicaoInicial + 3)}{posicoes[0]}       |       {posicoes[1]}       |       {posicoes[2]}\n" +
                $"{new string(' ', posicaoInicial)}_______________________________________\n" +
                $"{new string(' ', posicaoInicial + 3)}{posicoes[3]}       |       {posicoes[4]}       |       {posicoes[5]}\n" +
                $"{new string(' ', posicaoInicial)}_______________________________________\n" +
                $"{new string(' ', posicaoInicial + 3)}{posicoes[6]}       |       {posicoes[7]}       |       {posicoes[8]}\n\n";
            Console.ResetColor();
            return tabelaCentralizada;
        }

    }
}
