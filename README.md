# Gerenciamento de Usuários
## API Rest Backend .Net8.0

![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)
![.NET](https://img.shields.io/badge/.NET-5C2D91?style=for-the-badge&logo=.net&logoColor=white)
![Postgres](https://img.shields.io/badge/postgres-%23316192.svg?style=for-the-badge&logo=postgresql&logoColor=white)
![Postman](https://img.shields.io/badge/Postman-FF6C37?style=for-the-badge&logo=postman&logoColor=white)

Este projeto é uma API construída usando **.Net8.0, Postgres, Postman.**

## Descrição:

Consiste na implementação de um sistema de gerenciamento de
usuário com gráfico quantitativo de usuários ativos e cancelados
separados por tipos de usuários.
Para o desenvolvimento desta solução você deve implantar o back-end
utilizando a Stack "C# com Entity"

## Instalação

1. Clone o repositório:

```bash
git clone https://github.com/alefealves/webapi_dotnet_users.git
```

1. A API estará acessível em http://localhost:5026

## API Endpoints
A API fornece os seguintes endpoints:

**Users Endpoints**
```redução
POST /user - Crie um novo cliente
GET /user - Recupera todos os clientes
GET /user/{id} - Recupera um cliente
PUT /user/{id} - Atualiza um cliente
DELETE /user/{id} - Excluir um cliente
```

**Body POST Create User**
```json
{
	"IdUserAlter": 1,
	"nome": "zé",
	"sobrenome": "silva",
	"email": "teste1@teste.com",
	"senha": "123",
	"ConfirmarSenha": "123",
	"NivelAcesso": "COMUM"
}
```

## Contribuições

Contribuições são bem-vindas! Se você encontrar algum problema ou tiver sugestões de melhorias, abra um problema ou envie uma solicitação pull ao repositório.

Ao contribuir para este projeto, siga o estilo de código existente, [convenções de commit](https://www.conventionalcommits.org/en/v1.0.0/), e envie suas alterações em um branch separado.



