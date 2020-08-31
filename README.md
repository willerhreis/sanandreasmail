# Introdução
O estado de San Andreas está atualizando o seu sistema postal e você foi designado para
desenvolver uma nova solução que calcule o caminho e o tempo de entrega das
encomendas postadas no estado.

![alt text](https://github.com/willerhreis/sanandreasmail/blob/development/Docs/Mapa.png?raw=true)


### Correio de San Andreas 

O objetivo deste projeto é demonstrar conhecimento na construção de um software aplicando boas práticas de desenvolvimento.

Apesar de ser um projeto tecnicamente simples, que poderia ser realizado com apenas algumas classes e em tempo de execução, a escolha, para cumprir o objetivo, foi por utilizar camadas de serviços, repositórios e persistência em banco de dados.

### O Projeto

O projeto foi constituído com os seguintes recursos:
- Console Application .net core
- Banco de dados SQLite
- Entity Framework (ORM)
- Algoritmo de cálculo de menor trajeto

### Concepção

A princípio, possuímos a seguinte entrada: As cidades do estado de San Andreas.

![alt text](https://github.com/willerhreis/sanandreasmail/blob/development/Docs/Cidades.png?raw=true)

Foi atribuída a cada cidade uma sigla, para facilitar a compreensão e manejo do desenvolvimento.

É sabido que nos correios existem trechos pré-definidos que correspondem às rotas de entregas entre cidades. Cada trecho possui um tempo de percurso definido em dias:

![alt text](https://github.com/willerhreis/sanandreasmail/blob/development/Docs/Trechos.png?raw=true)

A partir de então, as encomendas devem ser distribuídas e a melhor rota deve ser calculada.

Assim, espera-se, como no exemplo abaixo, que o sistema calcule a rota e o seu tempo de percurso:

![alt text](https://github.com/willerhreis/sanandreasmail/blob/development/Docs/Encomendas.png?raw=true)

## Solução	

Perante o exposto, a solução encontrada foi a construção de um projeto Console Application que pudesse receber as entradas (em arquivos txt) e imprimisse a saída em um arquivo com o resultado do cálculo das rotas.

O primeiro passo então é desenhar o grafo representado para o mapa:

![alt text](https://github.com/willerhreis/sanandreasmail/blob/development/Docs/Grafo.png?raw=true)

Uma vez definida a complexidade do Grafo, foi escolhido o algoritmo de cálculo de menor caminho, Dijkstra. (Para saber mais, consulte aqui: https://en.wikipedia.org/wiki/Dijkstra_algorithm)

Após a determinação dos nós e o carregamento dos padrões do grafo no algoritmo, temos como saída as rotas.

Arquivo de entrada (encomendas.txt):
```
SF WS
LS BC
WS BC
```

Arquivo de saída (rotas.txt):
```
SF WS 1
LS LV BC 2
WS SF LV BC 5
```

## Execução

O projeto expõe um .exe e instrui através do console como proceder com a parametrização.

Utilize os exemplos de entrada contidos neste projeto, na pasta Docs.

## Contribuição
Camadas de testes assim como correções previstas para retorno de Exceptions e mensagens customizadas ainda precisam ser implementadas.

## Licença
[MIT](https://choosealicense.com/licenses/mit/)
