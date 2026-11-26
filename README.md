# EventoSimplesAPI

API REST desenvolvida em **.NET 9 (C#)** com **Minimal APIs**, **Entity Framework Core** e **SQLite** para gestao simples de eventos, locais, ingressos e participantes, com documentacao interativa via **Swagger / OpenAPI**.

## Funcionalidades

- **Eventos (/api/Evento):** listagem, consulta por ID, criacao, atualizacao e remocao.
- **Locais (/api/local):** gestao dos espacos onde os eventos acontecem.
- **Ingressos (/api/Ingresso):** controle de entradas e ingressos associados.
- **Participantes (/api/Participante):** cadastro e gestao de participantes.
- **Documentacao interativa:** exploracao e testes dos endpoints pelo Swagger UI.

## Tecnologias utilizadas

- **C# / .NET 9** (Minimal APIs)
- **Entity Framework Core 9** + **SQLite**
- **Swagger / Swashbuckle (Swashbuckle.AspNetCore)** e **Microsoft.AspNetCore.OpenApi**
- **Visual Studio**

## Estrutura do projeto

- Modelos/ — entidades do dominio (Evento, Ingressos, Local, Participante).
- Data/ — contexto do banco (EventoSimplesAPIContext) e *migrations* (Migrations/).
- Endpoints/ — definicao das rotas e operacoes CRUD por recurso.
- Program.cs — configuracao de servicos, banco de dados, Swagger e mapeamento de endpoints.
- EventoSimplesAPI.http — requisicoes HTTP de exemplo para testes locais.

## Como executar localmente

### Pre-requisitos
- [.NET 9 SDK](https://dotnet.microsoft.com/) instalado.

### Passo a passo

1. Clone o repositorio:
```bash
git clone <url-do-repositorio>
cd EventoSimplesAPI
```

2. Restaure os pacotes e aplique as *migrations*:
```bash
dotnet restore
dotnet ef database update
```

3. Execute a aplicacao:
```bash
dotnet run
```

4. Acesse o Swagger no navegador em http://localhost:5277/swagger/index.html (ou na porta exibida no terminal).

## Proximos passos

- [ ] Padronizar nomenclatura entre singular e plural nas rotas e entidades.
- [ ] Adicionar validacoes de entrada e regras de negocio (ex.: capacidade do local e datas).
- [ ] Implementar testes automatizados para os endpoints.
