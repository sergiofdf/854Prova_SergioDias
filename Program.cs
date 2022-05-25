// See https://aka.ms/new-console-template for more information

startJogo();

static void startJogo()
{
    Console.BackgroundColor = ConsoleColor.Black;
    Console.Clear();
    Console.WriteLine("***********************************");
    Console.WriteLine("***********************************\n");
    Console.WriteLine("********** BATALHA NAVAL **********\n");
    Console.WriteLine("***********************************");
    Console.WriteLine("***********************************\n");
    Console.WriteLine("** Desenvolvido por: Sergio Dias ");
    Console.WriteLine("** Desafio turma 854 ");
    Console.WriteLine("** Curso de lógica de programção C#");
    Console.WriteLine("** Professor: Heber da Silva\n");
    Console.WriteLine("***********************************");
    Console.WriteLine("***********************************\n");

    Thread.Sleep(500);
    Console.WriteLine("Carregando...");
    Thread.Sleep(3000);
    Console.Clear();
    controleCentralJogo();
}

static void controleCentralJogo()
{
    string jogador1;
    string jogador2;
    int proximoJogador = 0;
    int modalidade = menuInicial();

    if (modalidade == 1)
    {
        //Entrada de Nomes Jogadores
        Console.BackgroundColor = ConsoleColor.DarkBlue;
        Console.Clear();
        //for (int ix = 0; ix < Console.BufferHeight; ++ix) Console.WriteLine();
        jogador1 = defineNomeJogador(1);
        inverterJogador(1);

        jogador2 = defineNomeJogador(2);
        inverterJogador(2);

        //Posicionamento peças iniciais
        posicionarPecasIniciais(1);
        inverterJogador(1);
        posicionarPecasIniciais(2);
        inverterJogador(2);

    }
    else
    {
        Console.WriteLine("Modalidade ainda não desenvolvida");
    }
}

static int menuInicial()
{
    Console.WriteLine("Escolha a opção de jogo:");
    Console.WriteLine("1 - Multiplayer: jogo entre 2 jogadores");
    Console.WriteLine("2 - Singleplayer: jogar contra o computador");

    bool numeroValido = false;
    int numeroEscolha = 0;

    while (!numeroValido)
    {
        string escolhaDigitada = Console.ReadLine();
        numeroValido = (int.TryParse(escolhaDigitada, out numeroEscolha) && numeroEscolha > 0 && numeroEscolha <= 2);
        if (!numeroValido)
        {
            Console.WriteLine("Digite uma opção entre 1 e 2.");
        }
    }
    Thread.Sleep(500);
    Console.Clear();
    return numeroEscolha;
}

static string defineNomeJogador(int numeroJogador)
{
    Console.Write($"Digite o nome do jogador {numeroJogador}:\n");
    return Console.ReadLine();
}

static void posicionarPecasIniciais(int jogador)
{

    Dictionary<string, int> dimensaoNavios = new()
    {
        { "SB", 2 },
        { "DS", 3 },
        { "NT", 4 },
        { "PS", 5 }
    };

    Dictionary<string, int> qtNaviosInicial = new()
    {
        { "SB", 4 },
        { "DS", 3 },
        { "NT", 2 },
        { "PS", 1 }
    };

    int qtTotalInicial = 0;
    foreach (KeyValuePair<string, int> kv in qtNaviosInicial)
    {
        qtTotalInicial += kv.Value;
    }

    //Lista de peças com arrays [tipo_peça, posição_peça]
    List<string[]> listaPecas = new();

    while (listaPecas.Count <= qtTotalInicial)
    {
        Console.WriteLine("Qual o tipo de embarcação?");
        string tipoEmabarcacao = Console.ReadLine().Trim().ToUpper();
        if (!qtNaviosInicial.ContainsKey(tipoEmabarcacao))
        {
            Console.WriteLine("Tipo de embarcação inválida. Digite uma das opções [SB, DS, NT, PS]");
            continue;
        }
        if (listaPecas.Where(l => l[0] == tipoEmabarcacao).Count() >= qtNaviosInicial[tipoEmabarcacao])
        {
            Console.WriteLine($"Ja foram definidas todas as posições das embarcações do tipo {tipoEmabarcacao}.");
            continue;
        }
        bool posicaoValida = false;
        while (!posicaoValida)
        {
            Console.WriteLine("Qual a posição da embarcação?");
            string posicaoEmbarcacao = Console.ReadLine().Trim().ToUpper();
            if (string.IsNullOrEmpty(posicaoEmbarcacao) || !isPosicaoValida(posicaoEmbarcacao, listaPecas))
            {
                Console.WriteLine("Posição Inválida");
            }
            else
            {
                string[] novoItem = { tipoEmabarcacao, posicaoEmbarcacao };
                listaPecas.Add(novoItem);
                posicaoValida = true;
            }
        }
    }


}

