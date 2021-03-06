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
    Thread.Sleep(3000);
    Console.Clear();
    controleCentralJogo();
}

static void regrasDoJogo()
{
    Console.WriteLine("Sobre o Jogo - Batalha Naval\n");
    Console.WriteLine("Batalha naval é um jogo de tabuleiro de dois jogadores, no qual os jogadores têm de adivinhar em que quadrados estão os navios do oponente.");
    Console.WriteLine("Cada jogador possui seu próprio tabuleiro de dimensão 10x10 onde as linhas são representadas por letras (A-J) e as colunas são representadas por números (1-10).");
    Console.WriteLine("Os jogadores devem posicionar suas embarcações dentro dos quadrantes correspondentes.");
    Console.WriteLine("As embarcações devem ser posicionadas na vertical ou horizontal sempre formando uma reta e nunca em diagonal.");
    Console.WriteLine("Cada jodagor pode disparar uma vez em cada turno e para efetuar o disparo ele deve informar a posição do quadrante por letra e número. Exemplo: E7.");
    Console.WriteLine("Caso o disparo acerte uma embarcação aquele local é sinalizado.");
    Console.WriteLine("Quando um navio receber todos os disparos ele afunda.");
    Console.WriteLine("O jodo termina quando um dos dois jogadores afundar todos os navios do seu oponente.");
    Console.WriteLine("Cada jogador possui as seguintes embarcações:");
    Console.WriteLine("- 1 Porta-Aviões (5 quadrantes)");
    Console.WriteLine("- 2 Navio-Tanque (4 quadrantes)");
    Console.WriteLine("- 3 Destroyers (3 quadrantes)");
    Console.WriteLine("- 4 Submarinos (2 quadrantes)\n\n");
    Console.WriteLine("Pressione enter para voltar ao menu inicial");
    Console.ReadLine();
    startJogo();
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
            peçasOponente = executarJogada(nomeJogador, peçasOponente);
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
    else if (modalidade == 2)
    {
        // Entrada de Nome Jogador 1
        Console.BackgroundColor = ConsoleColor.DarkBlue;
        Console.Clear();
        jogador1 = defineNomeJogador(1);

        jogador2 = "player2Computador";

        // Posicionamento peças iniciais
        int[,] peçasJogador1 = posicionarPecasIniciais(jogador1);
        inverterJogador(1);

        int[,] peçasJogador2 = posicionarPecasIniciaisComputador(jogador2);
        inverterJogador(2);

        bool fimDeJogo = false;
        int jogadorAtual = 1;
        string nomeJogador = jogador1;
        int[,] peçasOponente;


        while (!fimDeJogo)
        {
            peçasOponente = jogadorAtual == 1 ? peçasJogador2 : peçasJogador1;
            nomeJogador = jogadorAtual == 1 ? jogador1 : jogador2;
            peçasOponente = executarJogada(nomeJogador, peçasOponente);
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
    else if (modalidade == 3)
    {
        regrasDoJogo();
    }
    else
    {
        startJogo();
    }
}

static int menuInicial()
{
    Console.WriteLine("Escolha a opção de jogo:");
    Console.WriteLine("1 - Multiplayer: jogo entre 2 jogadores");
    Console.WriteLine("2 - Singleplayer: jogar contra o computador");
    Console.WriteLine("3 - Veja as Regras do jogo");

    bool numeroValido = false;
    int numeroEscolha = 0;

    while (!numeroValido)
    {
        string escolhaDigitada = Console.ReadLine();
        numeroValido = (int.TryParse(escolhaDigitada, out numeroEscolha) && numeroEscolha > 0 && numeroEscolha <= 3);
        if (!numeroValido)
        {
            Console.WriteLine("Digite uma opção entre 1, 2 e 3.");
        }
    }
    Thread.Sleep(500);
    Console.Clear();
    return numeroEscolha;
}

static string defineNomeJogador(int numeroJogador)
{
    bool nomeValido = false;
    string nomeJogador = "";

    while (!nomeValido)
    {
        Console.Write($"Digite o nome do jogador {numeroJogador}:\n");
        nomeJogador = Console.ReadLine();
        nomeValido = true;
        if (string.IsNullOrEmpty(nomeJogador))
        {
            nomeValido = false;
            Console.Clear();
            Console.WriteLine("O nome não pode ser vazio.");
        }
    }
    return nomeJogador;
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

    Console.WriteLine("Chegou a hora de jogar! Lembre-se que você possui um total inicial de:");
    Console.WriteLine("- 4 Submarinos (SB): Ocupa 2 quadrantes;");
    Console.WriteLine("- 3 Destroyers (DS): Ocupa 3 quadrantes;");
    Console.WriteLine("- 2 Navios-tanque (NT): Ocupa 4 quadrantes;");
    Console.WriteLine("- 1 Porta-aviões (PS): Ocupa 5 quadrantes.\n");

    View(matrizComPosicoes);


    while (!listaCompleta)
    {
        Console.WriteLine($"Definindo Posições do jogador: {jogadorAtual}.");
        Console.WriteLine("Ainda falta posicionar:");
        Console.WriteLine("Embarcação   |   Quantos Faltam  | Quadrantes que ocupa");
        foreach (KeyValuePair<string, int> kv in qtNaviosFaltaPosiconar)
        {
            Console.WriteLine($"{kv.Key}           |       {kv.Value}           |       {dimensaoNavios[kv.Key]}");
        }
        Console.WriteLine("\nQual o tipo de embarcação?");

        string tipoEmabarcacao = Console.ReadLine().Trim().ToUpper();
        Console.WriteLine("");
        if (!qtNaviosInicial.ContainsKey(tipoEmabarcacao))
        {
            Console.WriteLine("Tipo de embarcação inválida. Digite uma das opções [SB, DS, NT, PS]\n");
            continue;
        }
        if (qtNaviosFaltaPosiconar[tipoEmabarcacao] == 0)
        {
            Console.WriteLine($"Ja foram definidas todas as posições das embarcações do tipo {tipoEmabarcacao}.\n");
            continue;
        }
        bool posicaoValida = false;
        while (!posicaoValida)
        {
            Console.WriteLine("Qual a posição da embarcação?");
            Console.WriteLine($"Para o navio escolhido, a dimensão da posição deve ser {dimensaoNavios[tipoEmabarcacao]}.");
            string posicaoEmbarcacao = Console.ReadLine().Trim().ToUpper();
            Console.WriteLine("");
            List<List<int>> listasNumericasPosicao = converteParaListaNumerica(posicaoEmbarcacao);
            if (string.IsNullOrEmpty(posicaoEmbarcacao) || !isPosicaoValida(posicaoEmbarcacao, listasNumericasPosicao, tipoEmabarcacao, dimensaoNavios))
            {
                Console.WriteLine("Posição Inválida\n");
                continue;
            }

            int[,] matrizPosicaoAtual = criarMatriz(listasNumericasPosicao);

            int[,] matrizAuxiliar = matrizComPosicoes;

            matrizAuxiliar = Sum(matrizComPosicoes, matrizPosicaoAtual);

            if (!isPosicaoLivre(matrizAuxiliar))
            {
                Console.WriteLine("Posição já está ocupada!\n");
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
        if (Enum.IsDefined(typeof(PosicoesLinhas), caracteresPosicao[i].ToString()))
        {
            int letraConvertida = (int)Enum.Parse(typeof(PosicoesLinhas), caracteresPosicao[i].ToString());
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
    var pattern = @"^([A-J])[1-9][0]?([A-J])[1-9][0]?$";
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

    if ((listaNumericaPosicoes[0][0] != listaNumericaPosicoes[0][1]) && (listaNumericaPosicoes[1][1] != listaNumericaPosicoes[1][0]))
    {
        Console.WriteLine("Posição deve ser na vertical ou horizontal.");
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
    Console.WriteLine("Mapa de posicionamento (0 = Livre, 1 = Ocupado)");
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
                Console.Write("{0}  ", Enum.GetName(typeof(PosicoesLinhas), i - 1));
            }
            else
            {
                Console.Write("{0}  ", a[i - 1, j - 1]);
            }
        }
        Console.WriteLine();

    }
    Console.WriteLine("\n");

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
                Console.Write("{0}  ", Enum.GetName(typeof(PosicoesLinhas), i - 1));
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

static int[,] executarJogada(string nomeJogador, int[,] pecasOponente)
{
    Console.WriteLine($"Jogador atual: {nomeJogador}\n");
    Console.WriteLine("Mapa atual do seu oponente:");

    criaMatrizStrings(pecasOponente);

    bool posicaoDisparoValida = false;



    while (!posicaoDisparoValida)
    {

        List<int> listaNumericaDisparo = new();

        bool isJogadorComputador = false;

        if (nomeJogador == "player2Computador")
        {
            Random rand = new();
            int linha = rand.Next(0, 10);
            int coluna = rand.Next(0, 10);

            listaNumericaDisparo.Add(linha);
            listaNumericaDisparo.Add(coluna);
            isJogadorComputador = true;
        }
        else
        {
            Console.WriteLine("Escolha a posição para realizar o disparo");
            string posicaoDisparo = Console.ReadLine().Trim().ToUpper();

            if (string.IsNullOrWhiteSpace(posicaoDisparo))
            {
                Console.WriteLine("Posição de disparo inválida!");
                continue;
            }

            var pattern = @"^([A-J])[1-9][0]?$";
            Regex regex = new(pattern);

            if (posicaoDisparo.Length >= 3 && posicaoDisparo.Substring(1) != "10")
            {
                Console.WriteLine("Posição de disparo inválida!");
                continue;
            }

            posicaoDisparoValida = regex.Match(posicaoDisparo).Success;
            if (!posicaoDisparoValida)
            {
                Console.WriteLine("Posição de disparo inválida!");
                continue;
            }

            int digitoConvertido;
            char[] caracteresPosicao = posicaoDisparo.ToCharArray();

            int letraConvertida = (int)Enum.Parse(typeof(PosicoesLinhas), caracteresPosicao[0].ToString());

            if (posicaoDisparo.Length > 2)
            {
                digitoConvertido = Convert.ToInt32(string.Concat(caracteresPosicao[1], caracteresPosicao[2])) - 1;
            }
            else
            {
                digitoConvertido = Convert.ToInt32(caracteresPosicao[1].ToString()) - 1;
            }

            listaNumericaDisparo.Add(letraConvertida);
            listaNumericaDisparo.Add(digitoConvertido);
        }


        int[,] matrizDisparoAtual = criarMatrizDisparo(listaNumericaDisparo);

        int[,] matrizAuxiliar = pecasOponente;

        matrizAuxiliar = Sum(matrizAuxiliar, matrizDisparoAtual);


        (int[,] matrizRetorno, bool disparoValido) = trataDisparo(matrizAuxiliar, isJogadorComputador);

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
    int countAcertos = 0;
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
                countAcertos++;
            }
        }
    }

    Console.WriteLine($"Placar: {countAcertos} / 30 ");

    ViewOponente(matrizStrings);
}

static int[,] criarMatrizDisparo(List<int> listaNumericaDisparo)
{
    int[,] matrizDisparo = new int[10, 10];
    matrizDisparo[listaNumericaDisparo[0], listaNumericaDisparo[1]] = 7;

    return matrizDisparo;
}

static (int[,] matrizRetorno, bool disparoValido) trataDisparo(int[,] matrizResultadoDisparo, bool isJogadorComputador)
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
                if (isJogadorComputador)
                {
                    Console.WriteLine("Posição de ataque inválida!");
                }
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

static int[,] posicionarPecasIniciaisComputador(string jogadorAtual)
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


    //Lista de peças com arrays [tipo_peça, posição_peça]
    List<string[]> listaPecas = new();
    bool listaCompleta = false;



    foreach (KeyValuePair<string, int> navio in qtNaviosInicial)
    {
        int qtParaPosicionar = navio.Value;
        while (qtParaPosicionar > 0)
        {

            Random rand = new();
            //0=horizontal 1 = vertical
            int posicionamento = rand.Next(0, 2);
            int linhaOuColunaEscolhida = -1;

            List<int> posicoesLivres = new();
            while (posicoesLivres.Count == 0)
            {
                linhaOuColunaEscolhida = rand.Next(0, 10);
                int dimensao = matrizComPosicoes.GetLength(0);
                for (int j = 0; j < dimensao; j++)
                {
                    if (j + dimensaoNavios[navio.Key] <= dimensao)
                    {
                        bool addPosicao = true;
                        for (int k = j; k < j + dimensaoNavios[navio.Key]; k++)
                        {
                            if (posicionamento == 0)
                            {
                                if (matrizComPosicoes[linhaOuColunaEscolhida, k] == 1)
                                {
                                    addPosicao = false;
                                    break;
                                }
                            }
                            if (posicionamento == 1)
                            {
                                if (matrizComPosicoes[k, linhaOuColunaEscolhida] == 1)
                                {
                                    addPosicao = false;
                                    break;
                                }
                            }
                        }
                        if (addPosicao)
                        {
                            posicoesLivres.Add(j);
                        }
                        else
                        {
                            break;
                        }
                    }
                }

            }
            int posicaoLivreEscolhida = rand.Next(0, posicoesLivres.Count());

            matrizComPosicoes = preencherMatriz(posicaoLivreEscolhida, dimensaoNavios[navio.Key], linhaOuColunaEscolhida, posicionamento, matrizComPosicoes);

            qtParaPosicionar--;
        }
    }
    int[,] pecasPosicionadas = matrizComPosicoes;
    //View(pecasPosicionadas);
    //int soma = 0;
    //foreach (var pos in pecasPosicionadas)
    //{
    //    soma += pos;
    //}
    //Console.WriteLine(soma);

    return pecasPosicionadas;

}

static int[,] preencherMatriz(int posicaoLivreEscolhida, int dimensaoNavio, int valorLinhaOuColuna, int posicionamento, int[,] matrizComPosicoes)
{
    if (posicionamento == 0)
    {
        for (int i = 0; i < dimensaoNavio; i++)
        {
            matrizComPosicoes[valorLinhaOuColuna, i] = 1;
        }
    }
    if (posicionamento == 1)
    {
        for (int i = 0; i < dimensaoNavio; i++)
        {
            matrizComPosicoes[i, valorLinhaOuColuna] = 1;
        }
    }
    return matrizComPosicoes;
}

public enum PosicoesLinhas { A, B, C, D, E, F, G, H, I, J }


