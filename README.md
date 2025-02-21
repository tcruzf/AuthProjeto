# ControllRR 🔧
![GitHub License](https://img.shields.io/github/license/tcruzf/AuthProjeto?style=flat-square)
![GitHub Last Commit](https://img.shields.io/github/last-commit/tcruzf/AuthProjeto?style=flat-square)
![GitHub Issues](https://img.shields.io/github/issues/tcruzf/AuthProjeto?style=flat-square)

Sistema para controle de manutenções e gestão de produtos com interface web.

📌 **Recursos Principais:**
- Gestão de ordens de serviço
- Controle de estoque de produtos
- Autenticação de usuários
- Dashboard de métricas
- Registro de manutenções preventivas e corretivas

## 📋 Índice
- [Pré-requisitos](#pré-requisitos)
- [Instalação](#instalação)
- [Configuração](#configuração)
- [Funcionalidades](#funcionalidades)
- [Contribuição](#contribuição)
- [Licença](#licença)
- [Contato](#contato)

## 🛠️ Pré-requisitos
- .NET 7.0 SDK
- PostgreSQL 14+
- Node.js 18.x
- Entity Framework Core CLI

## 🚀 Instalação
```bash
git clone https://github.com/tcruzf/AuthProjeto.git
cd AuthProjeto
dotnet restore


```bash
dotnet ef migrations remove -f --project ControllRR.Infrastructure
dotnet ef migrations add InitialMigration --project ControllRR.Infrastructure --output-dir Data/Migrations
dotnet ef database update --project ControllRR.Infrastructure