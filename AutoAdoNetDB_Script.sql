USE [master]
GO

/***************************************************************************************************
*	CREATE DATABASE
****************************************************************************************************/
CREATE DATABASE [AutoAdoNetDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'AutoAdoNetDB', FILENAME = N'/var/opt/mssql/data/AutoAdoNetDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'AutoAdoNetDB_log', FILENAME = N'/var/opt/mssql/data/AutoAdoNetDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [AutoAdoNetDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

ALTER DATABASE [AutoAdoNetDB] SET ANSI_NULL_DEFAULT OFF 
GO

ALTER DATABASE [AutoAdoNetDB] SET ANSI_NULLS OFF 
GO

ALTER DATABASE [AutoAdoNetDB] SET ANSI_PADDING OFF 
GO

ALTER DATABASE [AutoAdoNetDB] SET ANSI_WARNINGS OFF 
GO

ALTER DATABASE [AutoAdoNetDB] SET ARITHABORT OFF 
GO

ALTER DATABASE [AutoAdoNetDB] SET AUTO_CLOSE OFF 
GO

ALTER DATABASE [AutoAdoNetDB] SET AUTO_SHRINK OFF 
GO

ALTER DATABASE [AutoAdoNetDB] SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE [AutoAdoNetDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO

ALTER DATABASE [AutoAdoNetDB] SET CURSOR_DEFAULT  GLOBAL 
GO

ALTER DATABASE [AutoAdoNetDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO

ALTER DATABASE [AutoAdoNetDB] SET NUMERIC_ROUNDABORT OFF 
GO

ALTER DATABASE [AutoAdoNetDB] SET QUOTED_IDENTIFIER OFF 
GO

ALTER DATABASE [AutoAdoNetDB] SET RECURSIVE_TRIGGERS OFF 
GO

ALTER DATABASE [AutoAdoNetDB] SET  DISABLE_BROKER 
GO

ALTER DATABASE [AutoAdoNetDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO

ALTER DATABASE [AutoAdoNetDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO

ALTER DATABASE [AutoAdoNetDB] SET TRUSTWORTHY OFF 
GO

ALTER DATABASE [AutoAdoNetDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO

ALTER DATABASE [AutoAdoNetDB] SET PARAMETERIZATION SIMPLE 
GO

ALTER DATABASE [AutoAdoNetDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO

ALTER DATABASE [AutoAdoNetDB] SET HONOR_BROKER_PRIORITY OFF 
GO

ALTER DATABASE [AutoAdoNetDB] SET RECOVERY FULL 
GO

ALTER DATABASE [AutoAdoNetDB] SET  MULTI_USER 
GO

ALTER DATABASE [AutoAdoNetDB] SET PAGE_VERIFY CHECKSUM  
GO

ALTER DATABASE [AutoAdoNetDB] SET DB_CHAINING OFF 
GO

ALTER DATABASE [AutoAdoNetDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO

ALTER DATABASE [AutoAdoNetDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO

ALTER DATABASE [AutoAdoNetDB] SET DELAYED_DURABILITY = DISABLED 
GO

ALTER DATABASE [AutoAdoNetDB] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO

ALTER DATABASE [AutoAdoNetDB] SET QUERY_STORE = OFF
GO

ALTER DATABASE [AutoAdoNetDB] SET  READ_WRITE 
GO


USE [AutoAdoNetDB]
GO

/***************************************************************************************************
*	CREATE TABLE USER
****************************************************************************************************/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[User](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](250) NOT NULL,
	[DateBirth] [datetime] NOT NULL,
	[Gender] [char](3) NOT NULL,
	[Email] [varchar](250) NOT NULL,
	[Active] [bit] NOT NULL
) ON [PRIMARY]
GO


/***************************************************************************************************
*	SEED TABLE USER
****************************************************************************************************/
USE [AutoAdoNetDB]
GO

INSERT INTO [dbo].[User]([Name],[DateBirth],[Gender],[Email],[Active])VALUES('ALEX SANDRO MARTINS DE ARAUJO','1981-04-10','MA','uareke@gmail.com',1);
INSERT INTO [dbo].[User]([Name],[DateBirth],[Gender],[Email],[Active])VALUES('RAFAEL MARTINS DE ARAUJO','1991-05-06','MA','bigorage@gmail.com',1);
INSERT INTO [dbo].[User]([Name],[DateBirth],[Gender],[Email],[Active])VALUES('BARBARA SOUSA DE ARAUJO','2016-11-01','FE','barabra@gmail.com',1);
INSERT INTO [dbo].[User]([Name],[DateBirth],[Gender],[Email],[Active])VALUES('HEITOR SOUSA DE ARAUJO','2021-04-16','MA','heitor@gmail.com',1);
INSERT INTO [dbo].[User]([Name],[DateBirth],[Gender],[Email],[Active])VALUES('CRISTINA LIRA DA SILVA','2000-10-10','FE','cristinalira@gmail.com',1);
INSERT INTO [dbo].[User]([Name],[DateBirth],[Gender],[Email],[Active])VALUES('JOSEU BARBOSA PINTO','1995-05-20','MA','josue@gmail.com',1);

GO
