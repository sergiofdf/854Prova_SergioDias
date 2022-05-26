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
    int[,] matrizComPosicoes = new int[10, 10];

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
            Console.WriteLine($"Para o navio ecolhido, a dimensão da posição deve ser {dimensaoNavios[tipoEmabarcacao]}.");
            string posicaoEmbarcacao = Console.ReadLine().Trim().ToUpper();
            if (posicaoEmbarcacao.Length < 4 || posicaoEmbarcacao.Length > 6)
            {
                Console.WriteLine("Posição Inválida");
                continue;
            }
            List<List<int>> listasNumericasPosicao = converteParaListaNumerica(posicaoEmbarcacao);
            if (string.IsNullOrEmpty(posicaoEmbarcacao) || !isPosicaoValida(listasNumericasPosicao, listaPecas, tipoEmabarcacao, dimensaoNavios))
            {
                Console.WriteLine("Posição Inválida");
                continue;
            }



            //FALTA IMPLEMENTAR FUNCAO ABAIXO
            if (!isPosicaoLivre(posicaoEmbarcacao, listaPecas))
            {
                Console.WriteLine("Posição já está ocupada!");
                continue;
            }



            int[,] matrizPosicaoAtual = criarMatriz(listasNumericasPosicao);

            matrizComPosicoes = Sum(matrizComPosicoes, matrizPosicaoAtual);

            Console.Clear();
            View(matrizComPosicoes);


            string[] novoItem = { tipoEmabarcacao, posicaoEmbarcacao };
            listaPecas.Add(novoItem);
            posicaoValida = true;

        }
    }


}

static List<List<int>> converteParaListaNumerica(string posicaoComLetras)
{
    List<int> listaLetrasConvertidas = new();
    List<int> listaDigitosConvertidos = new();
    char[] caracteresPosicao = posicaoComLetras.ToCharArray();

    for (int i = 0; i < caracteresPosicao.Length; i++)
    {
        if (Char.IsDigit(caracteresPosicao[i]))
        {
            if (i < caracteresPosicao.Length - 1 && Char.IsDigit(caracteresPosicao[i + 1]))
            {
                int digito = Convert.ToInt32(string.Concat(caracteresPosicao[i], caracteresPosicao[i + 1]));
                listaDigitosConvertidos.Add(digito);
                i++;
            }

            else
            {
                int digito = Convert.ToInt32(string.Concat(caracteresPosicao[i]));
                listaDigitosConvertidos.Add(digito);
            }
        }
        if (Enum.IsDefined(typeof(posicoesLinhas), caracteresPosicao[i].ToString()))
        {
            int letraConvertida = (int)Enum.Parse(typeof(posicoesLinhas), caracteresPosicao[i].ToString());
            listaLetrasConvertidas.Add(letraConvertida);
        }
    }

    List<List<int>> listasConvertidas = new();
    listasConvertidas.Add(listaLetrasConvertidas);
    listasConvertidas.Add(listaDigitosConvertidos);
    return listasConvertidas;

}

static bool isPosicaoValida(List<List<int>> listaNumericaPosicoes, List<string[]> listaPeças, string tipoEmabarcacao, Dictionary<string, int> dimensaoNavios)
{
    if (listaNumericaPosicoes[0].Any(n => n > 9) || listaNumericaPosicoes[1].Any(n => n > 10))
    {
        return false;
    }

    int tamanhoPosicao = 0;

    if (listaNumericaPosicoes[0][0] == listaNumericaPosicoes[0][1])
    {
        tamanhoPosicao = listaNumericaPosicoes[1][1] - listaNumericaPosicoes[1][0] + 1;
    }
    else
    {
        tamanhoPosicao = listaNumericaPosicoes[0][1] - listaNumericaPosicoes[0][0] + 1;
    }

    if (tamanhoPosicao != dimensaoNavios[tipoEmabarcacao])
    {
        Console.WriteLine("Dimensão não condiz com o tipo de embarcação escolhido.");
        return false;
    }

    return true;
}

static int[,] criarMatriz(List<List<int>> listaNumericaPosicoes)
{


    int[,] matrizPosicao = new int[10, 10];
    for (int i = 0; i < 10; i++)
    {
        if (i >= listaNumericaPosicoes[0][0] && i <= listaNumericaPosicoes[0][1])
        {
            for (int j = 0; j < 10; j++)
            {
                if (j >= listaNumericaPosicoes[1][0] - 1 && j <= listaNumericaPosicoes[1][1] - 1)
                    matrizPosicao[i, j] = 1;
            }
        }
        else
        {
            for (int j = 0; j < 10; j++)
            {
                matrizPosicao[i, j] = 0;
            }
        }

    }
    return matrizPosicao;
}
static bool isPosicaoLivre(string posicaoParaChecar, List<string[]> listaPeças)
{
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
}

static int[,] Sum(int[,] a, int[,] b)
{
    int[,] result = new int[10, 10];
    for (int i = 0; i < 10; i++)
        for (int j = 0; j < 10; j++)
            result[i, j] = a[i, j] + b[i, j];
    return result;
}

static void View(int[,] a)
{
    for (int i = 0; i < 11; i++)
    {
        for (int j = 0; j < 11; j++)
        {
            if (i == 0 && j == 0)
            {
                Console.Write("   ");
            }
            else if (i == 0 && j > 0)
            {
                Console.Write("{0}  ", j);
            }
            else if (j == 0 && i > 0)
            {
                Console.Write("{0}  ", Enum.GetName(typeof(posicoesLinhas), i - 1));
            }
            else
            {
                Console.Write("{0}  ", a[i - 1, j - 1]);
            }
        }
        Console.WriteLine();

    }
    Console.WriteLine("\n\n");

}

public enum posicoesLinhas { A, B, C, D, E, F, G, H, I, J }


