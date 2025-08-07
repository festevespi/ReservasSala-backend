# Agendamento de Salas - API

Este projeto � uma API desenvolvida em .NET 9 para gerenciamento de reservas de salas em ambientes corporativos.  
O objetivo � permitir o cadastro, consulta, atualiza��o e exclus�o de reservas.

## Funcionalidades

- Cadastro de reservas de salas
- Consulta de reservas por ID
- Atualiza��o de reservas existentes
- Exclus�o de reservas
- Valida��o de conflitos de hor�rios
- Op��o de servir caf� e informar quantidade de pessoas

## Tecnologias Utilizadas

- .NET 9
- C# 13
- Entity Framework Core
- MySQL
- ASP.NET Core Web API

## Bibliotecas Utilizadas

- Microsoft.AspNetCore.OpenApi
- Microsoft.EntityFrameworkCore
- Microsoft.EntityFrameworkCore.Tools
- Microsoft.VisualStudio.Web.CodeGeneration.Design
- Pomelo.EntityFrameworkCore.MySql
- Swashbuckle.AspNetCore

## Estrutura do Projeto

- **Models**: Entidades do dom�nio (Reserva, Sala, Local)
- **Data**: Contexto do banco de dados e reposit�rios
- **Services**: Regras de neg�cio e valida��es
- **Controllers**: Endpoints da API
- **Migrations**: Scripts de cria��o e atualiza��o do banco de dados

## Como Executar

1. Clone o reposit�rio: \
	```git clone https://github.com/festevespi/ReservasSala-backend.git```
2. Configure a string de conex�o do MySQL em `appsettings.json`\
	* substitua os valores entre colchetes [] pela sua configura��o:
		- [SERVIDOR]
		- [USUARIO]
		- [SENHA]
3. Execute as migrations para criar o banco de dados:\
	a. no prompt de comando, navegue at� a pasta do projeto e execute:
	```dotnet ef database update```
4. Inicie a aplica��o (F5 para executar no Visual Studio ou `dotnet run` no terminal)
5. Acesse a documenta��o da API via Swagger/OpenAPI em `https://localhost:7249/swagger`

## Testes

Os testes unit�rios est�o localizados na pasta `MTR.AgendamentoSalas.API.Tests`.  
Para executar os testes:\
- Op��o Visual: Abra o Visual Studio e selecione clique bom o bot�o direito em `MTR.AgendamentoSalas.API.Tests` e clique em "Run Tests";
- Op��o linha de comando: execute o comando `dotnet test` no terminal.

## Observa��es

- Certifique-se de que o MySQL est� em execu��o e acess�vel.
- As valida��es de reserva impedem conflitos de hor�rios e reservas para datas passadas.
- Para reservas com caf�, informe a quantidade de pessoas.

## Autor

Projeto desenvolvido para avalia��o t�cnica de desenvolvedor FullStack.

