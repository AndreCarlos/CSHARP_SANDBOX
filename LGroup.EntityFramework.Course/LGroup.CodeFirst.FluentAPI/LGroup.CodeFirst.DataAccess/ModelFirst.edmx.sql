
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 04/04/2015 12:38:14
-- Generated from EDMX file: C:\Users\Andre Carlos\Documents\Visual Studio 2013\Projects\LGroup.CodeFirst.FluentAPI\LGroup.CodeFirst.DataAccess\ModelFirst.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [NOVOBANCO];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------


-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'TB_PONTO'
CREATE TABLE [dbo].[TB_PONTO] (
    [ID_PONTO] int IDENTITY(1,1) NOT NULL,
    [ID_FUNCIONARIO] int  NOT NULL,
    [DT_PONTO] datetime  NOT NULL
);
GO

-- Creating table 'TB_FUNCIONARIO'
CREATE TABLE [dbo].[TB_FUNCIONARIO] (
    [ID_FUNCIONARIO] int IDENTITY(1,1) NOT NULL,
    [NM_FUNCIONARIO] nvarchar(40)  NOT NULL,
    [NR_TELEFONE] nvarchar(15)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [ID_PONTO] in table 'TB_PONTO'
ALTER TABLE [dbo].[TB_PONTO]
ADD CONSTRAINT [PK_TB_PONTO]
    PRIMARY KEY CLUSTERED ([ID_PONTO] ASC);
GO

-- Creating primary key on [ID_FUNCIONARIO] in table 'TB_FUNCIONARIO'
ALTER TABLE [dbo].[TB_FUNCIONARIO]
ADD CONSTRAINT [PK_TB_FUNCIONARIO]
    PRIMARY KEY CLUSTERED ([ID_FUNCIONARIO] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [ID_FUNCIONARIO] in table 'TB_PONTO'
ALTER TABLE [dbo].[TB_PONTO]
ADD CONSTRAINT [FK_TB_FUNCIONARIOTB_PONTO]
    FOREIGN KEY ([ID_FUNCIONARIO])
    REFERENCES [dbo].[TB_FUNCIONARIO]
        ([ID_FUNCIONARIO])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TB_FUNCIONARIOTB_PONTO'
CREATE INDEX [IX_FK_TB_FUNCIONARIOTB_PONTO]
ON [dbo].[TB_PONTO]
    ([ID_FUNCIONARIO]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------