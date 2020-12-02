## MundiPagg - Credit card payment simulator

#### Utilização
Para executar o projeto, baixe o arquivo docker-compose.yml e, na pasta onde se localiza o arquivo, abra um terminal e rode o comando:
```
docker-compose up -d
```
Após tudo pronto, você poderá acessar as seguintes interfaces:
- RabbitMQ: http://localhost:15672/
- Mongo (através do Mongo express): http://localhost:8081/
- Aplicação (através do swagger): http://localhost:32770/swagger/index.html


#### Sobre
Nossas aplicações foram codificadas utilizando .Net Core 3.1 e documentada com o swagger,RabbitMQ como broker e o MongoDB para repositório de dados NoSQL.

O contexto é um pagamento com **cartão de crédito**.
Ao enviar um **POST** para criar uma novo pedido, essa requisição é recebida pela aplicação principal, traduzida para o formato [CreateOrder](https://docs.mundipagg.com/reference#criar-pedido), transformada em Json, salva no banco de dados e então é enviada para o broker em forma de mensagem.

Nesse processo, é gerado um JobId para acompanhar o processamento do pedido.

Uma outra aplicação, chamada de Consumer, irá capturar essa mensagem e processa-la utilizando o [API da MundiPagg](https://docs.mundipagg.com/docs/simulador-de-cartão-de-crédito). Por fim, enviará uma resposta em forma de mensagem para o nosso broker.

A aplicação principal, através de um background job, irá consumir essa mensagem resposta e atualizará as informações referente a essa transação no nosso banco de dados.

#### APIs
- [POST] /api/Order
    Cria um novo pedido, recebendo como parâmetro via body o Json:
```
{
  "order": "string",
  "eventSimulate": 1,
  "orderContent": 1
}
```
 - Campo "order": Json referente ao pedido. Caso seja enviado como vazio, nulo ou "string", a aplicação irá utilizar um pedido padrão.
 - Campo "eventSimulate": Indica como será a simulação do pagamento, com os valores:
  1. Success (Default), 
  2. Fail, 
  3. ProcessingThanSuccess, 
  4. ProcessingThanFail, 
  5. ErrorInSecondOperation, 
  6. SuccessThanPurchaserFailure, 
  7. ProcessingThanCanceled, 
  8. Other.
 - Campo "orderContent": Indica o formato do pedido, com os valores:
 1 - Json (Default), 
 2 - Xml.
 
 - [GET] /api/Order/{jobId}
    Consulta um pedido de acordo com o JobId.
  
 - [GER] /api/Order/code/{code}
    Consulta um pedido de acordo com o código do pedido.
 
 
