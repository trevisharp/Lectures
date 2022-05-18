# Complexity Example Console

Um exemplo de resolução tentando obter o maior valor de uma lista pirâmide. Listas Pirâmides seguem a seguinte regra: Ela é crescente até um pico em uma posição qualquer do vetor, a partir dai, ela é decrescente. Por exemplo o vetor [1, 2, 3, 2, 1] é um tipo de coleção pirâmide.

São implementadas 4 soluções:
- Aplica Bubble Sort e depois de seleciona o primeiro elemento. É O(n^2).
- Busca o maior valor do vetor desconsiderando que ele é um vetor pirâmide. É O(n).
- Busca o maior valor do vetor parando ao atingir o pico. É O(n), porém, mais rápido que o anterior.
- Usando uma busca binária para achar o maior. É O(lg(n)).
