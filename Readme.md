Analisador de código simples para projetos em C#. O resultado pode ser apresentado no console, ou exportado diretamente para
um arquivo.

Parametros

    -p <<pasta do projeto>>
    -f <<arquivo de saída>>

    Sendo -f usado somente quando se quer exportar os dados para um arquivo. Quando não informado, o resultado será apresentado
    na console. (dentro de .vscode, já está configurado para ambos os casos)

A idéia desse projeto foi implementar o máximo possível os conceitos de Clean Code e Clean Arquiteture.

As estatisticas retornadas pelo projeto são:

    Total de linhas de código
    Total de linhas de comentário
    Total de linhas em branco
    Maior linha de código por arquivo

O projeto pode ser expandido para analisar e trazer as estatísticas de outras linguagens além do C#. Para isso:

    - Adicione um novo item em Language.cs;
    - Adicione o novo analisador de código (\Analyzers);
    - Adicione um novo parser para a linguagem (\Parsers);
    - Adicione um novo serviço (\Services)
    - Ajuste AnalyzerFactory para retornar um novo objeto de acordo com a linguagem criada;

Caso deseje adicionar mais informações, a alteração no código será um pouco maior:

    - Altere os ValueObjects;
    - Altere os Parsers das linguagens, para capturarem a nova informação
    - Altere os Reports, para exibir as novas informações;

Alterar esse projeto parece desafiador, mas na verdade é simples! Basta seguir as dicas acima. Existem dois pontos principais no 
projeto: o Parser e o Report. 

O Parser é quem captura os dados do arquivo do projeto. Veja a classe CSharpFileParser.cs para entender como os dados são lidos e
interpretados.

O Report é responsável por gerar as estatísticas (FileStatisticsCalculate.cs) e apresenta-las na tela. Note que, para respeitar o SRP, 
eu dividi os pedaços dos relatórios em classes separadas (BarView e GridView), assim a classe CompleteReport.cs ficou mais limpa. Se 
precisar alterar alguma view, vá a classe correspondente e altere lá, de forma isolada...

Para dividir a saída do relatório entre Console e Arquivo, foram criadas duas classes, dentro da pasta Output: ReportConsole e ReportToFile.
Nessas classes, implementamos um StreamWriter especifico para cada situação (Console.OpenStandardOutput ou StreamWriter para arquivo)

O projeto também conta com testes unitários, com XUnit. Para realizar os testes, foi necessário criar uma classe FakeData,
para gerar os arquivos com a massa de testes. Estes arquivos são gerados em uma pasta aleatória, dentro de Environment.SpecialFolder.UserProfile.
A classe é IDisposable, e por isso, ao final, os dados de teste são eliminados. É importante dizer que, para os testes funcionarem,
é preciso ficar atento aos dados mockados: se eles estiverem incorretos, você vai ter problemas!