static bool isPosicaoValida(string posicaoParaChecar, List<string[]> listaPeças)
{
    char[] caracteresPosicao = posicaoParaChecar.ToCharArray();

    if (!Enum.IsDefined(typeof(posicoesLinhas), caracteresPosicao[0].ToString()))
    {
        Console.WriteLine("Posição Inválida");
        return false;
    }

    //A1B2
    if (posicaoParaChecar.Length == 4)
    {
        if (!Char.IsDigit(caracteresPosicao[1]) || !Char.IsDigit(caracteresPosicao[3]))
        {
            Console.WriteLine("Posição Inválida");
            return false;
        }
        if (!Enum.IsDefined(typeof(posicoesLinhas), caracteresPosicao[2].ToString()))
        {
            Console.WriteLine("Posição Inválida");
            return false;
        }
    }

    //A10B2 ou A2B10
    //01234    01234 
    if (posicaoParaChecar.Length == 5)
    {
        if (Char.IsDigit(caracteresPosicao[2]))
        {
            if (caracteresPosicao[2].ToString() != "0")
            {
                return false;
            }
            if (!Char.IsDigit(caracteresPosicao[1]) || !Char.IsDigit(caracteresPosicao[4]))
            {
                return false;
            }
            if (!Enum.IsDefined(typeof(posicoesLinhas), caracteresPosicao[3].ToString()))
            {
                return false;
            }
        }
        if (Char.IsDigit(caracteresPosicao[3]))
        {
            if (caracteresPosicao[4].ToString() != "0")
            {
                return false;
            }
            if (!Char.IsDigit(caracteresPosicao[1]))
            {
                return false;
            }
            if (!Enum.IsDefined(typeof(posicoesLinhas), caracteresPosicao[2].ToString()))
            {
                return false;
            }
        }

    }

    //A10B10
    //012345
    if (posicaoParaChecar.Length == 6)
    {
        if (!Char.IsDigit(caracteresPosicao[1]) || !Char.IsDigit(caracteresPosicao[2]) || !Char.IsDigit(caracteresPosicao[4]) || !Char.IsDigit(caracteresPosicao[5]))
        {
            Console.WriteLine("Posição Inválida");
            return false;
        }
        if (!Enum.IsDefined(typeof(posicoesLinhas), caracteresPosicao[3].ToString()))
        {
            Console.WriteLine("Posição Inválida");
            return false;
        }
        if (caracteresPosicao[1].ToString() != "1" || caracteresPosicao[2].ToString() != "0" || caracteresPosicao[4].ToString() != "1" || caracteresPosicao[5].ToString() != "0")
        {
            Console.WriteLine("Posição Inválida");
            return false;
        }
    }

    return true;
}

static void inverterJogador(int jogador)
{
    //Player 1 color
    ConsoleColor player1Background = ConsoleColor.DarkBlue;

    //Player 2 color
    ConsoleColor player2Background = ConsoleColor.DarkRed;

    Console.ResetColor();
    Console.Clear();
    //for (int ix = 0; ix < Console.BufferHeight; ++ix) Console.WriteLine();

    if (jogador == 1)
    {
        jogador = 2;
        Console.BackgroundColor = player2Background;
        Console.Clear();
    }
    else if (jogador == 2)
    {
        jogador = 1;
        Console.BackgroundColor = player1Background;
        Console.Clear();
    }
};

public enum posicoesLinhas { A, B, C, D, E, F, G, H, I, J }


