// See https://aka.ms/new-console-template for more information

using System.Text.RegularExpressions;

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

    Thread.Sleep(300);
    Console.WriteLine("Carregando...");
    Thread.Sleep(2000);
    Console.Clear();
    controleCentralJogo();
}

static void controleCentralJogo()
{
    string jogador1;
    string jogador2;
    int modalidade = menuInicial();

    if (modalidade == 1)
    {
        //Entrada de Nomes Jogadores
        Console.BackgroundColor = ConsoleColor.DarkBlue;
        Console.Clear();
        jogador1 = defineNomeJogador(1);
        inverterJogador(1);

        jogador2 = defineNomeJogador(2);
        inverterJogador(2);

        //Posicionamento peças iniciais
        int[,] peçasJogador1 = posicionarPecasIniciais(jogador1);
        inverterJogador(1);
        int[,] peçasJogador2 = posicionarPecasIniciais(jogador2);
        inverterJogador(2);

        bool fimDeJogo = false;
        int jogadorAtual = 1;
        string nomeJogador = jogador1;
        int[,] peçasOponente;

        while (!fimDeJogo)
        {
            peçasOponente = jogadorAtual == 1 ? peçasJogador2 : peçasJogador1;
            nomeJogador = jogadorAtual == 1 ? jogador1 : jogador2;
            peçasOponente = executarJogada(jogadorAtual, nomeJogador, peçasOponente);
            if (jogadorAtual == 1)
            {
                peçasJogador2 = peçasOponente;
            }
            if (jogadorAtual == 2)
            {
                peçasJogador1 = peçasOponente;
            }

            foreach (int posicao in peçasOponente)
            {
                if (posicao == 1)
                {
                    fimDeJogo = false;
                    jogadorAtual = inverterJogador(jogadorAtual);
                    break;
                }
                fimDeJogo = true;
            }
        }
        Console.WriteLine("FIM DE JOGO!");
        Console.WriteLine($"PARABÉNS {nomeJogador.ToUpper()}, VOCÊ VENCEU!!");
        Console.WriteLine("Pressione enter para encerrar o jogo...");
        Console.ReadLine();
    }
    else
    {
        Console.WriteLine("Modalidade ainda não desenvolvida");
        Thread.Sleep(2000);
        startJogo();
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

static int[,] posicionarPecasIniciais(string jogadorAtual)
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
    Dictionary<string, int> qtNaviosFaltaPosiconar = qtNaviosInicial;
    bool listaCompleta = false;
    while (!listaCompleta)
    {
        Console.WriteLine($"Definindo Posições do jogador: {jogadorAtual}.");
        Console.WriteLine("Falta posicionar:");
        foreach (KeyValuePair<string, int> kv in qtNaviosFaltaPosiconar)
        {
            Console.Write($"[{kv.Key}: {kv.Value}] | ");
        }
        Console.WriteLine("\n");
        Console.WriteLine("Qual o tipo de embarcação?");

        string tipoEmabarcacao = Console.ReadLine().Trim().ToUpper();
        if (!qtNaviosInicial.ContainsKey(tipoEmabarcacao))
        {
            Console.WriteLine("Tipo de embarcação inválida. Digite uma das opções [SB, DS, NT, PS]");
            continue;
        }
        if (qtNaviosFaltaPosiconar[tipoEmabarcacao] == 0)
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
            List<List<int>> listasNumericasPosicao = converteParaListaNumerica(posicaoEmbarcacao);
            if (string.IsNullOrEmpty(posicaoEmbarcacao) || !isPosicaoValida(posicaoEmbarcacao, listasNumericasPosicao, tipoEmabarcacao, dimensaoNavios))
            {
                Console.WriteLine("Posição Inválida");
                continue;
            }



            int[,] matrizPosicaoAtual = criarMatriz(listasNumericasPosicao);

            int[,] matrizAuxiliar = matrizComPosicoes;

            matrizAuxiliar = Sum(matrizComPosicoes, matrizPosicaoAtual);

            if (!isPosicaoLivre(matrizAuxiliar))
            {
                Console.WriteLine("Posição já está ocupada!");
                continue;
            }

            matrizComPosicoes = matrizAuxiliar;

            Console.Clear();
            View(matrizComPosicoes);

            string[] novoItem = { tipoEmabarcacao, posicaoEmbarcacao };
            listaPecas.Add(novoItem);
            qtNaviosFaltaPosiconar[tipoEmabarcacao] -= 1;
            posicaoValida = true;

        }
        listaCompleta = listaPecas.Count >= qtTotalInicial;
    }

    int[,] pecasPosicionadas = matrizComPosicoes;

    return pecasPosicionadas;
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

static bool isPosicaoValida(string posicaoEmbarcacao, List<List<int>> listaNumericaPosicoes, string tipoEmabarcacao, Dictionary<string, int> dimensaoNavios)
{
    var pattern = @"^([A-J])\d[0]?([A-J])\d[0]?$";
    Regex regex = new(pattern);
    if (!regex.Match(posicaoEmbarcacao).Success)
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
static bool isPosicaoLivre(int[,] matrizAuxiliar)
{
    foreach (var posicao in matrizAuxiliar)
    {
        if (posicao > 1)
        {
            return false;
        }
    }
    return true;
}

static int inverterJogador(int jogador)
{
    //Player 1 color
    ConsoleColor player1Background = ConsoleColor.DarkBlue;

    //Player 2 color
    ConsoleColor player2Background = ConsoleColor.DarkRed;

    Console.ResetColor();
    Console.Clear();

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

    return jogador;
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
    for (int i = 0; i < (a.GetLength(0) + 1); i++)
    {
        for (int j = 0; j < (a.GetLength(1) + 1); j++)
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

static void ViewOponente(string[,] a)
{
    for (int i = 0; i < (a.GetLength(0) + 1); i++)
    {
        for (int j = 0; j < (a.GetLength(1) + 1); j++)
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
                ConsoleColor currentBackground = Console.BackgroundColor;
                ConsoleColor currentForeground = Console.ForegroundColor;
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write("{0}  ", a[i - 1, j - 1]);
                Console.BackgroundColor = currentBackground;
                Console.ForegroundColor = currentForeground;
            }
        }
        Console.WriteLine();

    }
    Console.WriteLine("\n\n");

}

static int[,] executarJogada(int jogadorAtual, string nomeJogador, int[,] pecasOponente)
{
    Console.WriteLine($"Jogador atual: {nomeJogador}\n");
    Console.WriteLine("Mapa atual do seu oponente:");

    criaMatrizStrings(pecasOponente);

    bool posicaoDisparoValida = false;

    while (!posicaoDisparoValida)
    {
        Console.WriteLine("Escolha a posição para realizar o disparo");
        string posicaoDisparo = Console.ReadLine().Trim().ToUpper();

        if (string.IsNullOrWhiteSpace(posicaoDisparo))
        {
            Console.WriteLine("Posição de disparo inválida!");
            continue;
        }

        var pattern = @"^([A-J])\d[0]?$";
        Regex regex = new(pattern);

        posicaoDisparoValida = regex.Match(posicaoDisparo).Success;
        if (!posicaoDisparoValida)
        {
            Console.WriteLine("Posição de disparo inválida!");
            continue;
        }

        int digitoConvertido;
        char[] caracteresPosicao = posicaoDisparo.ToCharArray();

        int letraConvertida = (int)Enum.Parse(typeof(posicoesLinhas), caracteresPosicao[0].ToString());

        if (posicaoDisparo.Length > 2)
        {
            digitoConvertido = Convert.ToInt32(string.Concat(caracteresPosicao[1], caracteresPosicao[2])) - 1;
        }
        else
        {
            digitoConvertido = Convert.ToInt32(caracteresPosicao[1].ToString()) - 1;
        }


        List<int> listaNumericaDisparo = new();
        listaNumericaDisparo.Add(letraConvertida);
        listaNumericaDisparo.Add(digitoConvertido);

        int[,] matrizDisparoAtual = criarMatrizDisparo(listaNumericaDisparo);

        int[,] matrizAuxiliar = pecasOponente;

        matrizAuxiliar = Sum(matrizAuxiliar, matrizDisparoAtual);

        (int[,] matrizRetorno, bool disparoValido) = trataDisparo(matrizAuxiliar);

        if (!disparoValido)
        {
            posicaoDisparoValida = false;
            continue;
        }

        pecasOponente = matrizRetorno;
        criaMatrizStrings(pecasOponente);
        Console.WriteLine("Pressione enter para continuar o jogo...");
        Console.ReadLine();
        posicaoDisparoValida = true;
    }

    return pecasOponente;
}

static void criaMatrizStrings(int[,] pecasOponente)
{
    string[,] matrizStrings = new string[pecasOponente.GetLength(0), pecasOponente.GetLength(1)];

    for (int i = 0; i < pecasOponente.GetLength(0); i++)
    {
        for (int j = 0; j < pecasOponente.GetLength(1); j++)
        {
            if (pecasOponente[i, j] == 0 || pecasOponente[i, j] == 1)
            {
                matrizStrings[i, j] = " ";
            }
            if (pecasOponente[i, j] == -5)
            {
                matrizStrings[i, j] = "A";
            }
            if (pecasOponente[i, j] == -10)
            {
                matrizStrings[i, j] = "X";
            }
        }
    }

    ViewOponente(matrizStrings);
}

static int[,] criarMatrizDisparo(List<int> listaNumericaDisparo)
{
    int[,] matrizDisparo = new int[10, 10];
    matrizDisparo[listaNumericaDisparo[0], listaNumericaDisparo[1]] = 7;

    return matrizDisparo;
}

static (int[,] matrizRetorno, bool disparoValido) trataDisparo(int[,] matrizResultadoDisparo)
{
    int[,] matrizTratada = new int[matrizResultadoDisparo.GetLength(0), matrizResultadoDisparo.GetLength(1)];
    for (int i = 0; i < matrizResultadoDisparo.GetLength(0); i++)
    {
        for (int j = 0; j < matrizResultadoDisparo.GetLength(1); j++)
        {
            int valorPosicao = matrizResultadoDisparo[i, j];
            if (valorPosicao == 7)
            {
                matrizTratada[i, j] = -5;
                Console.Clear();
                Console.WriteLine("Não foi dessa vez! Posição atacada sem embarcação!");
            }
            else if (valorPosicao == 8)
            {
                matrizTratada[i, j] = -10;
                Console.Clear();
                Console.WriteLine("Parabéns! Posição atacada continha embarcação!");
            }
            else if (valorPosicao == -3 || valorPosicao == 2)
            {
                (int[,] matrizRetorno, bool disparoValido) resultadoDisparoInvalido = (matrizResultadoDisparo, false);
                Console.WriteLine("Posição de ataque inválida!");
                return resultadoDisparoInvalido;
            }
            else
            {
                matrizTratada[i, j] = valorPosicao;
            }
        }
    }
    (int[,] matrizRetorno, bool disparoValido) resultadoDisparoValido = (matrizTratada, true);
    return resultadoDisparoValido;
}
public enum posicoesLinhas { A, B, C, D, E, F, G, H, I, J }


