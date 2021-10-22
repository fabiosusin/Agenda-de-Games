Um clube de games é uma entidade virtual que reúne pessoas interessadas nos mais
diversos tipos de jogos virtuais. Entre as diversas atividades e recursos que fazem parte do
clube, há uma agenda compartilhada, que mantém informações sobre games que ocorreram e
que ocorrerão, organizada a partir dos interesses dos participantes. De cada game, mantémse: seu nome, sua categoria, sua data (e horário) de realização, e o link para acesso.
Os dados dessa agenda vão ser utilizados para várias consultas, mas nesse trabalho o foco é
construir uma estrutura de dados para armazenamento e pesquisa de games pelo seu nome,
que é único para cada game. Se quiser, você pode fazer a pesquisa pela combinação do
nome mais a data de realização.
A estrutura a ser utilizada é uma tabela hash de endereçamento aberto, ou seja, as colisões
de endereços devem ser resolvidas através da utilização de listas encadeadas para
games da agenda que gerarem o mesmo endereço. A função hash que vai ser utilizada, assim
como o tamanho da tabela, devem ser decididas e justificadas como parte do seu trabalho.

Implementação:
Nome: finalLOL2021
Data: 25/03/2021
Horário: 21h
Link: decisaoLOL.gam
Nome: superMarioshow
Data: 20/08/2021
Horário: 15h
Link: supermario.gam
0
1
2
3
4
5
6
...
Nome: minecraftTops2021
Data: 10/05/2021
Horário: 2h
Link: minecraft10.gam
Games
Nome: fortniteCaxias2021
Data: 12/09/2021
Horário: 12h
Link: fortnite18.gam
• a implementação pode ser feita em C, Java, C++, C#, Python;
• a interface pode ser bem simples (textual), o importante é que as operações funcionem;
• as listas de dados das colisões dos games da agenda devem ser implementadas
com alocação dinâmica de memória, através de listas encadeadas (simples,
duplas, triplas, pilhas, filas, etc). A tabela hash que define as posições para acesso
aos games da agenda pode ser implementado de acordo com sua escolha. Defina as
estruturas de dados e de implementação mais adequadas à execução das operações.
• a implementação das listas encadeadas não pode conter classes prontas da
linguagem de programação (Java, C++, C#, Python) que mantenham os encadeamentos
de forma automática, nem para as listas nem para os nodos (a implementação e
manipulação das listas encadeadas fazem parte do trabalho). As referências entre nodos
devem ser implementadas no trabalho e mantidas nas diferentes operações.
• as operações solicitadas podem fazer parte de um menu de opções.
Operações básicas a serem implementadas:
Pode haver variações nas entradas e saídas de dados das operações, novas operações
podem ser definidas, os nomes e/ou os parâmetros das operações podem ser modificados.
As principais operações a serem implementadas são:
TAD AgendaGames{
Dados: eventogame
Operações:
insere_game(E: dados do game) : insere o evento do game na agenda, calculando sua
posição na tabela hash e inserido o game na lista adequada
remove_game (E: nome, ou nome+data): remove o evento da tabela
consulta_game (E: nome, ou nome+data; S: dados do evento do game): consulta a
partir do cálculo do hash do game, e exibe todas as informações daquele game
consulta_todos_games (S: lista de games): lista todos os games da agenda, em ordem
alfabética do nome do game na agenda. Escolha um método de ordenação e implemente ele.
consulta_por_data (E: data; S: lista dos nomes dos games naquela data): você pode
decidir se a consulta vai ser pela data inteira, ou por parte dela
consulta_games_mesma_posicao (E: posição na tabela hash; S: lista de games
naquela posição da tabela)
lista_todos (S: lista de todos os games da tabela)
outras operações, você pode definir...
}
