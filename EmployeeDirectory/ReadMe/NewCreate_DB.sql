USE [master]
GO
/****** Object:  Database [EmployeeDirectory]    Script Date: 7/9/2017 10:17:43 PM ******/
CREATE DATABASE [EmployeeDirectory]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'EmployeeDirectory', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.SQLEXPRESS\MSSQL\DATA\EmployeeDirectory.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'EmployeeDirectory_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.SQLEXPRESS\MSSQL\DATA\EmployeeDirectory_log.ldf' , SIZE = 2048KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [EmployeeDirectory] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [EmployeeDirectory].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [EmployeeDirectory] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [EmployeeDirectory] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [EmployeeDirectory] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [EmployeeDirectory] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [EmployeeDirectory] SET ARITHABORT OFF 
GO
ALTER DATABASE [EmployeeDirectory] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [EmployeeDirectory] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [EmployeeDirectory] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [EmployeeDirectory] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [EmployeeDirectory] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [EmployeeDirectory] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [EmployeeDirectory] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [EmployeeDirectory] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [EmployeeDirectory] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [EmployeeDirectory] SET  ENABLE_BROKER 
GO
ALTER DATABASE [EmployeeDirectory] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [EmployeeDirectory] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [EmployeeDirectory] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [EmployeeDirectory] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [EmployeeDirectory] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [EmployeeDirectory] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [EmployeeDirectory] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [EmployeeDirectory] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [EmployeeDirectory] SET  MULTI_USER 
GO
ALTER DATABASE [EmployeeDirectory] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [EmployeeDirectory] SET DB_CHAINING OFF 
GO
ALTER DATABASE [EmployeeDirectory] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [EmployeeDirectory] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [EmployeeDirectory] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [EmployeeDirectory] SET QUERY_STORE = OFF
GO
USE [EmployeeDirectory]
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
GO
USE [EmployeeDirectory]
GO
/****** Object:  User [employeeWebDir]    Script Date: 7/9/2017 10:17:43 PM ******/
CREATE USER [employeeWebDir] FOR LOGIN [employeeDirWeb] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  Table [dbo].[Employees]    Script Date: 7/9/2017 10:17:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employees](
	[FirstName] [nchar](50) NOT NULL,
	[LastName] [nchar](50) NOT NULL,
	[JobTitle] [nchar](50) NOT NULL,
	[Location] [nchar](50) NOT NULL,
	[Email] [nchar](50) NOT NULL,
	[EmployeeId] [int] IDENTITY(1,1) NOT NULL,
	[IsAdmin] [bit] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 7/9/2017 10:17:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[ID] [int] NOT NULL,
	[FirstName] [nvarchar](200) NULL,
	[LastName] [nvarchar](200) NULL,
	[Email] [nvarchar](200) NOT NULL,
	[Password] [nvarchar](100) NOT NULL
) ON [PRIMARY]
GO
USE [master]
GO
ALTER DATABASE [EmployeeDirectory] SET  READ_WRITE 
GO
USE [EmployeeDirectory]
GO

/****** Object:  Trigger [dbo].[trigger_InsertInUserTable]    Script Date: 7/9/2017 10:20:12 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TRIGGER [dbo].[trigger_InsertInUserTable] ON [EmployeeDirectory].[dbo].[Employees]
FOR INSERT AS
BEGIN
    INSERT INTO 
    [dbo].[User]
    (
        [ID],
        [FirstName],
        [LastName],
		[Email],
		[Password]
    )
    SELECT 
        EmployeeId, 
        FirstName, 
        LastName,
		Email,
		'password'
    FROM INSERTED
END
GO

ALTER TABLE [dbo].[Employees] ENABLE TRIGGER [trigger_InsertInUserTable]
GO

