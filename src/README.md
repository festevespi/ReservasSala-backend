# Agendamento de Salas - API

Este projeto é uma API desenvolvida em .NET 9 para gerenciamento de reservas de salas em ambientes corporativos.  
O objetivo é permitir o cadastro, consulta, atualização e exclusão de reservas.

## Funcionalidades

- Cadastro de reservas de salas
- Consulta de reservas por ID
- Atualização de reservas existentes
- Exclusão de reservas
- Validação de conflitos de horários
- Opção de servir café e informar quantidade de pessoas

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

- **Models**: Entidades do domínio (Reserva, Sala, Local)
- **Data**: Contexto do banco de dados e repositórios
- **Services**: Regras de negócio e validações
- **Controllers**: Endpoints da API
- **Migrations**: Scripts de criação e atualização do banco de dados

## Como Executar

1. Clone o repositório: \
	```git clone https://github.com/festevespi/ReservasSala-backend.git```
2. Configure a string de conexão do MySQL em `appsettings.json`\
	* substitua os valores entre colchetes [] pela sua configuração:
		- [SERVIDOR]
		- [USUARIO]
		- [SENHA]
3. Execute as migrations para criar o banco de dados:\
	a. no prompt de comando, navegue até a pasta do projeto e execute:
	```dotnet ef database update```
4. Inicie a aplicação (F5 para executar no Visual Studio ou `dotnet run` no terminal)
5. Acesse a documentação da API via Swagger/OpenAPI em `https://localhost:7249/swagger`

## Testes

Os testes unitários estão localizados na pasta `MTR.AgendamentoSalas.API.Tests`.  
Para executar os testes:\
- Opção Visual: Abra o Visual Studio e selecione clique bom o botão direito em `MTR.AgendamentoSalas.API.Tests` e clique em "Run Tests";
- Opção linha de comando: execute o comando `dotnet test` no terminal.

## Observações

- Certifique-se de que o MySQL está em execução e acessível.
- As validações de reserva impedem conflitos de horários e reservas para datas passadas.
- Para reservas com café, informe a quantidade de pessoas.

## Autor

Projeto desenvolvido para avaliação técnica de desenvolvedor FullStack.

