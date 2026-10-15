# MiniMES — Sistema de Execução de Manufatura

MiniMES é um sistema de execução de manufatura (MES) didático, desenvolvido para demonstrar domínio das stacks usadas em implantações industriais: **SQL Server, IIS, ASP.NET Core, redes e integração com ERP**.

![.NET](https://img.shields.io/badge/.NET-10.0-purple)
![SQL Server](https://img.shields.io/badge/SQL%20Server-2022-red)
![IIS](https://img.shields.io/badge/IIS-Windows-blue)
![License](https://img.shields.io/badge/license-MIT-green)

---

## Sobre o projeto

O MiniMES simula uma fábrica de autopeças com três máquinas (Torno CNC, Fresadora e Injetora). O sistema:

1. Recebe ordens de produção do ERP via API REST
2. Permite que operadores registrem apontamentos (produção boa, refugo, paradas)
3. Consolida indicadores de produção em tempo real
4. Devolve a produção realizada ao ERP
5. É hospedado no IIS com SQL Server como banco de dados

---

## Funcionalidades

### Cadastros (CRUD completo)
- Turnos
- Operadores (com matrícula e vínculo a turno)
- Máquinas (com código, linha e endereço IP)
- Interrupções (motivos de parada)
- Ordens de Produção
- Apontamentos (produção boa e refugo)

### Dashboard de Indicadores
- Total produzido, refugo e qualidade
- Total de minutos parados
- Gráficos com Chart.js

### Integração com ERP
- `POST /api/erp/ordens` — recebe ordem do ERP
- `GET /api/erp/producao` — devolve produção realizada

---

## Stack tecnológica

| Camada | Tecnologia |
|---|---|
| Backend | ASP.NET Core MVC (.NET 10) |
| ORM | Entity Framework Core |
| Banco | SQL Server 2022 Express |
| Frontend | Razor Pages + Bootstrap 5 |
| Gráficos | Chart.js |
| Servidor web | IIS |
| Versionamento | Git + GitHub |

---

## Como executar

### Pré-requisitos
- .NET 10 SDK
- SQL Server Express
- SSMS

### Passo a passo

```bash
git clone https://github.com/Gsilva700/MiniMES.git
cd MiniMES